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
    public partial class dlgSendOrderEMail : BaseDialog
    {
        boPlanTourPoint m_tp;
        public dlgSendOrderEMail(boPlanTourPoint p_tp)
        {
            InitializeComponent();
            m_tp = p_tp;
            txtORD_EMAIL.Text = m_tp.ORD_EMAIL;
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
                List<PMTracedTour> tracedTour = new List<PMTracedTour>();
                PMTracedTour tt = new PMTracedTour() { TourID = m_tp.Tour.ID, Order = m_tp.PTP_ORDER };
                tracedTour.Add(tt);
                var token = NotificationMail.GetToken(tracedTour);
                NotificationMail.SendNotificationMail(txtORD_EMAIL.Text, token, PMapMessages.E_SNDEMAIL_OK3);
                UI.Message(PMapMessages.E_SNDEMAIL_OK, txtORD_EMAIL.Text);
                return true;


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
