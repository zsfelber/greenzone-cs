using GreenZoneUtil.Util;
namespace GreenZoneFxEngine.Gui.Chart
{
    partial class NormalChart
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
            this.SuspendLayout();
            // 
            // NormalChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "NormalChart";
            this.ResumeLayout(false);

            this.masterChartSectionPanel = new GreenZoneFxEngine.Gui.Chart.NormalChartSectionPanel();
            // 
            // masterChartSectionPanel
            // 
            this.masterChartSectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.masterChartSectionPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.masterChartSectionPanel.Location = new System.Drawing.Point(3, 3);
            this.masterChartSectionPanel.Margin = new System.Windows.Forms.Padding(1);
            this.masterChartSectionPanel.Name = "masterChartSectionPanel";
            this.masterChartSectionPanel.Size = new System.Drawing.Size(463, 254);
            this.masterChartSectionPanel.TabIndex = 0;
        }

        #endregion

        internal NormalChartSectionPanel masterChartSectionPanel;


    }
}
