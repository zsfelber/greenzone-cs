namespace GreenZoneParser
{
    partial class ParseTesterForm
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
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.browseCsButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.errorsTable = new System.Windows.Forms.DataGridView();
            this.errorMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorLineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorLengthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorPositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorObjectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.showTokensChb = new System.Windows.Forms.CheckBox();
            this.stepButton = new System.Windows.Forms.Button();
            this.breakPointBar = new System.Windows.Forms.Panel();
            this.curProdInfLabel = new System.Windows.Forms.Label();
            this.addNodeLabel = new System.Windows.Forms.Label();
            this.prodLabel = new System.Windows.Forms.Label();
            this.posLabel = new System.Windows.Forms.Label();
            this.debugParseChb = new System.Windows.Forms.CheckBox();
            this.errLabel = new System.Windows.Forms.Label();
            this.tkLabel = new System.Windows.Forms.Label();
            this.nlLabel = new System.Windows.Forms.Label();
            this.debugTokensChb = new System.Windows.Forms.CheckBox();
            this.columnLabel = new System.Windows.Forms.Label();
            this.lineLabel = new System.Windows.Forms.Label();
            this.parseButton = new System.Windows.Forms.Button();
            this.clearTablesBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.errorsTab = new System.Windows.Forms.TabPage();
            this.tokensTab = new System.Windows.Forms.TabPage();
            this.tokensTable = new System.Windows.Forms.DataGridView();
            this.TokenNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenLineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenLengthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenIndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TokenPositionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseXmlButton = new System.Windows.Forms.Button();
            this.sourceTextBox = new GreenZoneParser.RichTextBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.errorsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.errorsTab.SuspendLayout();
            this.tokensTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tokensTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileTextBox
            // 
            this.fileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTextBox.Location = new System.Drawing.Point(3, 34);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(478, 20);
            this.fileTextBox.TabIndex = 1;
            // 
            // browseCsButton
            // 
            this.browseCsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseCsButton.Location = new System.Drawing.Point(497, 32);
            this.browseCsButton.Name = "browseCsButton";
            this.browseCsButton.Size = new System.Drawing.Size(27, 23);
            this.browseCsButton.TabIndex = 2;
            this.browseCsButton.Text = "Cs";
            this.browseCsButton.UseVisualStyleBackColor = true;
            this.browseCsButton.Click += new System.EventHandler(this.browseCsButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(347, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
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
            this.ErrorLineColumn,
            this.ErrorColumnColumn,
            this.ErrorLengthColumn,
            this.ErrorPositionColumn,
            this.ErrorObjectColumn});
            this.errorsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorsTable.Location = new System.Drawing.Point(3, 3);
            this.errorsTable.Name = "errorsTable";
            this.errorsTable.ReadOnly = true;
            this.errorsTable.RowHeadersVisible = false;
            this.errorsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.errorsTable.Size = new System.Drawing.Size(405, 101);
            this.errorsTable.TabIndex = 4;
            // 
            // errorMessageColumn
            // 
            this.errorMessageColumn.FillWeight = 500F;
            this.errorMessageColumn.HeaderText = "Error";
            this.errorMessageColumn.Name = "errorMessageColumn";
            this.errorMessageColumn.ReadOnly = true;
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
            // ErrorLengthColumn
            // 
            this.ErrorLengthColumn.HeaderText = "Length";
            this.ErrorLengthColumn.Name = "ErrorLengthColumn";
            this.ErrorLengthColumn.ReadOnly = true;
            // 
            // ErrorPositionColumn
            // 
            this.ErrorPositionColumn.HeaderText = "Position";
            this.ErrorPositionColumn.Name = "ErrorPositionColumn";
            this.ErrorPositionColumn.ReadOnly = true;
            // 
            // ErrorObjectColumn
            // 
            this.ErrorObjectColumn.HeaderText = "object";
            this.ErrorObjectColumn.Name = "ErrorObjectColumn";
            this.ErrorObjectColumn.ReadOnly = true;
            this.ErrorObjectColumn.Visible = false;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(10, 6);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(22, 13);
            this.infoLabel.TabIndex = 5;
            this.infoLabel.Text = "     ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.CollapseButtons = GreenZoneUtil.Gui.CollapseButtons.None;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.LeftGap = 0;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.showTokensChb);
            this.splitContainer1.Panel1.Controls.Add(this.stepButton);
            this.splitContainer1.Panel1.Controls.Add(this.breakPointBar);
            this.splitContainer1.Panel1.Controls.Add(this.curProdInfLabel);
            this.splitContainer1.Panel1.Controls.Add(this.addNodeLabel);
            this.splitContainer1.Panel1.Controls.Add(this.prodLabel);
            this.splitContainer1.Panel1.Controls.Add(this.posLabel);
            this.splitContainer1.Panel1.Controls.Add(this.debugParseChb);
            this.splitContainer1.Panel1.Controls.Add(this.errLabel);
            this.splitContainer1.Panel1.Controls.Add(this.tkLabel);
            this.splitContainer1.Panel1.Controls.Add(this.nlLabel);
            this.splitContainer1.Panel1.Controls.Add(this.debugTokensChb);
            this.splitContainer1.Panel1.Controls.Add(this.columnLabel);
            this.splitContainer1.Panel1.Controls.Add(this.lineLabel);
            this.splitContainer1.Panel1.Controls.Add(this.parseButton);
            this.splitContainer1.Panel1.Controls.Add(this.sourceTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.loadButton);
            this.splitContainer1.Panel1CollapsedByClient = false;
            this.splitContainer1.Panel1NormalSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.clearTablesBtn);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.infoLabel);
            this.splitContainer1.Panel2CollapsedByClient = false;
            this.splitContainer1.Panel2NormalSize = 0;
            this.splitContainer1.RightGap = 0;
            this.splitContainer1.Size = new System.Drawing.Size(433, 440);
            this.splitContainer1.SplitterDistance = 275;
            this.splitContainer1.TabIndex = 6;
            this.splitContainer1.TabStop = false;
            // 
            // showTokensChb
            // 
            this.showTokensChb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showTokensChb.AutoSize = true;
            this.showTokensChb.Location = new System.Drawing.Point(337, 32);
            this.showTokensChb.Name = "showTokensChb";
            this.showTokensChb.Size = new System.Drawing.Size(88, 17);
            this.showTokensChb.TabIndex = 18;
            this.showTokensChb.Text = "Show tokens";
            this.showTokensChb.UseVisualStyleBackColor = true;
            // 
            // stepButton
            // 
            this.stepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stepButton.Location = new System.Drawing.Point(347, 103);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 23);
            this.stepButton.TabIndex = 17;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Visible = false;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // breakPointBar
            // 
            this.breakPointBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.breakPointBar.Location = new System.Drawing.Point(2, 0);
            this.breakPointBar.Margin = new System.Windows.Forms.Padding(0);
            this.breakPointBar.Name = "breakPointBar";
            this.breakPointBar.Size = new System.Drawing.Size(11, 275);
            this.breakPointBar.TabIndex = 16;
            this.breakPointBar.Click += new System.EventHandler(this.breakPointBar_Click);
            this.breakPointBar.Paint += new System.Windows.Forms.PaintEventHandler(this.breakPointBar_Paint);
            // 
            // curProdInfLabel
            // 
            this.curProdInfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.curProdInfLabel.AutoSize = true;
            this.curProdInfLabel.Location = new System.Drawing.Point(340, 195);
            this.curProdInfLabel.Name = "curProdInfLabel";
            this.curProdInfLabel.Size = new System.Drawing.Size(0, 13);
            this.curProdInfLabel.TabIndex = 15;
            // 
            // addNodeLabel
            // 
            this.addNodeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addNodeLabel.AutoSize = true;
            this.addNodeLabel.Location = new System.Drawing.Point(403, 182);
            this.addNodeLabel.Name = "addNodeLabel";
            this.addNodeLabel.Size = new System.Drawing.Size(23, 13);
            this.addNodeLabel.TabIndex = 14;
            this.addNodeLabel.Text = "ND";
            this.addNodeLabel.Visible = false;
            // 
            // prodLabel
            // 
            this.prodLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prodLabel.AutoSize = true;
            this.prodLabel.Location = new System.Drawing.Point(376, 182);
            this.prodLabel.Name = "prodLabel";
            this.prodLabel.Size = new System.Drawing.Size(22, 13);
            this.prodLabel.TabIndex = 13;
            this.prodLabel.Text = "PR";
            this.prodLabel.Visible = false;
            // 
            // posLabel
            // 
            this.posLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.posLabel.AutoSize = true;
            this.posLabel.Location = new System.Drawing.Point(347, 221);
            this.posLabel.Name = "posLabel";
            this.posLabel.Size = new System.Drawing.Size(25, 13);
            this.posLabel.TabIndex = 12;
            this.posLabel.Text = "p    ";
            // 
            // debugParseChb
            // 
            this.debugParseChb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.debugParseChb.AutoSize = true;
            this.debugParseChb.Location = new System.Drawing.Point(337, 78);
            this.debugParseChb.Name = "debugParseChb";
            this.debugParseChb.Size = new System.Drawing.Size(95, 17);
            this.debugParseChb.TabIndex = 11;
            this.debugParseChb.Text = "Debug parsing";
            this.debugParseChb.UseVisualStyleBackColor = true;
            this.debugParseChb.CheckedChanged += new System.EventHandler(this.debugParseChb_CheckedChanged);
            // 
            // errLabel
            // 
            this.errLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errLabel.AutoSize = true;
            this.errLabel.Location = new System.Drawing.Point(340, 164);
            this.errLabel.Name = "errLabel";
            this.errLabel.Size = new System.Drawing.Size(30, 13);
            this.errLabel.TabIndex = 10;
            this.errLabel.Text = "ERR";
            this.errLabel.Visible = false;
            this.errLabel.Click += new System.EventHandler(this.errLabel_Click);
            // 
            // tkLabel
            // 
            this.tkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tkLabel.AutoSize = true;
            this.tkLabel.Location = new System.Drawing.Point(376, 164);
            this.tkLabel.Name = "tkLabel";
            this.tkLabel.Size = new System.Drawing.Size(21, 13);
            this.tkLabel.TabIndex = 9;
            this.tkLabel.Text = "TK";
            this.tkLabel.Visible = false;
            this.tkLabel.Click += new System.EventHandler(this.tkLabel_Click);
            // 
            // nlLabel
            // 
            this.nlLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nlLabel.AutoSize = true;
            this.nlLabel.Location = new System.Drawing.Point(403, 164);
            this.nlLabel.Name = "nlLabel";
            this.nlLabel.Size = new System.Drawing.Size(21, 13);
            this.nlLabel.TabIndex = 8;
            this.nlLabel.Text = "NL";
            this.nlLabel.Visible = false;
            // 
            // debugTokensChb
            // 
            this.debugTokensChb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.debugTokensChb.AutoSize = true;
            this.debugTokensChb.Location = new System.Drawing.Point(337, 55);
            this.debugTokensChb.Name = "debugTokensChb";
            this.debugTokensChb.Size = new System.Drawing.Size(93, 17);
            this.debugTokensChb.TabIndex = 7;
            this.debugTokensChb.Text = "Debug tokens";
            this.debugTokensChb.UseVisualStyleBackColor = true;
            this.debugTokensChb.CheckedChanged += new System.EventHandler(this.debugChb_CheckedChanged);
            // 
            // columnLabel
            // 
            this.columnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.columnLabel.AutoSize = true;
            this.columnLabel.Location = new System.Drawing.Point(347, 259);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(27, 13);
            this.columnLabel.TabIndex = 6;
            this.columnLabel.Text = "ln    ";
            // 
            // lineLabel
            // 
            this.lineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lineLabel.AutoSize = true;
            this.lineLabel.Location = new System.Drawing.Point(347, 241);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(25, 13);
            this.lineLabel.TabIndex = 5;
            this.lineLabel.Text = "c    ";
            // 
            // parseButton
            // 
            this.parseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parseButton.Location = new System.Drawing.Point(347, 132);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 4;
            this.parseButton.Text = "Parse";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // clearTablesBtn
            // 
            this.clearTablesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTablesBtn.Location = new System.Drawing.Point(343, 6);
            this.clearTablesBtn.Name = "clearTablesBtn";
            this.clearTablesBtn.Size = new System.Drawing.Size(75, 23);
            this.clearTablesBtn.TabIndex = 7;
            this.clearTablesBtn.Text = "Clear tables";
            this.clearTablesBtn.UseVisualStyleBackColor = true;
            this.clearTablesBtn.Click += new System.EventHandler(this.clearTablesBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.errorsTab);
            this.tabControl1.Controls.Add(this.tokensTab);
            this.tabControl1.Location = new System.Drawing.Point(3, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(419, 133);
            this.tabControl1.TabIndex = 6;
            // 
            // errorsTab
            // 
            this.errorsTab.Controls.Add(this.errorsTable);
            this.errorsTab.Location = new System.Drawing.Point(4, 22);
            this.errorsTab.Name = "errorsTab";
            this.errorsTab.Padding = new System.Windows.Forms.Padding(3);
            this.errorsTab.Size = new System.Drawing.Size(411, 107);
            this.errorsTab.TabIndex = 0;
            this.errorsTab.Text = "Errors";
            this.errorsTab.UseVisualStyleBackColor = true;
            // 
            // tokensTab
            // 
            this.tokensTab.Controls.Add(this.tokensTable);
            this.tokensTab.Location = new System.Drawing.Point(4, 22);
            this.tokensTab.Name = "tokensTab";
            this.tokensTab.Padding = new System.Windows.Forms.Padding(3);
            this.tokensTab.Size = new System.Drawing.Size(409, 107);
            this.tokensTab.TabIndex = 1;
            this.tokensTab.Text = "Tokens";
            this.tokensTab.UseVisualStyleBackColor = true;
            // 
            // tokensTable
            // 
            this.tokensTable.AllowUserToAddRows = false;
            this.tokensTable.AllowUserToDeleteRows = false;
            this.tokensTable.AllowUserToResizeRows = false;
            this.tokensTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tokensTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tokensTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TokenNameColumn,
            this.TokenIdColumn,
            this.TokenLineColumn,
            this.TokenColumnColumn,
            this.TokenLengthColumn,
            this.TokenIndexColumn,
            this.TokenPositionColumn});
            this.tokensTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tokensTable.Location = new System.Drawing.Point(3, 3);
            this.tokensTable.Name = "tokensTable";
            this.tokensTable.ReadOnly = true;
            this.tokensTable.RowHeadersVisible = false;
            this.tokensTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tokensTable.Size = new System.Drawing.Size(403, 101);
            this.tokensTable.TabIndex = 5;
            // 
            // TokenNameColumn
            // 
            this.TokenNameColumn.FillWeight = 500F;
            this.TokenNameColumn.HeaderText = "Token";
            this.TokenNameColumn.Name = "TokenNameColumn";
            this.TokenNameColumn.ReadOnly = true;
            // 
            // TokenIdColumn
            // 
            this.TokenIdColumn.HeaderText = "Id";
            this.TokenIdColumn.Name = "TokenIdColumn";
            this.TokenIdColumn.ReadOnly = true;
            // 
            // TokenLineColumn
            // 
            this.TokenLineColumn.HeaderText = "Line";
            this.TokenLineColumn.Name = "TokenLineColumn";
            this.TokenLineColumn.ReadOnly = true;
            // 
            // TokenColumnColumn
            // 
            this.TokenColumnColumn.HeaderText = "Column";
            this.TokenColumnColumn.Name = "TokenColumnColumn";
            this.TokenColumnColumn.ReadOnly = true;
            // 
            // TokenLengthColumn
            // 
            this.TokenLengthColumn.HeaderText = "Length";
            this.TokenLengthColumn.Name = "TokenLengthColumn";
            this.TokenLengthColumn.ReadOnly = true;
            // 
            // TokenIndexColumn
            // 
            this.TokenIndexColumn.HeaderText = "Index";
            this.TokenIndexColumn.Name = "TokenIndexColumn";
            this.TokenIndexColumn.ReadOnly = true;
            // 
            // TokenPositionColumn
            // 
            this.TokenPositionColumn.HeaderText = "Position";
            this.TokenPositionColumn.Name = "TokenPositionColumn";
            this.TokenPositionColumn.ReadOnly = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.CollapseButtons = GreenZoneUtil.Gui.CollapseButtons.None;
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer2.LeftGap = 0;
            this.splitContainer2.Location = new System.Drawing.Point(3, 60);
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
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel2CollapsedByClient = false;
            this.splitContainer2.Panel2NormalSize = 0;
            this.splitContainer2.RightGap = 0;
            this.splitContainer2.Size = new System.Drawing.Size(580, 440);
            this.splitContainer2.SplitterDistance = 143;
            this.splitContainer2.TabIndex = 7;
            this.splitContainer2.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(143, 440);
            this.treeView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(586, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // browseXmlButton
            // 
            this.browseXmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseXmlButton.Location = new System.Drawing.Point(535, 32);
            this.browseXmlButton.Name = "browseXmlButton";
            this.browseXmlButton.Size = new System.Drawing.Size(37, 23);
            this.browseXmlButton.TabIndex = 9;
            this.browseXmlButton.Text = "Xml";
            this.browseXmlButton.UseVisualStyleBackColor = true;
            this.browseXmlButton.Click += new System.EventHandler(this.browseXmlButton_Click);
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourceTextBox.HideSelection = false;
            this.sourceTextBox.Location = new System.Drawing.Point(13, 0);
            this.sourceTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(318, 275);
            this.sourceTextBox.TabIndex = 0;
            this.sourceTextBox.Text = "";
            this.sourceTextBox.WordWrap = false;
            // 
            // ParseTesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 501);
            this.Controls.Add(this.browseXmlButton);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.browseCsButton);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ParseTesterForm";
            this.Text = "ParseTesterForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorsTable)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.errorsTab.ResumeLayout(false);
            this.tokensTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tokensTable)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBoxEx sourceTextBox;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Button browseCsButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.DataGridView errorsTable;
        private System.Windows.Forms.Label infoLabel;
        private GreenZoneUtil.Gui.SplitContainerEx splitContainer1;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage errorsTab;
        private System.Windows.Forms.TabPage tokensTab;
        private System.Windows.Forms.DataGridView tokensTable;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn errorMessageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorLineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorColumnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorLengthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorPositionColumn;
        private System.Windows.Forms.CheckBox debugTokensChb;
        private System.Windows.Forms.Label errLabel;
        private System.Windows.Forms.Label tkLabel;
        private System.Windows.Forms.Label nlLabel;
        private System.Windows.Forms.Button clearTablesBtn;
        private GreenZoneUtil.Gui.SplitContainerEx splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox debugParseChb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.Label posLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenLineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenColumnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenLengthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenIndexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TokenPositionColumn;
        private System.Windows.Forms.Label addNodeLabel;
        private System.Windows.Forms.Label prodLabel;
        private System.Windows.Forms.Label curProdInfLabel;
        private System.Windows.Forms.Panel breakPointBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorObjectColumn;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.Button browseXmlButton;
        private System.Windows.Forms.CheckBox showTokensChb;
    }
}

