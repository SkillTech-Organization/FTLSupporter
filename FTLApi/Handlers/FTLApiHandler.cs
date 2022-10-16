using FTLApi.DTO.Request;
using FTLApi.DTO.Response;
using FTLApi.Helpers;
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

        public Task<List<FTLResult>> FTLSupportAsync(FTLSupportRequest body, string content_Type, string accept, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var tastkList = body.TaskList.ToFTLTasks();
            var truckList = body.TruckList.ToFTLTrucks();

            var result = FTLInterface.FTLSupport(tastkList, truckList, maxTruckDistance);
            return Task.FromResult(result);
        }

        public Task<List<FTLResult>> FTLSupportXAsync(FTLSupportRequest body, string content_Type, string accept, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var tastkList = body.TaskList.ToFTLTasks();
            var truckList = body.TruckList.ToFTLTrucks();

            var result = FTLInterface.FTLSupportX(tastkList, truckList, maxTruckDistance);
            return Task.FromResult(result);
        }

        public Task IsAliveAsync(string accept, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new IsAliveOk
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            });
        }
    }
}
