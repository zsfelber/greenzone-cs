using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using EAnalyzer_CS;
using System.Threading;

public class AccountNode : EATreeNode
{
    public enum ChildType
    {
        SYMBOL_NODE, ORDER_ACTIVE, ORDER_HISTORY
    }
	private EAForm form;
    // WARNING It is being manipulated in external native DLL Thread !!
    private ZsfAccountConfig config;
    private PropertyChangedEventHandler UIThreadPropertyChanged;
    private CollectionChangeEventHandler UIThreadCollectionChanged;
    private Dictionary<string, SymbolNode> children;
    private AccountPanel accountPanel;

    public AccountNode(EAForm form, ZsfAccountConfig config)
	{
#if (DEBUG)
        EALogger.Log("AccountNode() " + config, EALogger.SEV_DEBUG_2);
#endif
        this.form = form;
        this.config = config;
		children = new Dictionary<string,SymbolNode>();
        UIThreadPropertyChanged = new PropertyChangedEventHandler(PropertyChanged);
        UIThreadCollectionChanged = new CollectionChangeEventHandler(CollectionChanged);
        object lck = Utils.EnterExisting(config, "AccountNode()");
        try
        {
            config.PropertyChanged += new PropertyChangedEventHandler(_PropertyChanged);
            config.CollectionChanged += new CollectionChangeEventHandler(_CollectionChanged);
            UpdateLabel();
            UpdateChildren(ChildType.SYMBOL_NODE);
        }
        finally
        {
            Utils.ExitExisting(config, "AccountNode()");
        }
    }

	public void UpdateLabel()
	{
		Text = config.AccountId.AccountNumber + " " + config.AccountId.AccountName;
	}
    public void UpdateChildren(ChildType type)
	{
        switch (type) {
            case ChildType.SYMBOL_NODE:
                object lck = Utils.EnterExisting(config, "AccountNode.UpdateChildren("+type+")");
                try
                {
		            Nodes.Clear();
		            foreach (KeyValuePair<string,ZsfSymbolConfig> kvp in config.SymbolConfig)
		            {
			            AddChild(kvp.Value);
		            }
                }
                finally {
                    Utils.ExitExisting(config, "AccountNode.UpdateChildren("+type+")");
                }
                break;
            default:
                break;
        }
    }
	public void AddChild(ZsfSymbolConfig symbolConfig)
	{
		SymbolNode child = new SymbolNode(form, symbolConfig);
		Nodes.Add(child);
		children[symbolConfig.Symbol] = child;
	}
    public void RemoveChild(ZsfSymbolConfig symbolConfig)
	{
		string key = symbolConfig.Symbol;
		SymbolNode child = children[key];
		children.Remove(key);
		Nodes.Remove(child);
	}

    public void AddChild(ZsfOrder order, ChildType type)
    {
        switch (type)
        {
            case ChildType.ORDER_ACTIVE:
            case ChildType.ORDER_HISTORY:
            default:
                break;
        }
    }
    public void RemoveChild(ZsfOrder order, ChildType type)
    {
        switch (type)
        {
            case ChildType.ORDER_ACTIVE:
            case ChildType.ORDER_HISTORY:
            default:
                break;
        }
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
        EALogger.Log("AccountNode.PropertyChangedX(" + config + ")   sender:" + sender + " e:" + e.PropertyName + ":" + e.OldVal + "->" + e.NewVal, EALogger.SEV_DEBUG_2);
#endif
    }
    private void CollectionChangedX(object sender, CollectionChangeEventArgsX e)
	{
#if (DEBUG)
        EALogger.Log("AccountNode.CollectionChangedX(" + config + ")   sender:" + sender + " e:" + e.Name + "." + e.Action + "(" + e.Element + ")", EALogger.SEV_DEBUG_2);
#endif
        if (e.Name == "SymbolConfig")
		{
			switch (e.Action)
			{
			case CollectionChangeAction.Add :
				AddChild((ZsfSymbolConfig)e.Element);
				break;
			case CollectionChangeAction.Remove :
				RemoveChild((ZsfSymbolConfig)e.Element);
				break;
			case CollectionChangeAction.Refresh :
                UpdateChildren(ChildType.SYMBOL_NODE);
				break;
			}
            TreeView.EndUpdate();
		}
	}
    internal override void AfterSelect(object sender, TreeViewEventArgs e)
    {
#if (DEBUG)
        EALogger.Log("AccountNode.AfterSelect(" + config + ")", EALogger.SEV_DEBUG_2);
#endif
        if (accountPanel == null)
        {
            accountPanel = new AccountPanel();
            accountPanel.propertyGrid1.SelectedObject = config;
        }
        accountPanel.Refresh();
        form.SplitContainer.Panel2.Controls.Clear();
        form.SplitContainer.Panel2.Controls.Add(accountPanel);
    }
}