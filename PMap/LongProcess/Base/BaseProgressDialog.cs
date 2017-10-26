using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PMap.Common;

namespace PMap.LongProcess.Base
{
    public partial class BaseProgressDialog : Form
    {
        private List<BaseLongProcess> m_LongProcessList = new List<BaseLongProcess>();

        protected delegate void nextStepDelegate();
        protected delegate void StopDelegate(BaseLongProcess p_LongProcess);
        protected delegate void SetParDelegate(int p_min, int p_max, string p_caption);
        protected delegate void SetCaptionDelegate(string p_caption);
        protected delegate void SetInfoTextDelegate(string p_infoText);
        protected delegate void SetVisibleDelegate( bool p_visible);

        private bool m_closeable = false;
        protected bool m_canAbort = false;
        protected int m_value = 0;
        protected int m_min = 0;
        protected int m_max = 0;

        protected readonly object m_LockObj = new object();
        public BaseProgressDialog()
        {
            InitializeComponent();

        }

        public BaseProgressDialog(int p_min, int p_max, string p_caption, bool p_canAbort)
            : this()
        {
            m_canAbort = p_canAbort;
            this._initFormParams(p_min, p_max, p_caption);
        }

        public List<BaseLongProcess> LongProcessList
        {
            get { return m_LongProcessList; }
        }

        public virtual void _initFormParams(int p_min, int p_max, string p_caption)
        {
            m_value = p_min;
            m_min = p_min;
            m_max = p_max;
            this.Text = p_caption;
        }

        public virtual void _setFormCaption(string p_caption)
        {
            this.Text = p_caption;
        }

        public virtual void _setVisible(bool p_visible)
        {
            this.Visible = p_visible;
        }
  
        public virtual void _setInfoText(string p_infoText)
        {
            throw new NotImplementedException();
        }

        public virtual void _nextProgressValue()
        {
            throw new NotImplementedException();
        }


        public void _stopProcess(BaseLongProcess p_LongProcess)
        {
            p_LongProcess.Stop();
            
            m_LongProcessList.Remove(p_LongProcess);
            if (m_LongProcessList.Count == 0)
            {
                m_closeable = true;
                this.Close();
            }
        }

        private void dialogProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !m_closeable;
        }


        public void SetParams(int p_min, int p_max, string p_caption)
        {
            try
            {
                Invoke(new SetParDelegate(this._initFormParams), p_min, p_max, p_caption);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }

        public void SetCaption(string p_caption)
        {
            try
            {
                Invoke(new SetCaptionDelegate(this._setFormCaption), p_caption);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }

        public void SetInfoText(string p_infoText)
        {
            try
            {
                Invoke(new SetInfoTextDelegate(this._setInfoText), p_infoText);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }


        public void NextStep()
        {
            try
            {
                Invoke(new nextStepDelegate(this._nextProgressValue));
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }

        public void StopThreadAndCloseForm(BaseLongProcess p_LongProcess)
        {
            try
            {
                Invoke(new StopDelegate(this._stopProcess), p_LongProcess);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }

        public void StopAllThreadsAndCloseForm()
        {
            for (int i = 0; i < LongProcessList.Count; i++)
            {
                try 
                {
                    StopThreadAndCloseForm(LongProcessList[i]);
                }
                finally
                {
                }
            
            }
        }
        public void SetVisible( bool p_visible)
        {
            try
            {
                Invoke(new SetVisibleDelegate(this._setVisible), p_visible);
            }
            catch (Exception e)
            {
                Util.ExceptionLog(e);
            }
        }

    }
}
