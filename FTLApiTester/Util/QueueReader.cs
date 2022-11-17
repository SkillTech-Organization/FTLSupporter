using Azure.Core;
using Azure.Storage.Queues;
using FTLApiTester.Settings;
using FTLSupporter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace FTLApiTester.Util
{
    internal class GetResultResponse
    {
        public FTLQueueResponse Result { get; set; }

        public bool NoMoreMessages { get; set; } = false;

        public bool ResultReceived { get; set; } = false;
    }

    internal class QueueReader
    {
        private readonly QueueClient queueClient;
        private readonly FTLApiTesterSettings settings;
        private readonly ILogger _logger;

        public QueueReader(FTLApiTesterSettings s, IConfiguration configuration)
        {
            settings = s;
            queueClient = new QueueClient(settings.AzureStorageConnectionString, settings.QueueName);
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public GetResultResponse GetResultMessage()
        {
            var resp = new GetResultResponse();

            var message = queueClient.ReceiveMessagesAsync(settings.MaxMessagesFromQueueAtOnce, new TimeSpan(0, settings.MaxMessageTimeSpanInMinutes, 0)).Result;
            if (message != null && message.Value != null)
            {
                var msgVal = message.Value;
                foreach(var msg in msgVal)
                {
                    var msgText = msg.MessageText;
                    if (string.IsNullOrWhiteSpace(msgText))
                    {
                        _logger.Information("Invalid message received. MessageID: " + msg.MessageId);
                    }
                    else
                    {
                        _logger.Verbose("Message text: " + msgText);

                        try
                        {
                            _logger.Information("Parsing message body...");
                            var queueResponse = JsonConvert.DeserializeObject<FTLQueueResponse>(msgText, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
                            _logger.Information("Parsing message body...done");

                            var res = queueResponse.Result;
                            if (res != null && res.Count > 0)
                            {
                                if (res.Any(x => x.Status == FTLResult.FTLResultStatus.RESULT))
                                {
                                    _logger.Information("Result found.");

                                    resp.Result = queueResponse;
                                    resp.ResultReceived = true;
                                }
                                else
                                {
                                    _logger.Information("Result field does not contain FTLSupport result(s).");
                                }
                            }
                            else
                            {
                                _logger.Information("Result field is null or empty.");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Error while parsing message body", ex);
                        }
                    }
                }
            }
            else
            {
                resp.NoMoreMessages = true;
            }

            return resp;
        }
    }
}
