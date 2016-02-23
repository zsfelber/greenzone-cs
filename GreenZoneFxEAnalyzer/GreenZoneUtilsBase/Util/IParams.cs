using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneUtil.Util
{
    public interface IParams
    {
        Dictionary<string, object> Params
        {
            get;
            set;
        }
    }
}
