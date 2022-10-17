using System.Text.Json.Serialization;

namespace FTLApi.DTO.Response
{
    public class ResponseOk
    {
        [JsonPropertyName("requestID")]
        public string RequestID;
    }
}
