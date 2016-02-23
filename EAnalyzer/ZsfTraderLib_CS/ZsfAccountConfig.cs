using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class ZsfAccountConfig : VariableConfig, ICopiable<ZsfAccountConfig>
{
    private ZsfTraderConfig parent;
    private ZsfAccountId accountId;
	private double balance;
	private int leverage;
	private double commission;
	private string currencySymbol;
    private bool isMarketExecution;
    private bool isEmulateSlippage;

    private SerializableDictionary<string,ZsfSymbolConfig> symbolConfig;
	private SerializableDictionary<int,ZsfOrder> ordersActive;
	private SerializableDictionary<int,ZsfOrder> ordersHistory;

    public ZsfAccountConfig() : this(null,null)
    {
    }
    
    public ZsfAccountConfig(ZsfTraderConfig parent, ZsfAccountId accountId)
	{
        this.parent = parent;
        this.accountId = accountId;
		symbolConfig = new SerializableDictionary<string,ZsfSymbolConfig>();
		ordersActive = new SerializableDictionary<int,ZsfOrder>();
		ordersHistory = new SerializableDictionary<int,ZsfOrder>();
	}

    public override object GetId()
    {
        return accountId; 
    }

    protected override void SetId(object id)
    {
        accountId = (ZsfAccountId)id;
    }

    public ZsfTraderConfig Parent
    {
        get
        {
            return parent;
        }
        set
        {
            if (parent != null)
            {
                throw new Exception("parent != null");
            }
            parent = value;
            OnPropertyChanged("Parent", null, value);
        }
    }

	public ZsfAccountId AccountId
	{
		get { return accountId; }
        set
        {
            if (accountId != null && accountId.Type != ZsfAccountId.TreeType.NONE)
            {
                throw new Exception("accountId is already initialized");
            }
            accountId = value;
            OnPropertyChanged("AccountId", null, value);
        }
    }
	public double Balance
	{
		get { return balance; }
		set { 
            double old=balance;
			balance = value;
            OnPropertyChanged("Balance", old, value);
        }
	}
	public int Leverage
	{
		get { return leverage; }
		set { 
            int old=leverage;
            leverage = value;
            OnPropertyChanged("Leverage", old, value);
        }
	}
	public double Commission
	{
		get { return commission; }
		set { 
            double old =commission;
            commission = value;
            OnPropertyChanged("Commission", old, value); 
        }
	}
	public string CurrencySymbol
	{
		get { return currencySymbol; }
		set { 
            string old =currencySymbol; 
            currencySymbol = value;
            OnPropertyChanged("CurrencySymbol", old, value);
        }
	}
	public bool IsMarketExecution
	{
		get { return isMarketExecution; }
		set { 
            bool old = isMarketExecution;
            isMarketExecution = value;
            OnPropertyChanged("IsMarketExecution", old, value);
        }
	}
    public bool IsEmulateSlippage
	{
        get { return isEmulateSlippage; }
		set {
            bool old = isEmulateSlippage;
            isEmulateSlippage = value;
            OnPropertyChanged("IsEmulateSlippage", old, value);
        }
	}
    

    public SerializableDictionary<string,ZsfSymbolConfig> SymbolConfig
	{
		get
		{
			return symbolConfig;
		}
		//set
		//	symbolConfig = value;
		//}
	}

    [XmlIgnore]
	public SerializableDictionary<int,ZsfOrder> OrdersActive
	{
		get
		{
			return ordersActive;
		}
		//void set(SerializableDictionary<int,ZsfSavedOrder> _ordersActive) {
		//	ordersActive = _ordersActive;
		//}
	}

    [XmlIgnore]
    public SerializableDictionary<int, ZsfOrder> OrdersHistory
	{
		get
		{
			return ordersHistory;
		}
		//void set(SerializableDictionary<int,ZsfSavedOrder> _ordersHistory) {
		//	ordersHistory = _ordersHistory;
		//}
	}

    public virtual void Copy(ZsfAccountConfig other, CopyMode copyMode = CopyMode.ALL)
    {
        if (!Utils.Equal(accountId, other.accountId))
        {
            throw new Exception("ZsfAccountConfig.Copy  accountId:" + accountId + " doesn't match to other " + other.accountId);
        }

        Copy((VariableConfig)other, copyMode);
        ReplaceSymbolConfig(other.symbolConfig, copyMode);
        ReplaceOrdersActive(other.ordersActive, copyMode);
        ReplaceOrdersHistory(other.ordersHistory, copyMode);

        Balance = other.balance;
        Leverage = other.leverage;
        Commission = other.commission;
        CurrencySymbol = other.currencySymbol;
        IsMarketExecution = other.isMarketExecution;
        IsEmulateSlippage = other.isEmulateSlippage;
    }

	public void ClearSymbolConfig()
	{
		ClearXXX(symbolConfig, "SymbolConfig");
	}
    public void ClearOrdersActive()
	{
		ClearXXX(ordersActive, "OrdersActive");
	}
    public void ClearOrdersHistory()
	{
		ClearXXX(ordersHistory, "OrdersHistory");
	}

    public void ReplaceSymbolConfig(Dictionary<string, ZsfSymbolConfig> _symbolConfig, CopyMode copyMode)
	{
        ReplaceXXX(symbolConfig, "SymbolConfig", _symbolConfig, copyMode);
	}
    public void ReplaceOrdersActive(Dictionary<int, ZsfOrder> _ordersActive, CopyMode copyMode)
	{
        ReplaceXXX(ordersActive, "OrdersActive", _ordersActive, copyMode);
	}
    public void ReplaceOrdersHistory(Dictionary<int, ZsfOrder> _ordersHistory, CopyMode copyMode)
	{
        ReplaceXXX(ordersHistory, "OrdersHistory", _ordersHistory, copyMode);
	}


    public void SetSymbolConfig(string symbol, ZsfSymbolConfig _symbolConfig, CopyMode copyMode)
	{
        SetXXX(symbolConfig, "SymbolConfig", symbol, _symbolConfig, copyMode);
	}
    public void SetOrderActive(int ticket, ZsfOrder order, CopyMode copyMode)
	{
        SetXXX(ordersActive, "OrdersActive", ticket, order, copyMode);
	}
    public void SetOrderHistory(int ticket, ZsfOrder order, CopyMode copyMode)
	{
		SetXXX(ordersHistory, "OrdersHistory", ticket, order, copyMode);
	}

    public void RemoveSymbolConfig(string symbol)
	{
		RemoveXXX(symbolConfig, "SymbolConfig", symbol);
	}
    public void RemoveOrderActive(int ticket)
	{
		RemoveXXX(ordersActive, "OrdersActive", ticket);
	}
    public void RemoveOrderHistory(int ticket)
	{
		RemoveXXX(ordersHistory, "OrdersHistory", ticket);
	}
}