using System.ComponentModel;
using System.Collections.Generic;
using System;


public abstract class BaseConfig : INotifyPropertyChanged, ILock
{

	public event PropertyChangedEventHandler PropertyChanged;

	public event CollectionChangeEventHandler CollectionChanged;

    private int locks = 0;

    protected void OnPropertyChanged(string name, object oldVal, object newVal)
	{
        if (!Utils.Equal(oldVal,newVal))
        {
            if (PropertyChanged == null)
            {
                EALogger.Log(this + " PropertyChanged == null  name:" + name + "  value:" + oldVal + "->" + newVal, EALogger.SEV_WARNING);
            }
            else
            {
#if (DEBUG)
                EALogger.Log(this + " PropertyChanged  name:" + name + "  value:" + oldVal + "->" + newVal, EALogger.SEV_DEBUG_2);
#endif
                PropertyChanged(this, new PropertyChangedEventArgsX(name, oldVal, newVal));
            }
        }
	}
	protected void OnCollectionChanged(object sender, string name, CollectionChangeAction action, object key, object element)
	{
        if (CollectionChanged == null)
        {
            EALogger.Log(this + " CollectionChanged == null  name:" + name + "  action:" + action + "  element:" + element, EALogger.SEV_WARNING);
        }
        else
        {
#if (DEBUG)
            EALogger.Log(this + " CollectionChanged  name:" + name + "  action:" + action + "  element:" + element, EALogger.SEV_DEBUG_2);
#endif
            CollectionChanged(sender, new CollectionChangeEventArgsX(name, action, key, element));
        }
	}


	protected void ClearXXX<TKey, TValue>(IDictionary<TKey,TValue> xxx, string name)
	{
		xxx.Clear();
        OnCollectionChanged(xxx, name, CollectionChangeAction.Refresh, null, null);
	}

	protected void ReplaceXXX<TKey, TValue>(ISerializableDictionary<TKey,TValue> xxx, string name, IDictionary<TKey,TValue> _xxx, CopyMode copyMode)
	{
        if (typeof(BaseConfig).IsAssignableFrom(typeof(TValue)))
        {
            SerializableDictionary<TKey, TValue> copy_xxx = new SerializableDictionary<TKey, TValue>();

            TValue o0 = Activator.CreateInstance<TValue>();
            ICopiable<TValue> o = (ICopiable<TValue>)o0;
            BaseConfig ob = (BaseConfig)o;

            copy_xxx.Copy(xxx);
            foreach (KeyValuePair<TKey, TValue> kvp in _xxx)
            {
                if (kvp.Value != null)
                {
                    ob.SetId(kvp.Key);
                    o.Copy(kvp.Value, copyMode);
                    SetXXX(xxx, name, kvp.Key, (TValue)o, copyMode);
                    if ((ICopiable<TValue>)xxx[kvp.Key] == o)
                    {
                        o0 = Activator.CreateInstance<TValue>();
                        o = (ICopiable<TValue>)o0;
                        ob = (BaseConfig)o;
                    }
                    copy_xxx.Remove(kvp.Key);
                }
            }
            foreach (KeyValuePair<TKey, TValue> kvp in copy_xxx)
            {
                RemoveXXX(xxx, name, kvp.Key);
            }
        }
        else
        {
            EALogger.Log("BaseConfig.ReplaceXXX  TValue:" + typeof(TValue) + " is not BaseConfig  this:" + this + " name:" + name, EALogger.SEV_DEBUG_2);
            
            ObjSerializableDictionary copy_vars = new ObjSerializableDictionary();
            ISerializableDictionary<object, object> xxx0 = (ISerializableDictionary<object, object>)xxx;
            IDictionary<object, object> _xxx0 = (IDictionary<object, object>)_xxx;

            copy_vars.Copy(xxx0);
            foreach (KeyValuePair<Object, Object> kvp in _xxx0)
            {
                SetXXX(xxx0, name, kvp.Key, kvp.Value, copyMode);
			    copy_vars.Remove(kvp.Key);
		    }
		    foreach (KeyValuePair<Object,Object> kvp in copy_vars) {
			    RemoveXXX(xxx0,name,kvp.Key);
		    }
        }
    }

    protected void SetXXX<TKey, TValue>(IDictionary<TKey, TValue> xxx, string name, TKey key, TValue value, CopyMode copyMode)
	{
        TValue old;
        if (!xxx.TryGetValue(key, out old))
		{
			xxx[key] = value;
			OnCollectionChanged(xxx, name, CollectionChangeAction.Add, key, value);
		}
		else
		{
			ICopiable<TValue> oldc = (ICopiable<TValue>)(old);
			if (oldc != null)
			{
                oldc.Copy(value, copyMode);
			}
			else
			{
				xxx[key] = value;
				OnCollectionChanged(xxx, name, CollectionChangeAction.Remove, key, old);
                OnCollectionChanged(xxx, name, CollectionChangeAction.Add, key, value);
			}
		}
	}

    protected void RemoveXXX<TKey, TValue>(IDictionary<TKey, TValue> xxx, string name, TKey key)
	{
        TValue old;
        if (xxx.TryGetValue(key, out old))
		{
			xxx.Remove(key);
			OnCollectionChanged(xxx, name, CollectionChangeAction.Remove, key, old);
		}
	}

    public override string ToString()
    {
        return GetType().Name + "[" + GetId()+"]";
    }

    public abstract object GetId();

    protected abstract void SetId(object id);


    public void IncLock()
    {
        lock(this) {
            locks++;
        }
    }

    public void DecLock()
    {
        lock(this) {
            locks--;
        }
    }

    public int GetLockCnt()
    {
        lock(this) {
            return locks;
        }
    }
}