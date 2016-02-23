namespace GreenZoneFxEngine.Gui.Chart
{
    partial class OrderChartSectionPanel
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
            this.propertiesButton = new System.Windows.Forms.Button();
            this.chartPane1 = new GreenZoneFxEngine.Gui.Chart.OrderChartPane();
            this.SuspendLayout();
            // 
            // propertiesButton
            // 
            this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesButton.BackColor = System.Drawing.Color.Transparent;
            this.propertiesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.propertiesButton.ForeColor = System.Drawing.Color.Transparent;
            this.propertiesButton.Image = global::GreenZoneFxEngine.Properties.Resources.properties_16xMD;
            this.propertiesButton.Location = new System.Drawing.Point(373, 0);
            this.propertiesButton.Margin = new System.Windows.Forms.Padding(0);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(15, 16);
            this.propertiesButton.TabIndex = 23;
            this.propertiesButton.UseVisualStyleBackColor = false;
            // 
            // chartPane1
            // 
            this.chartPane1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPane1.BackColor = System.Drawing.Color.White;
            this.chartPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartPane1.ForeColor = System.Drawing.Color.Black;
            this.chartPane1.Location = new System.Drawing.Point(0, 0);
            this.chartPane1.Margin = new System.Windows.Forms.Padding(0);
            this.chartPane1.Name = "chartPane1";
            this.chartPane1.Size = new System.Drawing.Size(388, 158);
            this.chartPane1.TabIndex = 20;
            // 
            // OrderChartSectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.chartPane1);
            this.Name = "OrderChartSectionPanel";
            this.Controls.SetChildIndex(this.chartPane1, 0);
            this.Controls.SetChildIndex(this.propertiesButton, 0);
            this.ResumeLayout(false);

        }

        #endregion

        internal OrderChartPane chartPane1;
        internal System.Windows.Forms.Button propertiesButton;
    }
}
