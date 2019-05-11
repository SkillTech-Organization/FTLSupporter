using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Collections;
using DevExpress.XtraEditors;
using PMapCore.Strings.Base;
using PMapCore.Common;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace PMapUI.Common
{
    public class UI
    {
        /// <summary>
        /// Messageboxtipusok
        /// </summary>

        public static void Init()
        {
        }

        /// <summary>
        /// Megerosites
        /// </summary>
        /// <param name="message">Uzenetkod</param>
        /// <returns>Igen vagy nem</returns>
        public static bool Confirm(string p_message, params object[] p_params)
        {
            String sMsg = String.Format(p_message, p_params);
            Util.Log2File(String.Format("{0}:{1}", Messages.UI_CONFIRM, sMsg), Global.MsgFileName);

            return XtraMessageBox.Show(sMsg,
                                 Messages.UI_CONFIRM,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Uzenet
        /// </summary>
        /// <param name="message">Uzenetkod</param>
        public static void Message(string p_message, params object[] p_params)
        {
            String sMsg = String.Format(p_message, p_params);
            Util.Log2File(String.Format("{0}:{1}", Messages.UI_MESSAGE, sMsg), Global.MsgFileName);

            XtraMessageBox.Show(sMsg,
                                 Messages.UI_MESSAGE,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);

        }

        /// <summary>
        /// Figyelmeztetes
        /// </summary>
        /// <param name="message">Uzenetkod</param>
        public static void Warning(string p_message, params object[] p_params)
        {
            String sMsg = String.Format(p_message, p_params);
            Util.Log2File(String.Format("{0}:{1}", Messages.UI_WARNING, sMsg), Global.MsgFileName);

            XtraMessageBox.Show(sMsg,
                                Messages.UI_WARNING,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Hiba
        /// </summary>
        /// <param name="message">Uzenetkod</param>
        public static void Error(string p_message, params object[] p_params)
        {
            String sMsg = String.Format(p_message, p_params);
            Util.Log2File(String.Format("{0}:{1}", Messages.UI_ERROR, sMsg), Global.MsgFileName);

            XtraMessageBox.Show(sMsg,
                                 Messages.UI_ERROR,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
        }


        public static string SaveGridLayoutToString(GridView p_gw)
        {

            MemoryStream ms = new MemoryStream();
            p_gw.SaveLayoutToStream(ms);
            string retVal = Encoding.UTF8.GetString(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            ms.Dispose();
            return retVal;
        }

        public static void RestoreGridLayoutFromString(GridView p_gw, string p_XMLLayout)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(p_XMLLayout);
            MemoryStream ms = new MemoryStream(byteArray);
            p_gw.RestoreLayoutFromStream(ms);
            ms.Close();
        }

    }
}
