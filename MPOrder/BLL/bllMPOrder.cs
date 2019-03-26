using MPOrder.BO;
using PMapCore.BLL;
using PMapCore.BLL.Base;
using PMapCore.BO;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.Localize;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder.BLL
{
    public class bllMPOrder : bllBase
    {
        public bllMPOrder(SQLServerAccess p_DBA)
            : base(p_DBA, "MPO_MPORDER")
        {
        }

        public List<boMPOrder> GetAllMPOrders(string p_where = "", params object[] p_pars)
        {
            string sSql = "select * from MPO_MPORDER MPO ";
            if (p_where != "")
                sSql += " where " + p_where;
            DataTable dt = DBA.Query2DataTable(sSql, p_pars);
            var linq = (from o in dt.AsEnumerable()
                        orderby o.Field<int>("ID")
                        select fillMPOrder(o));
            return linq.ToList();
        }

        private boMPOrder fillMPOrder(DataRow p_dr)
        {
            var res = new boMPOrder
            {
                ID = Util.getFieldValue<int>(p_dr, "ID"),
                CSVFileName = Util.getFieldValue<string>(p_dr, "CSVFileName"),
                SentToCT = Util.getFieldValue<bool>(p_dr, "SentToCT"),
                CompanyCode = Util.getFieldValue<string>(p_dr, "CompanyCode"),
                CustomerCode = Util.getFieldValue<string>(p_dr, "CustomerCode"),
                CustomerOrderNumber = Util.getFieldValue<string>(p_dr, "CustomerOrderNumber"),
                CustomerOrderDate = Util.getFieldValue<DateTime>(p_dr, "CustomerOrderDate"),
                ShippingDate = Util.getFieldValue<DateTime>(p_dr, "ShippingDate"),
                ShippingDateX = Util.getFieldValue<DateTime>(p_dr, "ShippingDateX"),
                WarehouseCode = Util.getFieldValue<string>(p_dr, "WarehouseCode"),
                TotalGrossWeightOfOrder = Util.getFieldValue<double>(p_dr, "TotalGrossWeightOfOrder"),
                NumberOfPalletForDel = Util.getFieldValue<double>(p_dr, "NumberOfPalletForDel"),
                NumberOfPalletForDelX = Util.getFieldValue<double>(p_dr, "NumberOfPalletForDelX"),
                ShippAddressID_DEP_CODE = Util.getFieldValue<string>(p_dr, "ShippAddressID_DEP_CODE"),
                ShippAddressCompanyName = Util.getFieldValue<string>(p_dr, "ShippAddressCompanyName"),
                ShippAddressZipCode = Util.getFieldValue<string>(p_dr, "ShippAddressZipCode"),
                ShippingAddressCity = Util.getFieldValue<string>(p_dr, "ShippingAddressCity"),
                ShippingAddressStreetAndNumber = Util.getFieldValue<string>(p_dr, "ShippingAddressStreetAndNumber"),
                Note = Util.getFieldValue<string>(p_dr, "Note"),
                RowNumber = Util.getFieldValue<int>(p_dr, "RowNumber"),
                ProductCode = Util.getFieldValue<string>(p_dr, "ProductCode"),
                U_M = Util.getFieldValue<string>(p_dr, "U_M"),
                ProdDescription = Util.getFieldValue<string>(p_dr, "ProdDescription"),
                ConfOrderQty = Util.getFieldValue<double>(p_dr, "ConfOrderQty"),
                ConfPlannedQty = Util.getFieldValue<double>(p_dr, "ConfPlannedQty"),
                NetWeight = Util.getFieldValue<double>(p_dr, "NetWeight"),
                PalletPlannedQty = Util.getFieldValue<double>(p_dr, "PalletPlannedQty"),
                PalletPlannedQtyX = Util.getFieldValue<double>(p_dr, "PalletPlannedQtyX"),
                PalletBulkQty = Util.getFieldValue<double>(p_dr, "PalletBulkQty"),
                PalletBulkQtyX = Util.getFieldValue<double>(p_dr, "PalletBulkQtyX"),
                GrossWeightPlanned = Util.getFieldValue<double>(p_dr, "GrossWeightPlanned"),
                GrossWeightPlannedX = Util.getFieldValue<double>(p_dr, "GrossWeightPlannedX"),
                ADR = Util.getFieldValue<bool>(p_dr, "ADR"),
                ADRMultiplier = Util.getFieldValue<double>(p_dr, "ADRMultiplier"),
                ADRMultiplierX = Util.getFieldValue<double>(p_dr, "ADRMultiplierX"),
                ADRLimitedQuantity = Util.getFieldValue<bool>(p_dr, "ADRLimitedQuantity"),
                Freeze = Util.getFieldValue<bool>(p_dr, "Freeze"),
                Melt = Util.getFieldValue<bool>(p_dr, "Melt"),
                UV = Util.getFieldValue<bool>(p_dr, "UV"),

                Bordero = Util.getFieldValue<string>(p_dr, "Bordero"),
                Carrier = Util.getFieldValue<string>(p_dr, "Carrier"),
                VehicleType = Util.getFieldValue<string>(p_dr, "VehicleType"),
                KM = Util.getFieldValue<double>(p_dr, "KM"),
                Forfait = Util.getFieldValue<double>(p_dr, "Forfait"),
                Currency = Util.getFieldValue<string>(p_dr, "Currency"),

                UnitWeight = Util.getFieldValue<double>(p_dr, "UnitWeight")
            };
            return res;
        }

        public List<boCSVFile> GetFiles()
        {
            string sSql = "select CSVFileName, ShippingDateX  from   MPO_MPORDER " + Environment.NewLine +
                          " group by CSVFileName, ShippingDateX order by ShippingDateX desc ";
            DataTable dt = DBA.Query2DataTable(sSql);
            var linq = (from o in dt.AsEnumerable()
                        select new boCSVFile
                        {
                            CSVFileName = Util.getFieldValue<string>(o, "CSVFileName"),
                            ShippingDateX = Util.getFieldValue<DateTime>(o, "ShippingDateX")
                        });
            return linq.ToList();

        }

        public boMPOrder GetMPOrder(int p_ID)
        {
            List<boMPOrder> lstMPOrder = GetAllMPOrders("ID = ? ", p_ID);
            if (lstMPOrder.Count == 0)
                return null;
            else
                return lstMPOrder[0];

        }


        public boMPOrder GetMPOrderByCODE(string p_CustomerOrderNumber, string p_ProductCode)
        {
            List<boMPOrder> lstMPOrder = GetAllMPOrders("upper(CustomerOrderNumber) = ? and upper(ProductCode) = ? ", p_CustomerOrderNumber.ToUpper(), p_ProductCode.ToUpper());
            if (lstMPOrder.Count == 0)
            {
                return null;
            }
            //            else if (lstMPOrder.Count == 1)
            else if (lstMPOrder.Count >= 1)
            {
                return lstMPOrder[0];
            }
            else
            {
                throw new DuplicatedCRR_CODEException();
            }
        }


        public bool IsExist(string p_CustomerOrderNumber, string p_ProductCode)
        {
            string sSql = "select case when exists( select ID from MPO_MPORDER where upper(CustomerOrderNumber) = ? and upper(ProductCode) = ?)  then 'OK' else '' end";
            var retVal = DBA.ExecuteScalar(sSql, p_CustomerOrderNumber, p_ProductCode);
            return retVal.ToString() == "OK";
        }


        public int AddMPOrder(boMPOrder p_MPOrder)
        {
            return AddItem(p_MPOrder, false);
        }

        public List<boMPOrderF> GetAllMPOrdersForGrid(string p_CSVFileName)
        {
            var lst = GetAllMPOrders("CSVFileName = ?", p_CSVFileName);
            if (lst != null)
            {
                var res = lst.GroupBy(g1 => new
                {
                    g1.SentToCT,
                    g1.CompanyCode,
                    g1.CustomerCode,
                    g1.CustomerOrderNumber,
                    g1.CustomerOrderDate,
                    g1.ShippingDate,
                    g1.ShippingDateX,
                    g1.WarehouseCode,
                    g1.ShippAddressID_DEP_CODE,
                    g1.ShippAddressCompanyName,
                    g1.ShippAddressZipCode,
                    g1.ShippingAddressCity,
                    g1.ShippingAddressStreetAndNumber,
                    g1.NumberOfPalletForDel,
                    g1.NumberOfPalletForDelX,
                    g1.Bordero,
                    g1.Carrier,
                    g1.VehicleType,
                    g1.KM,
                    g1.Forfait,
                    g1.Currency
                }).Select(s => new boMPOrderF()
                {
                    SentToCT = s.Key.SentToCT,
                    CompanyCode = s.Key.CompanyCode,
                    CustomerCode = s.Key.CustomerCode,
                    CustomerOrderNumber = s.Key.CustomerOrderNumber,
                    CustomerOrderDate = s.Key.CustomerOrderDate,
                    ShippingDate = s.Key.ShippingDate,
                    ShippingDateX = s.Key.ShippingDateX,
                    WarehouseCode = s.Key.WarehouseCode,
                    ShippAddressID_DEP_CODE = s.Key.ShippAddressID_DEP_CODE,
                    ShippAddressCompanyName = s.Key.ShippAddressCompanyName,
                    ShippAddressZipCode = s.Key.ShippAddressZipCode,
                    ShippingAddressCity = s.Key.ShippingAddressCity,
                    ShippingAddressStreetAndNumber = s.Key.ShippingAddressStreetAndNumber,
                    NumberOfPalletForDel = s.Key.NumberOfPalletForDel,
                    NumberOfPalletForDelX = s.Key.NumberOfPalletForDelX,
                    Bordero = s.Key.Bordero,
                    Carrier = s.Key.Carrier,
                    VehicleType = s.Key.VehicleType,
                    KM = s.Key.KM,
                    Forfait = s.Key.Forfait,
                    Currency = s.Key.Currency,
                    Items = s.ToList().Select(s2 => new boMPOrderT()
                    {
                        ID = s2.ID,
                        CustomerOrderNumber = s2.CustomerOrderNumber,
                        RowNumber = s2.RowNumber,
                        ProductCode = s2.ProductCode,
                        U_M = s2.U_M,
                        ProdDescription = s2.ProdDescription,
                        ConfOrderQty = s2.ConfOrderQty,
                        PalletPlannedQty = s2.PalletPlannedQty,
                        PalletPlannedQtyX = s2.PalletPlannedQtyX,
                        PalletBulkQty = s2.PalletBulkQty,
                        PalletBulkQtyX = s2.PalletBulkQtyX,
                        ADR = s2.ADR,
                        ADRMultiplier = s2.ADRMultiplier,
                        ADRLimitedQuantity = s2.ADRLimitedQuantity,
                        ConfPlannedQty = s2.ConfPlannedQty,
                        GrossWeightPlanned = s2.GrossWeightPlanned,
                        GrossWeightPlannedX = s2.GrossWeightPlannedX,
                        Freeze = s2.Freeze,
                        Melt = s2.Melt,
                        UV = s2.UV,
                        UnitWeight = s2.UnitWeight,
                        ADRMultiplierX = s2.ADRMultiplierX
                    }
                   ).ToList()
                }).ToList();
                return res;
            }
            else
            {
                return new List<boMPOrderF>();
            }
        }


        public void SetManualValuesF(string p_CSVFileName, string p_CustomerOrderNumber, double NumberOfPalletForDelX)
        {
            DBA.ExecuteNonQuery("update MPO_MPORDER set NumberOfPalletForDelX = ? where CSVFileName= ? and CustomerOrderNumber = ?", NumberOfPalletForDelX, p_CSVFileName, p_CustomerOrderNumber);
        }

        public void SetShippingDateX(string p_CSVFileName, DateTime p_ShippingDateX)
        {
            DBA.ExecuteNonQuery("update MPO_MPORDER set ShippingDateX = ?, SentToCT = case when ShippingDate = ? then 1 else SentToCT end where CSVFileName= ? ", p_ShippingDateX, p_ShippingDateX, p_CSVFileName);
        }

        public void SetManualValuesT(int p_ID, double p_ConfPlannedQty, double p_GrossWeightPlannedX, double p_ADRMultiplierX, double p_PalletPlannedQtyX, double p_PalletBulkQtyX)
        {
            DBA.ExecuteNonQuery("update MPO_MPORDER set ConfPlannedQty = ?, GrossWeightPlannedX = ?, ADRMultiplierX = ?, PalletPlannedQtyX = ?, PalletBulkQtyX = ? where ID=?", p_ConfPlannedQty, p_GrossWeightPlannedX, p_ADRMultiplierX, p_PalletPlannedQtyX, p_PalletBulkQtyX, p_ID);
        }

        public void SetSentToCT(string p_CSVFileName, string p_CustomerOrderNumber, bool p_SentToCT)
        {
            DBA.ExecuteNonQuery("update MPO_MPORDER set SentToCT = ? where CSVFileName= ? and CustomerOrderNumber=?", p_SentToCT, p_CSVFileName, p_CustomerOrderNumber);
        }
        public void SetAllSentToCT(string p_CSVFileName, bool p_SentToCT)
        {
            DBA.ExecuteNonQuery("update MPO_MPORDER set SentToCT = ? where CSVFileName= ? ", p_SentToCT, p_CSVFileName);
            if (!p_SentToCT)
            {
                DBA.ExecuteNonQuery("update MPO_MPORDER set NumberOfPalletForDelX = NumberOfPalletForDel, " + Environment.NewLine +
                                  " ConfPlannedQty = ConfOrderQty, GrossWeightPlannedX = GrossWeightPlanned, ADRMultiplierX = ADRMultiplier, PalletPlannedQtyX = PalletPlannedQty, PalletBulkQtyX = PalletBulkQty " + Environment.NewLine +
                                    " where CSVFileName= ? ", p_CSVFileName);

            }
        }

        public void SetBordero(int p_ID, string p_Bordero)
        {
            DBA.ExecuteNonQuery("update MPO_MPORDER set Bordero = ? where ID= ? ", p_Bordero, p_ID);
        }
        public void DeleteItem(int p_ID)
        {
            DBA.ExecuteNonQuery("delete MPO_MPORDER where id=?", p_ID);
        }

        public void DeleteItemByCustomerOrderNumber(string p_CSVFileName, string p_CustomerOrderNumber)
        {
            DBA.ExecuteNonQuery("delete MPO_MPORDER where CSVFileName= ? and CustomerOrderNumber=?", p_CSVFileName, p_CustomerOrderNumber);
        }
        public List<SendResult> SendToCT(List<boMPOrderF> p_data, BaseProgressDialog p_Form = null)
        {
            var res = new List<SendResult>();
            var bllOrderX = new bllOrder(DBA);
            var bllPlanX = new bllPlan(DBA);
            var bllZipX = new bllZIP(DBA);
            var bllDepotX = new bllDepot(DBA);
            var bllRouteX = new bllRoute(DBA);
            var bllCargoTypeX = new bllCargoType(DBA);

            foreach (var item in p_data)
            {
                if (p_Form != null)
                    p_Form.NextStep();

                using (TransactionBlock transObj = new PMapCore.DB.Base.TransactionBlock(DBA))
                {
                    try
                    {

                        boOrder ord = bllOrderX.GetOrderByORD_NUM(item.CustomerOrderNumber);

                        //1.CT-be küldjük és nincs még ORD_ORDER rekord.
                        if (item.SentToCT && ord == null)
                        {

                            boDepot dep = bllDepotX.GetDepotByDEP_CODE(item.ShippAddressID_DEP_CODE);
                            if (dep == null)
                            {
                                //ZIP ID megállapítása
                                var boZip = bllZipX.GetZIPbyNum(Int32.Parse("0" + item.ShippAddressZipCode.Replace(".", "")));

                                string fullAddr = (item.ShippAddressZipCode + " " + item.ShippingAddressCity + " " + item.ShippingAddressStreetAndNumber).Trim();
                                boDepot.EIMPADDRSTAT DEP_IMPADDRSTAT = boDepot.EIMPADDRSTAT.MISSADDR;
                                int ZIP_ID = (boZip != null ? boZip.ID : 0);
                                int NOD_ID = 0;
                                int EDG_ID = 0;
                                /*
                                bool bFound = bllRouteX.GeocodingByAddr(fullAddr, out ZIP_ID, out NOD_ID, out EDG_ID, out DEP_IMPADDRSTAT, false);
                                if (bFound && DEP_IMPADDRSTAT != boDepot.EIMPADDRSTAT.MISSADDR)
                                {
                                    boEdge edg = bllRouteX.GetEdgeByID(EDG_ID);
                                }
                                else
                                {
                                    res.Add(new SendToCTResult()
                                    {
                                        ResultType = SendToCTResult.RESTYPE.WARNING,
                                        CustomerOrderNumber = item.CustomerOrderNumber,
                                        Message = string.Format(PMapMessages.E_MPSENDTOCT_WRONGADDR, item.ShippAddressCompanyName, fullAddr)
                                    });
                                }
                                */

                                //Lerakó felvitele
                                dep = new boDepot()
                                {
                                    WHS_ID = 1,             //Csak egy raktárat kezelünk
                                    DEP_CODE = item.ShippAddressID_DEP_CODE,
                                    DEP_NAME = item.ShippAddressCompanyName,
                                    ZIP_ID = (boZip != null ? boZip.ID : ZIP_ID),      //HA használjuk a GeocodingByAddr-t akkor a ZIP_ID kell ide
                                    DEP_ADRSTREET = item.ShippingAddressStreetAndNumber,
                                    DEP_ADRNUM = "",
                                    DEP_OPEN = PMapIniParams.Instance.MapeiOpen,
                                    DEP_CLOSE = PMapIniParams.Instance.MapeiClose,
                                    DEP_COMMENT = "",
                                    DEP_SRVTIME = PMapIniParams.Instance.MapeiSrvTime,
                                    NOD_ID = NOD_ID,
                                    EDG_ID = EDG_ID,
                                    DEP_DELETED = false,
                                    REG_ID = 0,
                                    DEP_QTYSRVTIME = PMapIniParams.Instance.MapeiQtySrvTime,
                                    DEP_CLIENTNUM = item.CustomerCode,
                                    DEP_IMPADDRSTAT = (NOD_ID == 0 ? boDepot.EIMPADDRSTAT.MISSADDR : boDepot.EIMPADDRSTAT.OK),
                                    DEP_LIFETIME = 0
                                };
                                dep.ID = bllDepotX.AddDepot(dep);
                                bllDepotX.SetAllTruckToDep(dep.ID);
                            }

                            var cargoTypeStr = !string.IsNullOrWhiteSpace(item.VehicleType) ? item.VehicleType : PMapIniParams.Instance.MapeiDefCargoType;
                            var cargoType = bllCargoTypeX.GetCargoTypeByName1(cargoTypeStr);
                            if (cargoType == null)
                            {
                                res.Add(new SendResult()
                                {
                                    ResultType = SendResult.RESTYPE.ERROR,
                                    CustomerOrderNumber = item.CustomerOrderNumber,
                                    Message = string.Format(PMapMessages.E_MPSENDTOCT_INVCARGOTYPE, cargoTypeStr)
                                });
                            }
                            else
                            {

                                boOrder newOrder = new boOrder()
                                {
                                    OTP_ID = Global.OTP_OUTPUT,
                                    CTP_ID = (cargoType != null ? cargoType.ID : 1),                             //csak egyféle árutípust kezelünk
                                    DEP_ID = dep.ID,
                                    WHS_ID = 1,                             //csak központi raktár van
                                    ORD_NUM = item.CustomerOrderNumber,
                                    ORD_ORIGNUM = item.CustomerOrderNumber, //Masterplast mező
                                    ORD_DATE = item.ShippingDateX,
                                    ORD_CLIENTNUM = item.CompanyCode,
                                    //ORD_LOCKDATE                          //Új felvitelkor nem szabad tölteni
                                    ORD_FIRSTDATE = DateTime.Now.Date,
                                    ORD_QTY = item.GrossWeightPlannedXSum,
                                    ORD_ORIGQTY1 = item.GrossWeightPlannedXSum,
                                    ORD_ORIGQTY2 = 0,
                                    ORD_ORIGQTY3 = 0,
                                    ORD_ORIGQTY4 = 0,
                                    ORD_ORIGQTY5 = 0,
                                    ORD_SERVS = PMapIniParams.Instance.MapeiOpen,
                                    ORD_SERVE = PMapIniParams.Instance.MapeiClose,
                                    ORD_VOLUME = 0,
                                    ORD_LENGTH = 0,
                                    ORD_WIDTH = 0,
                                    ORD_HEIGHT = 0,
                                    ORD_ADRPOINTS = item.ADRMultiplierXSum,
                                    ORD_LOCKED = false,
                                    ORD_ISOPT = true,
                                    ORD_GATE = "",
                                    ORD_COMMENT = "",
                                    ORD_UPDATED = true,
                                    ORD_ACTIVE = true

                                };
                                newOrder.ID = bllOrderX.AddOrder(newOrder);

                                res.Add(new SendResult()
                                {
                                    ResultType = SendResult.RESTYPE.OK,
                                    CustomerOrderNumber = item.CustomerOrderNumber,
                                    Message = string.Format(PMapMessages.E_MPSENDTOCT_ADDOK)
                                });

                            }
                        }


                        //2.CT-be küldjük és már van ORD_ORDER rekord.
                        if (item.SentToCT && ord != null)
                        {
                            ord.ORD_QTY = item.GrossWeightPlannedXSum;
                            ord.ORD_ORIGQTY1 = item.GrossWeightPlannedXSum;
                            ord.ORD_ADRPOINTS = item.ADRMultiplierXSum;
                            bllOrderX.UpdateOrder(ord);
                            res.Add(new SendResult()
                            {
                                ResultType = SendResult.RESTYPE.OK,
                                CustomerOrderNumber = item.CustomerOrderNumber,
                                Message = string.Format(PMapMessages.E_MPSENDTOCT_UPDATEOK)
                            });
                        }

                        //3.CT-ből töröljük, és már van ORD_ORDER rekord.
                        if (!item.SentToCT && ord != null)
                        {
                            List<boPlan> lstPlan = new List<boPlan>();
                            lstPlan = bllPlanX.GetTouredPlansByOrderID(ord.ID);
                            if (lstPlan.Count > 0)
                            {
                                res.Add(new SendResult()
                                {
                                    ResultType = SendResult.RESTYPE.ERROR,
                                    CustomerOrderNumber = item.CustomerOrderNumber,
                                    Message = string.Format(PMapMessages.E_MPSENDTOCT_TOURED, item.ShippAddressCompanyName, string.Join(",", lstPlan.Select(s => s.PLN_NAME).ToList()))
                                });
                            }
                            else
                            {
                                bllPlanX.DeleteTourOrderByOrderID(ord.ID);
                                bllOrderX.DeleteOrder(ord.ID);
                                res.Add(new SendResult()
                                {
                                    ResultType = SendResult.RESTYPE.OK,
                                    CustomerOrderNumber = item.CustomerOrderNumber,
                                    Message = string.Format(PMapMessages.E_MPSENDTOCT_DELOK)
                                });
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        DBA.Rollback();
                        ExceptionDispatchInfo.Capture(e).Throw();
                        throw;
                    }
                }
            }
            return res;
        }

        public List<SendResult> SendToNetMover(string p_CSVFileName, int p_PLN_ID, string p_exportFile, BaseProgressDialog p_Form = null)
        {
            try
            {
                var bllIdGen = new bllIDGen(DBA);
                var res = new List<SendResult>();


                string sSql = "update MPO_MPORDER " + Environment.NewLine +
                        "set Bordero = '', Carrier = case when SentToCT = 1 then CarrierX else '' end, " + Environment.NewLine +
                        "KM = case when SentToCT = 1 then KMX else 0 end, " + Environment.NewLine +
                        "Forfait = case when SentToCT = 1 then ForfaitX else 0 end, " + Environment.NewLine +
                        "Currency = case when SentToCT = 1 then CurrencyX else '' end " + Environment.NewLine +
                        "from(select MPO.ID, CRR_NAME as CarrierX, xKMCOST.DIST / 1000 KMX, TPLANTOLL as ForfaitX, 'HUF' as CurrencyX " + Environment.NewLine +
                        "from MPO_MPORDER MPO " + Environment.NewLine +
                        "inner join		ORD_ORDER ORD on ORD.ORD_NUM = MPO.CustomerOrderNumber " + Environment.NewLine +
                        "inner join     TOD_TOURORDER TOD on TOD.ORD_ID = ORD.ID and TOD.PLN_ID = ? " + Environment.NewLine +
                        "inner join		PLN_PUBLICATEDPLAN PLN on PLN.ID = TOD.PLN_ID " + Environment.NewLine +
                        "inner join		TPL_TRUCKPLAN TPL on TPL.PLN_ID = PLN.ID " + Environment.NewLine +
                        "inner join		v_PLTOURKMCOST xKMCOST on xKMCOST.TPL_ID = TPL.ID " + Environment.NewLine +
                        "inner join		v_TPLANTOLL xTOLL on xTOLL.TPL_ID = TPL.ID " + Environment.NewLine +
                        "inner join		PTP_PLANTOURPOINT PTP on PTP.TPL_ID = TPL.ID and PTP.TOD_ID=TOD.ID " + Environment.NewLine +
                        "inner join		 TRK_TRUCK TRK on TRK.ID = TPL.TRK_ID " + Environment.NewLine +
                        "left outer join CRR_CARRIER CRR on CRR.ID = TRK.CRR_ID " + Environment.NewLine +
                        "left outer join CPP_CAPACITYPROF CPP on CPP.ID = TRK.CPP_ID " + Environment.NewLine +
                        "where MPO.CSVFileName = ? " + Environment.NewLine +
                        "and PLN.ID = ?) oo " + Environment.NewLine +
                        "where MPO_MPORDER.ID = oo.ID";
                DBA.ExecuteNonQuery(sSql, p_PLN_ID, p_CSVFileName, p_PLN_ID);

                /*
                sSql = "select  MPO.ID as MPO_ID, MPO.SentToCt,  MPO.Bordero, MPO.ShippingDateX, TPL.ID as TPL_ID, MPO.CustomerOrderNumber " + Environment.NewLine +
                        "from MPO_MPORDER MPO " + Environment.NewLine +
                        "inner join      ORD_ORDER ORD on ORD.ORD_NUM = MPO.CustomerOrderNumber " + Environment.NewLine +
                        "inner join      TOD_TOURORDER TOD on TOD.ORD_ID = ORD.ID and TOD.PLN_ID = ? " + Environment.NewLine +
                        "inner join      PLN_PUBLICATEDPLAN PLN on PLN.ID = TOD.PLN_ID " + Environment.NewLine +
                        "inner join      TPL_TRUCKPLAN TPL on TPL.PLN_ID = PLN.ID " + Environment.NewLine +
                        "inner join PTP_PLANTOURPOINT PTP on PTP.TPL_ID = TPL.ID and PTP.TOD_ID = TOD.ID" + Environment.NewLine +
                        "where MPO.CSVFileName = ? " + Environment.NewLine +
                        "and(PLN.ID = ?) " + Environment.NewLine +
                        "order by TPL.ID,PTP_ORDER ";
                */

                sSql = "select MPO.ID as MPO_ID, MPO.SentToCt,  MPO.Bordero, MPO.ShippingDateX, TPL.ID as TPL_ID, MPO.CustomerOrderNumber, PTP_ORDER, PTP_TYPE " + Environment.NewLine +
                       "from PTP_PLANTOURPOINT PTP " + Environment.NewLine +
                       "inner join      TPL_TRUCKPLAN TPL on TPL.ID = PTP.TPL_ID " + Environment.NewLine +
                       "left outer join TOD_TOURORDER TOD on TOD.ID = PTP.TOD_ID " + Environment.NewLine +
                       "left outer join ORD_ORDER ORD on ORD.ID = TOD.ORD_ID " + Environment.NewLine +
                       "left outer join MPO_MPORDER MPO on  MPO.CustomerOrderNumber = ORD.ORD_NUM " + Environment.NewLine +
                       "where (MPO.CSVFileName is null or MPO.CSVFileName = ? ) " + Environment.NewLine +
                       " and TPL.PLN_ID = ? " + Environment.NewLine +
                       "order by TPL.ID,PTP_ORDER";
                DataTable dt = DBA.Query2DataTable(sSql, p_CSVFileName, p_PLN_ID);

                int BorderoNumber = -1;
                string sBordero = "";
                foreach (DataRow rw in dt.Rows)
                {
                    if (p_Form != null)
                        p_Form.NextStep();

                    var PTP_TYPE = Util.getFieldValue<int>(rw, "PTP_TYPE");
                    if (PTP_TYPE == Global.PTP_TYPE_WHS_S)
                    {
                        sBordero = "";
                    }

                    if (Util.getFieldValue<int>(rw, "SentToCt") != 0 && PTP_TYPE == Global.PTP_TYPE_DEP)
                    {

                        if (string.IsNullOrWhiteSpace(Util.getFieldValue<string>(rw, "Bordero")))

                        {
                            if (string.IsNullOrWhiteSpace(sBordero))
                            {
                                BorderoNumber = bllIdGen.GetNextValueByName("BORDERO");
                                sBordero = Util.RightString(Util.getFieldValue<DateTime>(rw, "ShippingDateX").Date.Year.ToString(), 2) + "0"
                                         + BorderoNumber.ToString().PadLeft(6, '0');

                            }
                            SetBordero(Util.getFieldValue<int>(rw, "MPO_ID"), sBordero);
                        }
                    }
                    
                }

                Encoding ecFile = Encoding.GetEncoding(Global.PM_ENCODING);
                StreamWriter writer = new StreamWriter(p_exportFile, false, ecFile);
                var lstAll = GetAllMPOrders("CSVFileName = ?", p_CSVFileName);
                foreach (var item in lstAll)
                {
                    if (p_Form != null)
                        p_Form.NextStep();
                    
                    //levágjuk a suffixet

                    var CustomerOrderNumber = item.CustomerOrderNumber.Substring(0, item.CustomerOrderNumber.Length - 4);
                    var sLine = item.CompanyCode + ";" +
                                item.CustomerCode + ";" +
                                CustomerOrderNumber + ";" +
                                item.CustomerOrderDate.ToString("yyyyMMdd") + ";" +
                                (item.SentToCT ? item.ShippingDateX.ToString("yyyyMMdd") : item.ShippingDate.ToString("yyyyMMdd")) + ";" +
                                item.WarehouseCode + ";" +
                                item.TotalGrossWeightOfOrder.ToString().Replace(".", ",") + ";" +
                                (item.SentToCT ? item.NumberOfPalletForDelX : item.NumberOfPalletForDel).ToString().Replace(".", ",") + ";" +
                                item.ShippAddressID_DEP_CODE + ";" +
                                item.ShippAddressCompanyName + ";" +
                                item.ShippAddressZipCode + ";" +
                                item.ShippingAddressCity + ";" +
                                item.ShippingAddressStreetAndNumber + ";" +
                                item.Note + ";" +
                                item.RowNumber.ToString() + ";" +
                                item.ProductCode + ";" +
                                item.U_M + ";" +
                                item.ProdDescription + ";" +
                                item.ConfOrderQty.ToString().Replace(".", ",") + ";" +
                                item.ConfPlannedQty.ToString().Replace(".", ",") + ";" +
                                item.NetWeight.ToString().Replace(".", ",") + ";" +
                                (item.SentToCT ? item.PalletPlannedQtyX : item.PalletPlannedQty).ToString().Replace(".", ",") + ";" +
                                (item.SentToCT ? item.PalletBulkQtyX : item.PalletBulkQty).ToString().Replace(".", ",") + ";" +
                                (item.SentToCT ? item.GrossWeightPlannedX : item.GrossWeightPlanned).ToString().Replace(".", ",") + ";" +
                                (item.ADR ? "Y" : "N") + ";" +
                                (item.SentToCT ? item.ADRMultiplierX : item.ADRMultiplier).ToString().Replace(".", ",") + ";" +
                                (item.ADRLimitedQuantity ? "Y" : "N") + ";" +
                                (item.Freeze ? "Y" : "N") + ";" +
                                (item.Melt ? "Y" : "N") + ";" +
                                (item.UV ? "Y" : "N") + ";" +
                                (item.SentToCT ? item.Bordero : "") + ";" +
                                (item.SentToCT ? item.Carrier : "") + ";" +
                                item.VehicleType  + ";" +
                                (item.SentToCT ? Math.Round(item.KM).ToString().Replace(".", ",") : "") + ";" +
                                (item.SentToCT ? Math.Round(item.Forfait).ToString().Replace(".", ",") : "") + ";" +
                                (item.SentToCT ? item.Currency : "");
                    writer.WriteLine(sLine);
                }
                writer.Flush();
                writer.Close();

                return res;
            }


            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
        }
    }
}