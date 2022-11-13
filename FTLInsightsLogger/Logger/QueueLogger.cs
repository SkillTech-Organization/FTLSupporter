using Azure.Core;
using Azure.Storage.Queues;
using FTLInsightsLogger.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTLInsightsLogger.Logger
{
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
                    queueClient.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(message));
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
