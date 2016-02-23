namespace GreenZoneFxEngine
{
    partial class ChartPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartPanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toggleTopBarButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.symbolDdButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.periodDdButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.chartTypeDdButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.closeChartButton1 = new System.Windows.Forms.ToolStripButton();
            this.autoSeriesRangeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.addChartButton = new System.Windows.Forms.ToolStripSplitButton();
            this.connectCursorButton = new System.Windows.Forms.ToolStripButton();
            this.indicatorsDdButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton5 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton6 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.ToolStrip();
            this.closeChartButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.symPerMiniLabel = new System.Windows.Forms.Label();
            this.toggleTopBarButton2 = new System.Windows.Forms.Button();
            this.chart1 = new GreenZoneFxEngine.Gui.Chart.NormalChart();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleTopBarButton1,
            this.toolStripLabel5,
            this.symbolDdButton,
            this.periodDdButton,
            this.chartTypeDdButton,
            this.toolStripSeparator1,
            this.toolStripLabel7,
            this.toolStripSeparator4,
            this.closeChartButton1,
            this.autoSeriesRangeButton,
            this.toolStripLabel8,
            this.toolStripLabel1,
            this.addChartButton,
            this.connectCursorButton,
            this.indicatorsDdButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(605, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.EndDrag += new System.EventHandler(this.toolStrip1_EndDrag);
            // 
            // toggleTopBarButton1
            // 
            this.toggleTopBarButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleTopBarButton1.Image = global::GreenZoneFxEngine.Properties.Resources.toggle_16xLG;
            this.toggleTopBarButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleTopBarButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleTopBarButton1.Name = "toggleTopBarButton1";
            this.toggleTopBarButton1.Size = new System.Drawing.Size(23, 22);
            this.toggleTopBarButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toggleTopBarButton1.Click += new System.EventHandler(this.toggleTopBarButton1_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(19, 22);
            this.toolStripLabel5.Text = "    ";
            // 
            // symbolDdButton
            // 
            this.symbolDdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.symbolDdButton.Name = "symbolDdButton";
            this.symbolDdButton.Size = new System.Drawing.Size(60, 22);
            this.symbolDdButton.Text = "EURUSD";
            // 
            // periodDdButton
            // 
            this.periodDdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.periodDdButton.Name = "periodDdButton";
            this.periodDdButton.Size = new System.Drawing.Size(34, 22);
            this.periodDdButton.Text = "M1";
            // 
            // chartTypeDdButton
            // 
            this.chartTypeDdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chartTypeDdButton.Name = "chartTypeDdButton";
            this.chartTypeDdButton.Size = new System.Drawing.Size(53, 22);
            this.chartTypeDdButton.Text = "Candle";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel7.Text = "               ";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // closeChartButton1
            // 
            this.closeChartButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeChartButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeChartButton1.Image = global::GreenZoneFxEngine.Properties.Resources.Close_16xLG;
            this.closeChartButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.closeChartButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeChartButton1.Name = "closeChartButton1";
            this.closeChartButton1.Size = new System.Drawing.Size(23, 22);
            this.closeChartButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.closeChartButton1.Click += new System.EventHandler(this.closeChartButton1_Click);
            // 
            // autoSeriesRangeButton
            // 
            this.autoSeriesRangeButton.Checked = true;
            this.autoSeriesRangeButton.CheckOnClick = true;
            this.autoSeriesRangeButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSeriesRangeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.autoSeriesRangeButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomToFit;
            this.autoSeriesRangeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.autoSeriesRangeButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.autoSeriesRangeButton.Name = "autoSeriesRangeButton";
            this.autoSeriesRangeButton.Size = new System.Drawing.Size(23, 22);
            this.autoSeriesRangeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.autoSeriesRangeButton.Click += new System.EventHandler(this.autoSeriesRangeButton_Click);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel8.Text = "   ";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel1.Text = "     ";
            // 
            // addChartButton
            // 
            this.addChartButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.addChartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addChartButton.Image = global::GreenZoneFxEngine.Properties.Resources.action_add_16xMD;
            this.addChartButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addChartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addChartButton.Name = "addChartButton";
            this.addChartButton.Size = new System.Drawing.Size(32, 22);
            this.addChartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.addChartButton.ButtonClick += new System.EventHandler(this.addChartButton_ButtonClick);
            // 
            // connectCursorButton
            // 
            this.connectCursorButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.connectCursorButton.Checked = true;
            this.connectCursorButton.CheckOnClick = true;
            this.connectCursorButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.connectCursorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectCursorButton.Image = global::GreenZoneFxEngine.Properties.Resources.CursorBar_16xLG;
            this.connectCursorButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.connectCursorButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.connectCursorButton.Name = "connectCursorButton";
            this.connectCursorButton.Size = new System.Drawing.Size(23, 22);
            this.connectCursorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.connectCursorButton.Click += new System.EventHandler(this.connectCursorButton_Click);
            // 
            // indicatorsDdButton
            // 
            this.indicatorsDdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.indicatorsDdButton.Image = ((System.Drawing.Image)(resources.GetObject("indicatorsDdButton.Image")));
            this.indicatorsDdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.indicatorsDdButton.Name = "indicatorsDdButton";
            this.indicatorsDdButton.Size = new System.Drawing.Size(29, 22);
            this.indicatorsDdButton.Text = "toolStripDropDownButton7";
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripDropDownButton5
            // 
            this.toolStripDropDownButton5.Name = "toolStripDropDownButton5";
            this.toolStripDropDownButton5.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripDropDownButton6
            // 
            this.toolStripDropDownButton6.Name = "toolStripDropDownButton6";
            this.toolStripDropDownButton6.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(605, 351);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(605, 376);
            this.toolStripContainer1.TabIndex = 12;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 351);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSize = false;
            this.panel3.Dock = System.Windows.Forms.DockStyle.None;
            this.panel3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.panel3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeChartButton2});
            this.panel3.Location = new System.Drawing.Point(557, 0);
            this.panel3.Name = "panel3";
            this.panel3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.panel3.Size = new System.Drawing.Size(48, 25);
            this.panel3.TabIndex = 10;
            this.panel3.Text = "toolStrip2";
            this.panel3.Visible = false;
            // 
            // closeChartButton2
            // 
            this.closeChartButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeChartButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeChartButton2.Image = global::GreenZoneFxEngine.Properties.Resources.Close_16xLG;
            this.closeChartButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.closeChartButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeChartButton2.Name = "closeChartButton2";
            this.closeChartButton2.Size = new System.Drawing.Size(23, 22);
            this.closeChartButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.closeChartButton2.Click += new System.EventHandler(this.closeChartButton2_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.symPerMiniLabel);
            this.panel2.Controls.Add(this.toggleTopBarButton2);
            this.panel2.Location = new System.Drawing.Point(-1, -1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel2.Size = new System.Drawing.Size(103, 25);
            this.panel2.TabIndex = 8;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // symPerMiniLabel
            // 
            this.symPerMiniLabel.AutoSize = true;
            this.symPerMiniLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.symPerMiniLabel.Location = new System.Drawing.Point(29, 5);
            this.symPerMiniLabel.Name = "symPerMiniLabel";
            this.symPerMiniLabel.Size = new System.Drawing.Size(35, 14);
            this.symPerMiniLabel.TabIndex = 7;
            this.symPerMiniLabel.Text = "label1";
            this.symPerMiniLabel.Click += new System.EventHandler(this.symPerMiniLabel_Click);
            // 
            // toggleTopBarButton2
            // 
            this.toggleTopBarButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.toggleTopBarButton2.Image = global::GreenZoneFxEngine.Properties.Resources.toggle_16xLG;
            this.toggleTopBarButton2.Location = new System.Drawing.Point(2, 2);
            this.toggleTopBarButton2.Margin = new System.Windows.Forms.Padding(1);
            this.toggleTopBarButton2.Name = "toggleTopBarButton2";
            this.toggleTopBarButton2.Size = new System.Drawing.Size(23, 19);
            this.toggleTopBarButton2.TabIndex = 6;
            this.toggleTopBarButton2.UseVisualStyleBackColor = true;
            this.toggleTopBarButton2.Click += new System.EventHandler(this.toggleTopBarButton2_Click);
            // 
            // chart1
            // 
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(605, 351);
            this.chart1.TabIndex = 5;
            // 
            // ChartPanel
            // 
            this.Controls.Add(this.toolStripContainer1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ChartPanel";
            this.Size = new System.Drawing.Size(605, 376);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton symbolDdButton;
        private System.Windows.Forms.ToolStripDropDownButton periodDdButton;
        private System.Windows.Forms.ToolStripDropDownButton chartTypeDdButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton6;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toggleTopBarButton1;
        private System.Windows.Forms.ToolStripButton closeChartButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Button toggleTopBarButton2;
        private Gui.Chart.NormalChart chart1;
        private System.Windows.Forms.ToolStripButton connectCursorButton;
        private System.Windows.Forms.ToolStripButton autoSeriesRangeButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label symPerMiniLabel;
        private System.Windows.Forms.ToolStrip panel3;
        private System.Windows.Forms.ToolStripButton closeChartButton2;
        private System.Windows.Forms.ToolStripSplitButton addChartButton;
        private System.Windows.Forms.ToolStripDropDownButton indicatorsDdButton;


    }
}
