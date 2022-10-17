using System.Text.Json.Serialization;

namespace FTLApi.DTO.Response
{
    public class IsAliveOk
    {
        [JsonPropertyName("version")]
        public string Version;
    }
}
