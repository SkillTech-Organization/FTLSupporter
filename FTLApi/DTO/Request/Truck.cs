using FTLSupporter;
using static FTLSupporter.FTLTruck;

namespace FTLApi.DTO.Request
{
    public class Truck
    {
        public string TruckID;

        public int GVWR;

        public int Capacity;

        public string TruckType;

        public string CargoTypes;

        public int FixCost;

        public int KmCost;

        public int RelocateCost;

        public int MaxKM;

        public int MaxDuration;

        public int EngineEuro;

        public int ETollCat;

        public string RZones;

        public int Width;

        public int Height;

        public string TruckProps;

        public int RemainingDriveTime;

        public int RemainingRestTime;

        public int RemainingTimeToStartDailyRest;

        public int RemainingDailyDriveTime;

        public int RemainingDailyRestTime;

        public int RemainingWeeklyDriveTime;

        public int RemainingWeeklyRestTime;

        public int RemainingTwoWeeklyDriveTime;

        public int RemainingTwoWeeklyRestTime;

        public int RemainingRestTimeToCompensate;

        public int TruckTaskType;

        public string RunningTaskID;

        public bool CurrIsOneWay;

        public DateTime CurrTime;

        public int CurrLat;

        public int CurrLng;

        public List<TPoint> CurrTPoints;

        public int TPointCompleted;

        public static explicit operator FTLTruck(Truck t)
        {
            return new FTLTruck
            {
                TruckID = t.TruckID,
                GVWR = t.GVWR,
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
