using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Types
{
    public class TimeSeriesEOFException : Exception
    {
        public TimeSeriesEOFException()
            : base()
        {
        }
        public TimeSeriesEOFException(string message)
            : base(message)
        {
        }
    }
}
