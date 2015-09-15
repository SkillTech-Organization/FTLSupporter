﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMap.DB.Base
{
    class TransactionBlock : IDisposable
    {
        private DBAccess m_DBA;
        private bool m_isInTrans;
        private Cursor m_oldCursor;
        public TransactionBlock(DBAccess pDBA)
        {
            m_DBA = pDBA;
            m_isInTrans = false;
            if (!m_DBA.IsInTran())
            {
                m_isInTrans = true;
                m_DBA.BeginTran();
            }

            m_oldCursor = Cursor.Current;
            if( m_oldCursor != Cursors.WaitCursor)
                Cursor.Current= Cursors.WaitCursor;

            //            StackTrace trace = new StackTrace(1, true);
            //            Console.WriteLine("TRANS TRY  ido:" + DateTime.Now + " " + trace.GetFrame(1).GetMethod() + "-->" + trace.GetFrame(0).GetMethod());
        }


        #region IDisposable Members
        public void Dispose()
        {
            //            StackTrace trace = new StackTrace(1, true);
            //            Console.WriteLine("COMMIT " + DateTime.Now + " " + trace.GetFrame(1).GetMethod() + "-->" + trace.GetFrame(0).GetMethod());
            Cursor.Current = m_oldCursor;
            if (m_isInTrans && m_DBA.IsInTran())
                m_DBA.Commit();

        }
        #endregion
    }

}
