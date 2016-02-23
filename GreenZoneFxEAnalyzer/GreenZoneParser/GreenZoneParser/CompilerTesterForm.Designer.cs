namespace GreenZoneParser
{
    partial class CompilerTesterForm
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
            this.compileButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer3 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pathTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.removeXmlButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.addXmlButton = new System.Windows.Forms.Button();
            this.xmlTable = new System.Windows.Forms.DataGridView();
            this.XmlPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XmlFileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removeCsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.addCsButton = new System.Windows.Forms.Button();
            this.csTable = new System.Windows.Forms.DataGridView();
            this.CsPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CsFileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceTab = new System.Windows.Forms.TabPage();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.sourceTextBox = new GreenZoneParser.RichTextBoxEx();
            this.errorsTable = new System.Windows.Forms.DataGridView();
            this.errorMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorFileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorLineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorObjectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posLabel = new System.Windows.Forms.Label();
            this.lineLabel = new System.Windows.Forms.Label();
            this.colLabel = new System.Windows.Forms.Label();
            this.preParsePhaseChb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pathTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmlTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csTable)).BeginInit();
            this.sourceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // compileButton
            // 
            this.compileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.compileButton.Location = new System.Drawing.Point(624, 432);
            this.compileButton.Name = "compileButton";
            this.compileButton.Size = new System.Drawing.Size(75, 23);
            this.compileButton.TabIndex = 3;
            this.compileButton.Text = "Compile";
            this.compileButton.UseVisualStyleBackColor = true;
            this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.CollapseButtons = GreenZoneUtil.Gui.CollapseButtons.None;
            this.splitContainer2.LeftGap = 0;
            this.splitContainer2.Location = new System.Drawing.Point(6, 12);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            this.splitContainer2.Panel1CollapsedByClient = false;
            this.splitContainer2.Panel1NormalSize = 0;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2CollapsedByClient = false;
            this.splitContainer2.Panel2NormalSize = 0;
            this.splitContainer2.RightGap = 0;
            this.splitContainer2.Size = new System.Drawing.Size(701, 412);
            this.splitContainer2.SplitterDistance = 152;
            this.splitContainer2.TabIndex = 4;
            this.splitContainer2.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(152, 412);
            this.treeView1.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.CollapseButtons = GreenZoneUtil.Gui.CollapseButtons.None;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.LeftGap = 0;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer3.Panel1CollapsedByClient = false;
            this.splitContainer3.Panel1NormalSize = 0;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.errorsTable);
            this.splitContainer3.Panel2CollapsedByClient = false;
            this.splitContainer3.Panel2NormalSize = 0;
            this.splitContainer3.RightGap = 0;
            this.splitContainer3.Size = new System.Drawing.Size(545, 412);
            this.splitContainer3.SplitterDistance = 283;
            this.splitContainer3.TabIndex = 4;
            this.splitContainer3.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pathTab);
            this.tabControl1.Controls.Add(this.sourceTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(545, 283);
            this.tabControl1.TabIndex = 0;
            // 
            // pathTab
            // 
            this.pathTab.Controls.Add(this.splitContainer1);
            this.pathTab.Location = new System.Drawing.Point(4, 22);
            this.pathTab.Name = "pathTab";
            this.pathTab.Padding = new System.Windows.Forms.Padding(3);
            this.pathTab.Size = new System.Drawing.Size(537, 257);
            this.pathTab.TabIndex = 0;
            this.pathTab.Text = "Path";
            this.pathTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.CollapseButtons = GreenZoneUtil.Gui.CollapseButtons.None;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.LeftGap = 0;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.removeXmlButton);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.addXmlButton);
            this.splitContainer1.Panel1.Controls.Add(this.xmlTable);
            this.splitContainer1.Panel1CollapsedByClient = false;
            this.splitContainer1.Panel1NormalSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.removeCsButton);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.addCsButton);
            this.splitContainer1.Panel2.Controls.Add(this.csTable);
            this.splitContainer1.Panel2CollapsedByClient = false;
            this.splitContainer1.Panel2NormalSize = 0;
            this.splitContainer1.RightGap = 0;
            this.splitContainer1.Size = new System.Drawing.Size(531, 251);
            this.splitContainer1.SplitterDistance = 256;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.TabStop = false;
            // 
            // removeXmlButton
            // 
            this.removeXmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeXmlButton.Location = new System.Drawing.Point(96, 5);
            this.removeXmlButton.Name = "removeXmlButton";
            this.removeXmlButton.Size = new System.Drawing.Size(75, 23);
            this.removeXmlButton.TabIndex = 9;
            this.removeXmlButton.Text = "Remove";
            this.removeXmlButton.UseVisualStyleBackColor = true;
            this.removeXmlButton.Click += new System.EventHandler(this.removeXmlButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "XML include files";
            // 
            // addXmlButton
            // 
            this.addXmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addXmlButton.Location = new System.Drawing.Point(177, 5);
            this.addXmlButton.Name = "addXmlButton";
            this.addXmlButton.Size = new System.Drawing.Size(75, 23);
            this.addXmlButton.TabIndex = 6;
            this.addXmlButton.Text = "Add";
            this.addXmlButton.UseVisualStyleBackColor = true;
            this.addXmlButton.Click += new System.EventHandler(this.addXmlButton_Click);
            // 
            // xmlTable
            // 
            this.xmlTable.AllowUserToAddRows = false;
            this.xmlTable.AllowUserToDeleteRows = false;
            this.xmlTable.AllowUserToResizeRows = false;
            this.xmlTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.xmlTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xmlTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.XmlPathColumn,
            this.XmlFileColumn});
            this.xmlTable.Location = new System.Drawing.Point(3, 34);
            this.xmlTable.Name = "xmlTable";
            this.xmlTable.ReadOnly = true;
            this.xmlTable.RowHeadersVisible = false;
            this.xmlTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.xmlTable.Size = new System.Drawing.Size(250, 214);
            this.xmlTable.TabIndex = 5;
            // 
            // XmlPathColumn
            // 
            this.XmlPathColumn.FillWeight = 500F;
            this.XmlPathColumn.HeaderText = "Path";
            this.XmlPathColumn.Name = "XmlPathColumn";
            this.XmlPathColumn.ReadOnly = true;
            // 
            // XmlFileColumn
            // 
            this.XmlFileColumn.HeaderText = "File";
            this.XmlFileColumn.Name = "XmlFileColumn";
            this.XmlFileColumn.ReadOnly = true;
            // 
            // removeCsButton
            // 
            this.removeCsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeCsButton.Location = new System.Drawing.Point(110, 5);
            this.removeCsButton.Name = "removeCsButton";
            this.removeCsButton.Size = new System.Drawing.Size(75, 23);
            this.removeCsButton.TabIndex = 11;
            this.removeCsButton.Text = "Remove";
            this.removeCsButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Compiled Cs files";
            // 
            // addCsButton
            // 
            this.addCsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addCsButton.Location = new System.Drawing.Point(191, 5);
            this.addCsButton.Name = "addCsButton";
            this.addCsButton.Size = new System.Drawing.Size(75, 23);
            this.addCsButton.TabIndex = 7;
            this.addCsButton.Text = "Add";
            this.addCsButton.UseVisualStyleBackColor = true;
            // 
            // csTable
            // 
            this.csTable.AllowUserToAddRows = false;
            this.csTable.AllowUserToDeleteRows = false;
            this.csTable.AllowUserToResizeRows = false;
            this.csTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.csTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.csTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.csTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CsPathColumn,
            this.CsFileColumn});
            this.csTable.Location = new System.Drawing.Point(3, 34);
            this.csTable.Name = "csTable";
            this.csTable.ReadOnly = true;
            this.csTable.RowHeadersVisible = false;
            this.csTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.csTable.Size = new System.Drawing.Size(265, 214);
            this.csTable.TabIndex = 6;
            // 
            // CsPathColumn
            // 
            this.CsPathColumn.FillWeight = 500F;
            this.CsPathColumn.HeaderText = "Path";
            this.CsPathColumn.Name = "CsPathColumn";
            this.CsPathColumn.ReadOnly = true;
            // 
            // CsFileColumn
            // 
            this.CsFileColumn.HeaderText = "File";
            this.CsFileColumn.Name = "CsFileColumn";
            this.CsFileColumn.ReadOnly = true;
            // 
            // sourceTab
            // 
            this.sourceTab.Controls.Add(this.fileTextBox);
            this.sourceTab.Controls.Add(this.sourceTextBox);
            this.sourceTab.Location = new System.Drawing.Point(4, 22);
            this.sourceTab.Name = "sourceTab";
            this.sourceTab.Padding = new System.Windows.Forms.Padding(3);
            this.sourceTab.Size = new System.Drawing.Size(526, 255);
            this.sourceTab.TabIndex = 1;
            this.sourceTab.Text = "Source";
            this.sourceTab.UseVisualStyleBackColor = true;
            // 
            // fileTextBox
            // 
            this.fileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTextBox.Location = new System.Drawing.Point(3, 3);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.Size = new System.Drawing.Size(517, 20);
            this.fileTextBox.TabIndex = 5;
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourceTextBox.HideSelection = false;
            this.sourceTextBox.Location = new System.Drawing.Point(3, 26);
            this.sourceTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(517, 226);
            this.sourceTextBox.TabIndex = 1;
            this.sourceTextBox.Text = "";
            this.sourceTextBox.WordWrap = false;
            // 
            // errorsTable
            // 
            this.errorsTable.AllowUserToAddRows = false;
            this.errorsTable.AllowUserToDeleteRows = false;
            this.errorsTable.AllowUserToResizeRows = false;
            this.errorsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.errorsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.errorMessageColumn,
            this.ErrorFileColumn,
            this.ErrorLineColumn,
            this.ErrorColumnColumn,
            this.ErrorObjectColumn});
            this.errorsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorsTable.Location = new System.Drawing.Point(0, 0);
            this.errorsTable.Name = "errorsTable";
            this.errorsTable.ReadOnly = true;
            this.errorsTable.RowHeadersVisible = false;
            this.errorsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.errorsTable.Size = new System.Drawing.Size(545, 125);
            this.errorsTable.TabIndex = 5;
            // 
            // errorMessageColumn
            // 
            this.errorMessageColumn.FillWeight = 500F;
            this.errorMessageColumn.HeaderText = "Error";
            this.errorMessageColumn.Name = "errorMessageColumn";
            this.errorMessageColumn.ReadOnly = true;
            // 
            // ErrorFileColumn
            // 
            this.ErrorFileColumn.FillWeight = 200F;
            this.ErrorFileColumn.HeaderText = "File";
            this.ErrorFileColumn.Name = "ErrorFileColumn";
            this.ErrorFileColumn.ReadOnly = true;
            // 
            // ErrorLineColumn
            // 
            this.ErrorLineColumn.HeaderText = "Line";
            this.ErrorLineColumn.Name = "ErrorLineColumn";
            this.ErrorLineColumn.ReadOnly = true;
            // 
            // ErrorColumnColumn
            // 
            this.ErrorColumnColumn.HeaderText = "Column";
            this.ErrorColumnColumn.Name = "ErrorColumnColumn";
            this.ErrorColumnColumn.ReadOnly = true;
            // 
            // ErrorObjectColumn
            // 
            this.ErrorObjectColumn.HeaderText = "object";
            this.ErrorObjectColumn.Name = "ErrorObjectColumn";
            this.ErrorObjectColumn.ReadOnly = true;
            this.ErrorObjectColumn.Visible = false;
            // 
            // posLabel
            // 
            this.posLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.posLabel.AutoSize = true;
            this.posLabel.Location = new System.Drawing.Point(12, 437);
            this.posLabel.Name = "posLabel";
            this.posLabel.Size = new System.Drawing.Size(16, 13);
            this.posLabel.TabIndex = 9;
            this.posLabel.Text = "   ";
            // 
            // lineLabel
            // 
            this.lineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lineLabel.AutoSize = true;
            this.lineLabel.Location = new System.Drawing.Point(85, 437);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(16, 13);
            this.lineLabel.TabIndex = 10;
            this.lineLabel.Text = "   ";
            // 
            // colLabel
            // 
            this.colLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colLabel.AutoSize = true;
            this.colLabel.Location = new System.Drawing.Point(166, 437);
            this.colLabel.Name = "colLabel";
            this.colLabel.Size = new System.Drawing.Size(16, 13);
            this.colLabel.TabIndex = 11;
            this.colLabel.Text = "   ";
            // 
            // preParsePhaseChb
            // 
            this.preParsePhaseChb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.preParsePhaseChb.AutoSize = true;
            this.preParsePhaseChb.Location = new System.Drawing.Point(473, 436);
            this.preParsePhaseChb.Name = "preParsePhaseChb";
            this.preParsePhaseChb.Size = new System.Drawing.Size(125, 17);
            this.preParsePhaseChb.TabIndex = 12;
            this.preParsePhaseChb.Text = "Pre-parse phase only";
            this.preParsePhaseChb.UseVisualStyleBackColor = true;
            // 
            // CompilerTesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 462);
            this.Controls.Add(this.preParsePhaseChb);
            this.Controls.Add(this.colLabel);
            this.Controls.Add(this.lineLabel);
            this.Controls.Add(this.posLabel);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.compileButton);
            this.Name = "CompilerTesterForm";
            this.Text = "CompilerTesterForm";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.pathTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmlTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csTable)).EndInit();
            this.sourceTab.ResumeLayout(false);
            this.sourceTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button compileButton;
        private GreenZoneUtil.Gui.SplitContainerEx splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private GreenZoneUtil.Gui.SplitContainerEx splitContainer3;
        private System.Windows.Forms.DataGridView errorsTable;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pathTab;
        private GreenZoneUtil.Gui.SplitContainerEx splitContainer1;
        private System.Windows.Forms.Button removeXmlButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addXmlButton;
        private System.Windows.Forms.DataGridView xmlTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn XmlPathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn XmlFileColumn;
        private System.Windows.Forms.Button removeCsButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addCsButton;
        private System.Windows.Forms.DataGridView csTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn CsPathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CsFileColumn;
        private System.Windows.Forms.TabPage sourceTab;
        private RichTextBoxEx sourceTextBox;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn errorMessageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorFileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorLineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorColumnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorObjectColumn;
        private System.Windows.Forms.Label posLabel;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Label colLabel;
        private System.Windows.Forms.CheckBox preParsePhaseChb;
    }
}