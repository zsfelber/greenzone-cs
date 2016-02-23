using System.Windows.Forms;
using System.Drawing;
using System;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    public class TimeLabelPane : Control
    {
        public TimeLabelPane()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            ForeColor = SystemColors.ControlText;
        }

        TimeLabelPaneController controller;
        public TimeLabelPaneController Controller
        {
            get
            {
                return controller;
            }
        }

        public void Bind(GreenWinFormsMVContext context, TimeLabelPaneController controller)
        {
            this.controller = controller;
            new SimpleControlVCBinder(context, this, controller);
        }

    }
}
