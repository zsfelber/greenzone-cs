using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.Util;

namespace GreenZoneUtil.Gui
{
    public class WormSplitContainer : Panel
    {
        Orientation orientation;
        int splitterWidth;
        SplitContainerEx first;
        SplitContainerEx last;

        public WormSplitContainer()
        {
            Create1Split();
            Orientation = Orientation.Horizontal;
        }

        void Create1Split()
        {
            last = first = CreateSplitContainerEx();
            last.Panel2Collapsed = true;
            splitterWidth = first.SplitterWidth;
            first.Location = new Point(0, 0);
            first.Dock = DockStyle.Fill;
            Controls.Add(first);
        }

        public void Clear()
        {
            SuspendLayout();
            TWinFct.LockControlUpdate(this);

            try
            {
                Controls.Remove(first);
                Create1Split();
            }
            finally
            {
                TWinFct.UnLockControlUpdate(this);
                ResumeLayout();
            }
        }

        public List<Control> ChildControls
        {
            get
            {
                List<Control> result = new List<Control>();
                SplitContainerEx s = first;
                while (true)
                {
                    if (s.Panel1.HasChildren)
                    {
                        Control child = s.Panel1.Controls[0];
                        result.Add(child);
                    }
                    else
                    {
                        break;
                    }
                    if (s.Panel2.HasChildren)
                    {
                        s = (SplitContainerEx)s.Panel2.Controls[0];
                    }
                    else
                    {
                        break;
                    }
                }
                return result;
            }
        }

        
        
