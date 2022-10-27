using FTLApi.DTO.Request;
using FTLApi.DTO.Response;
using FTLInsightsLogger.Logger;
using FTLInsightsLogger.Settings;
using FTLSupporter;
using Microsoft.Extensions.Options;
using System.Reflection;
using Task = System.Threading.Tasks.Task;

namespace FTLApi.Handlers
{
    public class FTLApiHandler : IFTLApiHandler
    {
        private FTLLoggerSettings Settings { get; set; }

        private ITelemetryLogger Logger { get; set; }

        public FTLApiHandler (IOptions<FTLLoggerSettings> options)
        {
            Settings = options.Value;
            Logger = TelemetryClientFactory.Create(Settings);
        }

        public Task<FTLResponse> FTLSupportAsync(FTLSupportRequest body, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();
            try
            {
                var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, maxTruckDistance, Settings);
                if (initResult != null)
                {
                    response = initResult;
                }
                response.TaskList = body.TaskList;
                response.TruckList = body.TruckList;

                if (initResult != null && !initResult.HasError)
                {
                    Task.Run(() => FTLInterface.FTLSupport(body.TaskList, body.TruckList, maxTruckDistance));
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response.RequestID));
            }
            return Task.FromResult(response);
        }

        public Task<FTLResponse> FTLSupportXAsync(FTLSupportRequest body, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();
            try
            {
                var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, maxTruckDistance, Settings);
                if (initResult != null)
                {
                    response = initResult;
                }
                response.TaskList = body.TaskList;
                response.TruckList = body.TruckList;

                if (initResult != null && !initResult.HasError)
                {
                    Task.Run(() => FTLInterface.FTLSupportX(body.TaskList, body.TruckList, maxTruckDistance));
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response.RequestID));
            }
            return Task.FromResult(response);
        }

        public Task IsAliveAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new IsAliveOk
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            });
        }
    }
}
