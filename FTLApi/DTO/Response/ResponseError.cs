using System.Text.Json.Serialization;

namespace FTLApi.DTO.Response
{
    public class ResponseError
    {
        [JsonPropertyName("errorCode")]
        public string ErrorCode;

        [JsonPropertyName("errorMessages")]
        public List<string> ErrorMessages;
    }
}
