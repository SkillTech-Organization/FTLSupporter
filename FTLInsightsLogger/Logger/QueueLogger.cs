using Azure.Core;
using Azure.Storage.Queues;
using FTLInsightsLogger.Settings;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLInsightsLogger.Logger
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
    public interface IQueueLogger
    {
        void Log(string message, string requestId = "");

        void LogAsync(string message, string requestId = "");

        void Log(object message, string requestId = "");

        void LogAsync(object message, string requestId = "");

        void SetLogger(ITelemetryLogger telemetryLogger);
    }

    public class QueueLogger: IQueueLogger
    {
        private readonly FTLLoggerSettings settings;
        private readonly QueueClient queueClient;
        private ITelemetryLogger logger;

        public QueueLogger(FTLLoggerSettings s)
        {
            settings = s;
            queueClient = new QueueClient(settings.AzureStorageConnectionString, settings.QueueName);
        }

        public void SetLogger(ITelemetryLogger telemetryLogger)
        {
            this.logger = telemetryLogger;
        }

        public void Log(string message, string requestId = "")
        {
            try
            {
                if (queueClient.Exists())
                {
                    queueClient.SendMessage(message);
                }
            }
            catch(Exception ex)
            {
                logger?.Exception(ex, logger.GetExceptionProperty(requestId));
            }
        }

        public async void LogAsync(string message, string requestId = "")
        {
            try
            {
                if (await queueClient.ExistsAsync())
                {
                    queueClient.SendMessageAsync(message);
                }
            }
            catch (Exception ex)
            {
                logger?.Exception(ex, logger.GetExceptionProperty(requestId));
            }
        }

        public void Log(object message, string requestId = "")
        {
            try
            {
                if (queueClient.Exists())
                {
                    queueClient.SendMessage(StringCompressor.CompressString(Newtonsoft.Json.JsonConvert.SerializeObject(message)));
                }
            }
            catch (Exception ex)
            {
                logger?.Exception(ex, logger.GetExceptionProperty(requestId));
            }
        }

        public async void LogAsync(object message, string requestId = "")
        {
            try
            {
                if (await queueClient.ExistsAsync())
                {
                    queueClient.SendMessageAsync(Newtonsoft.Json.JsonConvert.SerializeObject(message));
                }
            }
            catch (Exception ex)
            {
                logger?.Exception(ex, logger.GetExceptionProperty(requestId));
            }
        }
    }
}
