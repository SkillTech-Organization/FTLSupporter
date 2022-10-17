using FTLApi.DTO.Request;
using FTLApi.DTO.Response;
using FTLSupporter;
using System.Reflection;
using Task = System.Threading.Tasks.Task;

namespace FTLApi.Handlers
{
    public class FTLApiHandler : IFTLApiHandler
    {
        public FTLApiHandler ()
        {

        }

        public Task<FTLResponse> FTLSupportAsync(FTLSupportRequest body, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, maxTruckDistance);

            var result = FTLInterface.FTLSupport(body.TaskList, body.TruckList, maxTruckDistance);
            return Task.FromResult(initResult);
        }

        public Task<FTLResponse> FTLSupportXAsync(FTLSupportRequest body, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, maxTruckDistance);

            var result = FTLInterface.FTLSupportX(body.TaskList, body.TruckList, maxTruckDistance);
            return Task.FromResult(initResult);
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
