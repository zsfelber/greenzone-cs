namespace GreenZoneFxEngine.Gui
{
    partial class OrdersToolbar
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
            this.components = new System.ComponentModel.Container();
            this.tsPanel1 = new System.Windows.Forms.ToolStrip();
            this.closeChartButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsPanel2 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.symbolCb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.commentTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.magicCb = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.expertCb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fromDtp = new System.Windows.Forms.DateTimePicker();
            this.toDtp = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.operationCb = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupByCb = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buyCheckBox = new System.Windows.Forms.CheckBox();
            this.sellCheckBox = new System.Windows.Forms.CheckBox();
            this.showFiltersCheckBox = new System.Windows.Forms.CheckBox();
            this.limitCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stopCheckBox = new System.Windows.Forms.CheckBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.tsPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsPanel1
            // 
            this.tsPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsPanel1.AutoSize = false;
            this.tsPanel1.Dock = System.Windows.Forms.DockStyle.None;
            this.tsPanel1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsPanel1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeChartButton2});
            this.tsPanel1.Location = new System.Drawing.Point(0, 0);
            this.tsPanel1.Name = "tsPanel1";
            this.tsPanel1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsPanel1.Size = new System.Drawing.Size(665, 25);
            this.tsPanel1.Stretch = true;
            this.tsPanel1.TabIndex = 71;
            this.tsPanel1.Text = "toolStrip2";
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
            // 
            // tsPanel2
            // 
            this.tsPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsPanel2.AutoSize = false;
            this.tsPanel2.Dock = System.Windows.Forms.DockStyle.None;
            this.tsPanel2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsPanel2.Location = new System.Drawing.Point(0, 30);
            this.tsPanel2.Name = "tsPanel2";
            this.tsPanel2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsPanel2.Size = new System.Drawing.Size(665, 25);
            this.tsPanel2.Stretch = true;
            this.tsPanel2.TabIndex = 102;
            this.tsPanel2.Text = "toolStrip2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.symbolCb);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.commentTb);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.magicCb);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.expertCb);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.fromDtp);
            this.panel1.Controls.Add(this.toDtp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.operationCb);
            this.panel1.Controls.Add(this.tsPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 59);
            this.panel1.TabIndex = 112;
            this.panel1.Visible = false;
            // 
            // symbolCb
            // 
            this.symbolCb.FormattingEnabled = true;
            this.symbolCb.Location = new System.Drawing.Point(64, 4);
            this.symbolCb.Name = "symbolCb";
            this.symbolCb.Size = new System.Drawing.Size(98, 21);
            this.symbolCb.TabIndex = 122;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 121;
            this.label4.Text = "Symbol :";
            // 
            // commentTb
            // 
            this.commentTb.Location = new System.Drawing.Point(64, 30);
            this.commentTb.Name = "commentTb";
            this.commentTb.Size = new System.Drawing.Size(235, 20);
            this.commentTb.TabIndex = 122;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 121;
            this.label9.Text = "Comment :";
            // 
            // magicCb
            // 
            this.magicCb.FormattingEnabled = true;
            this.magicCb.Location = new System.Drawing.Point(355, 29);
            this.magicCb.Name = "magicCb";
            this.magicCb.Size = new System.Drawing.Size(147, 21);
            this.magicCb.TabIndex = 120;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 119;
            this.label6.Text = "Magic :";
            // 
            // expertCb
            // 
            this.expertCb.FormattingEnabled = true;
            this.expertCb.Location = new System.Drawing.Point(355, 2);
            this.expertCb.Name = "expertCb";
            this.expertCb.Size = new System.Drawing.Size(147, 21);
            this.expertCb.TabIndex = 116;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(506, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 90;
            this.label2.Text = "To :";
            // 
            // fromDtp
            // 
            this.fromDtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDtp.Location = new System.Drawing.Point(549, 2);
            this.fromDtp.Name = "fromDtp";
            this.fromDtp.Size = new System.Drawing.Size(87, 20);
            this.fromDtp.TabIndex = 87;
            // 
            // toDtp
            // 
            this.toDtp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDtp.Location = new System.Drawing.Point(549, 28);
            this.toDtp.Name = "toDtp";
            this.toDtp.Size = new System.Drawing.Size(87, 20);
            this.toDtp.TabIndex = 88;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "From :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 113;
            this.label7.Text = "Op :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 112;
            this.label5.Text = "Expert :";
            // 
            // operationCb
            // 
            this.operationCb.FormattingEnabled = true;
            this.operationCb.Location = new System.Drawing.Point(201, 4);
            this.operationCb.Name = "operationCb";
            this.operationCb.Size = new System.Drawing.Size(98, 21);
            this.operationCb.TabIndex = 117;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupByCb);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.buyCheckBox);
            this.panel2.Controls.Add(this.sellCheckBox);
            this.panel2.Controls.Add(this.showFiltersCheckBox);
            this.panel2.Controls.Add(this.limitCheckBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.stopCheckBox);
            this.panel2.Controls.Add(this.resetButton);
            this.panel2.Controls.Add(this.tsPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(665, 28);
            this.panel2.TabIndex = 113;
            // 
            // groupByCb
            // 
            this.groupByCb.FormattingEnabled = true;
            this.groupByCb.Items.AddRange(new object[] {
            "",
            "Symbol",
            "Expert",
            "Magic"});
            this.groupByCb.Location = new System.Drawing.Point(355, 2);
            this.groupByCb.Name = "groupByCb";
            this.groupByCb.Size = new System.Drawing.Size(104, 21);
            this.groupByCb.TabIndex = 120;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(293, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 119;
            this.label8.Text = "Group by :";
            // 
            // buyCheckBox
            // 
            this.buyCheckBox.AutoSize = true;
            this.buyCheckBox.Location = new System.Drawing.Point(46, 5);
            this.buyCheckBox.Name = "buyCheckBox";
            this.buyCheckBox.Size = new System.Drawing.Size(44, 17);
            this.buyCheckBox.TabIndex = 83;
            this.buyCheckBox.Text = "Buy";
            this.buyCheckBox.UseVisualStyleBackColor = true;
            // 
            // sellCheckBox
            // 
            this.sellCheckBox.AutoSize = true;
            this.sellCheckBox.Location = new System.Drawing.Point(96, 5);
            this.sellCheckBox.Name = "sellCheckBox";
            this.sellCheckBox.Size = new System.Drawing.Size(43, 17);
            this.sellCheckBox.TabIndex = 84;
            this.sellCheckBox.Text = "Sell";
            this.sellCheckBox.UseVisualStyleBackColor = true;
            // 
            // showFiltersCb
            // 
            this.showFiltersCheckBox.AutoSize = true;
            this.showFiltersCheckBox.Location = new System.Drawing.Point(490, 5);
            this.showFiltersCheckBox.Name = "showFiltersCb";
            this.showFiltersCheckBox.Size = new System.Drawing.Size(50, 17);
            this.showFiltersCheckBox.TabIndex = 93;
            this.showFiltersCheckBox.Text = "More";
            this.showFiltersCheckBox.UseVisualStyleBackColor = true;
            // 
            // limitCheckBox
            // 
            this.limitCheckBox.AutoSize = true;
            this.limitCheckBox.Location = new System.Drawing.Point(160, 5);
            this.limitCheckBox.Name = "limitCheckBox";
            this.limitCheckBox.Size = new System.Drawing.Size(47, 17);
            this.limitCheckBox.TabIndex = 85;
            this.limitCheckBox.Text = "Limit";
            this.limitCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Show :";
            // 
            // stopCheckBox
            // 
            this.stopCheckBox.AutoSize = true;
            this.stopCheckBox.Location = new System.Drawing.Point(213, 5);
            this.stopCheckBox.Name = "stopCheckBox";
            this.stopCheckBox.Size = new System.Drawing.Size(48, 17);
            this.stopCheckBox.TabIndex = 86;
            this.stopCheckBox.Text = "Stop";
            this.stopCheckBox.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(549, 1);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(77, 23);
            this.resetButton.TabIndex = 91;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // OrdersToolbar
            // 
            this.AutoSize = true;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OrdersToolbar";
            this.Size = new System.Drawing.Size(665, 87);
            this.tsPanel1.ResumeLayout(false);
            this.tsPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsPanel1;
        internal System.Windows.Forms.ToolStripButton closeChartButton2;
        private System.Windows.Forms.ToolStrip tsPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox expertCb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox operationCb;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox buyCheckBox;
        private System.Windows.Forms.CheckBox sellCheckBox;
        private System.Windows.Forms.CheckBox showFiltersCheckBox;
        private System.Windows.Forms.CheckBox limitCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox stopCheckBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.DateTimePicker fromDtp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker toDtp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox magicCb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox commentTb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox groupByCb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox symbolCb;
        private System.Windows.Forms.Label label4;
    }
}
