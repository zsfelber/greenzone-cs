public class ZsfTraderId
{
    public enum TreeType {
        NONE, LIVE, UI
    }

    private string mt4LibPath;

    private TreeType type;

    public ZsfTraderId() : this(null,TreeType.NONE)
    {
    }

    public ZsfTraderId(string mt4LibPath, TreeType type) 
	{
        this.mt4LibPath = mt4LibPath;
        this.type = type;
	}

    public string Mt4LibPath
    {
        get
        {
            return mt4LibPath;
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
        return GetType().Name + "[" + mt4LibPath + " " + type + "]";
    }


    // TKey : ZsfTraderId   in  Dictionary<TKey,TValue> ...

    public override int GetHashCode()
    {
        return mt4LibPath.GetHashCode()+(int)type;
    }

    public override bool Equals(object obj)
    {
        return Equals((ZsfTraderId)obj);
    }

    public bool Equals(ZsfTraderId obj)
    {
        return obj != null && Utils.Equal(obj.mt4LibPath, this.mt4LibPath) && obj.type == this.type;
    }
}
