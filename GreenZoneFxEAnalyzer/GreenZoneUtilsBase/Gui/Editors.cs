using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using GreenZoneUtil.Util;

namespace GreenZoneUtil.Gui
{
    class WingdingsCharEditor : UITypeEditor
    {
        Font f = new Font("Wingdings", 10);
        Brush b = new SolidBrush(SystemColors.ControlText);

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            WingdingsChar foo = (WingdingsChar)value;
            if (svc != null)
            {
                CharMapDialog charMapDlg = new CharMapDialog("Wingdings", (char)foo.CharCode);
	            // Show the dialog.
	            DialogResult result = charMapDlg.ShowDialog();
	            // See if OK was pressed.
	            if (result == DialogResult.OK)
	            {
                    // update object
                    // no!
                    //foo.CharCode = charMapDlg.SelectedChar;
                    // but:
                    context.PropertyDescriptor.SetValue(context.Instance, new WingdingsChar(charMapDlg.SelectedChar));
                }
            }
            return value; // can also replace the wrapper object here
        }


        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        //
        // Summary:
        //     Paints a representation of the value of an object using the specified System.Drawing.Design.PaintValueEventArgs.
        //
        // Parameters:
        //   e:
        //     A System.Drawing.Design.PaintValueEventArgs that indicates what to paint
        //     and where to paint it.
        public override void PaintValue(PaintValueEventArgs e)
        {
            WingdingsChar foo = (WingdingsChar)e.Value;

            //e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            e.Graphics.DrawString("" + (char)foo.CharCode, f, b, 0, 0);
        }
    }

    class SelectableFileEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            SelectableFile foo = (SelectableFile)value;
            if (svc != null)
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.FileName = foo.Path;
                // Show the dialog.
                DialogResult result = fileDlg.ShowDialog();
                // See if OK was pressed.
                if (result == DialogResult.OK)
                {
                    // update object
                    // no!
                    //foo.CharCode = charMapDlg.SelectedChar;
                    // but:
                    context.PropertyDescriptor.SetValue(context.Instance, new SelectableFile(fileDlg.FileName));
                }
            }
            return value; // can also replace the wrapper object here
        }
    }



    class SelectableDirEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            SelectableDir foo = (SelectableDir)value;
            if (svc != null)
            {
                FolderBrowserDialog fileDlg = new FolderBrowserDialog();
                fileDlg.SelectedPath = foo.Path;
                // Show the dialog.
                DialogResult result = fileDlg.ShowDialog();
                // See if OK was pressed.
                if (result == DialogResult.OK)
                {
                    // update object
                    // no!
                    //foo.CharCode = charMapDlg.SelectedChar;
                    // but:
                    context.PropertyDescriptor.SetValue(context.Instance, new SelectableDir(fileDlg.SelectedPath));
                }
            }
            return value; // can also replace the wrapper object here
        }
    }
}

