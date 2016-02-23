using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SevenZip
{
    public class LZMAException : Exception
    {
        public LZMAException()
            : base()
        {
        }
        public LZMAException(string s)
            : base(s)
        {
        }
        public LZMAException(string s, Exception inner)
            : base(s, inner)
        {
        }
    }
}
