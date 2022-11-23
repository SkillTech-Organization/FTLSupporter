﻿using Azure.Core;
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

        public int MessageCount { get; set; }
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
                var messages = message.Value;
                resp.MessageCount += messages.Count();
                if (messages.Count() == 0)
                {
                    _logger.Information("No messages were received.");
                    resp.ResultReceived = false;
                    resp.NoMoreMessages = true;
                }
                else
                {
                    _logger.Information($"Found {messages.Count()} message(s). Processing...");

                    foreach (var msg in messages)
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
                                _logger.Debug("Parsing message...");
                                var queueResponse = JsonConvert.DeserializeObject<FTLQueueResponse>(msgText, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
                                _logger.Debug("Parsing message...done");

                                var res = queueResponse.Result;
                                if (res != null && res.Count > 0)
                                {
                                    if (res.Any(x => x.Status == FTLResult.FTLResultStatus.RESULT))
                                    {
                                        _logger.Information("Result found.");

                                        resp.Result = queueResponse;
                                        resp.ResultReceived = true;
                                        return resp;
                                    }
                                    //else
                                    //{
                                    //    _logger.Information("Result field does not contain FTLSupport result(s).");
                                    //}
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
            }
            else
            {
                resp.NoMoreMessages = true;
            }

            if (!resp.ResultReceived)
            {
                _logger.Information("Result was not found.");
            }

            return resp;
        }
    }
}