        public Orientation Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
                SplitContainerEx s = first;
                while (true)
                {
                    s.Orientation = value;
                    if (s.Panel2.HasChildren)
                    {
                        s = (SplitContainerEx)s.Panel2.Controls[0];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public int SplitterWidth
        {
            get
            {
                return splitterWidth;
            }
            set
            {
                splitterWidth = value;
                SplitContainerEx s = first;
                while (true)
                {
                    s.SplitterWidth = value;
                    if (s.Panel2.HasChildren)
                    {
                        s = (SplitContainerEx)s.Panel2.Controls[0];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        public int WormSize
        {
            get
            {
                switch (orientation)
                {
                    case Orientation.Horizontal: return Height;
                    case Orientation.Vertical: return Width;
                }
                return 0;
            }
        }
        
        public List<double> Ratio
        {
            get
            {
                List<double> ratio = new List<double>();
                int sum = 0;
                SplitContainerEx s = first;
                while (s.Panel1.HasChildren)
                {
                    int size = 0;
                    if (s.Panel2Collapsed)
                    {
                        switch (orientation)
                        {
                            case Orientation.Horizontal: size = s.Height; break;
                            case Orientation.Vertical: size = s.Width; break;
                        }
                        ratio.Add(size);
                        sum += size;
                        break;
                    }
                    else
                    {
                        size = s.SplitterDistance;
                        ratio.Add(size);
                        sum += size;
                        s = (SplitContainerEx)s.Panel2.Controls[0];
                    }
                }
                for (int i = 0; i < ratio.Count; i++)
                {
                    ratio[i] /= sum;
                }
                return ratio;
            }
            set
            {
                List<double> ratio = value;
                double sum1 = 0;
                for (int i = 0; i < ratio.Count; i++)
                {
                    sum1 += ratio[i];
                }

                double h = WormSize - (ratio.Count - 1) * splitterWidth;

                double sum2 = 0;
                List<double> hs = new List<double>();
                List<double> sumr = new List<double>();
                for (int i = ratio.Count - 1; i >= 0; i--)
                {
                    double r = ratio[i] / sum1;
                    double sh = r * h + splitterWidth / 2.0;
                    double srh = r * h;
                    sum2 += srh;

                    hs.Insert(0, sh);
                    sumr.Insert(0, sum2);

                    sum2 += splitterWidth;
                }

                SplitContainerEx s = first;
                for (int i = 0; i < ratio.Count; i++)
                {
                    int sh = (int) Math.Round(hs[i]);
                    int srh = (int) Math.Round(sumr[i]);
                    int x = 0;
                    switch (orientation)
                    {
                        case Orientation.Horizontal:
                            s.Height = (int)srh;
                            x = s.Height;
                            break;
                        case Orientation.Vertical:
                            s.Width = (int)srh; 
                            x = s.Width;
                            break;
                    }
                    try
                    {
                        s.SplitterDistance = Math.Min(sh, x);
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    if (s.Panel2.HasChildren)
                    {
                        s = (SplitContainerEx)s.Panel2.Controls[0];
                    }
                    else if (i != ratio.Count-1)
                    {
                        throw new NotSupportedException("i != ratio.Count-1  " + i + " != " + (ratio.Count - 1));
                    }
                }
            }
        }

        public void Add(Control control)
        {
            SuspendLayout();
            TWinFct.LockControlUpdate(this);

            try
            {
                List<double> ratio = Ratio;
                if (ratio.Count > 0)
                {
                    ratio.Add(ratio[ratio.Count - 1]);
                }
                else
                {
                    ratio.Add(1);
                }

                if (last.Panel1.Controls.Count > 0)
                {
                    SplitContainerEx newSc = CreateSplitContainerEx();
                    newSc.Orientation = orientation;
                    newSc.Location = new Point(0, 0);
                    newSc.Dock = DockStyle.Fill;
                    newSc.Panel2Collapsed = true;

                    last.Panel2.Controls.Add(newSc);
                    last.Panel2Collapsed = false;
                    last = newSc;
                }

                control.Location = new Point(0, 0);
                control.Dock = DockStyle.Fill;
                last.Panel1.Controls.Add(control);

                Ratio = ratio;
            }
            finally
            {
                TWinFct.UnLockControlUpdate(this);
                ResumeLayout();
            }
        }

        public void Remove(Control control)
        {
            remove(control, false);
        }

        public void RemoveFrom(Control control)
        {
            remove(control, true);
        }

        public void remove(Control control, bool removeAll)
        {
            SuspendLayout();
            TWinFct.LockControlUpdate(this);

            try
            {
                FoundContainer fc = Find(control);
                SplitContainerEx sc = fc.splitContainer;

                List<double> ratio = Ratio;
                double delta = ratio[fc.index];
                ratio[0] += delta;

                if (removeAll)
                {
                    ratio.RemoveRange(fc.index, ratio.Count - fc.index);
                }
                else
                {
                    ratio.RemoveAt(fc.index);
                }

                sc.Panel1.Controls.Remove(control);

                SplitContainerEx after = null;
                if (sc != last)
                {
                    after = (SplitContainerEx)sc.Panel2.Controls[0];
                    sc.Panel2.Controls.Remove(after);
                    if (removeAll)
                    {
                        after = null;
                    }
                }


                if (sc != first)
                {
                    SplitContainerEx before = (SplitContainerEx)sc.Parent.Parent;
                    before.Panel2.Controls.Remove(sc);
                    if (after != null)
                    {
                        before.Panel2.Controls.Add(after);
                    }
                    else
                    {
                        before.Panel2Collapsed = true;
                        last = before;
                    }
                }
                else
                {
                    if (after != null)
                    {
                        Controls.Remove(sc);
                        Controls.Add(after);
                        first = after;
                    }
                    else
                    {
                        last = first;
                    }
                }

                Ratio = ratio;

            }
            finally
            {
                TWinFct.UnLockControlUpdate(this);
                ResumeLayout();
            }
        }

        public FoundContainer Find(Control control)
        {
            SplitContainerEx s = first;
            int index = 0;
            while (true)
            {
                if (s.Panel1.Controls[0] == control)
                {
                    FoundContainer r = new FoundContainer(index, s);
                    return r;
                }
                if (s.Panel2.HasChildren)
                {
                    s = (SplitContainerEx)s.Panel2.Controls[0];
                }
                else
                {
                    break;
                }
                index++;
            }
            return null;
        }

        private SplitContainerEx CreateSplitContainerEx()
        {
            SplitContainerEx r = new SplitContainerEx();
            r.Orientation = orientation;
            r.Panel1MinSize = 0;
            r.Panel2MinSize = 0;
            return r;
        }
    }

    public class FoundContainer
    {
        public readonly int index;
        public readonly SplitContainerEx splitContainer;

        internal FoundContainer(int index, SplitContainerEx splitContainer)
        {
            this.index = index;
            this.splitContainer = splitContainer;
        }
    }
}
