using Microsoft.Extensions.Configuration;

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
        public int? MaxTruckDistance { get; set; }
        public int MaxMessagesFromQueueAtOnce { get; set; }
        public int MaxMessageTimeSpanInMinutes { get; set; }
    }
}