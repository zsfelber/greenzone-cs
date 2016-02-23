using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using GreenZoneUtil.Util;
using System.Collections;

namespace GreenZoneFxEngine.Util
{

    public interface ISeriesArr
    {
        void SetLengthAndDetachChildren(int value);
        int Length
        {
            get;
        }
        void Clear();

        int SearchMinimum(int start = 0, int len = 0);

        int SearchMaximum(int start = 0, int len = 0);
    }

    
    
    
    public unsafe class Memory
    {
        // Handle for the process heap. This handle is used in all calls to the
        // HeapXXX APIs in the methods below.
        static int ph = GetProcessHeap();
        // Private instance constructor to prevent instantiation.
        private Memory() { }
        // Allocates a memory block of the given size. The allocated memory is
        // automatically initialized to zero.
        public static void* Alloc(int size)
        {
            void* result = HeapAlloc(ph, HEAP_ZERO_MEMORY, size);
            if (result == null) throw new OutOfMemoryException();
            return result;
        }
        // Copies count bytes from src to dst. The source and destination
        // blocks are permitted to overlap.
        public static void Copy(void* src, void* dst, int count)
        {
            byte* ps = (byte*)src;
            byte* pd = (byte*)dst;
            if (ps > pd)
            {
                for (; count != 0; count--) *pd++ = *ps++;
            }
            else if (ps < pd)
            {
                for (ps += count, pd += count; count != 0; count--) *--pd = *--ps;
            }
        }

        public static void Clear(void* src, int count = 0)
        {
            if (count == 0)
            {
                count = SizeOf(src);
            }

            byte* ps = (byte*)src;

            for (int i = 0; i < count; i++, ps++) *ps = 0;
        }

        // Frees a memory block.
        public static void Free(void* block)
        {
            if (!HeapFree(ph, 0, block)) throw new InvalidOperationException();
        }
        // Re-allocates a memory block. If the reallocation request is for a
        // larger size, the additional region of memory is automatically
        // initialized to zero.
        public static void* ReAlloc(void* block, int size)
        {
            void* result = HeapReAlloc(ph, HEAP_ZERO_MEMORY, block, size);
            if (result == null) throw new OutOfMemoryException();
            return result;
        }
        // Returns the size of a memory block.
        public static int SizeOf(void* block)
        {
            int result = HeapSize(ph, 0, block);
            if (result == -1) throw new InvalidOperationException();
            return result;
        }
        // Heap API flags
        const int HEAP_ZERO_MEMORY = 0x00000008;
        // Heap API functions
        [DllImport("kernel32")]
        static extern int GetProcessHeap();
        [DllImport("kernel32")]
        static extern void* HeapAlloc(int hHeap, int flags, int size);
        [DllImport("kernel32")]
        static extern bool HeapFree(int hHeap, int flags, void* block);
        [DllImport("kernel32")]
        static extern void* HeapReAlloc(int hHeap, int flags,
           void* block, int size);
        [DllImport("kernel32")]
        static extern int HeapSize(int hHeap, int flags, void* block);
    }

    public unsafe abstract class VArr : ISeriesArr
    {
        internal abstract bool HasChild
        {
            set;
        }

        public abstract void SetLengthAndDetachChildren(int value);

        public abstract int Length
        {
            get;
            set;
        }

        public abstract void Clear();

        public abstract int SearchMinimum(int start = 0, int len = 0);

        public abstract int SearchMaximum(int start = 0, int len = 0);
    }

    public unsafe abstract class VArr<T, SELF> : VArr, IEnumerable where SELF : VArr<T, SELF>
    {
        internal readonly VArr Parent;
        int size;
        protected int length;
        public readonly int StartIndex;
        internal readonly bool Writable;

        protected byte* arr;
        private bool hasChild = false;

        public VArr(int size)
        {
            this.size = size;
            this.Parent = null;
            this.length = 0;
            this.StartIndex = 0;
        }

        public VArr(int size, int length)
        {
            this.size = size;
            this.Parent = null;
            this.length = length;
            this.StartIndex = 0;
            this.arr = (byte*)Memory.Alloc(length * size);
        }

        protected VArr(int size, VArr parent, byte* arr, int length, int startIndex, bool Writable)
        {
            this.size = size;
            parent.HasChild = true;
            this.Parent = parent;
            this.arr = arr;
            this.length = length;
            this.StartIndex = startIndex;
            this.Writable = Writable;
        }

