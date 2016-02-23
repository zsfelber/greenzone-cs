using System;
[Serializable]
public class ZsfOrder : BaseConfig, ICopiable<ZsfOrder>
{
    protected int ticket;
    private int type;
    private int openTime;
    private int magic;
    private int closeTime;

    private double lots;
    private double price;
    private double sl;
    private double tp;
    private double closePrice;

    private string symbol;
    private string comment;

    public ZsfOrder() : this(-1)
    {
    }

    public ZsfOrder(int ticket)
    {
        this.ticket = ticket;
    }

    public override object GetId()
    {
        return ticket;
    }

    protected override void SetId(object id)
    {
        ticket = (int)id;
    }

    public virtual int Ticket
    {
        get
        {
            return ticket;
        }
        set
        {
            if (ticket != -1)
            {
                throw new Exception("ticket != -1");
            }
            ticket = value;
            OnPropertyChanged("Ticket", null, value);
        }
    }
    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            int old = type;
            type = value;
            OnPropertyChanged("Type", old, type);
        }
    }
    public int OpenTime
    {
        get
        {
            return openTime;
        }
        set
        {
            int old = openTime;
            openTime = value;
            OnPropertyChanged("OpenTime", old, openTime);
        }
    }
    public int Magic
    {
        get
        {
            return magic;
        }
        set
        {
            int old = magic;
            magic = value;
            OnPropertyChanged("Magic", old, magic);
        }
    }
    public int CloseTime
    {
        get
        {
            return closeTime;
        }
        set
        {
            int old = closeTime;
            closeTime = value;
            OnPropertyChanged("CloseTime", old, closeTime);
        }
    }

    public double Lots
    {
        get
        {
            return lots;
        }
        set
        {
            double old = lots;
            lots = value;
            OnPropertyChanged("Lots", old, lots);
        }
    }
    public double Price
    {
        get
        {
            return price;
        }
        set
        {
            double old = price;
            price = value;
            OnPropertyChanged("Price", old, price);
        }
    }
    public double Sl
    {
        get
        {
            return sl;
        }
        set
        {
            double old = sl;
            sl = value;
            OnPropertyChanged("Sl", old, sl);
        }
    }
    public double Tp
    {
        get
        {
            return tp;
        }
        set
        {
            double old = tp;
            tp = value;
            OnPropertyChanged("Tp", old, tp);
        }
    }
    public double ClosePrice
    {
        get
        {
            return closePrice;
        }
        set
        {
            double old = closePrice;
            closePrice = value;
            OnPropertyChanged("ClosePrice", old, closePrice);
        }
    }

    public string Symbol
    {
        get
        {
            return symbol;
        }
        set
        {
            string old = symbol;
            symbol = value;
            OnPropertyChanged("Symbol", old, symbol);
        }
    }
    public string Comment
    {
        get
        {
            return comment;
        }
        set
        {
            string old = comment;
            comment = value;
            OnPropertyChanged("Comment", old, comment);
        }
    }


    public virtual void Copy(ZsfOrder other, CopyMode copyMode = CopyMode.ALL)
    {
        if (ticket != other.ticket)
        {
            throw new Exception("ZsfSavedOrder.Copy  ticket:" + ticket + " doesn't match to other " + other.ticket);
        }

        Type = other.type;
        OpenTime = other.openTime;
        Magic = other.magic;
        CloseTime = other.closeTime;

        Lots = other.lots;
        Price = other.price;
        Sl = other.sl;
        Tp = other.tp;
        ClosePrice = other.closePrice;

        Symbol = other.symbol;
        Comment = other.comment;
    }
}