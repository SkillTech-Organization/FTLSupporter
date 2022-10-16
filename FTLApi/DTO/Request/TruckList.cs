using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    public class TruckList
    {
        [JsonPropertyName("truckID")]
        public string TruckID;

        [JsonPropertyName("gvwr")]
        public int Gvwr;

        [JsonPropertyName("capacity")]
        public int Capacity;

        [JsonPropertyName("truckType")]
        public string TruckType;

        [JsonPropertyName("cargoTypes")]
        public string CargoTypes;

        [JsonPropertyName("fixCost")]
        public int FixCost;

        [JsonPropertyName("kmCost")]
        public int KmCost;

        [JsonPropertyName("relocateCost")]
        public int RelocateCost;

        [JsonPropertyName("maxKM")]
        public int MaxKM;

        [JsonPropertyName("maxDuration")]
        public int MaxDuration;

        [JsonPropertyName("engineEuro")]
        public int EngineEuro;

        [JsonPropertyName("eTollCat")]
        public int ETollCat;

        [JsonPropertyName("rZones")]
        public string RZones;

        [JsonPropertyName("width")]
        public int Width;

        [JsonPropertyName("height")]
        public int Height;

        [JsonPropertyName("truckProps")]
        public string TruckProps;

        [JsonPropertyName("remainingDriveTime")]
        public int RemainingDriveTime;

        [JsonPropertyName("remainingRestTime")]
        public int RemainingRestTime;

        [JsonPropertyName("remainingTimeToStartDailyRest")]
        public int RemainingTimeToStartDailyRest;

        [JsonPropertyName("remainingDailyDriveTime")]
        public int RemainingDailyDriveTime;

        [JsonPropertyName("remainingDailyRestTime")]
        public int RemainingDailyRestTime;

        [JsonPropertyName("remainingWeeklyDriveTime")]
        public int RemainingWeeklyDriveTime;

        [JsonPropertyName("remainingWeeklyRestTime")]
        public int RemainingWeeklyRestTime;

        [JsonPropertyName("remainingTwoWeeklyDriveTime")]
        public int RemainingTwoWeeklyDriveTime;

        [JsonPropertyName("remainingTwoWeeklyRestTime")]
        public int RemainingTwoWeeklyRestTime;

        [JsonPropertyName("remainingRestTimeToCompensate")]
        public int RemainingRestTimeToCompensate;

        [JsonPropertyName("truckTaskType")]
        public int TruckTaskType;

        [JsonPropertyName("runningTaskID")]
        public string RunningTaskID;

        [JsonPropertyName("currIsOneWay")]
        public bool CurrIsOneWay;

        [JsonPropertyName("currTime")]
        public DateTime CurrTime;

        [JsonPropertyName("currLat")]
        public int CurrLat;

        [JsonPropertyName("currLng")]
        public int CurrLng;

        [JsonPropertyName("currTPoints")]
        public List<CurrTPoint> CurrTPoints;

        [JsonPropertyName("tPointCompleted")]
        public int TPointCompleted;
    }
}
