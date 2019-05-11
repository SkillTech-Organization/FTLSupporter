using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMapCore.Common
{
    public class WaitCursor : IDisposable
    {
        private Cursor m_prevCursor;

        public WaitCursor()
        {
            Cursor m_prevCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Cursor.Current = m_prevCursor;
        }

        #endregion
    }
}
