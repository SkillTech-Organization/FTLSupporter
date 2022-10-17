using FTLSupporter;
using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class FTLSupportRequest
    {
        [JsonPropertyName("taskList")]
        public List<FTLTask> TaskList;

        [JsonPropertyName("truckList")]
        public List<FTLTruck> TruckList;
    }
}
