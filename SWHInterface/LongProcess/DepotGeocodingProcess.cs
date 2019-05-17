using PMapCore.BLL;
using PMapCore.BLL.DataXChange;
using PMapCore.BO;
using PMapCore.BO.DataXChange;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWHInterface.LongProcess
{
    internal class DepotGeocodingProcess : BaseLongProcess
    {
        List<boXRouteSection> m_lstRouteSection;
        private SQLServerAccess m_DB = null;                 //A multithread miatt saját adatelérés kell
        public DepotGeocodingProcess(ProcessNotifyIcon p_NotifyIcon, List<boXRouteSection> p_lstRouteSection)
            : base(p_NotifyIcon, ThreadPriority.Normal)
        {
            m_lstRouteSection = p_lstRouteSection;
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
        }

        protected override void DoWork()
        {
            try
            {

                bllRoute route = new bllRoute(m_DB);
                bllDepot depot = new bllDepot(m_DB);
                bllEtoll et = new bllEtoll(m_DB);

                PMapCommonVars.Instance.LstEToll=et.GetAllEtolls();



                int nItem = 0;
                foreach (boXRouteSection item in m_lstRouteSection)
                {
                    nItem++;
                    List<ObjectValidator.ValidationError> validationErros = ObjectValidator.ValidateObject(item);
                    if (validationErros.Count == 0)
                    {
                        int ZIP_ID = -1;
                        int NOD_ID = -1;
                        int EDG_ID = -1;
                        boDepot.EIMPADDRSTAT DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.MISSADDR;
                        var addr = item.ZIP_NUM.ToString() + " " + item.ZIP_CITY + " " + item.DEP_ADRSTREET + " " + item.DEP_ADRNUM;
                        bool bFound = route.GeocodingByAddr(addr, out ZIP_ID, out NOD_ID, out EDG_ID, out DEP_IMPADDRSTAT, false);
                        if (bFound)
                        {
                            item.ZIP_ID = ZIP_ID;
                            item.NOD_ID = NOD_ID;
                            item.EDG_ID = EDG_ID;

                            var node = route.GetNode(NOD_ID);
                            item.Lat = node.NOD_YPOS / Global.LatLngDivider;
                            item.Lng = node.NOD_XPOS / Global.LatLngDivider;
                        }
                        else
                        {
                            item.itemRes = new dtXResult()
                            {
                                ItemNo = nItem,
                                Field = "",
                                Status = dtXResult.EStatus.ERROR,
                                ErrMessage = addr + " address can't be geocoded"
                            };
                        }

                    }
                    else
                    {
                        foreach (var err in validationErros)
                        {
                            item.itemRes = new dtXResult()
                            {
                                ItemNo = nItem,
                                Field = err.Field,
                                Status = dtXResult.EStatus.VALIDATIONERROR,
                                ErrMessage = err.Message
                            };
                        }

                    }
                }
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;

            }
        }
    }
}
