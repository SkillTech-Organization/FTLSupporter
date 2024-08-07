﻿using Microsoft.Extensions.Configuration;

namespace FTLApiTester.Settings
{
    public class FTLApiTesterSettings
    {
        public FTLApiTesterSettings(IConfiguration configuration)
        {
            configuration.Bind("FTLApiTester", this);
        }

        public string AzureStorageConnectionString { get; set; }
        public string QueueName { get; set; }
        public string TestDataPath { get; set; }
        public string FTLApiBaseUrl { get; set; }
        public string ResultFileIdentifier { get; set; }
        public string TaskFileIdentifier { get; set; }
        public string TruckFileIdentifier { get; set; }
        public string TestResultFileIdentifier { get; set; }
        public string FTLSupportFileSuffix { get; set; }
        public string FTLSupportXFileSuffix { get; set; }
        public string FileExtension { get; set; }
        public int? MaxTruckDistance { get; set; }
        public int MaxMessagesFromQueueAtOnce { get; set; }
        public int MaxMessageTimeSpanInMinutes { get; set; }
        public int WaitBeforeBetweenQueueQueryInMs { get; set; }
        public int MaxMessageLimitPerRequest { get; set; }
        public int MaxWaitLimitForResultPerRequestInMinutes { get; set; }
        public bool ClearQueueBeforeGettingMessages { get; set; }
        public bool UseQueue { get; set; }
    }
}