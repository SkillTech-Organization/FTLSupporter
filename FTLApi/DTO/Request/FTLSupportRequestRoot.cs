using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class FTLSupportRequestRoot
    {
        [JsonPropertyName("taskList")]
        public List<TaskList> TaskList;

        [JsonPropertyName("truckList")]
        public List<TruckList> TruckList;
    }
}
