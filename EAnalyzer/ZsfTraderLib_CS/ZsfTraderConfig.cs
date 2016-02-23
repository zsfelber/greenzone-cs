using System;
using System.Collections.Generic;

[Serializable]
public class ZsfTraderConfig : CustomConfigXML, ICopiable<ZsfTraderConfig>
{
    private ZsfTraderId traderId;

    private int logSeverity;
    
    private SerializableDictionary<ZsfAccountId, ZsfAccountConfig> accountConfig;


    public ZsfTraderConfig() : this(null)
    {
    }

    public ZsfTraderConfig(ZsfTraderId traderId)
	{
        this.traderId = traderId;
		accountConfig = new SerializableDictionary<ZsfAccountId,ZsfAccountConfig>();
	}

    public override object GetId()
    {
        return traderId;
    }

    protected override void SetId(object id)
    {
        traderId = (ZsfTraderId)id;
    }

    public ZsfTraderId TraderId
    {
		get	{ return traderId; }
        set
        {
            if (traderId != null && traderId.Type!=ZsfTraderId.TreeType.NONE)
            {
                throw new Exception("traderId is already initialized");
            }
            traderId = value;
            OnPropertyChanged("TraderId", null, value);
        }
    }

    public int LoggingSeverity
	{
		get	{ return logSeverity; }
		set
		{
			int old = logSeverity;
			logSeverity = value;
            OnPropertyChanged("LoggingSeverity", old, value);
		}
	}

	public SerializableDictionary<ZsfAccountId,ZsfAccountConfig> AccountConfig
	{
		get
		{
			return accountConfig;
		}
		//set
		//	accountConfig = value;
		//}
	}


    public virtual void Copy(ZsfTraderConfig other, CopyMode copyMode = CopyMode.ALL)
    {
        if (!Utils.Equal(traderId, other.traderId))
        {
            throw new Exception("ZsfTraderConfig.Copy  traderId:" + traderId + " doesn't match to other " + other.traderId);
        }
        Copy((VariableConfig)other, copyMode);
        ReplaceAccountConfig(other.accountConfig, copyMode);
        LoggingSeverity = other.logSeverity;
    }

    public static ZsfTraderConfig Deserialize(string file)
    {
        return (ZsfTraderConfig)Deserialize(file, typeof(ZsfTraderConfig));
    }


    public void ClearAccountConfig()
	{
		ClearXXX(accountConfig, "AccountConfig");
	}

    public void ReplaceAccountConfig(Dictionary<ZsfAccountId, ZsfAccountConfig> _accountConfig, CopyMode copyMode)
	{
        ReplaceXXX(accountConfig, "AccountConfig", _accountConfig, copyMode);
	}

    public void SetAccountConfig(ZsfAccountId accountId, ZsfAccountConfig _accountConfig, CopyMode copyMode)
	{
        SetXXX(accountConfig, "AccountConfig", accountId, _accountConfig, copyMode);
	}

	public void RemoveAccountConfig(ZsfAccountId  accountId)
	{
		RemoveXXX(accountConfig, "AccountConfig", accountId);
	}
}