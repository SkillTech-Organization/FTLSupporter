using PMap.BO;
using PMap.Common;
using PMap.Forms.Base;
using PMap.Localize;
using PMap.WebTrace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMap.Forms
{
    public partial class dlgSendEMail : BaseDialog
    {
        boPlanTourPoint m_tp;
        public dlgSendEMail(boPlanTourPoint p_tp)
        {
            InitializeComponent();
            m_tp = p_tp;
            txtORD_EMAIL.Text = m_tp.ORD_EMAIL;
            txtORD_EMAIL.Text = "agyorgyi01@gmail.com,'agyorgyi01@gmail.com'";
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
              //  var token = new PMToken() { temporaryUserToken = "token lesy itt" };
                NotificationMail.SendNotificationMail(txtORD_EMAIL.Text, token);

                UI.Message(PMapMessages.E_SNDEMAIL_OK);
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
