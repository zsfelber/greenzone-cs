namespace Parse_Tree_C_Sharp
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtTableFile = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.txtParseTree = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtReparse = new System.Windows.Forms.TextBox();
            this.sourceFileTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(580, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(96, 32);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtTableFile
            // 
            this.txtTableFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTableFile.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableFile.Location = new System.Drawing.Point(73, 8);
            this.txtTableFile.Name = "txtTableFile";
            this.txtTableFile.Size = new System.Drawing.Size(499, 21);
            this.txtTableFile.TabIndex = 8;
            // 
            // btnParse
            // 
            this.btnParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParse.Location = new System.Drawing.Point(580, 46);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(96, 32);
            this.btnParse.TabIndex = 7;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // txtParseTree
            // 
            this.txtParseTree.BackColor = System.Drawing.SystemColors.Window;
            this.txtParseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtParseTree.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParseTree.Location = new System.Drawing.Point(0, 0);
            this.txtParseTree.Multiline = true;
            this.txtParseTree.Name = "txtParseTree";
            this.txtParseTree.ReadOnly = true;
            this.txtParseTree.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtParseTree.Size = new System.Drawing.Size(564, 167);
            this.txtParseTree.TabIndex = 6;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(8, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtParseTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtReparse);
            this.splitContainer1.Size = new System.Drawing.Size(564, 335);
            this.splitContainer1.SplitterDistance = 167;
            this.splitContainer1.TabIndex = 10;
            // 
            // txtReparse
            // 
            this.txtReparse.BackColor = System.Drawing.SystemColors.Window;
            this.txtReparse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReparse.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReparse.Location = new System.Drawing.Point(0, 0);
            this.txtReparse.Multiline = true;
            this.txtReparse.Name = "txtReparse";
            this.txtReparse.ReadOnly = true;
            this.txtReparse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReparse.Size = new System.Drawing.Size(564, 164);
            this.txtReparse.TabIndex = 7;
            // 
            // sourceFileTb
            // 
            this.sourceFileTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceFileTb.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceFileTb.Location = new System.Drawing.Point(73, 35);
            this.sourceFileTb.Name = "sourceFileTb";
            this.sourceFileTb.Size = new System.Drawing.Size(499, 21);
            this.sourceFileTb.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Grammar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Source";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourceFileTb);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtTableFile);
            this.Controls.Add(this.btnParse);
            this.Name = "frmMain";
            this.Text = "Draw Parse Tree";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.TextBox txtTableFile;
        internal System.Windows.Forms.Button btnParse;
        internal System.Windows.Forms.TextBox txtParseTree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.TextBox txtReparse;
        internal System.Windows.Forms.TextBox sourceFileTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

