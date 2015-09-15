using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Collections;
using DevExpress.XtraEditors;
using PMap.Localize.Base;

namespace PMap.Common
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
    }
}
