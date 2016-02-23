using GreenZoneUtil.Util;
using GreenZoneUtil.Gui;
namespace GreenZoneFxEngine.Gui.Chart
{
    partial class NonCursorChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NonCursorChart));
            this.zoomToScrollPriceButton = new GreenZoneUtil.Gui.NonselButton();
            this.zoomToFitButton = new GreenZoneUtil.Gui.NonselButton();
            this.zoomInVButton = new GreenZoneUtil.Gui.NonselButton();
            this.zoomInHButton = new GreenZoneUtil.Gui.NonselButton();
            this.zoomOutHButton = new GreenZoneUtil.Gui.NonselButton();
            this.zoomOutVButton = new GreenZoneUtil.Gui.NonselButton();
            this.timeLabelPane1 = new GreenZoneFxEngine.Gui.Chart.TimeLabelPane();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.None;
            this.tableLayoutPanel1.Ratio = ((System.Collections.Generic.List<double>)(resources.GetObject("tableLayoutPanel1.Ratio")));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(450, 244);
            // 
            // zoomToScrollPriceButton
            // 
            this.zoomToScrollPriceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomToScrollPriceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomToScrollPriceButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomToFitH_zsf;
            this.zoomToScrollPriceButton.Location = new System.Drawing.Point(402, 244);
            this.zoomToScrollPriceButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomToScrollPriceButton.Name = "zoomToScrollPriceButton";
            this.zoomToScrollPriceButton.Size = new System.Drawing.Size(16, 16);
            this.zoomToScrollPriceButton.TabIndex = 25;
            this.zoomToScrollPriceButton.TabStop = false;
            this.zoomToScrollPriceButton.UseVisualStyleBackColor = false;
            // 
            // zoomToFitButton
            // 
            this.zoomToFitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomToFitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomToFitButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomToFit;
            this.zoomToFitButton.Location = new System.Drawing.Point(434, 244);
            this.zoomToFitButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomToFitButton.Name = "zoomToFitButton";
            this.zoomToFitButton.Size = new System.Drawing.Size(16, 16);
            this.zoomToFitButton.TabIndex = 24;
            this.zoomToFitButton.TabStop = false;
            this.zoomToFitButton.UseVisualStyleBackColor = false;
            // 
            // zoomInVButton
            // 
            this.zoomInVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInVButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomInVButton.Image = global::GreenZoneFxEngine.Properties.Resources.zoom_16xMD;
            this.zoomInVButton.Location = new System.Drawing.Point(418, 244);
            this.zoomInVButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomInVButton.Name = "zoomInVButton";
            this.zoomInVButton.Size = new System.Drawing.Size(16, 16);
            this.zoomInVButton.TabIndex = 22;
            this.zoomInVButton.TabStop = false;
            this.zoomInVButton.UseVisualStyleBackColor = false;
            // 
            // zoomInHButton
            // 
            this.zoomInHButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInHButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomInHButton.Image = global::GreenZoneFxEngine.Properties.Resources.zoom_16xMD;
            this.zoomInHButton.Location = new System.Drawing.Point(434, 260);
            this.zoomInHButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomInHButton.Name = "zoomInHButton";
            this.zoomInHButton.Size = new System.Drawing.Size(16, 16);
            this.zoomInHButton.TabIndex = 23;
            this.zoomInHButton.TabStop = false;
            this.zoomInHButton.UseVisualStyleBackColor = false;
            // 
            // zoomOutHButton
            // 
            this.zoomOutHButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutHButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomOutHButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomOut_15x15;
            this.zoomOutHButton.Location = new System.Drawing.Point(402, 260);
            this.zoomOutHButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomOutHButton.Name = "zoomOutHButton";
            this.zoomOutHButton.Size = new System.Drawing.Size(16, 16);
            this.zoomOutHButton.TabIndex = 21;
            this.zoomOutHButton.TabStop = false;
            this.zoomOutHButton.UseVisualStyleBackColor = false;
            // 
            // zoomOutVButton
            // 
            this.zoomOutVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutVButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomOutVButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomOut_15x15;
            this.zoomOutVButton.Location = new System.Drawing.Point(418, 260);
            this.zoomOutVButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomOutVButton.Name = "zoomOutVButton";
            this.zoomOutVButton.Size = new System.Drawing.Size(16, 16);
            this.zoomOutVButton.TabIndex = 20;
            this.zoomOutVButton.TabStop = false;
            this.zoomOutVButton.UseVisualStyleBackColor = false;
            // 
            // timeLabelPane1
            // 
            this.timeLabelPane1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabelPane1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabelPane1.ForeColor = System.Drawing.Color.Black;
            this.timeLabelPane1.Location = new System.Drawing.Point(0, 244);
            this.timeLabelPane1.Margin = new System.Windows.Forms.Padding(0);
            this.timeLabelPane1.Name = "timeLabelPane1";
            this.timeLabelPane1.Size = new System.Drawing.Size(402, 32);
            this.timeLabelPane1.TabIndex = 18;
            // 
            // NonCursorChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zoomToScrollPriceButton);
            this.Controls.Add(this.zoomToFitButton);
            this.Controls.Add(this.zoomInVButton);
            this.Controls.Add(this.zoomInHButton);
            this.Controls.Add(this.zoomOutHButton);
            this.Controls.Add(this.zoomOutVButton);
            this.Controls.Add(this.timeLabelPane1);
            this.Name = "NonCursorChart";
            this.Controls.SetChildIndex(this.timeLabelPane1, 0);
            this.Controls.SetChildIndex(this.zoomOutVButton, 0);
            this.Controls.SetChildIndex(this.zoomOutHButton, 0);
            this.Controls.SetChildIndex(this.zoomInHButton, 0);
            this.Controls.SetChildIndex(this.zoomInVButton, 0);
            this.Controls.SetChildIndex(this.zoomToFitButton, 0);
            this.Controls.SetChildIndex(this.zoomToScrollPriceButton, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private NonselButton zoomOutVButton;
        private NonselButton zoomOutHButton;
        private NonselButton zoomInVButton;
        private NonselButton zoomInHButton;
        private NonselButton zoomToFitButton;
        private NonselButton zoomToScrollPriceButton;
        protected TimeLabelPane timeLabelPane1;


    }
}
