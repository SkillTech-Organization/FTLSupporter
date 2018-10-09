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
        public int ID { get; set; }

        //Vevő rendelés száma, Customer Order  code
        public string CustomerOrderNumber { get; set; }

        //Sor száma(rendelésen belül),ROW NUMBER
        public int RowNumber { get; set; }

        //Termékkód,Product Code
        public string ProductCode { get; set; }

        //Mennyiségi egység, U.M.
        public string U_M { get; set; }

        //Termék megnevezés, Prod Description
        public string ProdDescription { get; set; }

        //Rendelt mennyiség bruttó súly, Conf.Order Qty ROW
        public double ConfOrderQty { get; set; }

        //Rendelt mennyiség raklap,Pallet Order Qty(Row)
        public double PalletOrderQty { get; set; }

        //Szállítandó mennyiség raklap,Pallet Planned Qty(Row)
        public double PalletPlannedQty { get; set; }

        //,Pallet ‘Bulk’ qty(Row)
        public double PalletBulkQty { get; set; }

        //ADR,ADR
        public bool ADR { get; set; }

        //ADR szorzó, ADR Multiplier
        public double ADRMultiplier { get; set; }

        //ADR köteles mennyiség, limited_quantity
        public bool ADRLimitedQuantity { get; set; }

        #region szerkesztendő mezők

        //!!!szállítandó mennyiség(ret.val.),Conf.Planned Qty(Row)  
        public double ConfPlannedQty { get; set; }

        // Szállítandó bruttó súly,Gross Weight Planned(Row) EREDETI
        public double GrossWeightPlanned { get; set; }

        //!!!Szállítandó bruttó súly,Gross Weight Planned(Row)
        public double GrossWeightPlannedX { get; set; }

        //ADR szorzó, ADR Multiplier (egyelőre csak átadjuk a CT-be)
        [WriteFieldAttribute(Insert = true, Update = true)]
        public double ADRMultiplierX { get; set; }

        #endregion

        //Fagyérzékeny, Freeze
        public bool Freeze { get; set; }

        //Hőérzékeny,MELT
        public bool Melt { get; set; }

        //UV érzékeny,UV
        public bool UV { get; set; }
        public double UnitWeight { get; set; }

        #region joinolt mezők 
        #endregion

        #region számolt mezők 

        #endregion
    }

}
