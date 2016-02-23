using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace GreenZoneFxEngine.Types
{
    public abstract class GeneralFile
    {
        public GeneralFile()
        {
        }

        public abstract Stream Stream { get; }

        public abstract FileMode FileMode { get; }

        public abstract FileAccess FileAccess { get; }

        public abstract long Tell();

        public virtual long Size()
        {
            return Stream.Length;
        }

        public virtual void Seek(long offset, SeekOrigin origin)
        {
            Stream.Seek(offset, origin);
        }

        public void Close()
        {
            Stream.Close();
        }

        public virtual void Flush()
        {
            Stream.Flush();
        }

        public virtual bool IsEnding()
        {
            return Tell() >= Size();
        }

        public virtual bool IsLineEnding()
        {
            throw new NotSupportedException("Not a CSV / File is opened in WRITE mode");
        }

        public abstract string ReadString(int count = 0, bool defaultMode = true);

        public abstract int ReadInteger(FileIntegerType bytes = FileIntegerType.LONG_VALUE);

        public abstract double ReadDouble(FileDoubleType bytes = FileDoubleType.DOUBLE_VALUE);

        public abstract double ReadNumber();

        public abstract int ReadArray<T>(T[] array, int start, int count);


        public int WriteString(string str, int length)
        {
            return WriteString(str, true, length);
        }

        public virtual int WriteString(string str, bool defaultMode = true, int length = -1)
        {
            throw new NotSupportedException("File is opened in READ mode");
        }

        public virtual void WriteInteger(int v, FileIntegerType bytes = FileIntegerType.LONG_VALUE)
        {
            throw new NotSupportedException("File is opened in READ mode");
        }

        public virtual void WriteDouble(double v, FileDoubleType bytes = FileDoubleType.DOUBLE_VALUE)
        {
            throw new NotSupportedException("File is opened in READ mode");
        }

        public int WriteArray(params object[] array)
        {
            return WriteArray(array, 0, array.Length, false);
        }

        public virtual int WriteArray<T>(T[] array, int start, int count, bool defaultMode=true)
        {
            throw new NotSupportedException("File is opened in READ mode");
        }

        public virtual int Write(params object[] array)
        {
            throw new NotSupportedException("File is opened in READ mode");
        }


        string rpad(string str, char pad, int len)
        {
            StringBuilder b = new StringBuilder();
            b.Append(str);
            while (b.Length < len)
            {
                b.Append(pad);
            }
            return b.ToString();
        }
    }


    public enum FileIntegerType
    {
        CHAR_VALUE = 1,
        SHORT_VALUE = 2,
        LONG_VALUE = 4
    }
    public enum FileDoubleType
    {
        FLOAT_VALUE = 4,
        DOUBLE_VALUE = 8
    }
}