        ~VArr()
        {
            if (Parent == null)
            {
                Memory.Free(arr);
            }
        }

        internal byte* Arr
        {
            get
            {
                return arr;
            }
        }

        internal override bool HasChild
        {
            set
            {
                hasChild = value;
            }
        }

        public override int Length
        {
            get
            {
                return length;
            }
            set
            {
                if (hasChild)
                {
                    throw new NotSupportedException("hasChild");
                }
                SetLengthAndDetachChildren(value);
            }
        }

        public int StartIndexP
        {
            get
            {
                return StartIndex;
            }
        }


        public T[] AsArray
        {
            get
            {
                T[] array = new T[Length - StartIndex];
                int i = 0;
                for (byte* p = arr - StartIndex * size; i < array.Length; p += size, i++)
                {
                    array[i] = GetPointedValue(p);
                }
                return array;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= StartIndex && index < Length)
                {
                    T result = GetPointedValue(arr + index * size);
                    return result;
                }
                else
                {
                    throw new IndexOutOfRangeException("" + index + " of " + StartIndex + ".." + (Length - 1));
                }
            }
            set
            {
                if (index >= StartIndex && index < Length)
                {
                    SetPointedValue(arr + index * size, value);
                }
                else
                {
                    throw new IndexOutOfRangeException("" + index + " of " + StartIndex + ".." + (Length - 1));
                }
            }
        }

        public override void SetLengthAndDetachChildren(int value)
        {
            if (Parent != null)
            {
                throw new NotSupportedException("Parent != null");
            }
            int memlen;
            if (this.length == 0)
            {
                memlen = 0;
            }
            else
            {
                memlen = Memory.SizeOf(arr) / size;
            }
            if (value > memlen)
            {
                byte* arr2 = (byte*)Memory.Alloc(value * size);
                Memory.Copy(arr, arr2, memlen * size);
                Memory.Free(arr);
                arr = arr2;
            }
            this.length = value;
        }

        public override void Clear()
        {
            Memory.Clear(arr, Length);
        }

        public SELF SubArr(int from, int startIndex = 0, bool Writable = true)
        {
            if (from >= 0 && from < Length)
            {
                SELF result = CreateReference(from, Length - from, startIndex, Writable);
                return result;
            }
            else
            {
                throw new IndexOutOfRangeException("" + from + " of " + "0.." + (Length - 1));
            }
        }

        public SELF SubArr(int from, int length, int startIndex = 0, bool Writable = true)
        {
            if (from >= Length)
            {
                throw new IndexOutOfRangeException("from: " + from + " >= " + (Length - 1));
            }
            else if (length > Length - from)
            {
                throw new IndexOutOfRangeException("length: " + length + " >= " + (Length - from));
            }
            else
            {
                SELF result = CreateReference(from, length, startIndex, Writable);
                return result;
            }
        }


