using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EAnalyzer_CS
{
    public abstract partial class EAForm : Form
    {
        private int id;

        private ZsfTraderConfig config;

        private UIThread uiThread;

        private TraderNode rootNode;

        private bool finished;

        public EAForm(int id, ZsfTraderConfig config)
        {
            this.id = id;
            this.config = config;
            this.uiThread = new UIThread(this, new ThreadStart(startImpl));
            this.finished = false;
            Visible = false;
            InitializeComponent();
        }

        public int Id
        {
            get
            {
                return id;
            }
        }
        public UIThread UIThread
        {
            get
            {
                return uiThread;
            }
        }
        public TraderNode RootNode
        {
            get
            {
                return rootNode;
            }
        }
        public TreeView TreeView
        {
            get
            {
                return treeView1;
            }
        }
        public SplitContainer SplitContainer
        {
            get {
                return splitContainer1;
            }
        }

        public bool Finished
        {
            get
            {
                return finished;
            }
        }

        public void Start()
        {
            EALogger.Log("EAForm.start()", EALogger.SEV_DEBUG_1);
            uiThread.Start();
        }
        private void startImpl()
        {
            EALogger.Log("EAForm.startImpl()", EALogger.SEV_DEBUG_1);
            Application.Run(this);
            Utils.EnterExisting(config, "EAForm.startImpl()");
            try
            {
                finished = true;
            }
            finally {
                Utils.ExitExisting(config, "EAForm.startImpl()");
            }
        }

        public void InitializeTree()
        {
            EALogger.Log("EAForm.InitializeTree()", EALogger.SEV_DEBUG_1);
            rootNode = new TraderNode(this, config);
            treeView1.Nodes.Add(rootNode);
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);

            Visible = true;
        }

        public void MarkLoaded()
        {
            Utils.EnterExisting(config, "EAForm.MarkLoaded()");
            try
            {
                //progressBar1->Style = ProgressBarStyle::Blocks;
            }
            finally {
                Utils.ExitExisting(config, "EAForm.MarkLoaded()");
            }
        }


        protected abstract void EAForm_FormClosed(object sender, FormClosedEventArgs e);

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ((EATreeNode)e.Node).AfterSelect(sender, e);
        }

        private void EAForm_Load(object sender, EventArgs e)
        {
            EALogger.Log("EAForm.EAForm_Load()", EALogger.SEV_DEBUG_1);
            InitializeTree();
        }
    }
}
