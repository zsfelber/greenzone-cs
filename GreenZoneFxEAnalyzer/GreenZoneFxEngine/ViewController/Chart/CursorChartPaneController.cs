using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneUtil.ViewController;
using System.Drawing.Drawing2D;

using GreenZoneFxEngine.Util;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class CursorChartPaneController : ServerCursorChartPaneControllerBase
    {

        internal CursorChartPaneController(GreenRmiManager rmiManager, CursorChartSectionPanelController parent, CursorChartController chart)
            : base(rmiManager, parent, chart)
        {
            YearFont = Font;
            MonthFont = new Font(Font.FontFamily, 6);
            BackColor = Color.Gray;
            GradientColor = Color.LightGray;
            ForeColor = Color.White;
            ChartFrameColor = Color.Lime;
            CpBarVisible = true;
            ThumbRectBarVisible = false;
        }

        protected override void LayOut()
        {
            throw new NotImplementedException();
        }
    }


}
