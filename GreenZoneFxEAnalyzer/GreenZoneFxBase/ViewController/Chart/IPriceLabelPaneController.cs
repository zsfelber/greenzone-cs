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
    public interface IPriceLabelPaneController : IClientController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        bool PlainFonts
        {
            get;
            set;
        }
        
        StringFormat StringFormat
        {
            get;
            set;
        }

        Brush StringBrush
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
    public interface IClientPriceLabelPaneController : IPriceLabelPaneController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartSectionPanelController Parent
        {
            get;
        }
    }
}
