using FTLSupporter;
using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    public class Task
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

        public static explicit operator FTLTask(Task t)
        {
            return new FTLTask
            {
                CargoType = t.CargoType,
                Client = t.Client,
                ExclTruckProps = t.ExclTruckProps,
                InclTruckProps = t.InclTruckProps,
                TaskID = t.TaskID,
                TPoints = t.TPoints.Select(t => (FTLPoint)t).ToList(),
                TruckTypes = t.TruckTypes,
                Weight = t.Weight
            };
        }
    }
}
