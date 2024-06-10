using CommandLine;
using FTLApiService;
using FTLApiTester.Settings;
using FTLApiTester.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Net;

namespace FTLApiTester.Services
{

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

        public JsonSerializerSettings isoDateTimeConverter { get; set; }

        public ApiTesterService(FTLApiServiceClient client, FTLApiTesterSettings settings, IConfiguration configuration)
        {
            _client = client;
            _settings = settings;

            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            if (_settings.UseQueue)
            {
                QueueReader = new QueueReader(settings, configuration);
            }

            isoDateTimeConverter = new JsonSerializerSettings { DateFormatString = "yyyy.MM.dd HH:mm:ss", Culture = System.Globalization.CultureInfo.InvariantCulture };
        }

        public async Task DoWork(CancellationToken cancellationToken = default)
        {
            try
            {
                ProcessArgs();
                LoadDataThenTest();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
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
                if (_settings.UseQueue)
                {
                    DoTestRequestsWithQueue(data);
                }
                else
                {
                    DoTestRequestsWithHttp(data);
                }
            }
            else
            {
                _logger.Information("No available test data.");
            }
        }

        private void DoTestRequestsWithQueue(Dictionary<string, TestData> data)
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
                    if (_settings.ClearQueueBeforeGettingMessages)
                    {
                        _logger.Information("Clearing queue...");
                        QueueReader.ClearMessages();
                        _logger.Information("Queue cleared.");
                    }

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
                    while (!resp.ResultReceived && !resp.ErrorReceived && messageCount <= _settings.MaxMessageLimitPerRequest
                    && minuteCount <= _settings.MaxWaitLimitForResultPerRequestInMinutes);

                    if (resp.ErrorReceived)
                    {
                        var resultFileName = test.Key + _settings.TestResultFileIdentifier + "." + _settings.FileExtension;
                        try
                        {
                            _logger.Information($"Received error: {resp.Result.Log.Message}");
                            _logger.Information("Test failed due to received error.");
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Test failed due to unexpected error: " + ex.Message, ex);
                        }
                    }
                    else if (resp.ResultReceived)
                    {
                        var resultFileName = test.Key + _settings.TestResultFileIdentifier + "." + _settings.FileExtension;
                        try
                        {
                            var link = resp.Result.Link;

                            if (!string.IsNullOrWhiteSpace(link))
                            {
                                _logger.Information($"Getting FTLResponse from API...");

                                var ftlResponse = _client.ApiV1FTLSupporterResultAsync(link.Split('/')[^1]).Result;

                                if (ftlResponse != null)
                                {
                                    _logger.Information("Processing FTLResponse...");

                                    var results = ftlResponse.Result.ToList();

                                    var resultJson = JsonConvert.SerializeObject(results, isoDateTimeConverter);

                                    _logger.Information("Saving result to: " + resultFileName);
                                    _logger.Verbose(resultJson);
                                    SaveResult(resultJson, resultFileName);
                                }
                                else
                                {
                                    _logger.Error("Request failed: FTLResponse is null!");
                                }
                            }
                            else
                            {
                                _logger.Error("Link in FTLQueueResponse is empty or null!");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Test failed due to unexpected error: " + ex.Message, ex);
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

                _logger.Information($"Test ({i + 1} of {data.Keys.Count}), for ID {test.Key} ended. Time: {timer.Elapsed}");

                i++;
            }

            _logger.Information($"Total tests: {i}, failed: {testsFailed}, succeeded: {testsSucceeded}");
        }

        private void DoTestRequestsWithHttp(Dictionary<string, TestData> data)
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
                    var response = testCase.IsFTLSupport ? _client.ApiV1FTLSupporterFTLSupportAsync(testCase.Request).Result
                        : _client.ApiV1FTLSupporterFTLSupportXAsync(testCase.Request).Result;

                    var requestId = response?.RequestID;

                    _logger.Information("Request was successful.");

                    var ftlResponse = new FTLApiService.FTLResponse();
                    var tryCount = 0;
                    double minuteCount = 0;

                    timerResult.Start();

                    bool notYetInBlob = true;

                    do
                    {
                        _logger.Information("Calling API for getting results...");

                        Task.Delay(_settings.WaitBeforeBetweenQueueQueryInMs).Wait();

                        try
                        {
                            ftlResponse = _client.ApiV1FTLSupporterResultAsync(requestId).Result;
                            notYetInBlob = false;
                        }
                        // Csak a HTTP 404-es kivételeket hagyjuk figyelmen kívül, mivel az azt jelenti, hogy még nincs fent blob-ban az eredmény.
                        // Minden más esetben dobjuk tovább a hibát.
                        catch (HttpRequestException ex)
                        {
                            if (ex.StatusCode != HttpStatusCode.NotFound)
                            {
                                throw;
                            }
                        }
                        catch (AggregateException ex)
                        {
                            var inner = ex.InnerException as ApiException;
                            if (inner == null || (inner != null && inner.StatusCode != 404))
                            {
                                throw;
                            }
                        }
                        catch (ApiException ex)
                        {
                            if (ex.StatusCode != 404)
                            {
                                throw;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        if (notYetInBlob)
                        {
                            _logger.Information($"Response is not uploaded to blob storage yet, trying again in {_settings.WaitBeforeBetweenQueueQueryInMs / 1000} sec.");
                        }

                        ++tryCount;

                        _logger.Information($"So far tried getting result {tryCount} times.");

                        minuteCount = timerResult.Elapsed.TotalMinutes;
                    }
                    while ((ftlResponse == null || ftlResponse?.RequestID != requestId) && tryCount <= _settings.MaxMessageLimitPerRequest
                    && minuteCount <= _settings.MaxWaitLimitForResultPerRequestInMinutes);

                    _logger.Information("Processing FTLResponse...");

                    var results = ftlResponse.Result.ToList();
                    var resultJson = JsonConvert.SerializeObject(results, isoDateTimeConverter);

                    _logger.Information("Saving result to: " + requestId + ".json");
                    _logger.Verbose(resultJson);

                    var resultFileName = test.Key + _settings.TestResultFileIdentifier + "." + _settings.FileExtension; // requestId +
                    SaveResult(resultJson, resultFileName);
                }
                catch (Exception ex)
                {
                    _logger.Error("Request failed: " + ex.Message, ex);
                }

                timer.Stop();

                _logger.Information($"Test ({i + 1} of {data.Keys.Count}), for ID {test.Key} ended. Time: {timer.Elapsed}");

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
            catch (Exception ex)
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
                var tasks = JsonConvert.DeserializeObject<List<FTLTask>>(File.ReadAllText(taskPath), isoDateTimeConverter);
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
                var trucks = JsonConvert.DeserializeObject<List<FTLTruck>>(File.ReadAllText(truckPath), isoDateTimeConverter);
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
                var result = JsonConvert.DeserializeObject<FTLResponse>(File.ReadAllText(resultPath), isoDateTimeConverter);
                data.Result = result.Result.ToList();
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
