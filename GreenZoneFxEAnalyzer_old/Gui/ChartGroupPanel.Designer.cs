using GreenZoneUtil.Util;
namespace GreenZoneFxEngine
{
    partial class ChartGroupPanel
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toggleBottomBarButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.eaCombo = new System.Windows.Forms.ToolStripComboBox();
            this.openEaButton = new System.Windows.Forms.ToolStripButton();
            this.inTestButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.scriptCombo = new System.Windows.Forms.ToolStripComboBox();
            this.openScriptButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleBottomBarButton2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new WormSplitContainer();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toggleBottomBarButton2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(630, 260);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(630, 310);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleBottomBarButton1,
            this.toolStripLabel6,
            this.toolStripLabel1,
            this.eaCombo,
            this.openEaButton,
            this.inTestButton,
            this.toolStripSeparator5,
            this.toolStripLabel2,
            this.scriptCombo,
            this.openScriptButton,
            this.toolStripSeparator2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(630, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toggleBottomBarButton1
            // 
            this.toggleBottomBarButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleBottomBarButton1.Image = global::GreenZoneFxEngine.Properties.Resources.toggle_16xLG;
            this.toggleBottomBarButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleBottomBarButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleBottomBarButton1.Name = "toggleBottomBarButton1";
            this.toggleBottomBarButton1.Size = new System.Drawing.Size(23, 22);
            this.toggleBottomBarButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toggleBottomBarButton1.Click += new System.EventHandler(this.toggleBottomBarButton1_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Image = global::GreenZoneFxEngine.Properties.Resources.Symbols_Play_32xLG;
            this.toolStripLabel6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabel6.Text = "     ";
            this.toolStripLabel6.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(24, 22);
            this.toolStripLabel1.Text = "EA:";
            // 
            // eaCombo
            // 
            this.eaCombo.Name = "eaCombo";
            this.eaCombo.Size = new System.Drawing.Size(121, 25);
            this.eaCombo.Click += new System.EventHandler(this.eaCombo_Click);
            // 
            // openEaButton
            // 
            this.openEaButton.Enabled = false;
            this.openEaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openEaButton.Name = "openEaButton";
            this.openEaButton.Size = new System.Drawing.Size(37, 22);
            this.openEaButton.Text = "Open";
            this.openEaButton.Click += new System.EventHandler(this.openEaButton_Click);
            // 
            // inTestButton
            // 
            this.inTestButton.CheckOnClick = true;
            this.inTestButton.Enabled = false;
            this.inTestButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inTestButton.Name = "inTestButton";
            this.inTestButton.Size = new System.Drawing.Size(60, 22);
            this.inTestButton.Text = "not in test";
            this.inTestButton.Click += new System.EventHandler(this.inTestButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(83, 22);
            this.toolStripLabel2.Text = "   Launch script:";
            // 
            // scriptCombo
            // 
            this.scriptCombo.Name = "scriptCombo";
            this.scriptCombo.Size = new System.Drawing.Size(121, 25);
            this.scriptCombo.Click += new System.EventHandler(this.scriptCombo_Click);
            // 
            // openScriptButton
            // 
            this.openScriptButton.Enabled = false;
            this.openScriptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openScriptButton.Name = "openScriptButton";
            this.openScriptButton.Size = new System.Drawing.Size(37, 22);
            this.openScriptButton.Text = "Open";
            this.openScriptButton.Click += new System.EventHandler(this.openScriptButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toggleBottomBarButton2
            // 
            this.toggleBottomBarButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toggleBottomBarButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.toggleBottomBarButton2.Image = global::GreenZoneFxEngine.Properties.Resources.toggle_16xLG;
            this.toggleBottomBarButton2.Location = new System.Drawing.Point(0, 243);
            this.toggleBottomBarButton2.Name = "toggleBottomBarButton2";
            this.toggleBottomBarButton2.Size = new System.Drawing.Size(23, 16);
            this.toggleBottomBarButton2.TabIndex = 13;
            this.toggleBottomBarButton2.UseVisualStyleBackColor = true;
            this.toggleBottomBarButton2.Click += new System.EventHandler(this.toggleBottomBarButton2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 260);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // ChartGroupPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ChartGroupPanel";
            this.Size = new System.Drawing.Size(630, 310);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toggleBottomBarButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox eaCombo;
        private System.Windows.Forms.ToolStripButton openEaButton;
        private System.Windows.Forms.ToolStripButton inTestButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox scriptCombo;
        private System.Windows.Forms.ToolStripButton openScriptButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private WormSplitContainer tableLayoutPanel1;
        private System.Windows.Forms.Button toggleBottomBarButton2;

    }
}
