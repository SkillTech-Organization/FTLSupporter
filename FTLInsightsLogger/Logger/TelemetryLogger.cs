using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace FTLInsightsLogger.Logger
{
    public interface ITelemetryLogger
    {
        TelemetryClient Client { get; }

        /// <summary>
        /// Comitting log messages after every log message
        /// </summary>
        bool AutoCommitEnabled { get; }

        string IdDefaultValue { get; set; }
        string ExceptionPropertyLabel { get; set; }
        string RunPropertyLabel { get; set; }
        string StatusPropertyLabel { get; set; }

        Dictionary<string, string> GetExceptionProperty(string id);

        Dictionary<string, string> GetRunProperty(string id);

        Dictionary<string, string> GetStatusProperty(string id);

        void Info(string message, Dictionary<string, string> properties = null);

        void Error(string message, Dictionary<string, string> properties = null);

        void Warning(string message, Dictionary<string, string> properties = null);

        void Verbose(string message, Dictionary<string, string> properties = null);

        void Exception(Exception ex, Dictionary<string, string> properties = null);

        void Commit();

        Task CommitAsync(CancellationToken? cancellationToken);
    }

    public class TelemetryLogger : ITelemetryLogger, IDisposable
    {
        public TelemetryClient Client { get; private set; }

        public bool AutoCommitEnabled { get; private set; } = true;

        public string IdDefaultValue { get; set; } = "No Data";
        public string ExceptionPropertyLabel { get; set; } = "EXCEPTION";
        public string RunPropertyLabel { get; set; } = "RUN";
        public string StatusPropertyLabel { get; set; } = "STATUS";

        internal TelemetryLogger(string connectionString, bool autoCommit)
        {
            var configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString = connectionString;
            Client = new TelemetryClient(configuration);

            AutoCommitEnabled = autoCommit;
        }

        public void Info(string message, Dictionary<string, string> properties = null)
        {
            Client.TrackTrace(message, SeverityLevel.Information, properties);
            if (AutoCommitEnabled)
            {
                Commit();
            }
        }

        public void Error(string message, Dictionary<string, string> properties = null)
        {
            Client.TrackTrace(message, SeverityLevel.Error, properties);
            if (AutoCommitEnabled)
            {
                Commit();
            }
        }

        public void Warning(string message, Dictionary<string, string> properties = null)
        {
            Client.TrackTrace(message, SeverityLevel.Warning, properties);
            if (AutoCommitEnabled)
            {
                Commit();
            }
        }

        public void Verbose(string message, Dictionary<string, string> properties = null)
        {
            Client.TrackTrace(message, SeverityLevel.Verbose, properties);
            if (AutoCommitEnabled)
            {
                Commit();
            }
        }

        public void Exception(Exception ex, Dictionary<string, string> properties = null)
        {
            Client.TrackException(ex, properties);
            if (AutoCommitEnabled)
            {
                Commit();
            }
        }

        public void Commit()
        {
            Client.Flush();
        }

        public async Task CommitAsync(CancellationToken? cancellationToken)
        {
            await Client.FlushAsync(cancellationToken ?? CancellationToken.None);
        }

        public void Dispose()
        {
            var task = Client.FlushAsync(CancellationToken.None);
            task.Wait();
        }

        public Dictionary<string, string> GetExceptionProperty(string id)
        {
            var properties = new Dictionary<string, string>
            {
                { ExceptionPropertyLabel, id ?? IdDefaultValue }
            };
            return properties;
        }

        public Dictionary<string, string> GetRunProperty(string id)
        {
            var properties = new Dictionary<string, string>
            {
                { RunPropertyLabel, id ?? IdDefaultValue }
            };
            return properties;
        }

        public Dictionary<string, string> GetStatusProperty(string id)
        {
            var properties = new Dictionary<string, string>
            {
                { StatusPropertyLabel, id ?? IdDefaultValue }
            };
            return properties;
        }
    }

    public class TelemetryLoggerMock : ITelemetryLogger
    {
        public TelemetryClient Client { get; private set; }

        public bool AutoCommitEnabled { get; private set; }

        public string IdDefaultValue { get; set; }
        public string ExceptionPropertyLabel { get; set; }
        public string RunPropertyLabel { get; set; }
        public string StatusPropertyLabel { get; set; }

        public void Commit()
        {
        }

        public Task CommitAsync(CancellationToken? cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Error(string message, Dictionary<string, string> properties = null)
        {
        }

        public void Exception(Exception ex, Dictionary<string, string> properties = null)
        {
        }

        public Dictionary<string, string> GetExceptionProperty(string id)
        {
            return new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetRunProperty(string id)
        {
            return new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetStatusProperty(string id)
        {
            return new Dictionary<string, string>();
        }

        public void Info(string message, Dictionary<string, string> properties = null)
        {
        }

        public void Verbose(string message, Dictionary<string, string> properties = null)
        {
        }

        public void Warning(string message, Dictionary<string, string> properties = null)
        {
        }
    }
}
