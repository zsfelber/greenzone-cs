using System.Windows.Forms;
using System.ComponentModel;
using EAnalyzer_CS;
using System.Collections.Generic;
using System.Threading;

public class SymbolNode : EATreeNode
{
	private EAForm form;
    // WARNING It is being manipulated in external native DLL Thread !!
    private ZsfSymbolConfig config;
    private PropertyChangedEventHandler UIThreadPropertyChanged;
    private CollectionChangeEventHandler UIThreadCollectionChanged;
    private Dictionary<int, PeriodNode> children;
    private SymbolPanel symbolPanel;

    public SymbolNode(EAForm form, ZsfSymbolConfig config)
	{
#if (DEBUG)
        EALogger.Log("SymbolNode() " + config, EALogger.SEV_DEBUG_2);
#endif
        this.form = form;
        this.config = config;
        children = new Dictionary<int, PeriodNode>();
        UIThreadPropertyChanged = new PropertyChangedEventHandler(PropertyChanged);
        UIThreadCollectionChanged = new CollectionChangeEventHandler(CollectionChanged);
        object lck = Utils.EnterExisting(config, "SymbolNode()");
        try
        {
            config.PropertyChanged += new PropertyChangedEventHandler(_PropertyChanged);
            config.CollectionChanged += new CollectionChangeEventHandler(_CollectionChanged);
            UpdateLabel();
            UpdateChildren();
        }
        finally
        {
            Utils.ExitExisting(config, "SymbolNode()");
        }
    }

	public void UpdateLabel()
	{
		Text = config.Symbol;
	}
    public void UpdateChildren()
    {
        object lck = Utils.EnterExisting(config, "SymbolNode.UpdateChildren()");
        try
        {
            Nodes.Clear();
            foreach (KeyValuePair<int, ZsfPeriodConfig> kvp in config.PeriodConfig)
            {
                AddChild(kvp.Value);
            }
        }
        finally {
            Utils.ExitExisting(config, "SymbolNode.UpdateChildren()");
        }
    }
    public void AddChild(ZsfPeriodConfig periodConfig)
    {
        PeriodNode child = new PeriodNode(form, periodConfig);
        Nodes.Add(child);
        children[periodConfig.Period] = child;
    }
    public void RemoveChild(ZsfPeriodConfig periodConfig)
    {
        int key = periodConfig.Period;
        PeriodNode child = children[key];
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
        EALogger.Log("SymbolNode.PropertyChangedX(" + config + ")   sender:" + sender + " e:" + e.PropertyName + ":" + e.OldVal + "->" + e.NewVal, EALogger.SEV_DEBUG_2);
#endif
    }
    private void CollectionChangedX(object sender, CollectionChangeEventArgsX e)
	{
#if (DEBUG)
        EALogger.Log("SymbolNode.CollectionChangedX(" + config + ")   sender:" + sender + " e:" + e.Name + "." + e.Action + "(" + e.Element + ")", EALogger.SEV_DEBUG_2);
#endif
        if (e.Name == "PeriodConfig")
        {
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    AddChild((ZsfPeriodConfig)e.Element);
                    break;
                case CollectionChangeAction.Remove:
                    RemoveChild((ZsfPeriodConfig)e.Element);
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
        EALogger.Log("SymbolNode.AfterSelect(" + config + ")", EALogger.SEV_DEBUG_2);
#endif
        if (symbolPanel == null)
        {
            symbolPanel = new SymbolPanel();
            symbolPanel.propertyGrid1.SelectedObject = config;
        }
        symbolPanel.Refresh();
        form.SplitContainer.Panel2.Controls.Clear();
        form.SplitContainer.Panel2.Controls.Add(symbolPanel);
    }
}