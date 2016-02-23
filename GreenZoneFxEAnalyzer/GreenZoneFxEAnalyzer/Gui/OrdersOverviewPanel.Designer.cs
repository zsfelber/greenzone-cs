using GreenZoneFxEngine.Gui.Chart;
namespace GreenZoneFxEngine
{
    partial class OrdersOverviewPanel
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
            this.orderChart1 = new GreenZoneFxEngine.Gui.Chart.OrderChart();
            this.ordersToolbar1 = new GreenZoneFxEngine.Gui.OrdersToolbar();
            this.SuspendLayout();
            // 
            // orderChart1
            // 
            this.orderChart1.BackColor = System.Drawing.SystemColors.Control;
            this.orderChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderChart1.Location = new System.Drawing.Point(0, 28);
            this.orderChart1.Margin = new System.Windows.Forms.Padding(2);
            this.orderChart1.Name = "orderChart1";
            this.orderChart1.Size = new System.Drawing.Size(690, 358);
            this.orderChart1.TabIndex = 3;
            // 
            // ordersToolbar1
            // 
            this.ordersToolbar1.AutoSize = true;
            this.ordersToolbar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ordersToolbar1.BackColor = System.Drawing.SystemColors.Control;
            this.ordersToolbar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ordersToolbar1.Location = new System.Drawing.Point(0, 0);
            this.ordersToolbar1.Margin = new System.Windows.Forms.Padding(0);
            this.ordersToolbar1.Name = "ordersToolbar1";
            this.ordersToolbar1.Size = new System.Drawing.Size(690, 28);
            this.ordersToolbar1.TabIndex = 4;
            // 
            // OrdersOverviewPanel
            // 
            this.Controls.Add(this.orderChart1);
            this.Controls.Add(this.ordersToolbar1);
            this.Name = "OrdersOverviewPanel";
            this.Size = new System.Drawing.Size(690, 386);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OrderChart orderChart1;
        private Gui.OrdersToolbar ordersToolbar1;
    }
}
