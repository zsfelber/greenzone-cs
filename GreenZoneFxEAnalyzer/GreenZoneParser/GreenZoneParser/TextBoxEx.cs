using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GreenZoneParser
{
    public class RichTextBoxEx : RichTextBox
    {
        public new event PaintEventHandler Paint;

        public int CurrentColumn
        {
            get { return this.SelectionStart - this.GetFirstCharIndexOfCurrentLine() + 1; }
        }

        public int CurrentLine
        {
            get { return this.GetLineFromCharIndex(this.SelectionStart) + 1; }
        }

        public int NumberOfVisibleLines
        {
            get
            {
                int topIndex = GetCharIndexFromPosition(new Point(1, 1));
                int bottomIndex = GetCharIndexFromPosition(new Point(1, Height - 1));
                int topLine = GetLineFromCharIndex(topIndex);
                int bottomLine = GetLineFromCharIndex(bottomIndex);
                int n = bottomLine - topLine + 1;
                return n;
            }
        }

        public int LineHeight
        {
            get
            {
                int cix = GetFirstCharIndexFromLine(1);

                Point top = GetPositionFromCharIndex(0);
                Point bottom = GetPositionFromCharIndex(cix);

                int h = bottom.Y - top.Y;
                return h;
            }
        }

        public void GoTo(int line, int column, int length = 0)
        {
            //this.Focus();
            int ix = this.GetFirstCharIndexFromLine(line - 1) + column - 1;

            int startLine = GetLineFromCharIndex(ix);
            int numVisibleLines = NumberOfVisibleLines;

            int cix = GetFirstCharIndexFromLine(Math.Max(0, startLine - numVisibleLines / 3 + 1));
            Select(cix, 0);
            ScrollToCaret();

            Select(ix, length);
        }

        public int GetLineY(int line)
        {
            line--;
            int cix = GetFirstCharIndexFromLine(line);
            Point p = GetPositionFromCharIndex(cix);
            return p.Y;
        }

        public int GetYLine(int y)
        {
            int cix = GetCharIndexFromPosition(new Point(0, y));
            int line = GetLineFromCharIndex(cix);
            line++;
            return line;
        }

        //protected override void OnSelectionChanged(EventArgs e)
        //{
        //    base.OnSelectionChanged(e);
        //}

        private const int WM_PAINT = 15;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                if (Paint != null)
                {
                    Paint(this, null);
                }
            }

        }
    }
}