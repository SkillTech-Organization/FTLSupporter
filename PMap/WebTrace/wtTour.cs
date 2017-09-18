using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET.WindowsForms;
using System.Web.Script.Serialization;
using PMap.Common.Attrib;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PMap.Common.Azure;

namespace PMap.WebTrace
{
    [Serializable]
    [DataContract(Namespace = "Tour")]
    public class wtTour : AzureTableObjBase
    {

        public const string PartitonConst = "TOUR";

        [DataMember]
        [AzureTablePartitionKeyAttr]
        public string PartitionKey { get;  } = PartitonConst;

        private string m_ID { get; set; }
        [DataMember]
        [AzureTableRowKeyAttr]
        [AzureTableFieldAttr(FieldName = "RowKey")]
        public string ID
        {
            get { return m_ID; }
            set
            {
                m_ID = value;
                NotifyPropertyChanged("ID");
            }
        }

        [DataMember]
        [DisplayNameAttributeX(Name = "Szállító")]
        public string Carrier { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Rendszám")]
        public string TruckRegNo { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Összsúly")]
        public int TruckWeight { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Teljes magasság")]
        public int TruckHeight { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Teljes szélesség")]
        public int TruckWidth { get; set; }


        [DataMember]
        [DisplayNameAttributeX(Name = "Túra kezdés")]
        public DateTime Start { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Túra befejezés")]
        public DateTime End { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Teljes hossz (m)")]
        public double TourLength { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Összmennyiség")]
        public double Qty { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Össztérfogat")]
        public double Vol { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Összútdíj")]
        public double Toll { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Túrapontok száma")]
        public int TourPointCnt { get; set; }               //0 esetén nincs a járműnek túrája

        [DataMember]
        [DisplayNameAttributeX(Name = "Túra szinezése")]
        public string TourColor { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Jármű szinezése")]
        public string TruckColor { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Túrapontok listája")]
        public List<wtTourPoint> TourPoints { get; set; } = new List<wtTourPoint>();


    }

}
