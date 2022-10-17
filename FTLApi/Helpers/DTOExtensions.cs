using FTLSupporter;

namespace FTLApi.Helpers
{
    public static class DTOExtensions
    {
        public static List<FTLPoint> ToFTLPoints(this List<DTO.Request.TPoint> t)
        {
            return t.Select(t => (FTLPoint)t).ToList();
        }

        public static List<FTLTask> ToFTLTasks(this List<DTO.Request.Task> t)
        {
            return t.Select(t => (FTLTask)t).ToList();
        }

        public static List<FTLTruck> ToFTLTrucks(this List<DTO.Request.Truck> t)
        {
            return t.Select(t => (FTLTruck)t).ToList();
        }
    }
}
