using GreenZoneUtil.Util;
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
            this.tableLayoutPanel1 = new GreenZoneUtil.Util.WormSplitContainer();
            this.zoomToScrollPriceButton = new System.Windows.Forms.Button();
            this.zoomToFitButton = new System.Windows.Forms.Button();
            this.zoomInVButton = new System.Windows.Forms.Button();
            this.zoomInHButton = new System.Windows.Forms.Button();
            this.zoomOutHButton = new System.Windows.Forms.Button();
            this.zoomOutVButton = new System.Windows.Forms.Button();
            this.timeLabelPane1 = new GreenZoneFxEngine.Gui.Chart.TimeLabelPane();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tableLayoutPanel1.Ratio = ((System.Collections.Generic.List<double>)(resources.GetObject("tableLayoutPanel1.Ratio")));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(469, 244);
            this.tableLayoutPanel1.SplitterWidth = 4;
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // zoomToScrollPriceButton
            // 
            this.zoomToScrollPriceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomToScrollPriceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomToScrollPriceButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomToFitH_zsf;
            this.zoomToScrollPriceButton.Location = new System.Drawing.Point(421, 244);
            this.zoomToScrollPriceButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomToScrollPriceButton.Name = "zoomToScrollPriceButton";
            this.zoomToScrollPriceButton.Size = new System.Drawing.Size(16, 16);
            this.zoomToScrollPriceButton.TabIndex = 25;
            this.zoomToScrollPriceButton.TabStop = false;
            this.zoomToScrollPriceButton.UseVisualStyleBackColor = false;
            this.zoomToScrollPriceButton.Click += new System.EventHandler(this.zoomToScrollPriceButton_Click);
            // 
            // zoomToFitButton
            // 
            this.zoomToFitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomToFitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomToFitButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomToFit;
            this.zoomToFitButton.Location = new System.Drawing.Point(453, 244);
            this.zoomToFitButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomToFitButton.Name = "zoomToFitButton";
            this.zoomToFitButton.Size = new System.Drawing.Size(16, 16);
            this.zoomToFitButton.TabIndex = 24;
            this.zoomToFitButton.TabStop = false;
            this.zoomToFitButton.UseVisualStyleBackColor = false;
            this.zoomToFitButton.Click += new System.EventHandler(this.zoomToFitButton_Click);
            // 
            // zoomInVButton
            // 
            this.zoomInVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInVButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomInVButton.Image = global::GreenZoneFxEngine.Properties.Resources.zoom_16xMD;
            this.zoomInVButton.Location = new System.Drawing.Point(437, 244);
            this.zoomInVButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomInVButton.Name = "zoomInVButton";
            this.zoomInVButton.Size = new System.Drawing.Size(16, 16);
            this.zoomInVButton.TabIndex = 22;
            this.zoomInVButton.TabStop = false;
            this.zoomInVButton.UseVisualStyleBackColor = false;
            this.zoomInVButton.Click += new System.EventHandler(this.zoomInVButton_Click);
            // 
            // zoomInHButton
            // 
            this.zoomInHButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomInHButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomInHButton.Image = global::GreenZoneFxEngine.Properties.Resources.zoom_16xMD;
            this.zoomInHButton.Location = new System.Drawing.Point(453, 260);
            this.zoomInHButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomInHButton.Name = "zoomInHButton";
            this.zoomInHButton.Size = new System.Drawing.Size(16, 16);
            this.zoomInHButton.TabIndex = 23;
            this.zoomInHButton.TabStop = false;
            this.zoomInHButton.UseVisualStyleBackColor = false;
            this.zoomInHButton.Click += new System.EventHandler(this.zoomInHButton_Click);
            // 
            // zoomOutHButton
            // 
            this.zoomOutHButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutHButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomOutHButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomOut_15x15;
            this.zoomOutHButton.Location = new System.Drawing.Point(421, 260);
            this.zoomOutHButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomOutHButton.Name = "zoomOutHButton";
            this.zoomOutHButton.Size = new System.Drawing.Size(16, 16);
            this.zoomOutHButton.TabIndex = 21;
            this.zoomOutHButton.TabStop = false;
            this.zoomOutHButton.UseVisualStyleBackColor = false;
            this.zoomOutHButton.Click += new System.EventHandler(this.zoomOutHButton_Click);
            // 
            // zoomOutVButton
            // 
            this.zoomOutVButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomOutVButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomOutVButton.Image = global::GreenZoneFxEngine.Properties.Resources.ZoomOut_15x15;
            this.zoomOutVButton.Location = new System.Drawing.Point(437, 260);
            this.zoomOutVButton.Margin = new System.Windows.Forms.Padding(0);
            this.zoomOutVButton.Name = "zoomOutVButton";
            this.zoomOutVButton.Size = new System.Drawing.Size(16, 16);
            this.zoomOutVButton.TabIndex = 20;
            this.zoomOutVButton.TabStop = false;
            this.zoomOutVButton.UseVisualStyleBackColor = false;
            this.zoomOutVButton.Click += new System.EventHandler(this.zoomOutVButton_Click);
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
            this.timeLabelPane1.Size = new System.Drawing.Size(421, 32);
            this.timeLabelPane1.TabIndex = 18;
            // 
            // Chart
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
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Chart";
            this.Size = new System.Drawing.Size(469, 276);
            this.SizeChanged += new System.EventHandler(this.Chart_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        protected WormSplitContainer tableLayoutPanel1;
        private System.Windows.Forms.Button zoomOutVButton;
        private System.Windows.Forms.Button zoomOutHButton;
        private System.Windows.Forms.Button zoomInVButton;
        private System.Windows.Forms.Button zoomInHButton;
        private System.Windows.Forms.Button zoomToFitButton;
        private System.Windows.Forms.Button zoomToScrollPriceButton;
        protected TimeLabelPane timeLabelPane1;


    }
}
