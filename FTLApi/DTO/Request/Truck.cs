using FTLSupporter;
using System.Text.Json.Serialization;
using static FTLSupporter.FTLTruck;

namespace FTLApi.DTO.Request
{
    public class Truck
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
        public List<TPoint> CurrTPoints;

        [JsonPropertyName("tPointCompleted")]
        public int TPointCompleted;

        public static explicit operator FTLTruck(Truck t)
        {
            return new FTLTruck
            {
                TruckID = t.TruckID,
                GVWR = t.Gvwr,
                Capacity = t.Capacity,
                TruckType = t.TruckType,
                CargoTypes = t.CargoTypes,
                FixCost = t.FixCost,
                KMCost = t.KmCost,
                RelocateCost = t.RelocateCost,
                MaxKM = t.MaxKM,
                MaxDuration = t.MaxDuration,
                EngineEuro = t.EngineEuro,
                ETollCat = t.ETollCat,
                RZones = t.RZones,
                Width = t.Width,
                Height = t.Height,
                TruckProps = t.TruckProps,
                RemainingDriveTime = t.RemainingDriveTime,
                RemainingRestTime = t.RemainingRestTime,
                RemainingTimeToStartDailyRest = t.RemainingTimeToStartDailyRest,
                RemainingDailyDriveTime = t.RemainingDailyDriveTime,
                RemainingDailyRestTime = t.RemainingDailyRestTime,
                RemainingWeeklyDriveTime = t.RemainingWeeklyDriveTime,
                RemainingWeeklyRestTime = t.RemainingWeeklyRestTime,
                RemainingTwoWeeklyDriveTime = t.RemainingTwoWeeklyDriveTime,
                RemainingTwoWeeklyRestTime = t.RemainingTwoWeeklyRestTime,
                RemainingRestTimeToCompensate = t.RemainingRestTimeToCompensate,
                TruckTaskType = (eTruckTaskType)Enum.Parse(typeof(eTruckTaskType), t.TruckTaskType.ToString()),
                RunningTaskID = t.RunningTaskID,
                CurrIsOneWay = t.CurrIsOneWay,
                CurrTime = t.CurrTime,
                CurrLat = t.CurrLat,
                CurrLng = t.CurrLng,
                CurrTPoints = t.CurrTPoints.Select(t => (FTLPoint)t).ToList(),
                TPointCompleted = t.TPointCompleted,
            };
        }
    }
}
