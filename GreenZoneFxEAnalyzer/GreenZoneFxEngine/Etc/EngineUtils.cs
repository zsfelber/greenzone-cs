using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Etc
{
    public static class EngineUtils
    {
        /*
        public static void ShowLoadFromSetDialog(IExecutable exec, IWin32Window parent = null)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Set files (*.set)|*.set|All files (*.*)|*.*";
            DialogResult r = fileDlg.ShowDialog(parent);
            if (r == DialogResult.OK)
            {
                exec.LoadFromSet(fileDlg.FileName);
            }
        }

        public static void ShowSaveToSetDialog(IExecutable exec, IWin32Window parent = null)
        {
            SaveFileDialog fileDlg = new SaveFileDialog();
            fileDlg.Filter = "Set files (*.set)|*.set|All files (*.*)|*.*";
            DialogResult r = fileDlg.ShowDialog(parent);
            if (r == DialogResult.OK)
            {
                exec.SaveToSet(fileDlg.FileName);
            }
        }
         * */


        public static void ShowLoadFromSetController(GreenRmiManager rmiManager, IExecRuntime exec, Controller parent = null)
        {
            FileDialogController fileDlg = new FileDialogController(rmiManager);
            fileDlg.Filter = "Set files (*.set)|*.set|All files (*.*)|*.*";
            DialogResult r = fileDlg.ShowDialog(parent);
            if (r == DialogResult.OK)
            {
                exec.LoadFromSet(fileDlg.FileName);
            }
        }

        public static void ShowSaveToSetController(GreenRmiManager rmiManager, IExecRuntime exec, Controller parent = null)
        {
            FileDialogController fileDlg = new FileDialogController(rmiManager, true);
            fileDlg.Filter = "Set files (*.set)|*.set|All files (*.*)|*.*";
            DialogResult r = fileDlg.ShowDialog(parent);
            if (r == DialogResult.OK)
            {
                exec.SaveToSet(fileDlg.FileName);
            }
        }


        public static bool IsDrawerAvailable(ChartType chartType, TimePeriodConst period)
        {
            if (period.GetCategory() == TimePeriodCategory.TICKS)
            {
                return chartType == ChartType.LINE;
            }
            else
            {
                return true;
            }
        }
    }
}
