using System;
using System.ComponentModel;

public class CollectionChangeEventArgsX : CollectionChangeEventArgs
{

	private string name;
    private object key;

    public CollectionChangeEventArgsX(string name, CollectionChangeAction action, object key, object element)
        : base(action, element) 
	{
        this.key = key;
        this.name = name;
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public object Key
    {
        get
        {
            return key;
        }
    }
}