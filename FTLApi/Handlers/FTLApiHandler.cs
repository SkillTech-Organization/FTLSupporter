﻿using FTLApi.DTO.Request;
using FTLApi.DTO.Response;
using FTLInsightsLogger.Logger;
using FTLInsightsLogger.Settings;
using FTLSupporter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;
using Task = System.Threading.Tasks.Task;

namespace FTLApi.Handlers
{
    public class FTLApiHandler : IFTLApiHandler
    {
        private FTLLoggerSettings Settings { get; set; }

        private ITelemetryLogger Logger { get; set; }

        private const string BLOB_SUFFIX = "_response";
        private string MapStorageConnectString;
        private FTLInterface _FTLInterface;

        public FTLApiHandler(IOptions<FTLLoggerSettings> options, IOptions<MapStorageSettings> mapStorageSettings)
        {
            _FTLInterface = new FTLInterface();
            Settings = options.Value;
            Logger = TelemetryClientFactory.Create(Settings);
            Logger.LogToQueueMessage = _FTLInterface.LogToQueueMessage;
            MapStorageConnectString = mapStorageSettings.Value.AzureStorageConnectionString;
        }

        public Task<FTLResponse> FTLSupportAsync(FTLSupportRequest body, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();
            try
            {
                var requestId = _FTLInterface.GenerateRequestId();

                // POST mentése Blobba
                if (Logger.Blob != null)
                    Logger.Blob.LogString(JsonConvert.SerializeObject(body), requestId + "_request").Wait();

                var initResult = _FTLInterface.FTLInit(body.TaskList, body.TruckList, body.MaxTruckDistance, Settings, MapStorageConnectString, requestId);
                if (initResult != null)
                {
                    response = initResult;
                }

                if (initResult != null && !initResult.HasError)
                {
                    Task.Run(() => _FTLInterface.FTLSupport(body.TaskList, body.TruckList, body.MaxTruckDistance));
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response.RequestID), intoQueue: false);
                throw;
            }
            return Task.FromResult(response);
        }

        public ActionResult Result(string requestId)
        {
            var response = new FTLResponse();
            try
            {
                string blobName = requestId + BLOB_SUFFIX;

                if (!Logger.Blob.CheckIfBlobExists(blobName))
                {
                    return new NotFoundObjectResult("The requested resource was not found.");
                }

                response = Logger.Blob.GetLoggedJsonAs<FTLResponse>(blobName).Result;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Logger.GetExceptionProperty(response?.RequestID ?? ""), intoQueue: false);
                throw;
            }
            var res = (List<FTLResult>)response.Result;
            res.ForEach(r =>
           {
               if (r.Status == FTLResult.FTLResultStatus.RESULT)
               {
                   r.CalcTaskList.ForEach(ctl =>
                  {
                      ctl.CalcTours.ForEach(ct =>
                     {
                         if (ct.StatusEnum == FTLCalcTour.FTLCalcTourStatus.ERR)
                         {
                             ct.RelCalcRoute = new FTLCalcRoute();
                             ct.RetCalcRoute = new FTLCalcRoute();
                             ct.T1CalcRoute = new List<FTLCalcRoute>();
                             ct.T2CalcRoute = new List<FTLCalcRoute>();
                         }
                     });
                  });
               }
           });

            return new OkObjectResult(response);
        }

        public Task<FTLResponse> FTLSupportXAsync(FTLSupportRequest body, CancellationToken cancellationToken = default)
        {
            var response = new FTLResponse();
            try
            {
                var requestId = _FTLInterface.GenerateRequestId();

                // POST mentése Blobba
                Logger.Blob.LogString(JsonConvert.SerializeObject(body), requestId + "_request").Wait();

                var initResult = _FTLInterface.FTLInit(body.TaskList, body.TruckList, body.MaxTruckDistance, Settings, MapStorageConnectString, requestId);
                if (initResult != null)
                {
                    response = initResult;
                }

                if (initResult != null && !initResult.HasError)
                {
                    Task.Run(() => _FTLInterface.FTLSupportX(body.TaskList, body.TruckList, body.MaxTruckDistance));
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
