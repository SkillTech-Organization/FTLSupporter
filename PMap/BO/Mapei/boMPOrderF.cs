using PMapCore.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMapCore.BO.Mapei
{
    [Serializable]

    public class boMPOrderF
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }

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

        //Szállítási cím
        [WriteFieldAttribute(Insert = false, Update = false)]
        public string ShippingAddress { get {
                return (ShippAddressZipCode + " " + ShippingAddressCity + " " + ShippingAddressStreetAndNumber).Trim();
            } }

        //!!!szállítandó mennyiség(ret.val.),Conf.Planned Qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ConfPlannedQty { get; set; }

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
        public string Currency { get; set; }
    }

  
}
