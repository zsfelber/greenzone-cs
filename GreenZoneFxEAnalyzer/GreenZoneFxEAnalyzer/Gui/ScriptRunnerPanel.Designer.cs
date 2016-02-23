namespace GreenZoneFxEngine
{
    partial class ScriptRunnerPanel
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableRunScriptScript = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableRunScriptSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableRunScriptPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startStopButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.scriptInfLabel = new System.Windows.Forms.Label();
            this.symbolInfLabel = new System.Windows.Forms.Label();
            this.periodInfLabel = new System.Windows.Forms.Label();
            this.fromInfLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.toInfLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.reservedInf1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopAtButton = new System.Windows.Forms.Button();
            this.snapButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tableRunScriptScript,
            this.tableRunScriptSymbol,
            this.tableRunScriptPeriod});
            this.dataGridView1.Location = new System.Drawing.Point(3, 33);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 19;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(233, 163);
            this.dataGridView1.TabIndex = 81;
            // 
            // tableRunScriptScript
            // 
            this.tableRunScriptScript.FillWeight = 1F;
            this.tableRunScriptScript.HeaderText = "Script";
            this.tableRunScriptScript.MinimumWidth = 20;
            this.tableRunScriptScript.Name = "tableRunScriptScript";
            this.tableRunScriptScript.ReadOnly = true;
            // 
            // tableRunScriptSymbol
            // 
            this.tableRunScriptSymbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tableRunScriptSymbol.FillWeight = 1F;
            this.tableRunScriptSymbol.HeaderText = "Symbol";
            this.tableRunScriptSymbol.MinimumWidth = 65;
            this.tableRunScriptSymbol.Name = "tableRunScriptSymbol";
            this.tableRunScriptSymbol.ReadOnly = true;
            this.tableRunScriptSymbol.Width = 65;
            // 
            // tableRunScriptPeriod
            // 
            this.tableRunScriptPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tableRunScriptPeriod.FillWeight = 1F;
            this.tableRunScriptPeriod.HeaderText = "Period";
            this.tableRunScriptPeriod.MinimumWidth = 50;
            this.tableRunScriptPeriod.Name = "tableRunScriptPeriod";
            this.tableRunScriptPeriod.ReadOnly = true;
            this.tableRunScriptPeriod.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tableRunScriptPeriod.Width = 50;
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
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "Bars : 12 of 112234";
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
            this.label6.Location = new System.Drawing.Point(312, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 73;
            this.label6.Text = "Time Elapsed : 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Run Script -- 1 at a time";
            // 
            // startStopButton
            // 
            this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startStopButton.Location = new System.Drawing.Point(571, 128);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(93, 23);
            this.startStopButton.TabIndex = 68;
            this.startStopButton.Text = "&Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Wingdings", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label3.Location = new System.Drawing.Point(229, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 34);
            this.label3.TabIndex = 59;
            this.label3.Text = "Ü";
            // 
            // propertiesButton
            // 
            this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesButton.Location = new System.Drawing.Point(571, 44);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(93, 23);
            this.propertiesButton.TabIndex = 72;
            this.propertiesButton.Text = "&Properties";
            this.propertiesButton.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(264, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 17);
            this.label11.TabIndex = 86;
            this.label11.Text = "Script:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.SystemColors.Window;
            this.label12.Location = new System.Drawing.Point(264, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 17);
            this.label12.TabIndex = 87;
            this.label12.Text = "Symbol:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.BackColor = System.Drawing.SystemColors.Window;
            this.label13.Location = new System.Drawing.Point(264, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 17);
            this.label13.TabIndex = 88;
            this.label13.Text = "Period:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scriptInfLabel
            // 
            this.scriptInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.scriptInfLabel.Location = new System.Drawing.Point(340, 44);
            this.scriptInfLabel.Name = "scriptInfLabel";
            this.scriptInfLabel.Size = new System.Drawing.Size(218, 17);
            this.scriptInfLabel.TabIndex = 89;
            this.scriptInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // symbolInfLabel
            // 
            this.symbolInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.symbolInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.symbolInfLabel.Location = new System.Drawing.Point(340, 80);
            this.symbolInfLabel.Name = "symbolInfLabel";
            this.symbolInfLabel.Size = new System.Drawing.Size(218, 17);
            this.symbolInfLabel.TabIndex = 90;
            this.symbolInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // periodInfLabel
            // 
            this.periodInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.periodInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.periodInfLabel.Location = new System.Drawing.Point(340, 98);
            this.periodInfLabel.Name = "periodInfLabel";
            this.periodInfLabel.Size = new System.Drawing.Size(218, 17);
            this.periodInfLabel.TabIndex = 91;
            this.periodInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fromInfLabel
            // 
            this.fromInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fromInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.fromInfLabel.Location = new System.Drawing.Point(340, 116);
            this.fromInfLabel.Name = "fromInfLabel";
            this.fromInfLabel.Size = new System.Drawing.Size(218, 17);
            this.fromInfLabel.TabIndex = 93;
            this.fromInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.BackColor = System.Drawing.SystemColors.Window;
            this.label14.Location = new System.Drawing.Point(264, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 17);
            this.label14.TabIndex = 92;
            this.label14.Text = "From:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toInfLabel
            // 
            this.toInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toInfLabel.BackColor = System.Drawing.SystemColors.Window;
            this.toInfLabel.Location = new System.Drawing.Point(340, 134);
            this.toInfLabel.Name = "toInfLabel";
            this.toInfLabel.Size = new System.Drawing.Size(218, 17);
            this.toInfLabel.TabIndex = 95;
            this.toInfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.SystemColors.Window;
            this.label15.Location = new System.Drawing.Point(264, 134);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 17);
            this.label15.TabIndex = 94;
            this.label15.Text = "To:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reservedInf1
            // 
            this.reservedInf1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reservedInf1.BackColor = System.Drawing.SystemColors.Window;
            this.reservedInf1.Location = new System.Drawing.Point(340, 62);
            this.reservedInf1.Name = "reservedInf1";
            this.reservedInf1.Size = new System.Drawing.Size(218, 17);
            this.reservedInf1.TabIndex = 98;
            this.reservedInf1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.BackColor = System.Drawing.SystemColors.Window;
            this.label19.Location = new System.Drawing.Point(264, 62);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 17);
            this.label19.TabIndex = 96;
            this.label19.Text = "Status:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pauseButton.Location = new System.Drawing.Point(263, 6);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(59, 23);
            this.pauseButton.TabIndex = 105;
            this.pauseButton.Text = "&Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            // 
            // stopAtButton
            // 
            this.stopAtButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopAtButton.Location = new System.Drawing.Point(536, 226);
            this.stopAtButton.Name = "stopAtButton";
            this.stopAtButton.Size = new System.Drawing.Size(61, 23);
            this.stopAtButton.TabIndex = 109;
            this.stopAtButton.Text = "Stop at";
            this.stopAtButton.UseVisualStyleBackColor = true;
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
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(9, 203);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(648, 12);
            this.progressBar1.TabIndex = 110;
            // 
            // ScriptRunnerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.stopAtButton);
            this.Controls.Add(this.snapButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.reservedInf1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.toInfLabel);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.fromInfLabel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.periodInfLabel);
            this.Controls.Add(this.symbolInfLabel);
            this.Controls.Add(this.scriptInfLabel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.addLinkLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.label3);
            this.Name = "ScriptRunnerPanel";
            this.Size = new System.Drawing.Size(667, 252);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.LinkLabel addLinkLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button propertiesButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label scriptInfLabel;
        private System.Windows.Forms.Label symbolInfLabel;
        private System.Windows.Forms.Label periodInfLabel;
        private System.Windows.Forms.Label fromInfLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label toInfLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label reservedInf1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableRunScriptScript;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableRunScriptSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableRunScriptPeriod;
        private System.Windows.Forms.Button stopAtButton;
        private System.Windows.Forms.Button snapButton;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
