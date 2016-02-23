using GreenZoneUtil.Gui;
namespace GreenZoneFxEngine
{
    partial class Form1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Environments");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Symbols");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Indicators");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("EAs");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Scripts");
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lowLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.HLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.highLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.closeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new GreenZoneUtil.Gui.SplitContainerEx();
            this.tabControl1 = new GreenZoneUtil.Gui.CoolTabControl();
            this.launcherTabControl = new System.Windows.Forms.TabControl();
            this.eaTesterTab = new System.Windows.Forms.TabPage();
            this.eaTesterPanel1 = new GreenZoneFxEngine.EATesterPanel();
            this.scriptRunnerTab = new System.Windows.Forms.TabPage();
            this.scriptRunnerPanel1 = new GreenZoneFxEngine.ScriptRunnerPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEnvironmentAssistantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateEnvironmentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editEnvorinmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testerProptertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.environmentCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.valueLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.VLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.launcherTabControl.SuspendLayout();
            this.eaTesterTab.SuspendLayout();
            this.scriptRunnerTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(792, 545);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(792, 616);
            this.toolStripContainer1.TabIndex = 31;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripStatusLabel2,
            this.timeLabel,
            this.OLabel,
            this.openLabel,
            this.LLabel,
            this.lowLabel,
            this.HLabel,
            this.highLabel,
            this.CLabel,
            this.closeLabel,
            this.VLabel,
            this.valueLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(184, 17);
            this.statusLabel.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabel2.Text = "Bar:";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = false;
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(115, 17);
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OLabel
            // 
            this.OLabel.Name = "OLabel";
            this.OLabel.Size = new System.Drawing.Size(15, 17);
            this.OLabel.Text = "O";
            // 
            // openLabel
            // 
            this.openLabel.AutoSize = false;
            this.openLabel.Name = "openLabel";
            this.openLabel.Size = new System.Drawing.Size(50, 17);
            this.openLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LLabel
            // 
            this.LLabel.Name = "LLabel";
            this.LLabel.Size = new System.Drawing.Size(12, 17);
            this.LLabel.Text = "L";
            // 
            // lowLabel
            // 
            this.lowLabel.AutoSize = false;
            this.lowLabel.Name = "lowLabel";
            this.lowLabel.Size = new System.Drawing.Size(50, 17);
            this.lowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HLabel
            // 
            this.HLabel.Name = "HLabel";
            this.HLabel.Size = new System.Drawing.Size(14, 17);
            this.HLabel.Text = "H";
            // 
            // highLabel
            // 
            this.highLabel.AutoSize = false;
            this.highLabel.Name = "highLabel";
            this.highLabel.Size = new System.Drawing.Size(50, 17);
            this.highLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CLabel
            // 
            this.CLabel.Name = "CLabel";
            this.CLabel.Size = new System.Drawing.Size(14, 17);
            this.CLabel.Text = "C";
            // 
            // closeLabel
            // 
            this.closeLabel.AutoSize = false;
            this.closeLabel.Name = "closeLabel";
            this.closeLabel.Size = new System.Drawing.Size(50, 17);
            this.closeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Enabled = false;
            this.splitContainer1.Location = new System.Drawing.Point(3, 13);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 600;
            this.splitContainer1.Size = new System.Drawing.Size(789, 532);
            this.splitContainer1.SplitterDistance = 128;
            this.splitContainer1.TabIndex = 11;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Environments";
            treeNode1.Text = "Environments";
            treeNode2.Name = "Symbols";
            treeNode2.Text = "Symbols";
            treeNode3.Name = "Indicators";
            treeNode3.Text = "Indicators";
            treeNode4.Name = "EAs";
            treeNode4.Text = "EAs";
            treeNode5.Name = "Scripts";
            treeNode5.Text = "Scripts";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(128, 532);
            this.treeView1.TabIndex = 21;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel1MinSize = 250;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.launcherTabControl);
            this.splitContainer2.Size = new System.Drawing.Size(657, 532);
            this.splitContainer2.SplitterDistance = 251;
            this.splitContainer2.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.AddTabText = "Add Tab";
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DraggableAlignments = ((GreenZoneUtil.Gui.FlagTabAlignments)((GreenZoneUtil.Gui.FlagTabAlignments.Top | GreenZoneUtil.Gui.FlagTabAlignments.Bottom)));
            this.tabControl1.ItemSize = new System.Drawing.Size(50, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(657, 251);
            this.tabControl1.TabIndex = 26;
            // 
            // launcherTabControl
            // 
            this.launcherTabControl.Controls.Add(this.eaTesterTab);
            this.launcherTabControl.Controls.Add(this.scriptRunnerTab);
            this.launcherTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.launcherTabControl.Location = new System.Drawing.Point(0, 0);
            this.launcherTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.launcherTabControl.Name = "launcherTabControl";
            this.launcherTabControl.SelectedIndex = 0;
            this.launcherTabControl.Size = new System.Drawing.Size(657, 277);
            this.launcherTabControl.TabIndex = 28;
            // 
            // eaTesterTab
            // 
            this.eaTesterTab.BackColor = System.Drawing.SystemColors.ControlLight;
            this.eaTesterTab.Controls.Add(this.eaTesterPanel1);
            this.eaTesterTab.Location = new System.Drawing.Point(4, 22);
            this.eaTesterTab.Margin = new System.Windows.Forms.Padding(0);
            this.eaTesterTab.Name = "eaTesterTab";
            this.eaTesterTab.Size = new System.Drawing.Size(649, 251);
            this.eaTesterTab.TabIndex = 0;
            this.eaTesterTab.Text = "EA tester";
            // 
            // eaTesterPanel1
            // 
            this.eaTesterPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eaTesterPanel1.AutoScroll = true;
            this.eaTesterPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.eaTesterPanel1.Location = new System.Drawing.Point(0, 0);
            this.eaTesterPanel1.Name = "eaTesterPanel1";
            this.eaTesterPanel1.Size = new System.Drawing.Size(649, 251);
            this.eaTesterPanel1.TabIndex = 26;
            // 
            // scriptRunnerTab
            // 
            this.scriptRunnerTab.BackColor = System.Drawing.SystemColors.ControlLight;
            this.scriptRunnerTab.Controls.Add(this.scriptRunnerPanel1);
            this.scriptRunnerTab.Location = new System.Drawing.Point(4, 22);
            this.scriptRunnerTab.Name = "scriptRunnerTab";
            this.scriptRunnerTab.Padding = new System.Windows.Forms.Padding(3);
            this.scriptRunnerTab.Size = new System.Drawing.Size(649, 251);
            this.scriptRunnerTab.TabIndex = 1;
            this.scriptRunnerTab.Text = "Script runner";
            // 
            // scriptRunnerPanel1
            // 
            this.scriptRunnerPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptRunnerPanel1.AutoScroll = true;
            this.scriptRunnerPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.scriptRunnerPanel1.Location = new System.Drawing.Point(0, 0);
            this.scriptRunnerPanel1.Name = "scriptRunnerPanel1";
            this.scriptRunnerPanel1.Size = new System.Drawing.Size(649, 251);
            this.scriptRunnerPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 32;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exportToolStripMenuItem.Text = "E&xport";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigatorToolStripMenuItem,
            this.ordersOverviewToolStripMenuItem,
            this.ordersTableToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // navigatorToolStripMenuItem
            // 
            this.navigatorToolStripMenuItem.Checked = true;
            this.navigatorToolStripMenuItem.CheckOnClick = true;
            this.navigatorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.navigatorToolStripMenuItem.Name = "navigatorToolStripMenuItem";
            this.navigatorToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.navigatorToolStripMenuItem.Text = "&Navigator";
            // 
            // ordersOverviewToolStripMenuItem
            // 
            this.ordersOverviewToolStripMenuItem.CheckOnClick = true;
            this.ordersOverviewToolStripMenuItem.Name = "ordersOverviewToolStripMenuItem";
            this.ordersOverviewToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.ordersOverviewToolStripMenuItem.Text = "Orders Overview";
            // 
            // ordersTableToolStripMenuItem
            // 
            this.ordersTableToolStripMenuItem.CheckOnClick = true;
            this.ordersTableToolStripMenuItem.Name = "ordersTableToolStripMenuItem";
            this.ordersTableToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.ordersTableToolStripMenuItem.Text = "Orders Table";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEnvironmentAssistantToolStripMenuItem,
            this.updateEnvironmentMenuItem,
            this.editEnvorinmentsToolStripMenuItem,
            this.loadSessionToolStripMenuItem,
            this.saveSessionToolStripMenuItem,
            this.toolStripSeparator2,
            this.showConsoleToolStripMenuItem,
            this.testerProptertiesToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // newEnvironmentAssistantToolStripMenuItem
            // 
            this.newEnvironmentAssistantToolStripMenuItem.Name = "newEnvironmentAssistantToolStripMenuItem";
            this.newEnvironmentAssistantToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.newEnvironmentAssistantToolStripMenuItem.Text = "New Environment &Assistant";
            // 
            // updateEnvironmentMenuItem
            // 
            this.updateEnvironmentMenuItem.Name = "updateEnvironmentMenuItem";
            this.updateEnvironmentMenuItem.Size = new System.Drawing.Size(216, 22);
            this.updateEnvironmentMenuItem.Text = "Update Environment";
            // 
            // editEnvorinmentsToolStripMenuItem
            // 
            this.editEnvorinmentsToolStripMenuItem.Name = "editEnvorinmentsToolStripMenuItem";
            this.editEnvorinmentsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.editEnvorinmentsToolStripMenuItem.Text = "Edit &Environments";
            // 
            // loadSessionToolStripMenuItem
            // 
            this.loadSessionToolStripMenuItem.Name = "loadSessionToolStripMenuItem";
            this.loadSessionToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadSessionToolStripMenuItem.Text = "&Load Session";
            // 
            // saveSessionToolStripMenuItem
            // 
            this.saveSessionToolStripMenuItem.Name = "saveSessionToolStripMenuItem";
            this.saveSessionToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.saveSessionToolStripMenuItem.Text = "&Save Session";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(213, 6);
            // 
            // showConsoleToolStripMenuItem
            // 
            this.showConsoleToolStripMenuItem.Name = "showConsoleToolStripMenuItem";
            this.showConsoleToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showConsoleToolStripMenuItem.Text = "Show &Console";
            // 
            // testerProptertiesToolStripMenuItem
            // 
            this.testerProptertiesToolStripMenuItem.Name = "testerProptertiesToolStripMenuItem";
            this.testerProptertiesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.testerProptertiesToolStripMenuItem.Text = "Tester &Propterties";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.environmentCombo,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 34;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel1.Text = "  Environment:";
            // 
            // environmentCombo
            // 
            this.environmentCombo.AutoSize = false;
            this.environmentCombo.Name = "environmentCombo";
            this.environmentCombo.Size = new System.Drawing.Size(121, 21);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripButton1.Text = "&Edit";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(46, 22);
            this.toolStripButton2.Text = "&Update";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(32, 22);
            this.toolStripButton3.Text = "&New";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = false;
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(50, 17);
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VLabel
            // 
            this.VLabel.Name = "VLabel";
            this.VLabel.Size = new System.Drawing.Size(13, 17);
            this.VLabel.Text = "V";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 616);
            this.Controls.Add(this.toolStripContainer1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "GreenZoneFX EAnalyzer";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.launcherTabControl.ResumeLayout(false);
            this.eaTesterTab.ResumeLayout(false);
            this.scriptRunnerTab.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GreenZoneUtil.Gui.SplitContainerEx splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newEnvironmentAssistantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateEnvironmentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editEnvorinmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem showConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testerProptertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox environmentCombo;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripStatusLabel closeLabel;
        private GreenZoneUtil.Gui.SplitContainerEx splitContainer2;
        internal CoolTabControl tabControl1;
        private System.Windows.Forms.TabControl launcherTabControl;
        private System.Windows.Forms.TabPage eaTesterTab;
        private EATesterPanel eaTesterPanel1;
        private System.Windows.Forms.TabPage scriptRunnerTab;
        private ScriptRunnerPanel scriptRunnerPanel1;
        internal System.Windows.Forms.ToolStripStatusLabel timeLabel;
        internal System.Windows.Forms.ToolStripStatusLabel openLabel;
        internal System.Windows.Forms.ToolStripStatusLabel lowLabel;
        internal System.Windows.Forms.ToolStripStatusLabel highLabel;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        internal System.Windows.Forms.ToolStripStatusLabel OLabel;
        internal System.Windows.Forms.ToolStripStatusLabel LLabel;
        internal System.Windows.Forms.ToolStripStatusLabel HLabel;
        internal System.Windows.Forms.ToolStripStatusLabel CLabel;
        private System.Windows.Forms.ToolStripMenuItem ordersOverviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersTableToolStripMenuItem;
        internal System.Windows.Forms.ToolStripStatusLabel VLabel;
        internal System.Windows.Forms.ToolStripStatusLabel valueLabel;
    }
}

