using PMapCore.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder.BO
{
    [Serializable]

    public class boMPOrderF
    {
        public int ID { get; set; }

        //CT-be küldve ?
        public bool SentToCT { get; set; } = false;

        //,Company code
        public string CompanyCode { get; set; }

        //Vevőkód, CustomerCode
        public string CustomerCode { get; set; }

        //Vevő rendelés száma, Customer Order  code
        public string CustomerOrderNumber { get; set; }

        //-Vevő rendelés dátuma, Customer Order date
        public DateTime CustomerOrderDate { get; set; }

        //Szállítási dátum, Shipping date
        public DateTime ShippingDate { get; set; }

        //Szállítási terv dátum, Shipping date
        public DateTime ShippingDateX { get; set; }

        //Raktár kód, Warehouse code
        public string WarehouseCode { get; set; }

        //Szállítási Cím ID, Shipp.Address ID
        public string ShippAddressID { get; set; }

        //Szállítási cím cégnév, ShippAddre- Company name
        public string ShippAddressCompanyName { get; set; }

        //Szállítási cím IRSZ,ShippAddress - Zip Code
        public string ShippAddressZipCode { get; set; }

        //Szállítási cím város,Shipping address – City
        public string ShippingAddressCity { get; set; }

        //Szállítási cím, Shipping address - street and number
        public string ShippingAddressStreetAndNumber { get; set; }

        //Szállítási cím
        public string ShippingAddress { get {
                return (ShippAddressZipCode + " " + ShippingAddressCity + " " + ShippingAddressStreetAndNumber).Trim();
            } }

        //Szállítandó raklapok száma, Number of pallet for del.
        public double NumberOfPalletForDel { get; set; }

        //Szállítandó raklapok száma, Number of pallet for del.
        public double NumberOfPalletForDelX { get; set; }

        #region szerkesztendő mezők összesítve

        //!!!szállítandó mennyiség(ret.val.),Conf.Planned Qty(Row)  
        public double ConfPlannedQtySum { get { return Items.Sum(s => s.ConfPlannedQty); } }


        //!!!Szállítandó bruttó súly,Gross Weight Planned(Row)
        public double GrossWeightPlannedXSum { get { return Items.Sum(s => s.GrossWeightPlannedX); } }

        public double ADRMultiplierXSum { get { return Items.Sum(s => s.ADRMultiplierX); } }
        #endregion

        #region szerkesztendő mezők eredetije összesítve

        public double GrossWeightPlannedSum { get { return Items.Sum(s => s.GrossWeightPlanned); } }
        public double ADRMultiplierSum { get { return Items.Sum(s => s.ADRMultiplier); } }

        #endregion

        //!!!Fuvarszám,BORDERO
        public string Bordero { get; set; }

        //!!!Fuvaros,CARRIER
        public string Carrier { get; set; }

        //!!!Szállítóeszköz típus, VEHICLE TYPE
        public string VehicleType { get; set; }

        //!!!Teljes Fuvar km, KM
        public double KM { get; set; }

        //!!!Teljes Fuvar Útdíj, FORFAIT
        public double Forfait { get; set; }

        //!!!Útdíj pénznem(HUF)CURRENCY
        public string Currency { get; set; }

        public List<boMPOrderT> Items { get; set; } = new List<boMPOrderT>();
 
        #region joinolt mezők 
        #endregion

    }


}
