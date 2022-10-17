using PMapCore.Common.Attrib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace FTLSupporter
{
    [Serializable]
    public class FTLTask
    {
        public FTLTask ShallowCopy()
        {
            return (FTLTask)this.MemberwiseClone();
        }


        [ItemIDAttr]
        [DisplayNameAttributeX(Name = "Feladat azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TaskID")]
        [JsonPropertyName("taskID")]
        public string TaskID { get; set; } = "";

        [DisplayNameAttributeX(Name = "Rakománytípus", Order = 2)]
        [Required(ErrorMessage = "Kötelező mező:CargoType")]
        [JsonPropertyName("cargoType")]
        public string CargoType { get; set; } = "";

        [DisplayNameAttributeX(Name = "Teljesítő járműtípusok", Order = 3)]
        [JsonPropertyName("truckTypes")]
        public string TruckTypes { get; set; } = "";

        [DisplayNameAttributeX(Name = "Súly", Order = 4)]
        [JsonPropertyName("weight")]
        public double Weight { get; set; } = 0;

        [DisplayNameAttributeX(Name = "Megbízó", Order = 5)]
        [JsonPropertyName("client")]
        public string Client { get; set; } = "";

        [DisplayNameAttributeX(Name = "Túrapontok", Order = 6)]
        [Required(ErrorMessage = "Kötelező mező:TPoints")]
        [JsonPropertyName("tPoints")]
        public List<FTLPoint> TPoints { get; set; } = new List<FTLPoint>();

        [DisplayNameAttributeX(Name = "Engedélyező járműtulajdonságok", Order = 7)]
        [JsonPropertyName("inclTruckProps")]
        public string InclTruckProps { get; set; } = "";

        [DisplayNameAttributeX(Name = "Kizáró járműtulajdonságok", Order = 8)]
        [JsonPropertyName("exclTruckProps")]
        public string ExclTruckProps { get; set; } = "";

    }
}
