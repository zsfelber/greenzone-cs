using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Windows.Forms.VisualStyles;
using System.Drawing;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ServerChartPaneControllerEx : ServerChartPaneControllerBase
    {

        public ServerChartPaneControllerEx(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, ServerChartControllerEx chart)
            : base(rmiManager, parent)
        {
            Chart = chart;

            CpBarVisible = false;

            ChartLeftGap = 0;
            ChartRightGap = 0;

            SliderMinimum = 0;
            SliderMaximum = 100;
            SliderValue = 50;

            SliderThumbVisible = true;

            Size = new Size(100, 100);

            BarRectangle = new Rectangle(0, 0, 4, 0);
            CpBarRectangle = new Rectangle(0, 0, 1, 0);

            SliderBarColor = Color.FromArgb(100, 255, 69, 0);
            SliderThumbVisible = true;
            ThumbRectBarVisible = true;

            ChartCalcAutoGap = true;
            ChartLeftGap = 0;
            ChartRightGap = 0;
            Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

            BackColor = Color.White;
            ForeColor = Color.Black;
            GridColor = Color.Green;

            PopupMenu = new ComboController(rmiManager, this, false);
            SelectedIndDel = new ButtonController(rmiManager, PopupMenu, "Remove");
            SelectedProps = new ButtonController(rmiManager, PopupMenu, "Properties");

            // TODO Client
            //PopupMenu.VisibleChanged += new PropertyChangedEventHandler(PopupMenu_VisibleChanged);
            //SelectedProps.Pressed += new ControllerEventHandler(selectedProps_Pressed);
            //SelectedIndDel.Pressed += new ControllerEventHandler(selectedIndDel_Pressed);
            SupportsPaint = true;
        }

        public ServerChartPaneControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ServerChartPaneControllerEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
        }


        // TODO Client
        /*
        void PopupMenu_VisibleChanged(object sender, PropertyChangedEventArgs e)
        {
            // TODO client
            bool propsEn = selectedIndicatorBar != null;
            SelectedProps.Enabled = propsEn;
            SelectedIndDel.Enabled = propsEn;
            if (propsEn)
            {
                // TODO client
                IIndicatorRuntime ind = selectedIndicatorBar.indicator;
                PopupMenu.Text = ind.ExecutableInfo.Name + " " + ind.Session.ShortName;
            }
            else
            {
                PopupMenu.Text = "";
            }
        }

        void selectedProps_Pressed(object sender, ControllerEventArgs e)
        {
            // TODO client
            if (selectedIndicatorBar != null)
            {
                INormalChartController parent = (INormalChartController)this.parent.Parent;
                // TODO client
                parent.ChartPanel.ShowIndicatorProperties(selectedIndicatorBar.indicator);
                // TODO client
                ClearSeriesBarSelection();
            }
        }

        void selectedIndDel_Pressed(object sender, ControllerEventArgs e)
        {
            // TODO client
            if (selectedIndicatorBar != null)
            {
                INormalChartController parent = (INormalChartController)this.parent.Parent;
                // TODO client
                parent.ChartRuntime.RemoveIndicator(selectedIndicatorBar.indicator.Id);
                // TODO client
                parent.RemoveIndicatorPanel(selectedIndicatorBar.indicator);
                parent.UpdateSeries();
                parent.ChartPanel.UpdateIndicators();
                parent.MainWindow.SaveSession();
            }
        }
         * */
    }
}
