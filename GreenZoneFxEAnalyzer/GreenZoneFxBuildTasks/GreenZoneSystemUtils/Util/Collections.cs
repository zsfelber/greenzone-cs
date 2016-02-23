using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.Runtime;

namespace GreenZoneUtil.Util
{
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly IDictionary<TKey, TValue> items;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> items)
        {
            this.items = items;
        }

        public TValue this[TKey key]
        {
            get
            {
                return items[key];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool ContainsKey(TKey key)
        {
            return items.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return items.TryGetValue(key, out value);
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return items.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return items.Values;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return items.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }


    // Summary:
    //     Provides the base class for a generic read-only collection.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the collection.
    [Serializable]
    [ComVisible(false)]
    [DebuggerDisplay("Count = {Count}")]
    public class ReadOnlyCollection : IList, ICollection, IEnumerable
    {
        IList list;

        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     class that is a read-only wrapper around the specified list.
        //
        // Parameters:
        //   list:
        //     The list to wrap.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     list is null.
        public ReadOnlyCollection(IList list)
        {
            this.list = list;
        }

        // Summary:
        //     Gets the number of elements contained in the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     instance.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     instance.
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        //
        // Summary:
        //     Returns the System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     wraps.
        //
        // Returns:
        //     The System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     wraps.
        protected IList Items
        {
            get
            {
                return list;
            }
        }

        // Summary:
        //     Gets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.ReadOnlyCollection<T>.Count.
        public object this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // Summary:
        //     Determines whether an element is in the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //
        // Parameters:
        //   value:
        //     The object to locate in the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //     The value can be null for reference types.
        //
        // Returns:
        //     true if value is found in the System.Collections.ObjectModel.ReadOnlyCollection<T>;
        //     otherwise, false.
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public bool Contains(object value)
        {
            return list.Contains(value);
        }

        //
        // Summary:
        //     Copies the entire System.Collections.ObjectModel.ReadOnlyCollection<T> to
        //     a compatible one-dimensional System.Array, starting at the specified index
        //     of the target array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.ObjectModel.ReadOnlyCollection<T>. The System.Array
        //     must have zero-based indexing.
        //
        //   index:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.
        //
        //   System.ArgumentException:
        //     The number of elements in the source System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     is greater than the available space from index to the end of the destination
        //     array.
        public void CopyTo(object[] array, int index)
        {
            list.CopyTo(array, index);
        }

        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //
        // Returns:
        //     An System.Collections.Generic.IEnumerator<T> for the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();

        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     first occurrence within the entire System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //
        // Parameters:
        //   value:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the entire System.Collections.ObjectModel.ReadOnlyCollection<T>,
        //     if found; otherwise, -1.
        public int IndexOf(object value)
        {
            return list.IndexOf(value);
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { return list.IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { return list.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return list.SyncRoot; }
        }
    }


    // Summary:
    //     Provides the base class for a generic read-only collection.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the collection.
    [Serializable]
    [ComVisible(false)]
    [DebuggerDisplay("Count = {Count}")]
    public class ReadOnlyBridgeCollection<T> : BridgeCollection<T>, IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {

        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     class that is a read-only wrapper around the specified list.
        //
        // Parameters:
        //   list:
        //     The list to wrap.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     list is null.
        public ReadOnlyBridgeCollection(IList list)
            : base(list)
        {
        }

        // Summary:
        //     Gets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.ReadOnlyCollection<T>.Count.
        object IList.this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Clear()
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        T IList<T>.this[int index]
        {
            get
            {
                return (T)list[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            EnumeratorWrapper<T> result = new EnumeratorWrapper<T>(list.GetEnumerator());
            return result;
        }
    }






    // Summary:
    //     Provides the base class for a generic read-only collection.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the collection.
    [Serializable]
    [ComVisible(false)]
    [DebuggerDisplay("Count = {Count}")]
    public class BridgeCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        protected readonly IList list;

        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     class that is a read-only wrapper around the specified list.
        //
        // Parameters:
        //   list:
        //     The list to wrap.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     list is null.
        public BridgeCollection(IList list)
        {
            this.list = list;
        }

        // Summary:
        //     Gets the number of elements contained in the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     instance.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     instance.
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        //
        // Summary:
        //     Returns the System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     wraps.
        //
        // Returns:
        //     The System.Collections.Generic.IList<T> that the System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     wraps.
        protected IList Items
        {
            get
            {
                return list;
            }
        }

        // Summary:
        //     Gets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.-or-index is equal to or greater than System.Collections.ObjectModel.ReadOnlyCollection<T>.Count.
        public object this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        // Summary:
        //     Determines whether an element is in the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //
        // Parameters:
        //   value:
        //     The object to locate in the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //     The value can be null for reference types.
        //
        // Returns:
        //     true if value is found in the System.Collections.ObjectModel.ReadOnlyCollection<T>;
        //     otherwise, false.
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public bool Contains(object value)
        {
            return list.Contains(value);
        }

        //
        // Summary:
        //     Copies the entire System.Collections.ObjectModel.ReadOnlyCollection<T> to
        //     a compatible one-dimensional System.Array, starting at the specified index
        //     of the target array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.ObjectModel.ReadOnlyCollection<T>. The System.Array
        //     must have zero-based indexing.
        //
        //   index:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.
        //
        //   System.ArgumentException:
        //     The number of elements in the source System.Collections.ObjectModel.ReadOnlyCollection<T>
        //     is greater than the available space from index to the end of the destination
        //     array.
        public void CopyTo(object[] array, int index)
        {
            list.CopyTo(array, index);
        }

        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //
        // Returns:
        //     An System.Collections.Generic.IEnumerator<T> for the System.Collections.ObjectModel.ReadOnlyCollection<T>.
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();

        }
        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the
        //     first occurrence within the entire System.Collections.ObjectModel.ReadOnlyCollection<T>.
        //
        // Parameters:
        //   value:
        //     The object to locate in the System.Collections.Generic.List<T>. The value
        //     can be null for reference types.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the entire System.Collections.ObjectModel.ReadOnlyCollection<T>,
        //     if found; otherwise, -1.
        public int IndexOf(object value)
        {
            return list.IndexOf(value);
        }

        public int Add(object value)
        {
            return list.Add(value);
        }

        public void Clear()
        {
            list.Clear();
        }

        public void Insert(int index, object value)
        {
            list.Insert(index, value);
        }

        public bool IsFixedSize
        {
            get { return list.IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public void Remove(object value)
        {
            list.Remove(value);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public bool IsSynchronized
        {
            get { return list.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return list.SyncRoot; }
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        T IList<T>.this[int index]
        {
            get
            {
                return (T)list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            list.Remove((object)item);
            //TODO return ?
            return true;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            EnumeratorWrapper<T> result = new EnumeratorWrapper<T>(list.GetEnumerator());
            return result;
        }
    }


    public class EnumerableWrapper<T> : IEnumerable<T>
    {
        System.Collections.IEnumerable enumerable;

        public EnumerableWrapper(System.Collections.IEnumerable enumerable)
        {
            this.enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorWrapper<T>(this.enumerable.GetEnumerator());
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.enumerable.GetEnumerator();
        }
    }

    public class EnumeratorWrapper<T> : IEnumerator<T>
    {
        System.Collections.IEnumerator enumerator;

        public EnumeratorWrapper(System.Collections.IEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        public void Dispose()
        {
        }

        public T Current
        {
            get { return (T)this.enumerator.Current; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.enumerator.Current; }
        }

        public bool MoveNext()
        {
            return this.enumerator.MoveNext();
        }

        public void Reset()
        {
            this.enumerator.Reset();
        }
    }
}
