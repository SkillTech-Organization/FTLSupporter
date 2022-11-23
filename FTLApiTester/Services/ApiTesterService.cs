using CommandLine.Text;
using CommandLine;
using FTLApiService;
using FTLApiTester.Settings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FTLApiTester.Util;
using System.Diagnostics;
using Newtonsoft.Json.Converters;

namespace FTLApiTester.Services
{
    static class CompareExtension
    {
        public static bool DeepCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                if (!objValue.Equals(anotherValue)) result = false;
            }

            return result;
        }

        public static bool CompareEx(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            //properties: int, double, DateTime, etc, not class
            if (!obj.GetType().IsClass) return obj.Equals(another);

            var result = true;
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                //Recursion
                if (!objValue.DeepCompare(anotherValue)) result = false;
            }
            return result;
        }
    }

    internal class Args
    {
        [Option('i', "id", Required = false, HelpText = "ID for test data")]
        public string? ID { get; set; }

        [Option('p', "path", Required = false, HelpText = "Folder path for test data")]
        public string? Path { get; set; }

        [Option('m', "maxTruckDistance", Required = false, HelpText = "MaxTruckDistance")]
        public int? MaxTruckDistance { get; set; }
    }

    internal class TestData
    {
        public FTLSupportRequest Request { get; set; }

        public List<FTLResult> Result { get; set; }

        public bool IsFTLSupport { get; set; }
    }

    internal class ApiTesterService : IApiTesterService
    {
        private FTLApiServiceClient _client;
        private FTLApiTesterSettings _settings;
        private readonly ILogger _logger;

        private const string Delimeter = "_";
        private const int DefaultTruckDistance = 10000;

        private QueueReader QueueReader { get; set; }

        private string TestDataPath { get; set; }
        private string ID { get; set; }
        public int MaxTruckDistance { get; set; }

        public ApiTesterService(FTLApiServiceClient client, FTLApiTesterSettings settings, IConfiguration configuration)
        {
            _client = client;
            _settings = settings;

            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            QueueReader = new QueueReader(settings, configuration);
        }

        public async Task DoWork(CancellationToken cancellationToken = default)
        {
            try
            {
                ProcessArgs();
                LoadDataThenTest();
            }
            catch (Exception ex)
            {
                _logger.Error("Error: " + ex.Message, ex);
            }
        }

        private void ProcessArgs()
        {
            _logger.Information("Parsing parameters...");

            var args = Environment.GetCommandLineArgs();
            Parser.Default.ParseArguments<Args>(args)
               .WithParsed<Args>(o =>
               {
                   ID = o.ID;
                   TestDataPath = o.Path ?? _settings.TestDataPath ?? "";
                   MaxTruckDistance = o.MaxTruckDistance.HasValue ? o.MaxTruckDistance.Value : (_settings.MaxTruckDistance.HasValue ? _settings.MaxTruckDistance.Value : DefaultTruckDistance);
               });

            var runningAll = string.IsNullOrWhiteSpace(ID) ? "no ID provided, running all test data" : "running specific test data";
            _logger.Information($"Processed parameters: ID = {ID ?? ""} {runningAll}, Path = {TestDataPath}, MaxTruckDistance = {MaxTruckDistance}");

            LoadDataThenTest();
        }

        private void LoadDataThenTest()
        {
            _logger.Information("Loading test data...");

            var data = new Dictionary<string, TestData>();

            if (string.IsNullOrWhiteSpace(ID))
            {
                data = GetTestData();
            }
            else
            {
                data.Add(ID, GetTestData(ID));
            }

            _logger.Information("Loading test data...done.");

            if (data.Count > 0)
            {
                DoTestRequests(data);
            }
            else
            {
                _logger.Information("No available test data.");
            }
        }

        private void DoTestRequests(Dictionary<string, TestData> data)
        {
            _logger.Information($"Initiating test requests towards API. Base URL: {_settings.FTLApiBaseUrl}");

            var timer = new Stopwatch();
            var timerResult = new Stopwatch();

            var testsSucceeded = 0;
            var testsFailed = 0;

            var i = 0;
            foreach (var test in data)
            {
                _logger.Information($"Starting Test {i} of {data.Keys.Count}");

                timer.Start();
                timerResult.Reset();

                var testCase = test.Value;

                var endpoint = test.Value.IsFTLSupport ? "FTLSupport" : "FTLSupportX";
                _logger.Information($"Sending request for test data with ID {test.Key}, to endpoint {endpoint}");
                _logger.Verbose("Request content: " + JsonConvert.SerializeObject(testCase.Request));

                try
                {
                    var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(testCase.Request);
                    var response = testCase.IsFTLSupport ? _client.ApiV1FTLSupporterFTLSupportAsync(testCase.Request).Result
                        : _client.ApiV1FTLSupporterFTLSupportXAsync(testCase.Request).Result;
                    _logger.Information("Request was successful.");

                    var resp = new GetResultResponse();
                    var messageCount = 0;
                    double minuteCount = 0;

                    timerResult.Start();

                    do
                    {
                        _logger.Information("Getting messages from queue...");

                        Task.Delay(_settings.WaitBeforeBetweenQueueQueryInMs).Wait();

                        resp = QueueReader.GetResultMessage();

                        messageCount += resp.MessageCount;

                        _logger.Information($"So far received: {messageCount} message(s).");

                        minuteCount = timerResult.Elapsed.TotalMinutes;
                    }
                    while (!resp.ResultReceived && messageCount <= _settings.MaxMessageLimitPerRequest
                    && minuteCount <= _settings.MaxWaitLimitForResultPerRequestInMinutes);

                    if (resp.ResultReceived)
                    {
                        var resultFileName = test.Key + _settings.TestResultFileIdentifier + "." + _settings.FileExtension;
                        var resultJson = JsonConvert.SerializeObject(resp.Result.Result);

                        _logger.Information("Saving result to: " + resultFileName);
                        _logger.Verbose(resultJson);
                        SaveResult(resultJson, resultFileName);

                        _logger.Information("Comparing result with given test result...");
                        var testResultJson = JsonConvert.SerializeObject(testCase.Result);

                        if (resultJson == testResultJson)
                        {
                            _logger.Information($"Matching results, test {i} of {data.Keys.Count} succeded.");
                            testsSucceeded++;
                        }
                        else
                        {
                            _logger.Information($"Different results, test {i} of {data.Keys.Count} failed.");
                            testsFailed++;
                        }
                    }
                    else
                    {
                        if (messageCount > _settings.MaxMessageLimitPerRequest)
                        {
                            _logger.Information($"Maximum message limit {_settings.MaxMessageLimitPerRequest} was exceeded by a total of {messageCount} message(s).");
                        }
                        else if (minuteCount > _settings.MaxWaitLimitForResultPerRequestInMinutes)
                        {
                            _logger.Information($"Maximum wait limit {_settings.MaxWaitLimitForResultPerRequestInMinutes} minute(s) was exceeded by a total of {minuteCount} minute(s) wait time.");
                        }
                        _logger.Information("Result was not received for this request.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Request failed: " + ex.Message, ex);
                }

                timer.Stop();

                _logger.Information($"Test ({i} of {data.Keys.Count}), for ID {test.Key} ended. Time: {timer.Elapsed}");

                i++;
            }

            _logger.Information($"Total tests: {i}, failed: {testsFailed}, succeeded: {testsSucceeded}");
        }

        private void SaveResult(string result, string fileName)
        {
            try
            {
                File.WriteAllText(Path.Combine(TestDataPath, fileName), result);
            }
            catch(Exception ex)
            {
                _logger.Error("Saving result failed", ex);
            }
        }

        private TestData GetTestData(string id)
        {
            TestData data = new TestData();
            data.Request = new FTLSupportRequest
            {
                TaskList = new List<FTLTask>(),
                TruckList = new List<FTLTruck>()
            };

            data.IsFTLSupport = File.Exists(Path.Combine(TestDataPath, id + _settings.TaskFileIdentifier + _settings.FTLSupportFileSuffix + "." + _settings.FileExtension));
            var fileEnding = data.IsFTLSupport ?
                _settings.FTLSupportFileSuffix : _settings.FTLSupportXFileSuffix;
            fileEnding = fileEnding + "." + _settings.FileExtension;

            _logger.Information("Test type: " + (data.IsFTLSupport ? "FTLSupport" : "FTLSupportX"));

            _logger.Information("Loading task data...");

            var taskPath = Path.Combine(TestDataPath, id + _settings.TaskFileIdentifier + fileEnding);
            if (File.Exists(taskPath))
            {
                var tasks = JsonConvert.DeserializeObject<List<FTLTask>>(File.ReadAllText(taskPath),
                   new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" });
                data.Request.TaskList = tasks;
            }
            else
            {
                throw new Exception("Test data file cannot be found: " + taskPath);
            }

            _logger.Information("Loading task data...done");
            _logger.Information("Loading truck data...");

            var truckPath = Path.Combine(TestDataPath, id + _settings.TruckFileIdentifier + fileEnding);
            if (File.Exists(truckPath))
            {
                var trucks = JsonConvert.DeserializeObject<List<FTLTruck>>(File.ReadAllText(truckPath),
                   new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" });
                data.Request.TruckList = trucks;
            }
            else
            {
                throw new Exception("Test data file cannot be found: " + truckPath);
            }

            _logger.Information("Loading truck data...done");
            _logger.Information("Loading result data...");

            var resultPath = Path.Combine(TestDataPath, id + _settings.ResultFileIdentifier + fileEnding);
            if (File.Exists(resultPath))
            {
                var result = JsonConvert.DeserializeObject<List<FTLResult>>(File.ReadAllText(resultPath),
                   new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" });
                data.Result = result;
            }
            else
            {
                throw new Exception("Test data file cannot be found: " + resultPath);
            }

            _logger.Information("Loading result data...done");

            data.Request.MaxTruckDistance = MaxTruckDistance;

            _logger.Information($"MaxTruckDistance in request is set to {MaxTruckDistance}.");

            return data;
        }

        private Dictionary<string, TestData> GetTestData()
        {
            var data = new Dictionary<string, TestData>();

            _logger.Information("Checking provided test data path...");

            if (Directory.Exists(TestDataPath))
            {
                var files = Directory.GetFiles(TestDataPath);

                if (files.Length == 0)
                {
                    _logger.Information($"Folder on path {TestDataPath} is empty.");
                    return data;
                }

                _logger.Information($"Found {files.Length} files.");
                _logger.Debug("Found files: ", String.Join(", ", files));

                for (int i = 0; i < files.Length; i++)
                {
                    var fileNameSections = Path.GetFileName(files[i]).Split(Delimeter);
                    if (fileNameSections.Length >= 2)
                    {
                        var id = fileNameSections[0];
                        if (!data.ContainsKey(id))
                        {
                            _logger.Information("Loading data for id: " + id);

                            try
                            {
                                data.Add(id, GetTestData(id));
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"Loading test data for id {id} failed.", ex);
                            }
                        }
                    }
                }
            }
            else
            {
                _logger.Information($"Folder on path {TestDataPath} does not exist.");
            }

            return data;
        }
    }
}
