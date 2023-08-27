using FTLApi.DTO.Request;
using FTLApi.DTO.Response;
using FTLInsightsLogger.Logger;
using FTLInsightsLogger.Settings;
using FTLSupporter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Task = System.Threading.Tasks.Task;

namespace FTLApi.Handlers
{
    public class FTLApiHandler : IFTLApiHandler
    {
        private FTLLoggerSettings Settings { get; set; }

        private ITelemetryLogger Logger { get; set; }

        public FTLApiHandler(IOptions<FTLLoggerSettings> options)
        {
            Settings = options.Value;
            Logger = TelemetryClientFactory.Create(Settings);
            Logger.LogToQueueMessage = FTLInterface.LogToQueueMessage;
        }

        public Task<FTLResponse> FTLSupportAsync(FTLSupportRequest body, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();
            try
            {
                var requestId = FTLInterface.GenerateRequestId();

                // POST mentése Blobba
                Logger.Blob.LogString(JsonConvert.SerializeObject(body), requestId + "_request").Wait();

                var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, body.MaxTruckDistance, Settings, requestId);
                if (initResult != null)
                {
                    response = initResult;
                }

                if (initResult != null && !initResult.HasError)
                {
                    Task.Run(() => FTLInterface.FTLSupport(body.TaskList, body.TruckList, body.MaxTruckDistance));
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response.RequestID), intoQueue: false);
                throw;
            }
            return Task.FromResult(response);
        }

        public ActionResult Result(string blob_name)
        {
            var response = new FTLResponse();
            try
            {
                if (!Logger.Blob.CheckIfBlobExists(blob_name))
                {
                    return new NotFoundObjectResult("The requested resource was not found.");
                }

                //var json = Logger.Blob.GetLoggedString(id).Result;
                //Logger.Info("From blob JSON: " + json, Logger.GetExceptionProperty(response?.RequestID ?? ""), intoQueue: false);
                //response = Newtonsoft.Json.JsonConvert.DeserializeObject<FTLResponse>(json);
                //Logger.Info("From blob is null: " + (response == null).ToString(), Logger.GetExceptionProperty(response?.RequestID ?? ""), intoQueue: false);                
                response = Logger.Blob.GetLoggedJsonAs<FTLResponse>(blob_name).Result;
                //var asd = response.ToJson();
                //response?.Result.ForEach(x =>
                //{
                //    //Logger.Info("Data: " + Newtonsoft.Json.JsonConvert.SerializeObject(x.Data), Logger.GetExceptionProperty(response?.RequestID ?? ""), intoQueue: false);
                //    if (x.Data != null)
                //    {
                //        if (x.Status == FTLResult.FTLResultStatus.RESULT)
                //        {
                //            x.Data = ((JToken)x.Data).ToObject<List<FTLSupporter.FTLCalcTask>>();
                //        }
                //        else
                //        {
                //            x.Data = ((JToken)x.Data).ToObject<Dictionary<string, string>>();
                //        }
                //    }
                //    else
                //    {
                //        x.Data = new List<FTLSupporter.FTLCalcTask>();
                //    }
                //});
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response?.RequestID ?? ""), intoQueue: false);
                throw;
            }
            
            return new OkObjectResult(response);
        }

        public Task<FTLResponse> FTLSupportXAsync(FTLSupportRequest body, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();
            try
            {
                var requestId = FTLInterface.GenerateRequestId();

                // POST mentése Blobba
                Logger.Blob.LogString(JsonConvert.SerializeObject(body), requestId + "_request").Wait();

                var initResult = FTLInterface.FTLInit(body.TaskList, body.TruckList, body.MaxTruckDistance, Settings, requestId);
                if (initResult != null)
                {
                    response = initResult;
                }

                if (initResult != null && !initResult.HasError)
                {
                    Task.Run(() => FTLInterface.FTLSupportX(body.TaskList, body.TruckList, body.MaxTruckDistance));
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response.RequestID), intoQueue: false);
                throw;
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
