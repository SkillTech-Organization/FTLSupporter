using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class FTLSupportRequest
    {
        [JsonPropertyName("taskList")]
        public List<Task> TaskList;

        [JsonPropertyName("truckList")]
        public List<Truck> TruckList;
    }
}
