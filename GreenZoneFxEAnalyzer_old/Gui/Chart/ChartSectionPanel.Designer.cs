namespace GreenZoneFxEngine.Gui.Chart
{
    partial class ChartSectionPanel
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
            this.priceLabelPane = new GreenZoneFxEngine.Gui.Chart.PriceLabelPane();
            this.SuspendLayout();
            // 
            // priceLabelPane
            // 
            this.priceLabelPane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.priceLabelPane.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceLabelPane.ForeColor = System.Drawing.SystemColors.ControlText;
            this.priceLabelPane.Location = new System.Drawing.Point(388, 0);
            this.priceLabelPane.Margin = new System.Windows.Forms.Padding(0);
            this.priceLabelPane.Name = "priceLabelPane";
            this.priceLabelPane.Size = new System.Drawing.Size(48, 158);
            this.priceLabelPane.TabIndex = 21;
            this.priceLabelPane.Text = "priceLabelPane";
            // 
            // ChartSectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.priceLabelPane);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ChartSectionPanel";
            this.Size = new System.Drawing.Size(436, 158);
            this.ResumeLayout(false);

        }

        #endregion

        private PriceLabelPane priceLabelPane;

    }
}