        public SELF CreateReference(int from, int length, int startIndex = 0, bool Writable = true)
        {
            if (Parent != null || this.StartIndex < 0)
            {
                throw new NotSupportedException();
            }
            else if (from < 0)
            {
                throw new IndexOutOfRangeException("from: " + from + " < 0");
            }
            else if (length <= 0)
            {
                throw new IndexOutOfRangeException("length: " + length + " <= 0");
            }
            else if (startIndex < -from)
            {
                throw new IndexOutOfRangeException("startIndex < " + (-from));
            }
            else
            {
                int l = from + length;
                if (l > Length)
                {
                    byte* arr2 = (byte*)Memory.Alloc(l * size);
                    Memory.Copy(arr, arr2, this.length * size);
                    Memory.Free(arr);
                    arr = arr2;
                }
                SELF result = CreateChild(arr + from * size, length, startIndex, Writable);
                return result;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorWrapper<T>(AsArray.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return AsArray.GetEnumerator();
        }

        public abstract T GetPointedValue(byte* p);
        public abstract void SetPointedValue(byte* p, T value);
        public abstract SELF CreateChild(byte* arr, int length, int startIndex, bool Writable);
        public abstract int BinarySearch(T item, int arrayOrder = 1);
    }


    public unsafe class DArr : VArr<double, DArr>
    {
        public DArr()
            : base(sizeof(double))
        {
        }

        public DArr(int length)
            : base(sizeof(double), length)
        {
        }

        private DArr(DArr parent, byte* arr, int length, int startIndex, bool Writable)
            : base(sizeof(double), parent, arr, length, startIndex, Writable)
        {
        }

        public override double GetPointedValue(byte* p)
        {
            return *((double*)p);
        }

        public override void SetPointedValue(byte* p, double value)
        {
            *((double*)p) = value;
        }

        public override DArr CreateChild(byte* arr, int length, int startIndex, bool Writable)
        {
            DArr result = new DArr(this, arr, length, startIndex, Writable);
            return result;
        }



        public override int BinarySearch(double item, int arrayOrder = 1)
        {
            int result = PointerBinarySearch((double*)arr, item, Length, arrayOrder);
            return result;
        }

        public override int SearchMinimum(int start = 0, int len = 0)
        {
            if (len == 0)
            {
                len = this.length - start;
            }
            int result = start + PointerSearchMinimum((double*)arr + start, len);
            return result;
        }

        public override int SearchMaximum(int start = 0, int len = 0)
        {
            if (len == 0)
            {
                len = this.length - start;
            }
            int result = start + PointerSearchMaximum((double*)arr + start, len);
            return result;
        }

        public static int PointerBinarySearch(double* array, double item, int len, int arrayOrder = 1)
        {
            if (arrayOrder != 1 && arrayOrder != -1)
            {
                throw new ArgumentException("" + arrayOrder + " is not of -1,1");
            }
            int a = 0;
            int b = len - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                double midval = array[mid];

                switch (arrayOrder * Math.Sign(midval - item))
                {
                    case 0:
                        return mid;
                    case -1:
                        a = mid + 1;
                        break;
                    default:
                        b = mid - 1;
                        break;
                }
            }
            a = ~a;
            return a;
        }

        public static int PointerSearchMinimum(double* array, int len)
        {
            int index = -1;
            double min = double.MaxValue;
            for (int i = 0; i < len; i++, array++)
            {
                if (*array < min)
                {
                    index = i;
                    min = *array;
                }
            }
            return index;
        }

        public static int PointerSearchMaximum(double* array, int len)
        {
            int index = -1;
            double max = double.MinValue;
            for (int i = 0; i < len; i++, array++)
            {
                if (*array > max)
                {
                    index = i;
                    max = *array;
                }
            }
            return index;
        }
    }



    public unsafe class IArr : VArr<int, IArr>
    {
        public IArr()
            : base(sizeof(int))
        {
        }

        public IArr(int length)
            : base(sizeof(int), length)
        {
        }

        private IArr(IArr parent, byte* arr, int length, int startIndex, bool Writable)
            : base(sizeof(int), parent, arr, length, startIndex, Writable)
        {
        }

        public override int GetPointedValue(byte* p)
        {
            return *((int*)p);
        }

        public override void SetPointedValue(byte* p, int value)
        {
            *((int*)p) = value;
        }

        public override IArr CreateChild(byte* arr, int length, int startIndex, bool Writable)
        {
            IArr result = new IArr(this, arr, length, startIndex, Writable);
            return result;
        }



        public override int BinarySearch(int item, int arrayOrder = 1)
        {
            int result = PointerBinarySearch((int*)arr, item, Length, arrayOrder);
            return result;
        }

        public override int SearchMinimum(int start = 0, int len = 0)
        {
            if (len == 0)
            {
                len = this.length - start;
            }
            int result = start + PointerSearchMinimum((int*)arr + start, len);
            return result;
        }

        public override int SearchMaximum(int start = 0, int len = 0)
        {
            if (len == 0)
            {
                len = this.length - start;
            }
            int result = start + PointerSearchMaximum((int*)arr + start, len);
            return result;
        }

        public static int PointerBinarySearch(int* array, int item, int len, int arrayOrder = 1)
        {
            if (arrayOrder != 1 && arrayOrder != -1)
            {
                throw new ArgumentException("" + arrayOrder + " is not of -1,1");
            }
            int a = 0;
            int b = len - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                int midval = array[mid];

                switch (arrayOrder * Math.Sign(midval - item))
                {
                    case 0:
                        return mid;
                    case -1:
                        a = mid + 1;
                        break;
                    default:
                        b = mid - 1;
                        break;
                }
            }
            a = ~a;
            return a;
        }

