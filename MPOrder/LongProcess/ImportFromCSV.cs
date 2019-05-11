using MPOrder.BLL;
using MPOrder.BO;
using PMapCore.BLL;
using PMapCore.Common;
using PMapCore.DB.Base;
using PMapCore.LongProcess.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace MPOrder.LongProcess
{
    public class ImportFromCSV : BaseLongProcess
    {
        private SQLServerAccess m_DB;
        private string m_fileName;
        private DateTime m_ShippingDateX;

        public int AddedCount { get; private set; } = 0;
        public int ItemsCount { get; private set; } = 0;
        public List<SendResult> ErrResult { get; set; } = new List<SendResult>();

        public ImportFromCSV(BaseProgressDialog p_Form, string p_fileName, DateTime p_ShippingDateX)
            : base(p_Form, ThreadPriority.Normal)
        {
            m_DB = new SQLServerAccess();
            m_DB.ConnectToDB(PMapIniParams.Instance.DBServer, PMapIniParams.Instance.DBName, PMapIniParams.Instance.DBUser, PMapIniParams.Instance.DBPwd, PMapIniParams.Instance.DBCmdTimeOut);
            m_fileName = p_fileName;
            m_ShippingDateX = p_ShippingDateX;
        }
        protected override void DoWork()
        {
            try
            {

                //              var OrdNumSuffix = "_" + DateTime.Now.ToString("MM.dd");
                var OrdNumSuffix = "_" + m_ShippingDateX.DayOfYear.ToString().PadLeft(3, '0');
                Encoding ecFile = Encoding.GetEncoding(Global.PM_ENCODING);
                var items = new List<boMPOrder>();
                var lines = File.ReadAllLines(m_fileName, ecFile);

                foreach (string line in lines)
                {
                    ProcessForm.NextStep();



                    var val = line.Split(';');

                    int columnIndex = 0;
                    var CompanyCode = val[columnIndex++];                           //A
                    var CustomerCode = val[columnIndex++];                          //B
                    var CustomerOrderNumber = val[columnIndex++] + OrdNumSuffix;    //C
                    var CustomerOrderDate = csvDate(val[columnIndex++]);            //D
                    var ShippingDate = csvDate(val[columnIndex++]);                 //E
                    var WarehouseCode = val[columnIndex++];                         //F
                    var TotalGrossWeightOfOrder = csvDouble(val[columnIndex++]);    //G
                    var NumberOfPalletForDel = csvDouble(val[columnIndex++]);       //H
                    var ShippAddressID = val[columnIndex++];                        //I
                    var ShippAddressCompanyName = val[columnIndex++];               //J
                    var ShippAddressZipCode = val[columnIndex++];                   //K
                    var ShippingAddressCity = val[columnIndex++];                   //L
                    var ShippingAddressStreetAndNumber = val[columnIndex++];        //M
                    var Note = val[columnIndex++];                                  //N
                    var RowNumber = csvInt(val[columnIndex++]);                     //O
                    var ProductCode = val[columnIndex++];                           //P
                    var U_M = val[columnIndex++];                                   //Q
                    var ProdDescription = val[columnIndex++];                       //R
                    var ConfOrderQty = csvDouble(val[columnIndex++]);               //S
                    var ConfPlannedQty = csvDouble(val[columnIndex++]);             //T
                    var NetWeight = csvDouble(val[columnIndex++]);                  //U
                    var PalletPlannedQty = csvDouble(val[columnIndex++]);           //V
                    var PalletBulkQty = csvDouble(val[columnIndex++]);              //W
                    var GrossWeightPlanned = csvDouble(val[columnIndex++]);         //X
                    var ADR = val[columnIndex++];                                   //Y
                    var ADRMultiplier = csvDouble(val[columnIndex++]);              //Z
                    var ADRLimitedQuantity = val[columnIndex++];                    //AA
                    var Freeze = val[columnIndex++];                                //AB
                    var Melt = val[columnIndex++];                                  //AC
                    var UV = val[columnIndex++];                                    //AD
                    columnIndex++;                                                  //AE
                    columnIndex++;                                                  //AF
                    var VehicleType = val[columnIndex++];                           //AG

                    

                    double UnitWeight = GrossWeightPlanned / ConfOrderQty;

                    boMPOrder item = new boMPOrder()
                    {
                        CSVFileName = m_fileName,
                        SentToCT = (m_ShippingDateX.Date.CompareTo(ShippingDate.Date) == 0),
                        CompanyCode = CompanyCode,
                        CustomerCode = CustomerCode,
                        CustomerOrderNumber = CustomerOrderNumber,
                        CustomerOrderDate = CustomerOrderDate,
                        ShippingDate = ShippingDate,
                        ShippingDateX = m_ShippingDateX,
                        WarehouseCode = WarehouseCode,
                        TotalGrossWeightOfOrder = TotalGrossWeightOfOrder,
                        NumberOfPalletForDel = NumberOfPalletForDel,
                        NumberOfPalletForDelX = NumberOfPalletForDel,
                        ShippAddressID_DEP_CODE = ShippAddressID,
                        ShippAddressCompanyName = ShippAddressCompanyName,
                        ShippAddressZipCode = ShippAddressZipCode,
                        ShippingAddressCity = ShippingAddressCity,
                        ShippingAddressStreetAndNumber = ShippingAddressStreetAndNumber,
                        Note = Note,
                        RowNumber = RowNumber,
                        ProductCode = ProductCode,
                        U_M = U_M,
                        ProdDescription = ProdDescription,
                        ConfOrderQty = ConfOrderQty,
                        ConfPlannedQty = ConfOrderQty,
                        NetWeight = NetWeight,
                        PalletPlannedQty = PalletPlannedQty,
                        PalletPlannedQtyX = PalletPlannedQty,
                        PalletBulkQty = PalletBulkQty,
                        PalletBulkQtyX = PalletBulkQty,
                        GrossWeightPlanned = GrossWeightPlanned,
                        GrossWeightPlannedX = GrossWeightPlanned,
                        ADR = (ADR.ToUpper() == "I"),
                        ADRMultiplier = ADRMultiplier,
                        ADRLimitedQuantity = (ADRLimitedQuantity.ToUpper() == "I"),
                        Freeze = (Freeze.ToUpper() == "I"),
                        Melt = (Melt.ToUpper() == "I"),
                        UV = (UV.ToUpper() == "I"),
                        Bordero = "",
                        Carrier = "",
                        VehicleType = (!string.IsNullOrWhiteSpace(VehicleType) ? VehicleType : PMapIniParams.Instance.MapeiDefCargoType),
                        KM = 0,
                        Forfait = 0,
                        Currency = "HUF",
                        ADRMultiplierX = ADRMultiplier,
                        UnitWeight = UnitWeight
                    };
                    items.Add(item);

                }


                var bllMPOrderx = new bllMPOrder(PMapCommonVars.Instance.CT_DB);

                var added = 0;
                foreach (var item in items)
                {
                    ProcessForm.NextStep();
                    //lehetnek duplikátumok            if (!bllMPOrderx.IsExist(item.CustomerOrderNumber, item.ProductCode))
                    {

                        bllMPOrderx.AddMPOrder(item);
                        added++;
                    }
                    /*
                    else
                    {
                        ErrResult.Add(new SendResult()
                        {
                            ResultType = SendResult.RESTYPE.ERROR,
                            CustomerOrderNumber = item.CustomerOrderNumber,
                            Message = string.Format(PMapMessages.E_MPORD_CSVIMP_DPL)
                        });
                    }
                    */
                }
                AddedCount = added;
                ItemsCount = items.Count;

    //            bllMPOrderx.PostProcessVehicleType(p_fileName, "HUNSD");
    //            bllMPOrderx.PostProcessVehicleType(p_fileName, "HUNDA");
            }

            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }

        }

        private DateTime csvDate(string p_strDate)
        {
            var ret = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(p_strDate) && p_strDate.Length == 8)
            {
                ret = DateTime.Parse(Util.LeftString(p_strDate, 4) + "." + p_strDate.Substring(4, 2) + "." + Util.RightString(p_strDate, 2));
            }

            return ret;
        }
        private double csvDouble(string p_strDouble)
        {
            double ret = 0;
            if (Double.TryParse(p_strDouble, out ret))
                return ret;
            return ret;
        }

        private int csvInt(string p_strInt)
        {
            int ret = 0;
            if (Int32.TryParse(p_strInt, out ret))
                return ret;
            return ret;
        }
    }
}