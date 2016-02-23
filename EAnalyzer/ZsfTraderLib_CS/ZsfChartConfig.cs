using System;

[Serializable]
public class ZsfChartConfig : VariableConfig, ICopiable<ZsfChartConfig>
{
    private ZsfPeriodConfig parent;
    private int id;

    private int tickTime;

    public ZsfChartConfig() : this(null,-1)
    {
    }
    public ZsfChartConfig(ZsfPeriodConfig parent, int id)
    {
        this.parent = parent;
        this.id = id;
    }

    public override object GetId()
    {
        return id;
    }

    protected override void SetId(object id)
    {
        this.id = (int)id;
    }

    public ZsfPeriodConfig Parent
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


    public int Id
    {
        get { return id; }
        set
        {
            if (id != -1)
            {
                throw new Exception("id != -1");
            }
            id = value;
            OnPropertyChanged("Id", null, value);
        }
    }

    public int TickTime
    {
        get { return tickTime; }
        set
        {
            int old = tickTime;
            tickTime = value;
            OnPropertyChanged("TickTime", tickTime, value);
        }
    }


    public virtual void Copy(ZsfChartConfig other, CopyMode copyMode = CopyMode.ALL)
	{
        if (id != other.id)
        {
            throw new Exception("ZsfChartConfig.Copy  id:" + id + " doesn't match to other " + other.id);
        }

        Copy((VariableConfig)other, copyMode);
        tickTime = other.tickTime;
    }

}