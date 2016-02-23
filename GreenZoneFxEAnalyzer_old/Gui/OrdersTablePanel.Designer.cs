namespace GreenZoneFxEngine
{
    partial class OrdersTablePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersTablePanel));
            this.ordersToolbar1 = new GreenZoneFxEngine.Gui.OrdersToolbar();
            this.wormSplitContainer1 = new GreenZoneUtil.Util.WormSplitContainer();
            this.SuspendLayout();
            // 
            // ordersToolbar1
            // 
            this.ordersToolbar1.AutoSize = true;
            this.ordersToolbar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ordersToolbar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ordersToolbar1.Location = new System.Drawing.Point(0, 0);
            this.ordersToolbar1.Margin = new System.Windows.Forms.Padding(0);
            this.ordersToolbar1.Name = "ordersToolbar1";
            this.ordersToolbar1.Size = new System.Drawing.Size(693, 28);
            this.ordersToolbar1.TabIndex = 40;
            // 
            // wormSplitContainer1
            // 
            this.wormSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wormSplitContainer1.Location = new System.Drawing.Point(0, 28);
            this.wormSplitContainer1.Name = "wormSplitContainer1";
            this.wormSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.wormSplitContainer1.Ratio = ((System.Collections.Generic.List<double>)(resources.GetObject("wormSplitContainer1.Ratio")));
            this.wormSplitContainer1.Size = new System.Drawing.Size(693, 358);
            this.wormSplitContainer1.SplitterWidth = 4;
            this.wormSplitContainer1.TabIndex = 41;
            // 
            // OrdersTablePanel
            // 
            this.Controls.Add(this.wormSplitContainer1);
            this.Controls.Add(this.ordersToolbar1);
            this.Name = "OrdersTablePanel";
            this.Size = new System.Drawing.Size(693, 386);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gui.OrdersToolbar ordersToolbar1;
        private GreenZoneUtil.Util.WormSplitContainer wormSplitContainer1;
    }
}
