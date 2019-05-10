using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnimatedNotifyIconNS;
using PMapCore.Strings;
using PMapCore.Common;

namespace PMapCore.LongProcess.Base
{
    public partial class ProcessNotifyIcon : AnimatedNotifyIcon
    {
        
        private string[] m_notifyIconText = new string[3] { "", "", "" };
        private ContextMenuStrip m_menu;
        private ToolStripMenuItem m_CancelAct;

        private List<BaseLongProcess> m_LongProcessList = new List<BaseLongProcess>();

        public ProcessNotifyIcon()
            : base()
        {
            init();
        }


        public List<BaseLongProcess> LongProcessList
        {
            get { return m_LongProcessList; }
        }

        private void init()
        {

            //Menu felépítése
            m_menu = new ContextMenuStrip();
            m_CancelAct = new ToolStripMenuItem();
            m_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_CancelAct});
            m_menu.Name = "ProcessNotifyIcon";
            m_menu.Size = new System.Drawing.Size(153, 48);
 
            m_CancelAct.Name = "CancelAct";
            m_CancelAct.Size = new System.Drawing.Size(152, 22);
            m_CancelAct.Text = PMapMessages.M_PROC_STOP;
            m_CancelAct.Click += new System.EventHandler(this.CancelAct_Click);
 
            this.ContextMenuStrip = m_menu;


            Icon[] _icons = new Icon[] { Properties.Resources._1, Properties.Resources._3, Properties.Resources._5, 
                                Properties.Resources._6, Properties.Resources._8, Properties.Resources._10, 
                                Properties.Resources._11, Properties.Resources._13 };

            this.SetAnimationFrames(_icons);
            this.Text = PMapMessages.M_PROC_INIT;
            this.FrameRate = 0.5;
            this.Visible = false;

        }

        public void _setNotifyIconText(string p_infoText)
        {
            for (int i = 1; i < m_notifyIconText.Length; i++)
                m_notifyIconText[i - 1] = m_notifyIconText[i];
            m_notifyIconText[m_notifyIconText.Length - 1] = p_infoText;
            if (this.Visible)
            {
                string sNotifyText = "";
                foreach (string sText in m_notifyIconText)
                {
                    if (sText.Length > 0)
                    {
                        if (sNotifyText.Length > 0)
                            sNotifyText += "\n";
                        sNotifyText += sText.PadRight(60 / m_notifyIconText.Length);
                    }
                }
                this.Text = sNotifyText;
                this.BalloonTipText = sNotifyText;
                this.ShowBalloonTip(1000);
            }
        }

        private void CancelAct_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LongProcessList.Count; i++)
            {
                LongProcessList[i].Stop();
            }
        }


 
        public void StartNotify()
        {
            using (GlobalLocker lockObj = new GlobalLocker(Global.lockObjectNotify))
            {
                if (!this.IsRunning)
                    this.Start();
            }
        }

        public void StopNotify( BaseLongProcess p_stoppedProc)
        {
            using (GlobalLocker lockObj = new GlobalLocker(Global.lockObjectNotify))
            {
                m_LongProcessList.Remove( p_stoppedProc);
                if( m_LongProcessList.Count == 0)
                {
                    this.Stop();
                }
            }
        }

    }
}
