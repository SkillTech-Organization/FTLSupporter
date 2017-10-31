using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using SMcMaster;
using PMap.Localize;
using DevExpress.Utils;
using DevExpress.XtraNavBar;
using PMap.Common;

namespace PMap.Forms.Base
{
    public partial class BaseDialog : BaseForm,  IDisposable 
    {
        public enum eEditMode
        {
            newmode,
            editmode,
            viewmode,           // read-only form
            infomode,           // információ tétellapon
            selectmode,
            unset
        }


        protected eEditMode m_editMode;
        protected string m_lastError = "";
        protected int m_itemID = -1;
        protected Hashtable m_fieldValues = new Hashtable();
        protected bool m_valueChanged = false;
        
        protected bool AskOnExit {get;set;}

        public BaseDialog()
            :this(eEditMode.unset)
        {
            this.AcceptButton = buttonOK;
            InitForm();
        }

        public BaseDialog(eEditMode p_editMode)
        {
            InitializeComponent();
            m_editMode = p_editMode;
            this.AcceptButton = buttonOK;
            AskOnExit = true;


        }

        public eEditMode EditMode
        {
            get { return m_editMode; }
            set { m_editMode = value; }

        }

        public int ItemID
        {
            get { return m_itemID; }
            set { m_itemID = value; }
        }

#region felülírandó metódusok
        /// <summary>
        /// Form tartalmának validálása
        /// </summary>
        /// <returns>Az a kontrol, amely tartalmával probléma van</returns>
        public virtual Control ValidateForm()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true, ha minden rendben és bezárható a form</returns>
        public virtual bool OKPressed()
        {
            return true;
        }

        public virtual void CancelPressed()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

#endregion

        /// <summary>
        /// Inicializalas
        /// </summary>
        public virtual void InitDialog()
        {
            InitDialog(TabOrderManager.TabScheme.AcrossFirst);
        }

