using FTLSupporter;

namespace FTLApi.DTO.Request
{
    public class Task
    {
        public string TaskID;

        public string CargoType;

        public string TruckTypes;

        public int Weight;

        public string Client;

        public List<TPoint> TPoints;

        public string InclTruckProps;

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
