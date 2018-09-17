using PMapCore.Common.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPOrder.BO
{
    [Serializable]
    public class boMPOrderT
    {
        [WriteFieldAttribute(Insert = false, Update = false)]
        public int ID { get; set; }

        //Vevő rendelés száma, Customer Order  code
        [WriteFieldAttribute(Insert = true, Update = true)]
        public string CustomerOrderNumber { get; set; }
        
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
        //Rendelt mennyiség raklap,Pallet Order Qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double PalletOrderQty { get; set; }

        //Szállítandó mennyiség raklap,Pallet Planned Qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double PalletPlannedQty { get; set; }

        //,Pallet ‘Bulk’ qty(Row)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double PalletBulkQty { get; set; }

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

    }

}
