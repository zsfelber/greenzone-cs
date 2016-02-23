namespace GreenZoneFxEngine.Gui.Chart
{
    partial class NormalChartSectionPanel
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
            this.chartPane1 = new GreenZoneFxEngine.Gui.Chart.NormalChartPane();

            // 
            // chartPane1
            // 
            this.chartPane1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPane1.BackColor = System.Drawing.Color.White;
            this.chartPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartPane1.Location = new System.Drawing.Point(0, 0);
            this.chartPane1.Margin = new System.Windows.Forms.Padding(0);
            this.chartPane1.Name = "chartPane1";
            this.chartPane1.Size = new System.Drawing.Size(388, 158);
            this.chartPane1.TabIndex = 20;

            this.Controls.Add(this.chartPane1);
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion

        internal NormalChartPane chartPane1;
    }
}
