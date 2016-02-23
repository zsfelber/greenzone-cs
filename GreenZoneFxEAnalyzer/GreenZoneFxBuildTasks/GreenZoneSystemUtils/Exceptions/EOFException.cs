using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Types
{
    public class EOFException : Exception
    {
        public EOFException(string message)
            : base(message)
        {
        }
    }
}
