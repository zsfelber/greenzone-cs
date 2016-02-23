public class ZsfAccountId
{
    public enum TreeType {
        NONE, NORMAL, TESTING
    }

    private string accountName;

    private int accountNumber;

    private TreeType type;

    public ZsfAccountId()
        : this(null, -1, TreeType.NONE)
    {
    }

    public ZsfAccountId(string accountName, int accountNumber, TreeType type) 
	{
        this.accountName = accountName;
        this.accountNumber = accountNumber;
        this.type = type;
	}

    public string AccountName
    {
        get
        {
            return accountName;
        }
    }

    public int AccountNumber
    {
        get
        {
            return accountNumber;
        }
    }

    public TreeType Type
    {
        get
        {
            return type;
        }
    }

    public override string ToString()
    {
        return GetType().Name + "["+accountName+" "+accountNumber+" "+type+"]";
    }


    // TKey : ZsfAccountId   in  Dictionary<TKey,TValue> ...

    public override int GetHashCode()
    {
        return accountName.GetHashCode()+accountNumber+(int)type;
    }

    public override bool Equals(object obj)
    {
        return Equals((ZsfAccountId)obj);
    }

    public bool Equals(ZsfAccountId obj)
    {
        return obj != null && Utils.Equal(obj.accountName, this.accountName) && obj.accountNumber == this.accountNumber && obj.type == this.type;
    }
}
