using GreenZoneUtil.Util;
using GreenZoneUtil.Gui;
namespace GreenZoneFxEngine.Gui.Chart
{
    partial class Chart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chart));
            this.tableLayoutPanel1 = new GreenZoneUtil.Gui.WormSplitContainer();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tableLayoutPanel1.Ratio = ((System.Collections.Generic.List<double>)(resources.GetObject("tableLayoutPanel1.Ratio")));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(450, 276);
            this.tableLayoutPanel1.SplitterWidth = 4;
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Chart";
            this.Size = new System.Drawing.Size(450, 276);
            this.ResumeLayout(false);

        }

        #endregion

        protected WormSplitContainer tableLayoutPanel1;


    }
}
