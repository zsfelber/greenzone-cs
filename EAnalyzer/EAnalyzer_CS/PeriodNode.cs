using System.Windows.Forms;
using System.ComponentModel;
using EAnalyzer_CS;
using System.Collections.Generic;
using System.Threading;

public class PeriodNode : EATreeNode
{
	private EAForm form;
    // WARNING It is being manipulated in external native DLL Thread !!
    internal ZsfPeriodConfig config;
    private PropertyChangedEventHandler UIThreadPropertyChanged;
    private CollectionChangeEventHandler UIThreadCollectionChanged;
    private Dictionary<int, ChartNode> children;
    private PeriodPanel periodPanel;

    public PeriodNode(EAForm form, ZsfPeriodConfig config)
	{
#if (DEBUG)
        EALogger.Log("PeriodNode() " + config, EALogger.SEV_DEBUG_2);
#endif
        this.form = form;
        this.config = config;
        children = new Dictionary<int, ChartNode>();
        UIThreadPropertyChanged = new PropertyChangedEventHandler(PropertyChanged);
        UIThreadCollectionChanged = new CollectionChangeEventHandler(CollectionChanged);
        object lck = Utils.EnterExisting(config, "PeriodNode()");
        try
        {
            config.PropertyChanged += new PropertyChangedEventHandler(_PropertyChanged);
            config.CollectionChanged += new CollectionChangeEventHandler(_CollectionChanged);
            UpdateLabel();
            UpdateChildren();
        }
        finally
        {
            Utils.ExitExisting(config, "PeriodNode()");
        }
    }

	public void UpdateLabel()
	{
        switch (config.Period) {
            case Mt4Constants.PERIOD_M1 : Text = "1 minute";break;
            case Mt4Constants.PERIOD_M5: Text = "5 minutes"; break;
            case Mt4Constants.PERIOD_M15: Text = "15 minutes"; break;
            case Mt4Constants.PERIOD_M30: Text = "30 minutes"; break;
            case Mt4Constants.PERIOD_H1: Text = "1 hour"; break;
            case Mt4Constants.PERIOD_H4: Text = "4 hour"; break;
            case Mt4Constants.PERIOD_D1: Text = "Daily"; break;
            case Mt4Constants.PERIOD_W1: Text = "Weekly"; break;
            case Mt4Constants.PERIOD_MN1: Text = "Monthly"; break;
        }
	}
    public void UpdateChildren()
    {
        object lck = Utils.EnterExisting(config, "PeriodNode.UpdateChildren()");
        try
        {
            Nodes.Clear();
            foreach (KeyValuePair<int, ZsfChartConfig> kvp in config.ChartConfig)
            {
                AddChild(kvp.Value);
            }
        }
        finally {
            Utils.ExitExisting(config, "PeriodNode.UpdateChildren()");
        }
    }
    public void AddChild(ZsfChartConfig chartConfig)
    {
        ChartNode child = new ChartNode(form, this, chartConfig);
        Nodes.Add(child);
        children[chartConfig.Id] = child;
    }
    public void RemoveChild(ZsfChartConfig chartConfig)
    {
        int key = chartConfig.Id;
        ChartNode child = children[key];
        children.Remove(key);
        Nodes.Remove(child);
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
		PropertyChangedX(sender, (PropertyChangedEventArgsX) e);
	}
	private void CollectionChanged(object sender, CollectionChangeEventArgs e)
	{
		CollectionChangedX(sender, (CollectionChangeEventArgsX) e);
	}

    private void PropertyChangedX(object sender, PropertyChangedEventArgsX e)
	{
#if (DEBUG)
        EALogger.Log("PeriodNode.PropertyChangedX(" + config + ")   sender:" + sender + " e:" + e.PropertyName + ":" + e.OldVal + "->" + e.NewVal, EALogger.SEV_DEBUG_2);
#endif
    }
    private void CollectionChangedX(object sender, CollectionChangeEventArgsX e)
	{
#if (DEBUG)
        EALogger.Log("PeriodNode.CollectionChangedX(" + config + ")   sender:" + sender + " e:" + e.Name + "." + e.Action + "(" + e.Element + ")", EALogger.SEV_DEBUG_2);
#endif
        if (e.Name == "ChartConfig")
        {
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    AddChild((ZsfChartConfig)e.Element);
                    break;
                case CollectionChangeAction.Remove:
                    RemoveChild((ZsfChartConfig)e.Element);
                    break;
                case CollectionChangeAction.Refresh:
                    UpdateChildren();
                    break;
            }
            TreeView.EndUpdate();
        }
    }
    internal override void AfterSelect(object sender, TreeViewEventArgs e)
    {
#if (DEBUG)
        EALogger.Log("PeriodNode.AfterSelect(" + config + ")", EALogger.SEV_DEBUG_2);
#endif
        if (periodPanel == null)
        {
            periodPanel = new PeriodPanel();
            periodPanel.propertyGrid1.SelectedObject = config;
        }
        periodPanel.Refresh();
        form.SplitContainer.Panel2.Controls.Clear();
        form.SplitContainer.Panel2.Controls.Add(periodPanel);
    }
}