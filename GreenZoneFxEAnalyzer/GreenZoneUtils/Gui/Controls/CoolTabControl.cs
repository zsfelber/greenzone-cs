using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using GreenZoneUtil.Util;
using GreenZoneUtil.Properties;

namespace GreenZoneUtil.Gui
{
    [Flags]
    public enum FlagTabAlignments
    {
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        TopBottom = 5,
        LeftRight = 10,
        All = 15
    }

    //[ToolboxBitmap(typeof(DraggableTabControl))]
    /// <summary>
    /// Summary description for DraggableTabPage.
    /// </summary>
    public class CoolTabControl : System.Windows.Forms.TabControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ImageList imageList1;

        public event EventHandler AddTabClick;

        bool initialized;

        public CoolTabControl()
        {
            this.components = new System.ComponentModel.Container();
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);

            ImageList = this.imageList1;

            // 
            // imageList1
            // 
            this.imageList1.Images.Add(Resources.AddMark_10580);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AddMark_10580.png");

            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage1.Size = new System.Drawing.Size(649, 225);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.ImageIndex = 0;
            this.button5.ImageList = this.imageList1;
            this.button5.Location = new System.Drawing.Point(3, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Add Tab";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new EventHandler(button5_Click);

            TabPages.Add(tabPage1);



            AllowDrop = true;
            // TODO: Add any initialization after the InitForm call
            initialized = true;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public FlagTabAlignments DraggableAlignments
        {
            get;
            set;
        }

        public string AddTabText
        {
            get
            {
                return this.button5.Text;
            }
            set
            {
                this.button5.Text = value;
            }
        }

        bool enableAddTabButton = true;
        public bool EnableAddTabButton
        {
            get
            {
                return enableAddTabButton;
            }
        }

        int FixedLastTabs
        {
            get
            {
                return EnableAddTabButton ? 1 : 0;
            }
        }
        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        bool _MoveTab = false;
        public void MoveTab(TabPage drag_tab, int drop_location_index)
        {
            if (!_MoveTab)
            {
                try
                {
                    _MoveTab = true;

                    int item_drag_index = FindIndex(drag_tab);
                    ArrayList pages = new ArrayList();

                    //Put all tab pages into an array.
                    for (int i = 0; i < TabPages.Count; i++)
                    {
                        //Except the one we are dragging.
                        if (i != item_drag_index)
                            pages.Add(TabPages[i]);
                    }

                    //Now put the one we are dragging it at the proper location.
                    pages.Insert(drop_location_index, drag_tab);

                    //Make them all go away for a nanosec.
                    TabPages.Clear();

                    //Add them all back in.
                    TabPages.AddRange((TabPage[])pages.ToArray(typeof(TabPage)));

                    //Make sure the drag tab is selected.
                    SelectedTab = drag_tab;
                }
                finally {
                    _MoveTab = false;
                }
            }
        }

        protected override void OnDragOver(System.Windows.Forms.DragEventArgs e)
        {
            base.OnDragOver(e);

            Point pt = new Point(e.X, e.Y);
            //We need client coordinates.
            pt = PointToClient(pt);

            //Get the tab we are hovering over.
            TabPage hover_tab = GetTabPageByTab(pt);

            //Make sure we are on a tab.
            if (hover_tab != null)
            {
                //Make sure there is a TabPage being dragged.
                if (e.Data.GetDataPresent(typeof(TabPage)))
                {
                    e.Effect = DragDropEffects.Move;
                    TabPage drag_tab = (TabPage)e.Data.GetData(typeof(TabPage));
                    int drop_location_index = FindIndex(hover_tab);

                    //Don't do anything if we are hovering over ourself.
                    if (drop_location_index != -1 && drag_tab != hover_tab)
                    {
                        SuspendLayout();
                        TWinFct.LockControlUpdate(this);

                        try
                        {
                            MoveTab(drag_tab, drop_location_index);
                        }
                        finally
                        {
                            TWinFct.UnLockControlUpdate(this);
                            ResumeLayout();
                        }
                    }
                }
            }
            else
            {
                Rectangle tabRect = new Rectangle();
                for (int i = 0; i < TabPages.Count; )
                {
                    tabRect = GetTabRect(i);
                    break;
                }

                Rectangle r1, r2, r3, r4;
                r1 = new Rectangle(0, 0, Width, tabRect.Height);
                r2 = new Rectangle(0, Height - tabRect.Height, Width, tabRect.Height);
                r3 = new Rectangle(0, tabRect.Height, tabRect.Height, Height - tabRect.Height);
                r4 = new Rectangle(Width - tabRect.Height, tabRect.Height, tabRect.Height, Height - tabRect.Height);

                if (r1.Contains(pt))
                {
                    if ((DraggableAlignments & FlagTabAlignments.Top) != 0 && Alignment != TabAlignment.Top)
                    {
                        e.Effect = DragDropEffects.Move;
                        Alignment = TabAlignment.Top;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else if (r2.Contains(pt))
                {
                    if ((DraggableAlignments & FlagTabAlignments.Bottom) != 0 && Alignment != TabAlignment.Bottom)
                    {
                        e.Effect = DragDropEffects.Move;
                        Alignment = TabAlignment.Bottom;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else if (r3.Contains(pt))
                {
                    if ((DraggableAlignments & FlagTabAlignments.Left) != 0 && Alignment != TabAlignment.Left)
                    {
                        e.Effect = DragDropEffects.Move;
                        Alignment = TabAlignment.Left;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else if (r4.Contains(pt))
                {
                    if ((DraggableAlignments & FlagTabAlignments.Right) != 0 && Alignment != TabAlignment.Right)
                    {
                        e.Effect = DragDropEffects.Move;
                        Alignment = TabAlignment.Right;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point pt = new Point(e.X, e.Y);
            TabPage tp = GetTabPageByTab(pt);

            if (tp != null)
            {
                DoDragDrop(tp, DragDropEffects.All);
            }
        }

        /// <summary>
        /// Finds the TabPage whose tab is contains the given point.
        /// </summary>
        /// <param name="pt">The point (given in client coordinates) to look for a TabPage.</param>
        /// <returns>The TabPage whose tab is at the given point (null if there isn't one).</returns>
        private TabPage GetTabPageByTab(Point pt)
        {
            TabPage tp = null;

            for (int i = 0; i < TabPages.Count; i++)
            {
                if (GetTabRect(i).Contains(pt))
                {
                    tp = TabPages[i];
                    break;
                }
            }

            return tp;
        }

        /// <summary>
        /// Loops over all the TabPages to find the index of the given TabPage.
        /// </summary>
        /// <param name="page">The TabPage we want the index for.</param>
        /// <returns>The index of the given TabPage(-1 if it isn't found.)</returns>
        private int FindIndex(TabPage page)
        {
            for (int i = 0; i < TabPages.Count - FixedLastTabs; i++)
            {
                if (TabPages[i] == page)
                    return i;
            }

            return -1;
        }

        void RefreshAddTabButton()
        {
            if (!_OnControlAdded && !_MoveTab)
            {
                if (TabCount > 1)
                {
                    tabPage1.Text = button5.Text;
                    tabPage1.ImageIndex = 0;
                }
                else
                {
                    tabPage1.Text = "";
                    tabPage1.ImageIndex = -1;
                }
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (!_OnControlAdded)
            {
                if (TabCount >= 2 && SelectedIndex == TabCount - 1)
                {
                    if (AddTabClick != null)
                    {
                        AddTabClick(tabPage1, e);
                    }
                }
                else
                {
                    base.OnSelectedIndexChanged(e);
                }
            }
        }

        bool _OnControlAdded = false;
        protected override void OnControlAdded(ControlEventArgs e)
        {
            try
            {
                base.OnControlAdded(e);

                if (!_OnControlAdded && !_MoveTab)
                {
                    RefreshAddTabButton();
                    _OnControlAdded = true;

                    if (e.Control != tabPage1 && TabPages[TabCount - 1] != tabPage1)
                    {
                        TabPages.Remove(tabPage1);
                        TabPages.Add(tabPage1);
                    }
                }
            }
            finally
            {
                _OnControlAdded = false;
                int si = FindIndex((TabPage)e.Control);
                SelectedIndex = si;
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (!_OnControlAdded)
            {
                base.OnControlRemoved(e);
                RefreshAddTabButton();
            }
        }

        void button5_Click(object sender, EventArgs e)
        {
            if (AddTabClick != null)
            {
                AddTabClick(sender, e);
            }
        }
    }
}
