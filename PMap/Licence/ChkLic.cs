using PMap.Common;
using PMap.Common.Azure;
using PMap.Localize;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace PMap.Licence
{
    internal static class ChkLic
    {
        internal static void Check(string p_IDFile)
        {
            byte[] buffer;



            try
            {
                buffer = Util.FileToByteArray( p_IDFile);
                using (Aes oAes = Aes.Create())
                {
                    string xml = AES.DecryptStringFromBytes_Aes(buffer, Encoding.Default.GetBytes(PMapID.pw), Encoding.Default.GetBytes(PMapID.iv));
                    PMapID pi = Util.XmlToObject<PMapID>(xml);
                    AzureTableStore.Instance.AzureAccount = pi.AzureAccountName;
                    AzureTableStore.Instance.AzureKey = pi.AzureAccountKey;

                    PMapLicence pl = AzureTableStore.Instance.Retrieve<PMapLicence>(pi.ID.ToString(), "");

                    if( pl.Expired < DateTime.Now.Date)
                        throw (new PMapLicenceException(String.Format( PMapMessages.E_LIC_EXPIRED, pl.Expired)));

                    PMapCommonVars.Instance.AppInstance = pi.AppInstance;
                    PMapCommonVars.Instance.Expired = pl.Expired;

                    string sMachineID = FingerPrint.Value();

                    if (pl.MachineID != null && pl.MachineID != "" && pl.MachineID != sMachineID)
                    {
                        var warn = new PMapLicWarn() 
                                { AppInstance = pl.AppInstance,
                                  OldMachineID = pl.MachineID,
                                  NewMachineID = sMachineID,
                                  PMapTimestamp = DateTime.Now.ToString(Global.DATETIMEFORMAT)
                                };
                        AzureTableStore.Instance.Insert(warn);
                        
                    }
                    if (pl.MachineID != sMachineID)
                    {
                        pl.MachineID = sMachineID;
                        AzureTableStore.Instance.Modify(pl);
                    }
                }
            }
                /*
            catch (FileNotFoundException fe)
            {
                UI.Error(PMapMessages.E_LIC_IDNOTFOUND);
                throw(fe);
            }
                 */
            catch (Exception ex)
            {
                throw(ex);
            }
        }
    }
}
