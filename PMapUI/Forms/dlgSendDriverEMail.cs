using PMapCore.BO;
using PMapCore.Common;
using PMapUI.Forms.Base;
using PMapCore.Strings;
using PMapCore.WebTrace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMapUI.Common;

namespace PMapUI.Forms
{
    public partial class dlgSendDriverEMail : BaseDialog
    {
        string m_emails = "";
        boPlanTour m_tx;
        public dlgSendDriverEMail(boPlanTour p_tx)
        {
            InitializeComponent();
            m_tx = p_tx;

            if (m_tx.TRK_COMMENT.Contains("@"))
            {
                var emailAddr = m_tx.TRK_COMMENT.Replace(" ", "");
                emailAddr = emailAddr.Replace("\"", "");
                emailAddr = emailAddr.Replace("'", "");
                emailAddr = emailAddr.Replace(",", ";");

                var emailAddress = emailAddr.Split(';').ToList();
                foreach (string em in emailAddress)
                {
                    if (Util.IsValidEmail(em))
                    {
                        if (!string.IsNullOrWhiteSpace(m_emails))
                            m_emails += ";";
                        m_emails += em;
                    }
                }
            }

            txtORD_EMAIL.Text = m_emails;
  //          txtORD_EMAIL.Text = "agyorgyi01@gmail.com,'agyorgyi01@gmail.com'";
        }


        public override Control ValidateForm()
        {
            if (String.IsNullOrEmpty(txtORD_EMAIL.Text))
            {
                UI.Error(PMapMessages.E_SNDEMAIL_MAIL);
                return txtORD_EMAIL;
            }

            return null;
        }

        public override bool OKPressed()
        {
            try
            {

                PMTracedTour tt = new PMTracedTour() { TourID = m_tx.ID, Order = 0 };

                var token = NotificationMail.GetToken(new List<PMTracedTour>() { tt });

                var emailAddress = txtORD_EMAIL.Text.Split(';').ToList();
                foreach (string em in emailAddress)
                {
                    if (Util.IsValidEmail(em))
                    {
                        NotificationMail.SendNotificationMailDrv(em, token, m_tx);
                    }
                }
                //HIBAKEZELÉST !!!
                UI.Message(PMapMessages.E_SNDEMAIL_OK, txtORD_EMAIL.Text);
                Util.Log2File(String.Format(PMapMessages.M_MAIL_SENT, txtORD_EMAIL.Text));
            }
            catch (Exception e)
            {
                Util.Log2File(e.Message);
                UI.Error(e.Message);
            }


            return true;
        }
    }
}
