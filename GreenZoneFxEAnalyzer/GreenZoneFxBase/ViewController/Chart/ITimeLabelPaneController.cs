using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{

    [GreenRmi]
    public interface ITimeLabelPaneController : IClientController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IChartController Parent
        {
            get;
        }

        Brush StringBrush
        {
            get;
            set;
        }

        StringFormat StringFormat
        {
            get;
            set;
        }

        Font[] Fonts
        {
            get;
            set;
        }

    }

    [GreenRmi]
    public interface IClientTimeLabelPaneController : ITimeLabelPaneController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartController Parent
        {
            get;
        }
    }
}
