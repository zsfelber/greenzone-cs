using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Properties;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.Gui;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine
{
    public partial class EATesterPanel : UserControl
    {
        private DataGridViewColumn SelectEAColumn;
        private DataGridViewColumn tableTestEasEACol;
        private DataGridViewTextBoxColumn tableTestEasPeriodCol;
        private DataGridViewImageColumn IconColumn;

        public EATesterPanel()
        {
            InitializeComponent();

            this.SelectEAColumn = new DataGridViewColumn(new CheckBoxCell0());
            this.tableTestEasEACol = new DataGridViewColumn(new TreeNodeWithTextCell());
            this.tableTestEasPeriodCol = new DataGridViewTextBoxColumn();
            this.IconColumn = new DataGridViewImageColumn();

            // 
            // SelectEAColumn
            // 
            this.SelectEAColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.SelectEAColumn.FillWeight = 1F;
            this.SelectEAColumn.Frozen = true;
            this.SelectEAColumn.HeaderText = "";
            this.SelectEAColumn.MinimumWidth = 20;
            this.SelectEAColumn.Name = "SelectEAColumn";
            this.SelectEAColumn.DataPropertyName = "Selected";
            this.SelectEAColumn.ReadOnly = true;
            this.SelectEAColumn.Resizable = DataGridViewTriState.False;
            this.SelectEAColumn.Width = 20;
            // 
            // tableTestEasEA
            // 
            this.tableTestEasEACol.FillWeight = 1F;
            this.tableTestEasEACol.HeaderText = "EA";
            this.tableTestEasEACol.MinimumWidth = 20;
            this.tableTestEasEACol.Name = "tableTestEasEA";
            this.tableTestEasEACol.DataPropertyName = "Title";
            this.tableTestEasEACol.ReadOnly = true;
            // 
            // tableTestEasPeriod
            // 
            this.tableTestEasPeriodCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.tableTestEasPeriodCol.FillWeight = 1F;
            this.tableTestEasPeriodCol.HeaderText = "Period";
            this.tableTestEasPeriodCol.MinimumWidth = 50;
            this.tableTestEasPeriodCol.Name = "tableTestEasPeriod";
            this.tableTestEasPeriodCol.DataPropertyName = "Period";
            this.tableTestEasPeriodCol.ReadOnly = true;
            this.tableTestEasPeriodCol.Resizable = DataGridViewTriState.False;
            this.tableTestEasPeriodCol.Width = 50;
            // 
            // IconColumn
            // 
            this.IconColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.IconColumn.FillWeight = 1F;
            this.IconColumn.HeaderText = "";
            this.IconColumn.MinimumWidth = 14;
            this.IconColumn.Name = "IconColumn";
            this.IconColumn.DataPropertyName = "Icon";
            this.IconColumn.ReadOnly = true;
            this.IconColumn.Width = 14;

            this.dataGridView1.Columns.Add(this.SelectEAColumn);
            this.dataGridView1.Columns.Add(this.tableTestEasEACol);
            this.dataGridView1.Columns.Add(this.tableTestEasPeriodCol);
            this.dataGridView1.Columns.Add(this.IconColumn);

        }

        public void Bind(GreenWinFormsMVContext context, EATesterController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            //new LabelVCBinder1(context, label1, controller.label1);
            new TrackBarVCBinder(context, speedTrackBar, controller.SpeedTrackBar);
            new DataGridViewVCBinder(context, dataGridView1, controller.DataGridView1);
            new LabelVCBinder1(context, addLinkLabel, controller.AddLinkLabel);
            new LabelVCBinder1(context, methodLabel , controller.MethodLabel);
            //new LabelVCBinder1(context, label8 , controller.label8);
            //new LabelVCBinder1(context, label7 , controller.label7);
            //new LabelVCBinder1(context, label6 , controller.label6);
            new ComboBoxVCBinder(context, methodCombo , controller.MethodCombo);
            //new LabelVCBinder1(context, label2 , controller.label2);
            new ButtonVCBinder(context, startStopButton , controller.StartStopButton);
            new LabelVCBinder1(context, methodInfLabel , controller.MethodInfLabel);
            //new LabelVCBinder1(context, label3 , controller.label3);
            new ButtonVCBinder(context, propertiesButton , controller.PropertiesButton);
            //new LabelVCBinder1(context, label11 , controller.label11);
            new LabelVCBinder1(context, dataPeriodLabel , controller.DataPeriodLabel);
            new LabelVCBinder1(context, eaInfLabel , controller.EaInfLabel);
            new LabelVCBinder1(context, symbolInfLabel , controller.SymbolInfLabel);
            new ComboBoxVCBinder(context, dataPeriodCombo , controller.DataPeriodCombo);
            new LabelVCBinder1(context, fromInfLabel , controller.FromInfLabel);
            //new LabelVCBinder1(context, label14 , controller.label14);
            new LabelVCBinder1(context, toInfLabel , controller.ToInfLabel);
            //new LabelVCBinder1(context, label15 , controller.label15);
            new ButtonVCBinder(context, pauseButton , controller.PauseButton);
            new ProgressTrackBarVCBinder(context, progressTrackBar1 , controller.ProgressTrackBar1);
            new ButtonVCBinder(context, pauseAtButton , controller.PauseAtButton);
            new ButtonVCBinder(context, snapButton , controller.SnapButton);
            new CheckBoxVCBinder(context, scrollAcrossTabsCb , controller.ScrollAcrossTabsCb);
            new CheckBoxVCBinder(context, skipEmptyPeriodsCb , controller.SkipEmptyPeriodsCb);
            new CheckBoxVCBinder(context, updateSpreadTickCb, controller.UpdateSpreadTickCb);

            new DataGridColumnVCBinder(context, SelectEAColumn, controller.SelectEAColumn);
            new DataGridColumnVCBinder(context, tableTestEasEACol, controller.TableTestEasEACol);
            new DataGridColumnVCBinder(context, tableTestEasPeriodCol, controller.TableTestEasPeriodCol);
            new DataGridColumnVCBinder(context, IconColumn, controller.IconColumn);

            new ToolTipVCBinder(context, toolTip1, controller.ToolTip1);
        }

        class TreeNodeWithTextCell : DataGridViewTextBoxCell
        {
            Brush brush;
            Pen bgPen;
            Pen selPen;
            Pen gridPen;

            public TreeNodeWithTextCell()
                : base()
            {
            }

            protected override void Paint(
                Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates cellState,
                Object value,
                Object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts baseParts
            )
            {
                if (brush == null)
                {
                    brush = new SolidBrush(cellStyle.ForeColor);
                    bgPen = new Pen(cellStyle.BackColor);
                    selPen = new Pen(cellStyle.SelectionBackColor);
                    gridPen = new Pen(DataGridView.GridColor);
                }

                EATesterPanel eaPanel = (EATesterPanel)DataGridView.Parent;
                GridRowController gridRow = (GridRowController)DataGridView.Rows[rowIndex].Tag;
                EATesterController.TableRow row = (EATesterController.TableRow)gridRow.DataBoundObject;

                DataGridViewPaintParts parts = DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.Focus;
                //DataGridViewPaintParts parts = DataGridViewPaintParts.All;

                if (DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.None)
                {
                    Pen pen1 = row.isLast ? gridPen : cellState == DataGridViewElementStates.Selected ? selPen : bgPen;
                    Pen pen2 = gridPen;
                    graphics.DrawLine(pen1, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right - 1, cellBounds.Bottom - 1);
                    graphics.DrawLine(pen2, cellBounds.Right - 1, 0, cellBounds.Right - 1, cellBounds.Bottom - 1);
                }

                if (row.ChartView == null)
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);
                }
                else if (!row.isLast)
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "     " + formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);

                    graphics.DrawImage(Resources.treenode_mid_20, cellBounds.X + 5, cellBounds.Y, 11, 20);
                }
                else
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "     " + formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);

                    graphics.DrawImage(Resources.treenode_bot_20, cellBounds.X + 5, cellBounds.Y, 11, 20);
                }
            }
        }

        class CheckBoxCell0 : DataGridViewCheckBoxCell
        {
            public CheckBoxCell0()
                : base()
            {
            }
            protected override void Paint(
                Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates cellState,
                Object value,
                Object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts baseParts
            )
            {
                EATesterPanel eaPanel = (EATesterPanel)DataGridView.Parent;
                GridRowController gridRow = (GridRowController)DataGridView.Rows[rowIndex].Tag;
                EATesterController.TableRow row = (EATesterController.TableRow)gridRow.DataBoundObject;
                DataGridViewPaintParts parts;
                if (DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.None)
                {
                    parts = DataGridViewPaintParts.Border;
                }
                else
                {
                    parts = 0;
                }

                if (row.ChartView != null)
                {
                    parts |= DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.Focus;
                }
                else
                {
                    parts |= DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.SelectionBackground;
                }

                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);
            }
        }
    }

}
