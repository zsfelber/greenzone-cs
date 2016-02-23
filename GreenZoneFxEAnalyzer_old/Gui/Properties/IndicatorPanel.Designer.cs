namespace GreenZoneFxEngine.Gui
{
    partial class IndicatorPanel
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.reset1Button = new System.Windows.Forms.Button();
            this.indicatorRuntimePanel = new GreenZoneFxEngine.IndicatorPropertiesPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reset2Button = new System.Windows.Forms.Button();
            this.indexesPrgrd = new System.Windows.Forms.PropertyGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reset3Button = new System.Windows.Forms.Button();
            this.levelsPrgrd = new System.Windows.Forms.PropertyGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 339);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.reset1Button);
            this.tabPage1.Controls.Add(this.indicatorRuntimePanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(426, 313);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Indicator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // reset1Button
            // 
            this.reset1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reset1Button.Location = new System.Drawing.Point(6, 284);
            this.reset1Button.Name = "reset1Button";
            this.reset1Button.Size = new System.Drawing.Size(75, 23);
            this.reset1Button.TabIndex = 13;
            this.reset1Button.Text = "Reset";
            this.reset1Button.UseVisualStyleBackColor = true;
            this.reset1Button.Click += new System.EventHandler(this.reset1Button_Click);
            // 
            // indicatorRuntimePanel
            // 
            this.indicatorRuntimePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.indicatorRuntimePanel.Location = new System.Drawing.Point(0, 3);
            this.indicatorRuntimePanel.Name = "indicatorRuntimePanel";
            this.indicatorRuntimePanel.Size = new System.Drawing.Size(426, 275);
            this.indicatorRuntimePanel.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.reset2Button);
            this.tabPage2.Controls.Add(this.indexesPrgrd);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(426, 313);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Buffer styles";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // reset2Button
            // 
            this.reset2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reset2Button.Location = new System.Drawing.Point(6, 284);
            this.reset2Button.Name = "reset2Button";
            this.reset2Button.Size = new System.Drawing.Size(75, 23);
            this.reset2Button.TabIndex = 13;
            this.reset2Button.Text = "Reset";
            this.reset2Button.UseVisualStyleBackColor = true;
            // 
            // indexesPrgrd
            // 
            this.indexesPrgrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.indexesPrgrd.Location = new System.Drawing.Point(0, 3);
            this.indexesPrgrd.Name = "indexesPrgrd";
            this.indexesPrgrd.Size = new System.Drawing.Size(426, 275);
            this.indexesPrgrd.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.reset3Button);
            this.tabPage3.Controls.Add(this.levelsPrgrd);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(426, 313);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Level styles";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // reset3Button
            // 
            this.reset3Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reset3Button.Location = new System.Drawing.Point(6, 284);
            this.reset3Button.Name = "reset3Button";
            this.reset3Button.Size = new System.Drawing.Size(75, 23);
            this.reset3Button.TabIndex = 13;
            this.reset3Button.Text = "Reset";
            this.reset3Button.UseVisualStyleBackColor = true;
            // 
            // levelsPrgrd
            // 
            this.levelsPrgrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.levelsPrgrd.Location = new System.Drawing.Point(0, 3);
            this.levelsPrgrd.Name = "levelsPrgrd";
            this.levelsPrgrd.Size = new System.Drawing.Size(426, 275);
            this.levelsPrgrd.TabIndex = 0;
            // 
            // IndicatorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "IndicatorPanel";
            this.Size = new System.Drawing.Size(434, 339);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        internal IndicatorPropertiesPanel indicatorRuntimePanel;
        internal System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.TabPage tabPage3;
        internal System.Windows.Forms.PropertyGrid indexesPrgrd;
        internal System.Windows.Forms.PropertyGrid levelsPrgrd;
        private System.Windows.Forms.Button reset1Button;
        internal System.Windows.Forms.Button reset2Button;
        internal System.Windows.Forms.Button reset3Button;

    }
}
