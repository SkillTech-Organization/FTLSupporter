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
            var response = new FTLResponse();

            var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, maxTruckDistance);
            if (initResult != null)
            {
                response = initResult;
            }
            response.TaskList = body.TaskList;
            response.TruckList = body.TruckList;

            if (initResult != null && !initResult.HasError)
            {
                response.Result = FTLInterface.FTLSupport(body.TaskList, body.TruckList, maxTruckDistance);
            }

            return Task.FromResult(response);
        }

        public Task<FTLResponse> FTLSupportXAsync(FTLSupportRequest body, int maxTruckDistance, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();

            var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, maxTruckDistance);
            if (initResult != null)
            {
                response = initResult;
            }
            response.TaskList = body.TaskList;
            response.TruckList = body.TruckList;

            if (initResult != null && !initResult.HasError)
            {
                response.Result = FTLInterface.FTLSupportX(body.TaskList, body.TruckList, maxTruckDistance);
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
