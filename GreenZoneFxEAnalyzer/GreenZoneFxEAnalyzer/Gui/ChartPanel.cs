using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Properties;
using GreenZoneFxEngine.Gui.Chart;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class ChartPanel : UserControl
    {
        private ToolStripControlHost dtTScomponent1, dtTScomponent2;
        private DateTimePicker fromTimePicker;
        private DateTimePicker toTimePicker;

        public ChartPanel()
        {
            InitializeComponent();
            fromTimePicker = new DateTimePicker();
            fromTimePicker.Width = 85;
            fromTimePicker.Format = DateTimePickerFormat.Custom;
            fromTimePicker.CustomFormat = GreenZoneUtils.GetShortDateTimePattern();
            toTimePicker = new DateTimePicker();
            toTimePicker.Width = 85;
            toTimePicker.Format = DateTimePickerFormat.Custom;
            toTimePicker.CustomFormat = GreenZoneUtils.GetShortDateTimePattern();
            dtTScomponent1 = new ToolStripControlHost(fromTimePicker);
            dtTScomponent2 = new ToolStripControlHost(toTimePicker);

            topToolStrip.Items.Add(new ToolStripLabel("From:"));
            topToolStrip.Items.Add(dtTScomponent1);
            topToolStrip.Items.Add(new ToolStripLabel("To:"));
            topToolStrip.Items.Add(dtTScomponent2);

        }

        public void Bind(GreenWinFormsMVContext context, ChartViewController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            new SimpleControlVCBinder(context, topToolStrip, controller.TopToolStrip);
            new ToolStripDropDownButtonVCBinder(context, symbolDdButton, controller.SymbolDdButton);
            new ToolStripDropDownButtonVCBinder(context, periodDdButton, controller.PeriodDdButton);
            new ToolStripDropDownButtonVCBinder(context, chartTypeDdButton, controller.ChartTypeDdButton);
            //new ToolStripDropDownButtonVCBinder(context, toolStripDropDownButton4 ,controller.toolStripDropDownButton4;
            //new ToolStripDropDownButtonVCBinder(context, toolStripDropDownButton6 ,controller.toolStripDropDownButton6;
            //new ToolStripDropDownButtonVCBinder(context, toolStripDropDownButton5 ,controller.toolStripDropDownButton5;
            //new ToolStripLabelVCBinder1(context, toolStripLabel3 ,controller.toolStripLabel3;
            //new ToolStripLabelVCBinder1(context, toolStripLabel4 ,controller.toolStripLabel4;
            //new ToolStripLabelVCBinder1(context, toolStripLabel5 ,controller.toolStripLabel5;
            //new ToolStripLabelVCBinder1(context, toolStripLabel7 ,controller.toolStripLabel7;
            //new ToolStripLabelVCBinder1(context, toolStripLabel8 ,controller.toolStripLabel8;
            //new SimpleControlVCBinder(context, panel1 ,controller.panel1;
            new ToolStripButtonVCBinder(context, toggleTopBarButton1, controller.ToggleTopBarButton1);
            new ToolStripButtonVCBinder(context, closeChartButton1, controller.CloseChartButton1);
            //new ToolStripLabelVCBinder1(context, toolStripLabel1 ,controller.toolStripLabel1;
            new ButtonVCBinder(context, toggleTopBarButton2, controller.ToggleTopBarButton2);
            chart1.Bind(context, controller.Chart1);
            cursorChart1.Bind(context, controller.CursorChart);
            new ToolStripChbuttonVCBinder(context, connectCursorButton, controller.ConnectCursorButton);
            new ToolStripChbuttonVCBinder(context, autoSeriesRangeButton, controller.AutoSeriesRangeButton);
            //new SimpleControlVCBinder(context, panel2 ,controller.panel2;
            new LabelButtonVCBinder(context, symPerMiniLabel, controller.SymPerMiniLabel);
            new PanelButtonVCBinder(context, panel2, controller.SymPerMiniLabel);
            new SimpleControlVCBinder(context, panel3, controller.SymPerMiniLabel);
            new ToolStripButtonVCBinder(context, closeChartButton2, controller.CloseChartButton2);
            new ToolStripSplitButtonVCBinder(context, addChartButton, controller.AddChartButton);
            new ToolStripDropDownButtonVCBinder(context, indicatorsDdButton, controller.IndicatorsDdButton);

            new DateTimePickerVCBinder(context, fromTimePicker, controller.FromTimePicker);
            new DateTimePickerVCBinder(context, toTimePicker, controller.ToTimePicker);
        }
    }
}
