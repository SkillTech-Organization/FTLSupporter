using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using PMapCore.Common;

namespace PMapCore.LongProcess.Base
{
    /// <summary>
    /// Hosszú folyamat alaposztály
    /// </summary>
    public class BaseLongProcess
    {

        // Main thread sets this event to stop worker thread:
        public ManualResetEvent EventStop { get; private set; }
        // Worker thread sets this event when it is stopped:
        public ManualResetEvent EventStopped { get; private set; }

        protected Thread m_WorkingThread = null;


        private BaseProgressDialog m_processForm = null;
        protected ProcessNotifyIcon m_notifyIcon = null;
        protected ThreadPriority m_ThreadPriority;
 
        public BaseLongProcess(BaseProgressDialog p_Form, ThreadPriority p_ThreadPriority)
        {
            m_ThreadPriority = p_ThreadPriority;
            if (p_Form != null)
            {
                ProcessForm = p_Form;
                ProcessForm.LongProcessList.Add(this);
            }
            ProcessNotifyIcon = null;
            init();
        }

        public BaseLongProcess(ProcessNotifyIcon p_notifyIcon, ThreadPriority p_ThreadPriority)
        {
            m_ThreadPriority = p_ThreadPriority;
            if (p_notifyIcon != null)
            {
                ProcessNotifyIcon = p_notifyIcon;
                ProcessNotifyIcon.LongProcessList.Add(this);
            }
            ProcessForm = null;
            init();
        }

        public BaseLongProcess(ThreadPriority p_ThreadPriority)
        {
            m_ThreadPriority = p_ThreadPriority;
            ProcessNotifyIcon = null;
            ProcessForm = null;
            init();
        }

        private void init()
        {

            EventStop = new ManualResetEvent(false);
            EventStopped = new ManualResetEvent(false);

            // reset events
            //Sets the state of the specified event to nonsignaled,
            //which causes threads to block.
            if (EventStop != null)
                EventStop.Reset();

            if (EventStopped != null)
                EventStopped.Reset();

        }


        public void Run()
        {
            m_WorkingThread = new Thread(new ThreadStart(this.DoWorkWrapper));
            m_WorkingThread.Priority = m_ThreadPriority;

            if (ProcessForm != null)
            {
                m_WorkingThread.Name = "TH_" + ProcessForm.Text;
            }
            else
            {
                m_WorkingThread.Name = "TH_" + DateTime.Now.ToString();

            }
            m_WorkingThread.Start();
        }

        public void RunWait()
        {
            m_WorkingThread = new Thread(new ParameterizedThreadStart(delegate(object state)
            {
                ManualResetEvent handle = (ManualResetEvent)state;
                this.DoWorkWrapper();
                handle.Set();
            }));

            m_WorkingThread.Priority = m_ThreadPriority;

            ManualResetEvent waitHandle = new ManualResetEvent(false);
            m_WorkingThread.Start(waitHandle);
            waitHandle.WaitOne();
        }

        private void DoWorkWrapper()
        {
            if (m_notifyIcon != null)
                m_notifyIcon.StartNotify();

            if (ProcessForm != null)
            {
                while (!ProcessForm.IsHandleCreated) ;
                DoWork();
                EventStopped.Set();
                ProcessForm.StopThreadAndCloseForm(this);
            }
            else
            {
                DoWork();
                EventStopped.Set();
            }

            if (m_notifyIcon != null)
                m_notifyIcon.StopNotify(this);
        }

        protected virtual void DoWork()
        {

            if (EventStop != null && EventStop.WaitOne(0, true))
            {
                EventStopped.Set();
                return;
            }
        }


        public virtual void Stop()
        {

            if (IsAlive())
            {
                //set event "Stop"
                //Sets the state of the specified event to signaled
                EventStop.Set();

                // wait when thread  will stop or finish
                while (IsAlive())
                {
                    if (WaitHandle.WaitAll((new ManualResetEvent[] { EventStopped }), 100, true))
                    {
                            break;
                    }
                    Application.DoEvents();
                }
                if (m_WorkingThread != null)
                {
                    m_WorkingThread.Abort();
                    m_WorkingThread = null;
                }
                System.GC.Collect();

                Process proc = Process.GetCurrentProcess();
                Util.Log2File("After killed thread:" + proc.PrivateMemorySize64.ToString());
            }
        }

        public BaseProgressDialog ProcessForm
        {
            get { return m_processForm; }
            private set { m_processForm = value; }
        }

        public ProcessNotifyIcon ProcessNotifyIcon
        {
            get { return m_notifyIcon; }
            private set { m_notifyIcon = value; }
        }


        public bool IsAlive()
        {
            if (m_WorkingThread != null)
                return m_WorkingThread.IsAlive;
            else
                return false;
        }

        public void SetNotifyIconText(string p_infoText)
        {
            if (m_notifyIcon != null)
                m_notifyIcon._setNotifyIconText(p_infoText);
        }
    }
}
