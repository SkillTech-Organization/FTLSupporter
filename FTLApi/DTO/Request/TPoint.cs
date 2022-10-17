using FTLSupporter;
using System.Text.Json.Serialization;

namespace FTLApi.DTO.Request
{
    public class TPoint
    {
        [JsonPropertyName("tpid")]
        public string Tpid;

        [JsonPropertyName("name")]
        public string Name;

        [JsonPropertyName("addr")]
        public string Addr;

        [JsonPropertyName("open")]
        public DateTime Open;

        [JsonPropertyName("close")]
        public DateTime Close;

        [JsonPropertyName("srvDuration")]
        public int SrvDuration;

        [JsonPropertyName("extraPeriod")]
        public int ExtraPeriod;

        [JsonPropertyName("lat")]
        public int Lat;

        [JsonPropertyName("lng")]
        public int Lng;

        [JsonPropertyName("realArrival")]
        public DateTime RealArrival;

        public static explicit operator FTLPoint(TPoint t)
        {
            return new FTLPoint
            {
                TPID = t.Tpid,
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
