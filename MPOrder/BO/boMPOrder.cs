using PMapCore.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder.BO
{
    [Serializable]
    public class boMPOrder
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }

        //CT-be küldve ?
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool SentToCT { get; set; } = false;

        //,Company code
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string CompanyCode { get; set; }

        //Vevőkód, CustomerCode
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string CustomerCode { get; set; }

        //Vevő rendelés száma, Customer Order  code
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string CustomerOrderNumber { get; set; }

        //-Vevő rendelés dátuma, Customer Order date
        [WriteFieldAttribute(Insert = true, Update = true)]
        public DateTime CustomerOrderDate { get; set; }

        //Szállítási dátum, Shipping date
        [WriteFieldAttribute(Insert = true, Update = true)]
        public DateTime ShippingDate { get; set; }

        //Raktár kód, Warehouse code
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string WarehouseCode { get; set; }

        //Rendelés Össz bruttó súly, Total gross weight of order
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double TotalGrossWeightOfOrder { get; set; }

        //Szállítandó raklapok száma, Number of pallet for del.
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double NumberOfPalletForDel { get; set; }

        //Szállítási Cím ID, Shipp.Address ID
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ShippAddressID { get; set; }

        //Szállítási cím cégnév, ShippAddre- Company name
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ShippAddressCompanyName { get; set; }

        //Szállítási cím IRSZ,ShippAddress - Zip Code
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ShippAddressZipCode { get; set; }

        //Szállítási cím város,Shipping address – City
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ShippingAddressCity { get; set; }

        //Szállítási cím, Shipping address - street and number
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ShippingAddressStreetAndNumber { get; set; }

        //Megjegyzés, NOTE
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string Note { get; set; }

        //Sor száma(rendelésen belül),ROW NUMBER
        [WriteFieldAttribute(Insert = true, Update = true)]
        public int RowNumber { get; set; }

        //Termékkód,Product Code
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ProductCode { get; set; }

        //Mennyiségi egység, U.M.
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string U_M { get; set; }

        //Termék megnevezés, Prod Description
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string ProdDescription { get; set; }

        //Rendelt mennyiség bruttó súly, Conf.Order Qty ROW
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ConfOrderQty { get; set; }

        //!!!szállítandó mennyiség(ret.val.),Conf.Planned Qty(Row)  
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ConfPlannedQty { get; set; }

        //Rendelt mennyiség raklap,Pallet Order Qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double PalletOrderQty { get; set; }

        //Szállítandó mennyiség raklap,Pallet Planned Qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double PalletPlannedQty { get; set; }

        //,Pallet ‘Bulk’ qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double PalletBulkQty { get; set; }

        //!!!Szállítandó bruttó súly,Gross Weight Planned(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double GrossWeightPlanned { get; set; }

        //ADR,ADR
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool ADR { get; set; }

        //ADR szorzó, ADR Multiplier
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ADRMultiplier { get; set; }

        //ADR köteles mennyiség, limited_quantity
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool ADRLimitedQuantity { get; set; }

        //Fagyérzékeny, Freeze
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool Freeze { get; set; }

        //Hőérzékeny,MELT
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool Melt { get; set; }

        //UV érzékeny,UV
        [WriteFieldAttribute(Insert = true, Update = true)]
        public bool UV { get; set; }

        //!!!Fuvarszám,BORDERO
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string Bordero { get; set; }

        //!!!Fuvaros,CARRIER
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string Carrier { get; set; }

        //!!!Szállítóeszköz típus, VEHICLE TYPE
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string VehicleType { get; set; }

        //!!!Teljes Fuvar km, KM
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double KM { get; set; }

        //!!!Teljes Fuvar Útdíj, FORFAIT
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double Forfait { get; set; }

        //!!!Útdíj pénznem(HUF)CURRENCY
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string Currency { get; set; } = "HUF";

        [WriteFieldAttribute(Insert = true, Update = true)]
        public double UnitWeight { get; set; }
 
        //ADR szorzó, ADR Multiplier (Új érték, egyelőre csak átadjuk a CT-be)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ADRMultiplierX { get; set; }

        #region joinolt mezők 
        #endregion
    }
}
