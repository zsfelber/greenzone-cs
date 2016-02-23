using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;


namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ServerChartSectionPanelControllerEx : ServerChartSectionPanelControllerBase
    {

        public ServerChartSectionPanelControllerEx(GreenRmiManager rmiManager, ServerChartControllerEx parent)
            : base(rmiManager, parent)
        {
            PriceLabelPane1 = CreatePriceLabelPaneController();

            CreateChartPane(parent);
            ChartPane.SliderBarColor = parent.SliderBarColor;

        }

        public ServerChartSectionPanelControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
        }

        protected ServerChartSectionPanelControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected abstract IPriceLabelPaneController CreatePriceLabelPaneController();

        public override SeriesRange SectionRange
        {
            get
            {
                return Owner.SeriesRange;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string PriceFormat
        {
            get
            {
                string f = Owner.SymbolFormat;
                return f;
            }
        }


        public override int Scale
        {
            get
            {
                return 0;
            }
        }

        protected abstract void CreateChartPane(ServerChartControllerEx parent);
    }
}
