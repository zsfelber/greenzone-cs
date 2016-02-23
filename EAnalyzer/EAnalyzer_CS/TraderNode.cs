using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using EAnalyzer_CS;
using System.Threading;

public class TraderNode : EATreeNode
{
	private EAForm form;
    // WARNING It is being manipulated in external native DLL Thread !!
    private ZsfTraderConfig config;
    private PropertyChangedEventHandler UIThreadPropertyChanged;
    private CollectionChangeEventHandler UIThreadCollectionChanged;
    private Dictionary<ZsfAccountId, AccountNode> children;
    private TraderPanel traderPanel;

    public TraderNode(EAForm form, ZsfTraderConfig config)
	{
#if (DEBUG)
        EALogger.Log("TraderNode() " + config, EALogger.SEV_DEBUG_2);
#endif
        this.form = form;
        this.config = config;
		children = new Dictionary<ZsfAccountId,AccountNode>();
        UIThreadPropertyChanged = new PropertyChangedEventHandler(PropertyChanged);
        UIThreadCollectionChanged = new CollectionChangeEventHandler(CollectionChanged);
        object lck = Utils.EnterExisting(config, "TraderNode()");
        try
        {
            config.PropertyChanged += new PropertyChangedEventHandler(_PropertyChanged);
            config.CollectionChanged += new CollectionChangeEventHandler(_CollectionChanged);
            UpdateLabel();
            UpdateChildren();
        }
        finally
        {
            Utils.ExitExisting(config, "TraderNode()");
        }
	}

	public void UpdateLabel()
	{
		Text = "EAnalyzer Platform";
	}
	public void UpdateChildren()
	{
        object lck = Utils.EnterExisting(config, "TraderNode.UpdateChildren()");
        try
        {
            Nodes.Clear();
            foreach (KeyValuePair<ZsfAccountId, ZsfAccountConfig> kvp in config.AccountConfig)
            {
                AddChild(kvp.Value);
            }
        }
        finally {
            Utils.ExitExisting(config, "TraderNode.UpdateChildren()");
        }
	}
	public void AddChild(ZsfAccountConfig accountConfig)
	{
		AccountNode child = new AccountNode(form, accountConfig);
		Nodes.Add(child);
        children[new ZsfAccountId(accountConfig.AccountId.AccountName, accountConfig.AccountId.AccountNumber, accountConfig.AccountId.Type)] = child;
	}
	public void RemoveChild(ZsfAccountConfig accountConfig)
	{
        ZsfAccountId key = new ZsfAccountId(accountConfig.AccountId.AccountName, accountConfig.AccountId.AccountNumber, accountConfig.AccountId.Type);
		AccountNode child = children[key];
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
        EALogger.Log("TraderNode.PropertyChangedX(" + config + ")   sender:" + sender + " e:" + e.PropertyName + ":" + e.OldVal + "->" + e.NewVal, EALogger.SEV_DEBUG_2);
#endif
    }
    private void CollectionChangedX(object sender, CollectionChangeEventArgsX e)
	{
#if (DEBUG)
        EALogger.Log("TraderNode.CollectionChangedX(" + config + ")   sender:" + sender + " e:" + e.Name + "." + e.Action + "(" + e.Element + ")", EALogger.SEV_DEBUG_2);
#endif
        if (e.Name == "AccountConfig")
		{
			switch (e.Action)
			{
			case CollectionChangeAction.Add :
				AddChild((ZsfAccountConfig)e.Element);
				break;
			case CollectionChangeAction.Remove :
				RemoveChild((ZsfAccountConfig)e.Element);
				break;
			case CollectionChangeAction.Refresh :
				UpdateChildren();
				break;
			}
            TreeView.EndUpdate();
        }
	}
    internal override void AfterSelect(object sender, TreeViewEventArgs e)
    {
#if (DEBUG)
        EALogger.Log("TraderNode.AfterSelect(" + config + ")",EALogger.SEV_DEBUG_2);
#endif
        if (traderPanel == null)
        {
            traderPanel = new TraderPanel();
            traderPanel.propertyGrid1.SelectedObject = config;
        }
        traderPanel.Refresh();
        form.SplitContainer.Panel2.Controls.Clear();
        form.SplitContainer.Panel2.Controls.Add(traderPanel);
    }
}

