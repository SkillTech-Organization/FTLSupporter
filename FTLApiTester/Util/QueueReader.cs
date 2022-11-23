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
using System.IO.Compression;

namespace FTLApiTester.Util
{
    internal static class StringCompressor
    {
        /// <summary>
        /// Compresses the string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }

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

                            msgText = StringCompressor.DecompressString(msgText);

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
