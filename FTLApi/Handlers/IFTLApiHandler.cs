using FTLApi.DTO.Request;
using FTLSupporter;
using Microsoft.AspNetCore.Mvc;

namespace FTLApi.Handlers
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.17.0.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public interface IFTLApiHandler
    {

        /// <summary>
        /// Calculate by FTLSupporter engine
        /// </summary>
        /// <param name="body"></param>
        /// <param name="content_Type"></param>
        /// <param name="accept"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<v> FTLSupportAsync(FTLSupportRequest body, int maxTruckDistance, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Calculate by FTLSupporterX engine
        /// </summary>
        /// <param name="body"></param>
        /// <param name="content_Type"></param>
        /// <param name="accept"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<FTLResponse> FTLSupportXAsync(FTLSupportRequest body, int maxTruckDistance, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// get the 'isalive' status of the FTLSupporter service
        /// </summary>
        /// <param name="accept"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task IsAliveAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

    }
}
