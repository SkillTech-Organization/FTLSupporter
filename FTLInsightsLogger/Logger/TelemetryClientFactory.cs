using System;
using FTLInsightsLogger.Settings;

namespace FTLInsightsLogger.Logger
{
    public class TelemetryClientFactory
    {
        public static ITelemetryLogger Create(FTLLoggerSettings settings)
        {
            try
            {
                if (settings == null || string.IsNullOrWhiteSpace(settings.ApplicationInsightsConnectionString))
                {
                    return new TelemetryLoggerMock();
                }
                return new TelemetryLogger(settings.ApplicationInsightsConnectionString, settings.AutoCommitAfterEveryLogEnabled);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
