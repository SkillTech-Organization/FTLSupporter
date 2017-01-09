﻿using GMap.NET;
using PMap;
using PMap.Common;
using PMap.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FTLSupporter
{
    [Serializable]
    public class FTLTruck
    {
        public enum eTruckTaskType
        {
            Available,                  // Elérhető
            Planned,                    // Tervezett
            Running                     // Futó
        }

        public FTLTruck()
        {
            /* 
            m_currTPoints.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
                delegate(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    if (RetPoint == null)
                    {
                        FTLPoint firstPt = m_currTPoints.FirstOrDefault();
                        if (firstPt != null)
                            RetPoint = new PointLatLng(firstPt.Lat, firstPt.Lng);
                    }
                });
             */
        }

        public FTLTruck ShallowCopy()
        {
            return (FTLTruck)this.MemberwiseClone();
        }


        [ItemIDAttr]
        [DisplayNameAttributeX(Name = "Jármű azonosító", Order = 1)]
        [Required(ErrorMessage = "Kötelező mező:TruckID")]                          /* jellemzően a rendszám */
        public string TruckID { get; set; }

        [DisplayNameAttributeX(Name = "Összsúly", Order = 2)]           /* gross vehicle weight rating */
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:GVWR")]
        public int GVWR { get; set; }

        [DisplayNameAttributeX(Name = "Kapacitás (súly)", Order = 3)]
        public int Capacity { get; set; }

        [DisplayNameAttributeX(Name = "Járműtípus", Order = 4)]
        [Required(ErrorMessage = "Kötelező mező:TruckType")]
        public string TruckType { get; set; }

        [DisplayNameAttributeX(Name = "Kiszolgálható szállítási feladattípusok", Order = 5)]
        [Required(ErrorMessage = "Kötelező mező:CargoTypes")]
        public string CargoTypes { get; set; }

        [DisplayNameAttributeX(Name = "Fix költség", Order = 6)]
        public double FixCost { get; set; }

        [DisplayNameAttributeX(Name = "Túra KM költség", Order = 7)]
        public double KMCost { get; set; }

        [DisplayNameAttributeX(Name = "Átállás KM", Order = 8)]
        public double RelocateCost { get; set; }


        [DisplayNameAttributeX(Name = "Teljesítés max. KM", Order = 9)]
        public double MaxKM { get; set; }

        [DisplayNameAttributeX(Name = "Teljesítés max. idő", Order = 10)]
        public double MaxDuration { get; set; }


        [DisplayNameAttributeX(Name = "motor EURO besorolás", Order = 11)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:EngineEuro")]
        public int EngineEuro { get; set; }                 // 1,2,3,4,.... ==> EURO I, EURO II, EURO III, EURO IV...


        //A díjszámításnál használandó járműkategória. MEGJ: Behajtási övezet ID != járműkategória 
        [DisplayNameAttributeX(Name = "Útdíj járműkategória", Order = 12)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:ETollCat")]
        [ErrorIfConstAttrX(EvalMode.IsBigger, Global.ETOLLCAT_BIGGER12T, "EtollCat:Érték maximum túllépés!")]
        public int ETollCat  { get; set; }                 // 1,2,3,4,.... ==> J1, J2, J3, J4        
        
        //
        [DisplayNameAttributeX(Name = "Behajtási övezetek kódok", Order = 13)]
        public string RZones { get; set; }

        [DisplayNameAttributeX(Name = "Egyéb járműtulajdonságok", Order = 14)]
        public string TruckProps { get; set; }


        /******************* Járműfeladat ******************************/

        [DisplayNameAttributeX(Name = "Jármű szállítási feladat típus", Order = 15)]
        [Required(ErrorMessage = "Kötelező mező:TruckTaskType")]
        public eTruckTaskType TruckTaskType { get; set; }

        [DisplayNameAttributeX(Name = "Futó szállítási feladat azonosító", Order = 16)]
        public string RunningTaskID { get; set; }


        [DisplayNameAttributeX(Name = "Irányos túra?", Order = 17)]
        public bool CurrIsOneWay { get; set; }

        private DateTime m_CurrTime;
        [DisplayNameAttributeX(Name = "Aktuális időpont", Order = 18)]
        [Required(ErrorMessage = "Kötelező mező:TimeCurr")]
        public DateTime CurrTime
        {
            get
            {
                if (TruckTaskType == eTruckTaskType.Planned)        /* tervezett túra esetén az első túrapont indulás időpontja */
                {
                    FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                    if (firstPt != null)
                        return firstPt.RealDeparture;
                    else
                        return DateTime.MinValue;
                }
                else
                    return m_CurrTime;

            }
            set
            {
                if (TruckTaskType != eTruckTaskType.Planned)
                {
                    m_CurrTime = value;
                }
            }
        }

        private double m_CurrLat;
        [DisplayNameAttributeX(Name = "Aktuális hosszúsági koordináta", Order = 19)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:CurrLat")]
        public double CurrLat
        {
            get
            {
                if (TruckTaskType == eTruckTaskType.Planned)        /* tervezett túra esetén az első túrapont indulás helye */
                {
                    FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                    if (firstPt != null)
                        return firstPt.Lat;
                    else
                        return 0;
                }
                else
                    return m_CurrLat;

            }
            set
            {
                if (TruckTaskType != eTruckTaskType.Planned)
                {
                    m_CurrLat = value;
                }
                SetRetPoint();
            }
        }

        private double m_CurrLng;
        [DisplayNameAttributeX(Name = "Aktuális szélességi koordináta", Order = 20)]
        [ErrorIfConstAttrX(EvalMode.IsEqual, 0, "Kötelező mező:CurrLng")]
        public double CurrLng
        {
            get
            {
                if (TruckTaskType == eTruckTaskType.Planned)        /* tervezett túra esetén az első túrapont indulás helye */
                {
                    FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                    if (firstPt != null)
                        return firstPt.Lng;
                    else
                        return 0;
                }
                else
                    return m_CurrLng;

            }
            set
            {
                if (TruckTaskType != eTruckTaskType.Planned)
                {
                    m_CurrLng = value;
                }
                SetRetPoint();
            }
        }

        //internal ObservableCollection<FTLPoint> m_currTPoints = new ObservableCollection<FTLPoint>();
        [DisplayNameAttributeX(Name = "Teljesítés alatt álló túrapontok", Order = 21)]
        [Required(ErrorMessage = "Kötelező mező:CurrTPoints")]
        public List<FTLPoint> CurrTPoints { get; set; }


        [DisplayNameAttributeX(Name = "Hány túrapont van teljesítve?", Order = 22)]
        public int TPointCompleted { get; set; }


        internal int RST_ID                             //Összsúly alapján a behajtási övezet ID
        {
            get
            {
                if (GVWR <= 3500)
                    return Global.RST_MAX35T;
                else if (GVWR <= 7500)
                    return Global.RST_MAX75T;
                else if (GVWR <= 12000)
                    return Global.RST_MAX12T;
                else if (GVWR > 12000)
                    return Global.RST_BIGGER12T;
                return Global.RST_NORESTRICT;
            }
        }


        internal string RZN_ID_LIST { get; set; }
        internal int NOD_ID_CURR { get; set; }

        //A visszatérés koordinátája. A több körös túratervezés esetén, Available típusú járművekkel számolunk a 
        //kimaradt szállítási feladatokra
        //Ebben az esetben csak a jármű utolsó túrájának szabad visszatérést számolni, ami a legelső túra 
        //megadásakor érvényes aktuális pozíció (CurrLat/CurrLng-nek) vagy legelső teljesítő túrapont koordinátára
        //történik. Emiatt egy külön propertyben kell tárolni a visszatérési koordiátát.
        //A property LAZY módon értékelődik ki. Az első használatkor értékeljük ki. Amikor a RetPoint-ot lekérdezzük, 
        //már minden adat fel van töltve ahhoz, hogy korrekt módon megállapíjtsuk értékét. ObservatbleCollection -ra váltani a 
        //List-ről az Interface miatt egyelőre nem lehet.
        //
        private PointLatLng? m_retPoint;
        internal PointLatLng? RetPoint
        {
            get
            {
                SetRetPoint();
                return m_retPoint;
            }
        }

        private void SetRetPoint()
        {
            using (GlobalLocker lockObj = new GlobalLocker(Global.lockObject))
            {
                if (m_retPoint == null)
                {
                    switch (TruckTaskType)
                    {
                        case eTruckTaskType.Available:
                            if (m_CurrLat != 0 && m_CurrLng != 0)
                                m_retPoint = new PointLatLng(m_CurrLat, m_CurrLng);
                            break;
                        case eTruckTaskType.Planned:
                        case eTruckTaskType.Running:
                            FTLPoint firstPt = CurrTPoints.FirstOrDefault();
                            if (firstPt != null)
                                m_retPoint = new PointLatLng(firstPt.Lat, firstPt.Lng);

                            break;
                        default:
                            break;
                    }
                }
            }
        }
        internal int RET_NOD_ID { get; set; }
    }
}
