using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.Markers;
using GMap.NET.WindowsForms;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using PMapCore.Common.Attrib;
using Newtonsoft.Json;
using PMapCore.Common.Azure;
using System.ComponentModel;

namespace PMapCore.WebTrace
{
    [Serializable]
    [DataContract(Namespace = "TourPoint")]
    public class PMTourPoint : AzureTableObjBase
    {
        public enum enTourPointTypes
        {
            [Description("Warehouse")]
            WHS,
            [Description("Depot")]
            DEP
        }

        [DataMember]
        [DisplayNameAttributeX(Name = "Túra azonosítója")]
        [AzureTablePartitionKeyAttr]
        public int TourID { get; set; }


        [DataMember]
        [AzureTableRowKeyAttr]
        [DisplayNameAttributeX(Name = "Túrapont sorszáma")]
        public int Order { get; set; }


    
        [DataMember]
        [DisplayNameAttributeX(Name = "Távolság az előző túraponttól")]
        public double Distance { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Megérkezés")]
        public DateTime ArrTime { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Kiszolgálás kezdete")]
        public DateTime ServTime { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Távozás")]
        public DateTime DepTime { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Raktár/lerakó/felrakó kód")]
        public string Code { get; set; }
        [DataMember]
        [DisplayNameAttributeX(Name = "Raktár/lerakó/felrakó megnevezés")]
        public string Name { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Raktár/lerakó/felrakó cím")]
        public string Addr { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Raktár/lerakó/felrakó nyitva tartás kezdete")]
        public DateTime Open { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Raktár/lerakó/felrakó nyitva tartás vége")]
        public DateTime Close { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Raktár/lerakó/felrakó pozíció")]
        public string Position { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Megrendelés száma (külső azonosító)")]
        public string OrdNum { get; set; }

        [DataMember]
        [DisplayNameAttributeX(Name = "Útvonal-pontok")]
        public List<PMMapPoint> MapPoints { get; set; } = new List<PMMapPoint>();


        [DisplayNameAttributeX(Name = "Túrapont típus")]

        [IgnoreDataMember]
        private enTourPointTypes m_type { get; set; }

        [DataMember]
        public string Type
        {
            get { return Enum.GetName(typeof(enTourPointTypes), m_type); }
            set
            {
                if (value != null)
                    m_type = (enTourPointTypes)Enum.Parse(typeof(enTourPointTypes), value);
                else
                    m_type = enTourPointTypes.DEP;

                NotifyPropertyChanged("Type");
            }
        }
        

    }

}
