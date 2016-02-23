using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    class PriceLabelPane : Control
    {
        public PriceLabelPane()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            ForeColor = SystemColors.ControlText;
        }

        PriceLabelPaneController controller;
        public PriceLabelPaneController Controller
        {
            get
            {
                return controller;
            }
        }

        public void Bind(GreenWinFormsMVContext context, PriceLabelPaneController controller)
        {
            this.controller = controller;
            new SimpleControlVCBinder(context, this, controller);
        }

    }
}
