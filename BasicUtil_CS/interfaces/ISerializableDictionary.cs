using System.Collections.Generic;

public interface ISerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    void Copy(IDictionary<TKey, TValue> src);
}
