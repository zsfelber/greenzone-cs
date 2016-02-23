using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneUtil.Util
{
    public interface IWingdingsChar
    {
        int CharCode
        {
            get;
            set;
        }
    }

    public interface ISelectableFile
    {
        string Path
        {
            get;
        }
    }

    public interface ISelectableDir
    {
        string Path
        {
            get;
        }
    }
}
