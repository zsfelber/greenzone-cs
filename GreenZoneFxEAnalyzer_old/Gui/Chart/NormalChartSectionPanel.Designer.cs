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
            this.chartPane1.AskColor = System.Drawing.Color.Red;
            this.chartPane1.BackColor = System.Drawing.Color.White;
            this.chartPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartPane1.ChartCalcAutoGap = true;
            this.chartPane1.ChartLeftGap = 0;
            this.chartPane1.ChartRightGap = 0;
            this.chartPane1.CpBarValue = 0;
            this.chartPane1.CpBarVisible = false;
            this.chartPane1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartPane1.ForeColor = System.Drawing.Color.Black;
            this.chartPane1.GridColor = System.Drawing.Color.Green;
            this.chartPane1.InactiveColor = System.Drawing.Color.Gray;
            this.chartPane1.LevelFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartPane1.Location = new System.Drawing.Point(0, 0);
            this.chartPane1.Margin = new System.Windows.Forms.Padding(0);
            this.chartPane1.Name = "chartPane1";
            this.chartPane1.Size = new System.Drawing.Size(388, 158);
            this.chartPane1.SliderBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(0)))));
            this.chartPane1.SliderMaximum = 1000;
            this.chartPane1.SliderMinimum = 0;
            this.chartPane1.SliderThumbVisible = true;
            this.chartPane1.SliderValue = 1000;
            this.chartPane1.TabIndex = 20;

            this.Controls.Add(this.chartPane1);
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion

        internal NormalChartPane chartPane1;
    }
}
