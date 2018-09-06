﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET.WindowsForms;
using System.Web.Script.Serialization;
using PMapCore.Common.Attrib;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PMapCore.Common.Azure;

namespace PMapCore.WebTrace
{
    [Serializable]
    [DataContract(Namespace = "Tour")]
    public class PMTour : AzureTableObjBase
    {

        public const string PartitonConst = "TOUR";

        [DataMember]
        [AzureTablePartitionKeyAttr]
        public string PartitionKey { get; set; } = PartitonConst;

        private string m_ID { get; set; }
        [DataMember]
        [AzureTableRowKeyAttr]
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
        public int PLN_ID { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Szállító")]
        public string Carrier { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Rendszám")]
        public string TruckRegNo { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Behajtási övezet lista")]
        public string RZN_ID_LIST { get; set; }

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

        [IgnoreDataMember]
        [JsonProperty("TourPointCnt")]
        [DisplayNameAttributeX(Name = "Túrapontok száma")]
        public int TourPointCnt {
            get
            {
                return TourPoints.Count();
            }
        }          

        [DataMember]
        [DisplayNameAttributeX(Name = "Túra szinezése")]
        public string TourColor { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Jármű szinezése")]
        public string TruckColor { get; set; }

        [IgnoreDataMember]
        [JsonProperty("TourPoints")]
        [DisplayNameAttributeX(Name = "Túrapontok listája")]
        public List<PMTourPoint> TourPoints { get; set; } = new List<PMTourPoint>();


    }

}
