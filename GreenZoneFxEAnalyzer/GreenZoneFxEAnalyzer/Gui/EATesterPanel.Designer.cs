using GreenZoneUtil.Gui;
namespace GreenZoneFxEngine
{
    partial class EATesterPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.speedTrackBar = new System.Windows.Forms.TrackBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.methodCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startStopButton = new System.Windows.Forms.Button();
            this.methodInfLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.eaInfLabel = new System.Windows.Forms.Label();
            this.symbolInfLabel = new System.Windows.Forms.Label();
            this.dataPeriodCombo = new System.Windows.Forms.ComboBox();
            this.fromInfLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.toInfLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.pauseAtButton = new System.Windows.Forms.Button();
            this.snapButton = new System.Windows.Forms.Button();
            this.scrollAcrossTabsCb = new System.Windows.Forms.CheckBox();
            this.skipEmptyPeriodsCb = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.updateSpreadTickCb = new System.Windows.Forms.CheckBox();
            this.progressTrackBar1 = new GreenZoneUtil.Gui.ProgressTrackBar();
            this.dataPeriodLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.methodLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "&Speed:";
            // 
            // speedTrackBar
            // 
            this.speedTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedTrackBar.AutoSize = false;
            this.speedTrackBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.speedTrackBar.Enabled = false;
            this.speedTrackBar.Location = new System.Drawing.Point(375, 6);
            this.speedTrackBar.Name = "speedTrackBar";
            this.speedTrackBar.Size = new System.Drawing.Size(295, 22);
            this.speedTrackBar.TabIndex = 82;
            this.speedTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 33);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 18;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(233, 163);
            this.dataGridView1.TabIndex = 81;
            // 
            // addLinkLabel
            // 
            this.addLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addLinkLabel.AutoSize = true;
            this.addLinkLabel.Location = new System.Drawing.Point(210, 11);
            this.addLinkLabel.Name = "addLinkLabel";
            this.addLinkLabel.Size = new System.Drawing.Size(26, 13);
            this.addLinkLabel.TabIndex = 80;
            this.addLinkLabel.TabStop = true;
            this.addLinkLabel.Text = "Add";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(193, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "Trade : 12 of 234";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Date :  1/ 1/1970  12:21am";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(314, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 73;
            this.label6.Text = "Balance : 0";
            // 
            // methodCombo
            // 
            this.methodCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.methodCombo.Enabled = false;
            this.methodCombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.methodCombo.FormattingEnabled = true;
            this.methodCombo.ItemHeight = 13;
            this.methodCombo.Location = new System.Drawing.Point(346, 71);
            this.methodCombo.Margin = new System.Windows.Forms.Padding(0);
            this.methodCombo.Name = "methodCombo";
            this.methodCombo.Size = new System.Drawing.Size(213, 21);
            this.methodCombo.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Test EAs -- 1 or more";
            // 
            // startStopButton
            // 
            this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startStopButton.Enabled = false;
            this.startStopButton.Location = new System.Drawing.Point(571, 173);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(93, 23);
            this.startStopButton.TabIndex = 68;
            this.startStopButton.Text = "&Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            // 
            // methodInfLabel
            // 
            this.methodInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.methodInfLabel.AutoEllipsis = true;
            this.methodInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.methodInfLabel.Location = new System.Drawing.Point(265, 93);
            this.methodInfLabel.Name = "methodInfLabel";
            this.methodInfLabel.Size = new System.Drawing.Size(294, 27);
            this.methodInfLabel.TabIndex = 85;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Wingdings", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label3.Location = new System.Drawing.Point(229, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 34);
            this.label3.TabIndex = 59;
            this.label3.Text = "Ü";
            // 
            // propertiesButton
            // 
            this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesButton.Enabled = false;
            this.propertiesButton.Location = new System.Drawing.Point(571, 71);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(93, 23);
            this.propertiesButton.TabIndex = 72;
            this.propertiesButton.Text = "&Properties";
            this.propertiesButton.UseVisualStyleBackColor = true;
            // 
            // eaInfLabel
            // 
            this.eaInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eaInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.eaInfLabel.Location = new System.Drawing.Point(346, 143);
            this.eaInfLabel.Name = "eaInfLabel";
            this.eaInfLabel.Size = new System.Drawing.Size(149, 17);
            this.eaInfLabel.TabIndex = 89;
            this.eaInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // symbolInfLabel
            // 
            this.symbolInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.symbolInfLabel.Location = new System.Drawing.Point(496, 143);
            this.symbolInfLabel.Name = "symbolInfLabel";
            this.symbolInfLabel.Size = new System.Drawing.Size(63, 17);
            this.symbolInfLabel.TabIndex = 90;
            this.symbolInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataPeriodCombo
            // 
            this.dataPeriodCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPeriodCombo.Enabled = false;
            this.dataPeriodCombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dataPeriodCombo.Location = new System.Drawing.Point(346, 121);
            this.dataPeriodCombo.Margin = new System.Windows.Forms.Padding(0);
            this.dataPeriodCombo.Name = "dataPeriodCombo";
            this.dataPeriodCombo.Size = new System.Drawing.Size(213, 21);
            this.dataPeriodCombo.TabIndex = 91;
            this.toolTip1.SetToolTip(this.dataPeriodCombo, "Data modelling period used for generating test (based also on Method)");
            // 
            // fromInfLabel
            // 
            this.fromInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fromInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.fromInfLabel.Location = new System.Drawing.Point(346, 161);
            this.fromInfLabel.Name = "fromInfLabel";
            this.fromInfLabel.Size = new System.Drawing.Size(213, 17);
            this.fromInfLabel.TabIndex = 93;
            this.fromInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.BackColor = System.Drawing.SystemColors.Window;
            this.label14.Location = new System.Drawing.Point(265, 161);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 17);
            this.label14.TabIndex = 92;
            this.label14.Text = "From:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toInfLabel
            // 
            this.toInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.toInfLabel.Location = new System.Drawing.Point(346, 179);
            this.toInfLabel.Name = "toInfLabel";
            this.toInfLabel.Size = new System.Drawing.Size(213, 17);
            this.toInfLabel.TabIndex = 95;
            this.toInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.SystemColors.Window;
            this.label15.Location = new System.Drawing.Point(265, 179);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 17);
            this.label15.TabIndex = 94;
            this.label15.Text = "To:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(263, 6);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(59, 23);
            this.pauseButton.TabIndex = 106;
            this.pauseButton.Text = "&Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            // 
            // pauseAtButton
            // 
            this.pauseAtButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseAtButton.Location = new System.Drawing.Point(536, 226);
            this.pauseAtButton.Name = "pauseAtButton";
            this.pauseAtButton.Size = new System.Drawing.Size(61, 23);
            this.pauseAtButton.TabIndex = 109;
            this.pauseAtButton.Text = "Pause at";
            this.pauseAtButton.UseVisualStyleBackColor = true;
            // 
            // snapButton
            // 
            this.snapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.snapButton.Location = new System.Drawing.Point(603, 226);
            this.snapButton.Name = "snapButton";
            this.snapButton.Size = new System.Drawing.Size(61, 23);
            this.snapButton.TabIndex = 108;
            this.snapButton.Text = "Snap";
            this.snapButton.UseVisualStyleBackColor = true;
            // 
            // scrollAcrossTabsCb
            // 
            this.scrollAcrossTabsCb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollAcrossTabsCb.AutoSize = true;
            this.scrollAcrossTabsCb.Enabled = false;
            this.scrollAcrossTabsCb.Location = new System.Drawing.Point(431, 40);
            this.scrollAcrossTabsCb.Name = "scrollAcrossTabsCb";
            this.scrollAcrossTabsCb.Size = new System.Drawing.Size(118, 17);
            this.scrollAcrossTabsCb.TabIndex = 112;
            this.scrollAcrossTabsCb.Text = "Scroll across charts";
            this.scrollAcrossTabsCb.UseVisualStyleBackColor = true;
            // 
            // skipEmptyPeriodsCb
            // 
            this.skipEmptyPeriodsCb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skipEmptyPeriodsCb.AutoSize = true;
            this.skipEmptyPeriodsCb.Checked = true;
            this.skipEmptyPeriodsCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skipEmptyPeriodsCb.Enabled = false;
            this.skipEmptyPeriodsCb.Location = new System.Drawing.Point(555, 40);
            this.skipEmptyPeriodsCb.Name = "skipEmptyPeriodsCb";
            this.skipEmptyPeriodsCb.Size = new System.Drawing.Size(115, 17);
            this.skipEmptyPeriodsCb.TabIndex = 113;
            this.skipEmptyPeriodsCb.Text = "Skip empty periods";
            this.skipEmptyPeriodsCb.UseVisualStyleBackColor = true;
            // 
            // updateSpreadTickCb
            // 
            this.updateSpreadTickCb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateSpreadTickCb.AutoSize = true;
            this.updateSpreadTickCb.Enabled = false;
            this.updateSpreadTickCb.Location = new System.Drawing.Point(263, 40);
            this.updateSpreadTickCb.Name = "updateSpreadTickCb";
            this.updateSpreadTickCb.Size = new System.Drawing.Size(163, 17);
            this.updateSpreadTickCb.TabIndex = 114;
            this.updateSpreadTickCb.Text = "Update spread from tick data";
            this.updateSpreadTickCb.UseVisualStyleBackColor = true;
            // 
            // progressTrackBar1
            // 
            this.progressTrackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressTrackBar1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.progressTrackBar1.BrushColor = System.Drawing.Color.Gray;
            this.progressTrackBar1.DrawSegments = false;
            this.progressTrackBar1.DrawTicks = false;
            this.progressTrackBar1.Enabled = false;
            this.progressTrackBar1.ForeColor = System.Drawing.Color.Black;
            this.progressTrackBar1.Location = new System.Drawing.Point(3, 202);
            this.progressTrackBar1.MaxProgress = 100;
            this.progressTrackBar1.MaxValue = 100;
            this.progressTrackBar1.Name = "progressTrackBar1";
            this.progressTrackBar1.ProgressBarHeight = 0;
            this.progressTrackBar1.ProgressValue = 0;
            this.progressTrackBar1.SegmentSize = 5;
            this.progressTrackBar1.Size = new System.Drawing.Size(661, 23);
            this.progressTrackBar1.TabIndex = 107;
            this.progressTrackBar1.TickPosition = 0;
            // 
            // dataPeriodLabel
            // 
            this.dataPeriodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPeriodLabel.BackColor = System.Drawing.SystemColors.Window;
            this.dataPeriodLabel.Image = global::GreenZoneFxEngine.Properties.Resources.Error_red_12x11;
            this.dataPeriodLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dataPeriodLabel.Location = new System.Drawing.Point(265, 121);
            this.dataPeriodLabel.Name = "dataPeriodLabel";
            this.dataPeriodLabel.Size = new System.Drawing.Size(80, 21);
            this.dataPeriodLabel.TabIndex = 88;
            this.dataPeriodLabel.Text = "Data period:";
            this.dataPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(265, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 17);
            this.label11.TabIndex = 86;
            this.label11.Text = "EA+symbol:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // methodLabel
            // 
            this.methodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.methodLabel.BackColor = System.Drawing.SystemColors.Window;
            this.methodLabel.Image = global::GreenZoneFxEngine.Properties.Resources.Warning_yellow_7231_12x11;
            this.methodLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.methodLabel.Location = new System.Drawing.Point(265, 71);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(80, 21);
            this.methodLabel.TabIndex = 78;
            this.methodLabel.Text = "Method:";
            this.methodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EATesterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.updateSpreadTickCb);
            this.Controls.Add(this.skipEmptyPeriodsCb);
            this.Controls.Add(this.scrollAcrossTabsCb);
            this.Controls.Add(this.pauseAtButton);
            this.Controls.Add(this.snapButton);
            this.Controls.Add(this.progressTrackBar1);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.toInfLabel);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.fromInfLabel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dataPeriodCombo);
            this.Controls.Add(this.symbolInfLabel);
            this.Controls.Add(this.eaInfLabel);
            this.Controls.Add(this.dataPeriodLabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.methodInfLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.speedTrackBar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.addLinkLabel);
            this.Controls.Add(this.methodLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.methodCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.label3);
            this.Name = "EATesterPanel";
            this.Size = new System.Drawing.Size(667, 252);
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar speedTrackBar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.LinkLabel addLinkLabel;
        private System.Windows.Forms.Label methodLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox methodCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Label methodInfLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button propertiesButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label dataPeriodLabel;
        private System.Windows.Forms.Label eaInfLabel;
        private System.Windows.Forms.Label symbolInfLabel;
        private System.Windows.Forms.ComboBox dataPeriodCombo;
        private System.Windows.Forms.Label fromInfLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label toInfLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button pauseButton;
        private ProgressTrackBar progressTrackBar1;
        private System.Windows.Forms.Button pauseAtButton;
        private System.Windows.Forms.Button snapButton;
        private System.Windows.Forms.CheckBox scrollAcrossTabsCb;
        private System.Windows.Forms.CheckBox skipEmptyPeriodsCb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox updateSpreadTickCb;
    }
}
