using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VBInterface
{
    public class PositionableStreamReader : StreamReader
    {
        public PositionableStreamReader(string path, Encoding p_encoding)
            : base(path, p_encoding)
        { }

        private int myLineEndingCharacterLength = Environment.NewLine.Length;
        public int LineEndingCharacterLength
        {
            get { return myLineEndingCharacterLength; }
            set { myLineEndingCharacterLength = value; }
        }

        public override string ReadLine()
        {
            string line = base.ReadLine();
            if (null != line)
                myStreamPosition += line.Length + myLineEndingCharacterLength;
            return line;
        }

        private long myStreamPosition = 0;
        public long Position
        {
            get { return myStreamPosition; }
            set
            {
                myStreamPosition = value;
                this.BaseStream.Position = value;
                this.DiscardBufferedData();
            }
        }
    }
}
