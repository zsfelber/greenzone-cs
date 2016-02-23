using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GreenZoneUtil.Gui
{
    public class ContextMenuStripWTitle : ContextMenuStrip
    {

        public ContextMenuStripWTitle()
        {
        }

        public ContextMenuStripWTitle(string text)
        {
            Text = text;
        }

        ContextMenuHeader titleLabel;
        public ContextMenuHeader TitleLabel
        {
            get
            {
                return titleLabel;
            }
            set
            {
                if (titleLabel != value)
                {
                    if (titleLabel != null)
                    {
                        Items.Remove(titleLabel);
                    }

                    titleLabel = value;
                    
                    if (titleLabel != null)
                    {
                        titleLabel.Text = Text;
                        titleLabel.Image = Image;
                        Items.Insert(0, titleLabel);
                    }
                }
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (titleLabel != null)
                {
                    titleLabel.Text = Text;
                }
            }
        }

        Image image;
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                if (titleLabel != null)
                {
                    titleLabel.Image = image;
                }
            }
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ContextMenuHeader)
            {
                // skip
            }
            else
            {
                base.OnItemClicked(e);
            }
        }
    }

    public class ContextMenuHeader : ToolStripMenuItem
    {
        public ContextMenuHeader()
        {
            BackColor = SystemColors.GradientInactiveCaption;
            ForeColor = SystemColors.MenuText;
            Font = new Font(Font.FontFamily, 7f, FontStyle.Bold);
        }

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            ToolStripDropDownMenu menu = new ToolStripDropDownMenu();
            menu.OwnerItem = this;
            return menu;
        }

        protected override bool DismissWhenClicked
        {
            get
            {
                return false;
            }
        }
    }
}