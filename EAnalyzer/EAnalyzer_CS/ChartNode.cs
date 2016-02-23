using System.Windows.Forms;
using System.ComponentModel;
using EAnalyzer_CS;
using System.Threading;
using ZedGraph;
using System;
using System.Drawing;
using System.Collections.Generic;

public class ChartNode : EATreeNode
{
	private EAForm form;
    private PeriodNode parent;
    // WARNING It is being manipulated in external native DLL Thread !!
    private ZsfChartConfig config;
    private PropertyChangedEventHandler UIThreadPropertyChanged;
    private CollectionChangeEventHandler UIThreadCollectionChanged;
    private ChartPanel chartPanel;

    private ZedGraphControl zgcontrol;
    private StockPointList spl;
    private OHLCBarItem myCurve;

    public ChartNode(EAForm form, PeriodNode parent, ZsfChartConfig config)
	{
#if (DEBUG)
        EALogger.Log("ChartNode() " + config, EALogger.SEV_DEBUG_2);
#endif
        this.form = form;
        this.parent = parent;
        this.config = config;
        UIThreadPropertyChanged = new PropertyChangedEventHandler(PropertyChanged);
        UIThreadCollectionChanged = new CollectionChangeEventHandler(CollectionChanged);
        Utils.EnterExisting(config, "ChartNode()");
        try {
            config.PropertyChanged += new PropertyChangedEventHandler(_PropertyChanged);
            // parent !
            parent.config.CollectionChanged += new CollectionChangeEventHandler(_CollectionChanged);
            UpdateLabel();
            UpdateChildren();
        }
        finally
        {
            Utils.ExitExisting(config, "ChartNode()");
        }
    }

	public void UpdateLabel()
	{
        Text = "Chart #"+config.Id;
	}
    public void UpdateChildren()
    {
        object lck = Utils.EnterExisting(config, "ChartNode.UpdateChildren()");
        try
        {
            if (spl == null)
            {
                spl = new StockPointList();
            }
            else
            {
                spl.Clear();
            }

            ZsfPeriodConfig periodConfig = parent.config;
            foreach (KeyValuePair<int, ZsfOLHC> kvp in periodConfig.Entries)
            {
                double x = new XDate((double)kvp.Key);
                double open = kvp.Value.Open;
                double low = kvp.Value.Low;
                double high = kvp.Value.High;
                double close = kvp.Value.Close;

                StockPt pt = new StockPt(x, high, low, open, close, 100000);
                spl.Insert(0, pt);
            }
        }
        finally
        {
            Utils.ExitExisting(config, "ChartNode.UpdateChildren()");
        }
    }
    public void AddChild(ZsfOLHC entry)
    {
        double x = new XDate((double)entry.Time);
        double open = entry.Open;
        double low = entry.Low;
        double high = entry.High;
        double close = entry.Close;

        if (myCurve == null)
        {
            CreateZed();
        }
        StockPt pt = new StockPt(x, high, low, open, close, 100000);
        for (int i=myCurve.Points.Count-1; i>=0; i--) {
            StockPt stp = (StockPt) myCurve.Points[i];
            if (stp.Date<entry.Time) {
                spl.Insert(i+1,new StockPt(x, high, low, open, close, 100000));
                break;
            }
        }
        myCurve.Points = spl;
    }
    public void RemoveChild(ZsfOLHC entry)
    {
        for (int i = myCurve.Points.Count - 1; i >= 0; i--)
        {
            StockPt stp = (StockPt)myCurve.Points[i];
            if ((int)stp.Date == entry.Time)
            {
                spl.RemoveAt(i);
            }
        }
        myCurve.Points = spl;
    }

    // WARNING It comes from external native DLL Thread !!
    private void _PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
        form.Invoke(UIThreadPropertyChanged, sender, e);
    }
    // WARNING It comes from external native DLL Thread !!
    private void _CollectionChanged(object sender, CollectionChangeEventArgs e)
	{
        form.Invoke(UIThreadCollectionChanged, sender, e);
    }


    private void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        PropertyChangedX(sender, (PropertyChangedEventArgsX)e);
    }
    private void CollectionChanged(object sender, CollectionChangeEventArgs e)
    {
        CollectionChangedX(sender, (CollectionChangeEventArgsX)e);
    }
    private void PropertyChangedX(object sender, PropertyChangedEventArgsX e)
    {
#if (DEBUG)
        EALogger.Log("ChartNode.PropertyChangedX(" + config + ")   sender:" + sender + " e:" + e.PropertyName + ":" + e.OldVal + "->" + e.NewVal, EALogger.SEV_DEBUG_2);
#endif
    }
    private void CollectionChangedX(object sender, CollectionChangeEventArgsX e)
    {
#if (DEBUG)
        EALogger.Log("ChartNode.CollectionChangedX(" + config + ")   sender:" + sender + " e:" + e.Name + "." + e.Action + "(" + e.Element + ")", EALogger.SEV_DEBUG_2);
#endif
        if (e.Name == "Entries")
        {
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    AddChild((ZsfOLHC)e.Element);
                    break;
                case CollectionChangeAction.Remove:
                    RemoveChild((ZsfOLHC)e.Element);
                    break;
                case CollectionChangeAction.Refresh:
                    UpdateChildren();
                    break;
            }
            zgcontrol.AxisChange();
        }

    }
    internal override void AfterSelect(object sender, TreeViewEventArgs e)
    {
#if (DEBUG)
        EALogger.Log("ChartNode.AfterSelect(" + config + ")", EALogger.SEV_DEBUG_2);
#endif
        if (chartPanel == null)
        {
            chartPanel = new ChartPanel();
            chartPanel.propertyGrid1.SelectedObject = config;

            CreateZed();

            chartPanel.panel1.Controls.Clear();
            chartPanel.panel1.Controls.Add(zgcontrol);
            zgcontrol.Anchor = AnchorStyles.Left | AnchorStyles.Top
                                                | AnchorStyles.Right | AnchorStyles.Bottom;
            zgcontrol.Width = chartPanel.panel1.Width;
            zgcontrol.Height = chartPanel.panel1.Height;

            GraphPane myPane = zgcontrol.GraphPane;
            // Use DateAsOrdinal to skip weekend gaps
            myPane.XAxis.Type = AxisType.DateAsOrdinal;

            // pretty it up a little
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45.0f);

        }
        // tell the control to rescale itself
        zgcontrol.AxisChange();

        chartPanel.Refresh();
        form.SplitContainer.Panel2.Controls.Clear();
        form.SplitContainer.Panel2.Controls.Add(chartPanel);
    }

    private void CreateZed()
    {
        zgcontrol = new ZedGraphControl();
        GraphPane myPane = zgcontrol.GraphPane;

        myCurve = myPane.AddOHLCBar("trades", spl, Color.Black);
        myCurve.Bar.IsAutoSize = true;
        myCurve.Bar.Color = Color.Blue;
    }
}