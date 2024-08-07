﻿namespace FTLInsightsLogger.Settings
{
    public class FTLLoggerSettings
    {
        public string ApplicationInsightsConnectionString { get; set; }

        public string AzureStorageConnectionString { get; set; }

        public string QueueName { get; set; }

        public bool UseQueue { get; set; }

        public bool AutoCommitAfterEveryLogEnabled { get; set; }

        public string ResultBlobContainer { get; set; }

        public string ResultLinkBase { get; set; }
    }
}
