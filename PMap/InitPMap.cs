﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.Internals;
using GMap.NET;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.Security.AccessControl;
using GMap.NET.MapProviders;
using PMap.MapProvider;
using PMap.Common;
using PMap.Common.PPlan;
using PMap.Licence;
using PMap.Localize;

namespace PMap
{
    public static class InitPMap
    {

        public enum startErrCode
        {
            OK,
            FatalErr,
            NoInternetConn
        }

        public static startErrCode Start(bool p_checkConnection, bool p_showLicenceErr = true)
        {
            try
            {
                ChkLic.Check(PMapIniParams.Instance.IDFile);
            }
            catch (PMapLicenceException licex)
            {

                if (p_showLicenceErr)
                    UI.Error(licex.Message);
                throw (licex);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (p_showLicenceErr && PMapCommonVars.Instance.Expired.AddMonths(-1) < DateTime.Now.Date)
                UI.Warning(PMapMessages.W_LIC_EXPIRED_WARN, PMapCommonVars.Instance.Expired.ToString(Global.DATEFORMAT));

             

            /*
                      System.Environment.Exit(0);
                       Application.Exit();
            */

                        startErrCode result = startErrCode.OK;

            /*
                        if (DateTime.Now.Date >= new DateTime(2010, 9, 22))
                        {

                            Random rnd = new Random((int)DateTime.Now.Millisecond);
                            if (rnd.Next(0, 3) == 1)
                            {
                                int d = 1;
                                d--;
                                int i = 10 / d;
                                Util.log2File("PxP" + i.ToString());
                            }
                        }
            */
            switch (PMapIniParams.Instance.MapType)
            {
                case Global.mtGMap:
                    PMapCommonVars.Instance.MapProvider = GMapProviders.GoogleTerrainMap;
                    break;
                case Global.mtOpenStreetMap:
                    PMapCommonVars.Instance.MapProvider = GMapProviders.OpenStreetMap;
                    break;
                /*
                 Ezekre nics útvonalszámítás implementálva
                case Global.mtBingMap:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.BingMap;
                    break;
                case Global.mtYahooMap:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.YahooMap;
                    break;
                case Global.mtOviMap:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.OviMap;
                    break;
                case Global.mtTest:
                    PPlanCommonVars.Instance.MapProvider = GMapProviders.YandexMap;
                    break;
                 
                 
                 */

                default:
                    PMapCommonVars.Instance.MapProvider = GMapProviders.GoogleTerrainMap;
                    break;

            }


            PMapCommonVars.Instance.MapAccessMode = AccessMode.ServerOnly;
    //??        PPlanCommonVars.Instance.Changed = true;

            GoogleMapProvider.Instance.APIKey = PMapIniParams.Instance.GoogleMapsAPIKey;

            if (PMapIniParams.Instance.UseProxy)
            {

                GMapProvider.WebProxy = new WebProxy(PMapIniParams.Instance.ProxyServer, PMapIniParams.Instance.ProxyPort);
                GMapProvider.WebProxy.Credentials = new NetworkCredential(PMapIniParams.Instance.ProxyUser, PMapIniParams.Instance.ProxyPassword, PMapIniParams.Instance.ProxyDomain);
            }
            else
                GMapProvider.WebProxy = null;

            
            //Debug átirányítása
            //
            if (PMapIniParams.Instance.LogVerbose >= PMapIniParams.eLogVerbose.debug)
            {
                string DbgFileName = Path.Combine(PMapIniParams.Instance.LogDir, Global.DbgFileName);

                TextWriterTraceListener[] listeners = new TextWriterTraceListener[] 
                {
                new TextWriterTraceListener(DbgFileName),
                new TextWriterTraceListener(Console.Out)
                };
                Debug.Listeners.AddRange(listeners);
            }


            if (p_checkConnection)
            {
                try
                {
                    System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("www.google.com");
                }
                catch
                {
                    result = startErrCode.NoInternetConn;
                }
            }

            return result;
        }
    }
}