        public virtual void InitDialog(TabOrderManager.TabScheme p_tabscheme)
        {
            errProvider.RightToLeft = true;
            InitForm();

            SuspendLayout();
            /*
            FontStyle fs = errProvider.Font.Style;
            errProvider.Font.Font = new Font(NyilWConfig.ScreenFont, fs);
            */

            if (EditMode == eEditMode.viewmode || EditMode == eEditMode.infomode)
            {
                buttonOK.Visible = false;
                tblDlgButtons.ColumnCount = 1;
                buttonCancel.Dock = DockStyle.None;
                buttonCancel.Anchor = AnchorStyles.None;
                buttonCancel.Text = buttonOK.Text;
            }

            ResumeLayout();

            SetControlEventHandlers(this);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            if (ValidateForm() == null)
            {
                if (OKPressed())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CancelPressed();
        }


        /// <summary>
        /// Beallitja minden kontroll onLeave esemenyét beállítja és a viewmode-ban a kontrolok disabled-be rakása
        /// Most tesztkent TextChange esemeny
        /// </summary>
        /// <param name="c">Kontrol</param>
        protected void SetControlEventHandlers(Control c)
        {
            if (c != null && c.Controls != null)
            {
                if (c != this && (EditMode == eEditMode.newmode || EditMode == eEditMode.editmode))
                {
                    c.Leave += new EventHandler(this.control_Leave);
                }

                // Ide beiktatjuk a View modban torteno letiltast
                if (EditMode == eEditMode.viewmode)
                {
                    setCtrlDisabled(c);
                }
                for (int i = 0; i < c.Controls.Count; i++)
                {
                    SetControlEventHandlers(c.Controls[i]);
                }
            }

        }


        /// <summary>
        /// A kontrolok onLeave eseménye
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_Leave(object sender, EventArgs e)
        {
            if (sender != null)
            {
                  m_valueChanged = true;
            }
        }



        protected void setCtrlDisabled(Control p_Ctrl)
        {
            if (p_Ctrl.GetType() == typeof(DevExpress.XtraGrid.GridControl))
            {
                DevExpress.XtraGrid.GridControl gr = (DevExpress.XtraGrid.GridControl)p_Ctrl;
                if (gr != null)
                {
                    GridView gw = gr.FocusedView as GridView;
                    if (gw != null)
                    {
                        gw.OptionsBehavior.Editable = false;
                        gw.OptionsSelection.EnableAppearanceFocusedCell = false;
                        gw.OptionsView.EnableAppearanceEvenRow = true;
                        gw.OptionsView.EnableAppearanceOddRow = true;
                    }
                }
            }

            if (Util.IsBaseType(p_Ctrl.GetType(), typeof(TextBox)))
            {
                TextBox tb = (TextBox)p_Ctrl;
                tb.ReadOnly = true;
                p_Ctrl.BackColor = Global.DISABLEDCOLOR;
                p_Ctrl.TabStop = false;
            }

            if (Util.IsBaseType(p_Ctrl.GetType(), typeof(Label)))
            {
                Label lb = (Label)p_Ctrl;
                if (lb.BorderStyle != BorderStyle.None)
                    p_Ctrl.BackColor = Global.DISABLEDCOLOR;
                p_Ctrl.TabStop = false;
            }

            if (Util.IsBaseType(p_Ctrl.GetType(), typeof(CheckBox)))
            {
                CheckBox cb = (CheckBox)p_Ctrl;
                cb.Enabled = false;
                p_Ctrl.BackColor = Global.DISABLEDCOLOR;
                p_Ctrl.ForeColor = Color.Black;
                p_Ctrl.TabStop = false;
            }

            if (Util.IsBaseType(p_Ctrl.GetType(), typeof(DateEdit)))
            {
                DateEdit de = (DateEdit)p_Ctrl;
                de.Properties.ReadOnly = true;
                p_Ctrl.BackColor = Global.DISABLEDCOLOR;
                p_Ctrl.TabStop = false;
            }

            if (Util.IsBaseType(p_Ctrl.GetType(), typeof(System.Windows.Forms.ComboBox)))
            {
                System.Windows.Forms.ComboBox cbo = (System.Windows.Forms.ComboBox)p_Ctrl;
                cbo.DropDownStyle = ComboBoxStyle.DropDown;
                p_Ctrl.BackColor = Global.DISABLEDCOLOR;
                p_Ctrl.TabStop = false;
            }
        }


        /// <summary>
        /// Dialóguson kényelmes billenyűzetkezelés
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            Keys ctrlEnter = Keys.Enter | Keys.Control;
            bool bOk = false;



            if (keyData == ctrlEnter)
            {
                if (buttonOK.Enabled)
                    OKPressed();
                return false;
            }
            else if (keyData == Keys.Return)
            {

                //Nyomógomb ENTER-re nem léptetünk, hanem
                if (Util.IsBaseType(this.ActiveControl.GetType(), typeof(Button)))
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }

                //Ha griden történt az ENTER, nem léptetünk, szerkesztő üzemmódba megyünk.
                if (Util.IsBaseType(this.ActiveControl.GetType(), typeof(GridControl)))
                {

                    GridControl gr = (GridControl)this.ActiveControl;
                    GridView gw = gr.FocusedView as GridView;
                    if (gw.RowCount != 0 && gw.Editable)
                    {
                        //szerkeszhető, editor megjelenítése
                        gw.ShowEditor();
                        return true;
                    }
                    else
                    {
                        //nem szerkeszthető, tovább a következő kontrolra
                        this.SelectNextControl(gr, true, true, true, true);
                        return true;
                    }
                }

                //Ha egy gridBEN(!!!) lévő konrollban történt az ENTER, akkor nem léptetünk, hagyni kell a gridet működni...
                //
                GridControl parentGrid = (GridControl)Util.FindParentByType(this.ActiveControl, typeof(GridControl));
                if (parentGrid != null)
                {

                    GridView gw = parentGrid.FocusedView as GridView;
                    if (gw.OptionsView.NewItemRowPosition == NewItemRowPosition.None && gw.FocusedRowHandle == gw.RowCount - 1)
                    {
                        // ha a legutosó szerkeszthető cellában nyomunk ENTERt, és nincs új tétel felvitelére lehetőség, 
                        // akkor léptetés a következő kontrolra.

                        GridColumn lastEditableGC = null;
                        for (int i = 0; i < gw.Columns.Count; i++)
                        {
                            if (gw.Columns[i].OptionsColumn.AllowEdit)
                                lastEditableGC = gw.Columns[i];
                        }
                        if (gw.FocusedColumn == lastEditableGC)
                        {
                            gw.PostEditor();
                            gw.HideEditor();
                            this.SelectNextControl(parentGrid, true, true, true, true);
                            return true;
                        }
                        else
                            return base.ProcessCmdKey(ref msg, keyData);    //van még szerkeszthető mező, a gridre bízzuk 
                        //                        return false;    //van még szerkeszthető mező, a gridre bízzuk 
                    }
                    else if (gw.RowCount != 0)
                        return base.ProcessCmdKey(ref msg, Keys.Tab);
                }

                Control currControl = this.ActiveControl;

                //Léptetés a következő kontrolra
                this.SelectNextControl(this.ActiveControl, true, true, true, true);     //Itt lefut a fókuszált kontrol LEAVE és VALIDATE eseménykezelője

                // TAB-okon végigmegy
                if (this.ActiveControl == buttonOK || this.ActiveControl == buttonCancel)
                {
                    TabControl tab = (TabControl)Util.FindParentByType(currControl, typeof(TabControl));
                    if (tab != null)
                    {
                        if (tab.SelectedIndex < tab.TabCount - 1)
                        {
                            tab.SelectedIndex++;
                            tab.SelectNextControl(currControl, true, true, true, true);
                            return true;
                        }
                    }

                    XtraTabControl xtab = (XtraTabControl)Util.FindParentByType(currControl, typeof(XtraTabControl));
                    if (xtab != null)
                    {
                        if (xtab.SelectedTabPageIndex < xtab.TabPages.Count - 1)
                        {
                            xtab.SelectedTabPageIndex++;
                            xtab.SelectNextControl(currControl, true, true, true, true);
                            return true;
                        }
                    }
                }
                if (this.ActiveControl == buttonCancel)
                {
                    // Ha a CANCEL gombra adja a fókuszt a SelectNextControl,
                    //megnézzük, hogy SelectNextControl határásra végrehajtott LEAVE és VALIDATE eseménykezelőkben 
                    // nem engedélyeződött-e egy újabb kontrol

                    ////Itt már nem fut le a kontrol LEAVE és VALIDATE eseménykezelője, mert nem az a fókuszált
                    this.SelectNextControl(currControl, true, true, true, true);    


                    //Ha ezután is a CANCEL gombon vagyunk, az aktív kontrol legyen az eredeti kontrol 
                    if( this.ActiveControl == buttonCancel)
                        this.ActiveControl = currControl;
                }

                //Ha gridbe ENTER-eztünk, egyből kerüljünk át szerkesztő üzemmódba
                if (this.ActiveControl.GetType() == typeof(DevExpress.XtraGrid.GridControl))
                {
                    DevExpress.XtraGrid.GridControl gr = (DevExpress.XtraGrid.GridControl)this.ActiveControl;
                    gr.FocusedView.ShowEditor();
                }
                return true;
            }
            else if ((keyData == Keys.Down))
            {

                if (bOk && Util.IsBaseType(this.ActiveControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)this.ActiveControl;
                    bOk = cb.SelectedIndex == cb.Items.Count - 1;
                }

                if (this.ActiveControl.GetType() == typeof(DevExpress.XtraGrid.GridControl))
                {
                    DevExpress.XtraGrid.GridControl gr = (DevExpress.XtraGrid.GridControl)this.ActiveControl;
                    GridView gw = gr.FocusedView as GridView;
                    bOk = gw.FocusedRowHandle < 0 || gw.FocusedRowHandle == gw.RowCount - 1;
                }

                if (bOk)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    return true;
                }
            }
            else if (keyData == Keys.Up)
            {
                if (bOk && Util.IsBaseType(this.ActiveControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)this.ActiveControl;
                    bOk = cb.SelectedIndex == 0;
                }

                //                bOk = !Util.IsBaseType(this.ActiveControl.GetType(), typeof(System.Windows.Forms.ComboBox)) &&
                //                    !Util.IsBaseType(this.ActiveControl.GetType(), typeof(ctrlSelWarehouse));

                if (this.ActiveControl.GetType() == typeof(DevExpress.XtraGrid.GridControl))
                {
                    DevExpress.XtraGrid.GridControl gr = (DevExpress.XtraGrid.GridControl)this.ActiveControl;
                    GridView gw = gr.FocusedView as GridView;
                    if (gw.FocusedRowHandle == GridControl.NewItemRowHandle && gw.RowCount > 1)
                    {
                        //    gw.CancelUpdateCurrentRow();
                        //    gw.MoveLast();
                        bOk = false;
                    }
                    else
                        bOk = gw.FocusedRowHandle == GridControl.InvalidRowHandle ||
                            gw.FocusedRowHandle == GridControl.NewItemRowHandle ||
                            gw.FocusedRowHandle == 0 || (gw.FocusedRowHandle == 0 && gw.RowCount <= 1);
                }

                if (bOk)
                {
                    this.SelectNextControl(this.ActiveControl, false, true, true, true);
                    return true;
                }
            }
            else if (keyData == Keys.Tab)
            {
                if (this.ActiveControl.GetType() == typeof(DevExpress.XtraGrid.GridControl))
                {
                    //Tabbal az üres gridet átlépjük
                    DevExpress.XtraGrid.GridControl gr = (DevExpress.XtraGrid.GridControl)this.ActiveControl;
                    GridView gw = gr.FocusedView as GridView;
                    if (gw.RowCount == 0)
                    {
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void buttonOK_Enter(object sender, EventArgs e)
        {

            buttonOK.Font = new Font(buttonOK.Font, FontStyle.Bold);
        }

        private void buttonOK_Leave(object sender, EventArgs e)
        {
            buttonOK.Font = new Font(buttonOK.Font, FontStyle.Regular);
        }

        private void buttonCancel_Enter(object sender, EventArgs e)
        {
            buttonCancel.Font = new Font(buttonCancel.Font, FontStyle.Bold);
        }

        private void buttonCancel_Leave(object sender, EventArgs e)
        {
            buttonCancel.Font = new Font(buttonCancel.Font, FontStyle.Regular);
        }

        private void BaseDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_valueChanged && DialogResult == System.Windows.Forms.DialogResult.Cancel &&
                ( AskOnExit && !UI.Confirm(PMapMessages.E_ASK_EXIT)))
            {
                e.Cancel = true;
            }

        }
    }
}
