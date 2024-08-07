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
using System.IO.Compression;
using CommonUtils;

namespace FTLApiTester.Util
{
    internal class GetResultResponse
    {
        public FTLQueueResponse Result { get; set; }

        public List<FTLSupporter.FTLResult> FTLResults { get; set; }

        public bool NoMoreMessages { get; set; } = false;

        public bool ResultReceived { get; set; } = false;

        public bool ErrorReceived { get; set; } = false;

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

        public void ClearMessages()
        {
            queueClient.ClearMessages();
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
                                var queueResponse = msgText.ToDeserializedJson<FTLQueueResponse>();
                                _logger.Debug("Parsing message...done");

                                if (queueResponse == null)
                                {
                                    _logger.Information("Message from queue is not FTLResult.");
                                }
                                else
                                {
                                    var res = queueResponse;
                                    if (res != null)
                                    {
                                        if (res.Status == FTLQueueResponse.FTLQueueResponseStatus.RESULT)
                                        {
                                            _logger.Information("Result found.");

                                            resp.Result = queueResponse;
                                            resp.ResultReceived = true;
                                            return resp;
                                        }
                                        else if (res.Status == FTLQueueResponse.FTLQueueResponseStatus.ERROR)
                                        {
                                            _logger.Information("Error found.");

                                            resp.Result = queueResponse;
                                            resp.ErrorReceived = true;
                                            return resp;
                                        }
                                        else
                                        {
                                            _logger.Information("Log found.");
                                        }
                                    }
                                    else
                                    {
                                        _logger.Information("Result field in FTLQueueResponse is null or empty.");
                                    }
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
