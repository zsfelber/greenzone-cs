using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.CompilerServices;

namespace GreenZoneUtil.Util
{

    public class GreenZoneUtils : GreenZoneUtilsBase
    {

        public static void BuildVisibleColumnsModel(GreenRmiManager rmiManager, GridController table, ComboController popup, bool sortByName)
        {
            IDictionary<string, GridColumnController> cols;
            if (sortByName)
            {
                cols = new SortedDictionary<string, GridColumnController>();
            }
            else
            {
                cols = new Dictionary<string, GridColumnController>();
            }

            foreach (GridColumnController col in table.Columns)
            {
                GridColumnController col0;
                if (cols.TryGetValue(col.Text, out col0))
                {
                    cols.Remove(col0.Text);
                    cols[col0.Text + " (" + col0.DataPropertyName + ")"] = col0;
                    cols[col.Text + " (" + col.DataPropertyName + ")"] = col;
                }
                else
                {
                    cols[col.Text] = col;
                }
            }

            popup.Items.Clear();

            foreach (var e in cols)
            {
                ToggleButtonController mi = new ToggleButtonController(rmiManager, popup, e.Key);
                mi.Checked = e.Value.Visible;
                mi.CheckOnClick = true;
                mi.Tag = e.Value;
                popup.Add(mi);

                mi.CheckedChanged += new GreenZoneUtil.ViewController.PropertyChangedEventHandler(BuildVisibleColumnsModel_mi_CheckedChanged);
            }
        }

        static void BuildVisibleColumnsModel_mi_CheckedChanged(object sender, ControllerEventArgs e)
        {
            ToggleButtonController mi = (ToggleButtonController)sender;
            GridColumnController col = (GridColumnController)mi.Tag;
            col.Visible = mi.Checked;
        }
        
        public static Icon ImageToIcon(System.Drawing.Image image)
        {
            if (image == null)
            {
                return null;
            }
            else
            {
                return Icon.FromHandle(((Bitmap)image).GetHicon());
            }
        }

        public static Image IconToImage(Icon icon)
        {
            if (icon == null)
            {
                return null;
            }
            else
            {
                return Image.FromHbitmap(icon.ToBitmap().GetHbitmap());
            }
        }


        public static List<Type> GetNamespaceClasses(Assembly _asm, string nameSpace, params Type[] attributeTypes)
        {
            Assembly[] asms;
            if (_asm == null)
            {
                asms = AppDomain.CurrentDomain.GetAssemblies();
            }
            else
            {
                //asms = new Assembly[] { Assembly.GetExecutingAssembly() };
                asms = new Assembly[] { _asm };
            }

            List<Type> classlist = new List<Type>();

            foreach (Assembly asm in asms)
            {
                foreach (Type type in asm.GetTypes())
                {
                    bool allFnd = true;
                    foreach (Type at in attributeTypes)
                    {
                        object[] attrs = type.GetCustomAttributes(at, false);
                        allFnd = attrs != null && attrs.Length > 0;
                        if (!allFnd)
                        {
                            break;
                        }
                    }
                    if (allFnd && (nameSpace == null || type.Namespace.Equals(nameSpace)))
                    {
                        classlist.Add(type);
                    }
                }
            }

            return classlist;
        }

        public static Type FindTypeInAssemblies(string fullName)
        {
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

            Type type = null;
            foreach (Assembly asm in asms)
            {
                type = asm.GetType(fullName);
                if (type != null)
                {
                    break;
                }
            }

            return type;
        }



    }


    public static class TWinFct
    {
        [DllImport("user32")]
        private static extern int LockWindowUpdate(IntPtr hwndLock);

        private static List<Control> stack = new List<Control>();

        public static int LockControlUpdate(Control AControl)
        {
            stack.Add(AControl);
            return LockWindowUpdate(AControl.Handle);
        }

        public static int UnLockControlUpdate(Control AControl)
        {
            if (stack[stack.Count - 1] == AControl)
            {
                stack.RemoveAt(stack.Count - 1);
            }
            else
            {
                throw new NotSupportedException(AControl + " vs " + stack[stack.Count - 1]);
            }

            if (stack.Count == 0)
            {
                return LockWindowUpdate(IntPtr.Zero);
            }
            else
            {
                return stack.Count;
            }
        }
    }
}