using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    public class TaskList
    {
        [JsonPropertyName("taskID")]
        public string TaskID;

        [JsonPropertyName("cargoType")]
        public string CargoType;

        [JsonPropertyName("truckTypes")]
        public string TruckTypes;

        [JsonPropertyName("weight")]
        public int Weight;

        [JsonPropertyName("client")]
        public string Client;

        [JsonPropertyName("tPoints")]
        public List<TPoint> TPoints;

        [JsonPropertyName("inclTruckProps")]
        public string InclTruckProps;

        [JsonPropertyName("exclTruckProps")]
        public string ExclTruckProps;
    }
}
