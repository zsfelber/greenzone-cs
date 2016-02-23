using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{
    
    [GreenRmi]
    public interface IChartGroupSession : IRmiBase
    {
        int CursorPosition
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        List<IChartSession> ChartSessions
        {
            get;
            set;
        }

        void AddChart(IChartSession chartSession);

        void RemoveChart(IChartSession chart);
    }
}
