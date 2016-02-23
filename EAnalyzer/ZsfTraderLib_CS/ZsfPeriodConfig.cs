using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class ZsfPeriodConfig : VariableConfig, ICopiable<ZsfPeriodConfig>
{
    private ZsfSymbolConfig parent;
    private int period;
    private ZsfOLHC currentEntry;

    private SerializableDictionary<int, ZsfChartConfig> chartConfig;
    private SerializableDictionary<int, ZsfOLHC> entries;

    public ZsfPeriodConfig() : this(null,-1)
    {
    }
    public ZsfPeriodConfig(ZsfSymbolConfig parent, int period)
    {
        this.parent = parent;
        this.period = period;
        chartConfig = new SerializableDictionary<int, ZsfChartConfig>();
        entries = new SerializableDictionary<int, ZsfOLHC>();
    }

    public override object GetId()
    {
        return period;
    }

    protected override void SetId(object id)
    {
        period = (int)id;
    }

    public ZsfSymbolConfig Parent
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


    public int Period
    {
        get { return period; }
        set
        {
            if (period != -1)
            {
                throw new Exception("period != -1");
            }
            period = value;
            OnPropertyChanged("Period", null, value);
        }
    }


    [XmlIgnore]
    public ZsfOLHC CurrentEntry
    {
        get
        {
            return  currentEntry;
        }
        set
        {
            if (currentEntry!=null && value!=null && currentEntry.Time==value.Time)
            {
                currentEntry.Copy(value);
            }
            else
            {
                ZsfOLHC old = currentEntry;
                currentEntry = value;
                OnPropertyChanged("CurrentEntry", old, value);
            }
        }
    }

    public SerializableDictionary<int, ZsfChartConfig> ChartConfig
    {
        get
        {
            return chartConfig;
        }
    }

    [XmlIgnore]
    public SerializableDictionary<int, ZsfOLHC> Entries
    {
        get
        {
            return entries;
        }
    }


    public virtual void Copy(ZsfPeriodConfig other, CopyMode copyMode = CopyMode.ALL)
	{
        if (period != other.period)
        {
            throw new Exception("ZsfPeriodConfig.Copy  period:" + period + " doesn't match to other " + other.period);
        }

        Copy((VariableConfig)other, copyMode);
        ReplaceChartConfig(other.chartConfig, copyMode);
        ReplaceEntries(other.entries, copyMode);

        // !
        CurrentEntry = other.currentEntry;
    }

    public void ClearChartConfig()
    {
        ClearXXX(chartConfig, "ChartConfig");
    }

    public void ReplaceChartConfig(Dictionary<int, ZsfChartConfig> _chartConfig, CopyMode copyMode)
    {
        ReplaceXXX(chartConfig, "ChartConfig", _chartConfig, copyMode);
    }


    public void SetChartConfig(int id, ZsfChartConfig _chartConfig, CopyMode copyMode)
    {
        SetXXX(chartConfig, "ChartConfig", id, _chartConfig, copyMode);
    }

    public void RemoveChartConfig(int id)
    {
        RemoveXXX(chartConfig, "ChartConfig", id);
    }



    public void ClearEntries()
    {
        ClearXXX(entries, "Entries");
    }

    public void ReplaceEntries(Dictionary<int, ZsfOLHC> _entries, CopyMode copyMode)
    {
        ReplaceXXX(entries, "Entries", _entries, copyMode);
    }


    public void SetEntry(int id, ZsfOLHC _entry, CopyMode copyMode)
    {
        SetXXX(entries, "Entries", id, _entry, copyMode);
    }

    public void RemoveEntry(int id)
    {
        RemoveXXX(entries, "Entries", id);
    }
}