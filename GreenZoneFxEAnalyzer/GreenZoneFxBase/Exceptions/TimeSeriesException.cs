using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Types
{
    public class TimeSeriesException : Exception
    {
        public TimeSeriesException()
            : base()
        {
        }
        public TimeSeriesException(string message)
            : base(message)
        {
        }
        public TimeSeriesException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }
}
