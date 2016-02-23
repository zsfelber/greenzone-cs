
using System;
[Serializable]
public class ZsfOLHC : BaseConfig, ICopiable<ZsfOLHC>
{
    protected int time;

    private double open;
    private double low;
    private double high;
    private double close;

    public ZsfOLHC() : this(-1)
    {
    }

    public ZsfOLHC(int time)
    {
        this.time = time;
    }

    public override object GetId()
    {
        return time;
    }

    protected override void SetId(object id)
    {
        time = (int)id;
    }

    public int Time
    {
        get { return time; }
        set
        {
            if (time != -1)
            {
                throw new Exception("time != -1");
            }
            time = value;
            OnPropertyChanged("Time", null, value);
        }
    }

    public double Open
    {
        get { return open; }
        set
        {
            double old = open;
            open = value;
            OnPropertyChanged("Open", old, value);
        }
    }
    public double Low
    {
        get { return low; }
        set
        {
            double old = low;
            low = value;
            OnPropertyChanged("Low", old, value);
        }
    }
    public double High
    {
        get { return high; }
        set
        {
            double old = high;
            high = value;
            OnPropertyChanged("High", old, value);
        }
    }
    public double Close
    {
        get { return close; }
        set
        {
            double old = close;
            close = value;
            OnPropertyChanged("Close", old, value);
        }
    }

    public virtual void Copy(ZsfOLHC other, CopyMode copyMode = CopyMode.ALL)
    {
        if (time != other.time)
        {
            throw new Exception("OLHC.Copy  time:" + time + " doesn't match to other " + other.time);
        }

        Open = other.open;
        Low = other.low;
        High = other.high;
        Close = other.close;
    }
}