        public static int PointerSearchMinimum(int* array, int len)
        {
            int index = -1;
            int min = int.MaxValue;
            for (int i = 0; i < len; i++, array++)
            {
                if (*array < min)
                {
                    index = i;
                    min = *array;
                }
            }
            return index;
        }

        public static int PointerSearchMaximum(int* array, int len)
        {
            int index = -1;
            int max = int.MinValue;
            for (int i = 0; i < len; i++, array++)
            {
                if (*array > max)
                {
                    index = i;
                    max = *array;
                }
            }
            return index;
        }
    }



    public unsafe class LArr : VArr<long, LArr>
    {
        public LArr()
            : base(sizeof(long))
        {
        }

        public LArr(int length)
            : base(sizeof(long), length)
        {
        }

        private LArr(LArr parent, byte* arr, int length, int startIndex, bool Writable)
            : base(sizeof(long), parent, arr, length, startIndex, Writable)
        {
        }

        public override long GetPointedValue(byte* p)
        {
            return *((long*)p);
        }

        public override void SetPointedValue(byte* p, long value)
        {
            *((long*)p) = value;
        }

        public override LArr CreateChild(byte* arr, int length, int startIndex, bool Writable)
        {
            LArr result = new LArr(this, arr, length, startIndex, Writable);
            return result;
        }



        public override int BinarySearch(long item, int arrayOrder = 1)
        {
            int result = PointerBinarySearch((long*)arr, item, Length, arrayOrder);
            return result;
        }

        public override int SearchMinimum(int start = 0, int len = 0)
        {
            if (len == 0)
            {
                len = this.length - start;
            }
            int result = start + PointerSearchMinimum((long*)arr + start, len);
            return result;
        }

        public override int SearchMaximum(int start = 0, int len = 0)
        {
            if (len == 0)
            {
                len = this.length - start;
            }
            int result = start + PointerSearchMaximum((long*)arr + start, len);
            return result;
        }

        public static int PointerBinarySearch(long* array, long item, int len, int arrayOrder = 1)
        {
            if (arrayOrder != 1 && arrayOrder != -1)
            {
                throw new ArgumentException("" + arrayOrder + " is not of -1,1");
            }
            int a = 0;
            int b = len - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                long midval = array[mid];

                switch (arrayOrder * Math.Sign(midval - item))
                {
                    case 0:
                        return mid;
                    case -1:
                        a = mid + 1;
                        break;
                    default:
                        b = mid - 1;
                        break;
                }
            }
            a = ~a;
            return a;
        }

        public static int PointerSearchMinimum(long* array, int len)
        {
            int index = -1;
            long min = long.MaxValue;
            for (int i = 0; i < len; i++, array++)
            {
                if (*array < min)
                {
                    index = i;
                    min = *array;
                }
            }
            return index;
        }

        public static int PointerSearchMaximum(long* array, int len)
        {
            int index = -1;
            long max = long.MinValue;
            for (int i = 0; i < len; i++, array++)
            {
                if (*array > max)
                {
                    index = i;
                    max = *array;
                }
            }
            return index;
        }
    }

    public unsafe class LArrAsIArr : VArr<int, LArrAsIArr>
    {

        public LArrAsIArr(LArr parent, bool Writable = true)
            : base(sizeof(long), parent, parent.Arr, parent.Length, parent.StartIndex, Writable)
        {
        }

        public override int GetPointedValue(byte* p)
        {
            //long* lp0 = (long*)p;
            int* lp = ((int*)p) + 1;
            return (*lp);
        }

        public override void SetPointedValue(byte* p, int value)
        {
            //long* lp0 = (long*)p;
            int* lp = ((int*)p) + 1;
            *lp = value;
        }

        public override LArrAsIArr CreateChild(byte* arr, int length, int startIndex, bool Writable)
        {
            throw new NotSupportedException();
        }

        public override int BinarySearch(int item, int arrayOrder = 1)
        {
            throw new NotSupportedException();
        }

        public override int SearchMinimum(int start = 0, int len = 0)
        {
            throw new NotSupportedException();
        }

        public override int SearchMaximum(int start = 0, int len = 0)
        {
            throw new NotSupportedException();
        }

    }


}
