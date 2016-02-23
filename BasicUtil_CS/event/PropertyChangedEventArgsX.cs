using System.ComponentModel;

public class PropertyChangedEventArgsX : PropertyChangedEventArgs
{

	private object oldVal;

	private object newVal;


	public PropertyChangedEventArgsX(string name, object oldVal, object newVal) : base(name)
	{
        this.oldVal = oldVal;
        this.newVal = newVal;
	}

	public object OldVal
	{
		get
		{
			return oldVal;
		}
	}

	public object NewVal
	{
		get
		{
			return newVal;
		}
	}
}