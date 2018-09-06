using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMapCore.DB.Base;
using GMap.NET;
using GMap.NET.MapProviders;
using PMapCore.BLL;
using PMapCore.BO;

namespace PMapCore.Common
{
    public class PMapCommonVars
    {

        //Lazy objects are thread safe, double checked and they have better performance than locks.
        //see it: http://csharpindepth.com/Articles/General/Singleton.aspx
        private static readonly Lazy<PMapCommonVars> m_instance = new Lazy<PMapCommonVars>(() => new PMapCommonVars(), true);


        static public PMapCommonVars Instance                                  //inicializálódik, ezért biztos létrejon az instance osztály)
        {
            get
            {
                return m_instance.Value;            //It's thread safe!
            }
        }

        private PMapCommonVars()
        {
            AppInstance = "???";
            CT_DB = null;
            MapProvider = GMapProviders.GoogleTerrainMap;
            RZN_ID_LISTCahce = new Dictionary<int, string>();
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public SQLServerAccess CT_DB { get; private set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int USR_ID { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public AccessMode MapAccessMode { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public GMapProvider MapProvider { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public RoutingProvider RoutingProvider
        {
            get
            {
                if (MapProvider != null)
                    return MapProvider as RoutingProvider;
                else
                    return GMapProviders.GoogleMap; // use google if provider does not implement routing
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        private List<boEtoll> m_LstEToll = null;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public List<boEtoll> LstEToll
        {
            get
            {
                if (m_LstEToll == null)
                {
                    bllEtoll et = new bllEtoll(CT_DB);
                    m_LstEToll = et.GetAllEtolls();
                }
                return m_LstEToll;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string AppInstance { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public DateTime Expired { get; set; }


        public void ConnectToDB()
        {
            CT_DB = new SQLServerAccess();
            CT_DB.ConnectToDB( PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);

            //TODO: ide kell rakni az automatikus updatert.
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Dictionary<int, string> RZN_ID_LISTCahce = null;  //Behajtási zóna ID cache

        public bool IsCheckMode { get; set; } = false;

        public string AzureTableStoreApiKey { get;  set; }
        public string AzureSendGridApiKey { get;  set; }

        public int TPArea { get; set; } = 12000;
   
    }

}
