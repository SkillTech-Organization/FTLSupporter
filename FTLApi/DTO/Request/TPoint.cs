using FTLSupporter;

namespace FTLApi.DTO.Request
{
    public class TPoint
    {
        public string TPID;

        public string Name;

        public string Addr;

        public DateTime Open;

        public DateTime Close;

        public int SrvDuration;

        public int ExtraPeriod;

        public int Lat;

        public int Lng;

        public DateTime RealArrival;

        public static explicit operator FTLPoint(TPoint t)
        {
            return new FTLPoint
            {
                TPID = t.TPID,
                Name = t.Name,
                Addr = t.Addr,
                Open = t.Open,
                Close = t.Close,
                SrvDuration = t.SrvDuration,
                ExtraPeriod = t.ExtraPeriod,
                Lat = t.Lat,
                Lng = t.Lng,
                RealArrival = t.RealArrival
            };
        }
    }
}
