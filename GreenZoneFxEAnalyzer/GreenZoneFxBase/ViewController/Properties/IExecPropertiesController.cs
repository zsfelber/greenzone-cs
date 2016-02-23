using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Reflection;
using GreenZoneUtil.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    [GreenRmi]
    public interface IExecPropertiesController : IController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        BufferedPropertyGridController PropertyGrid1
        {
            get;
            set;
        }
    }
}
