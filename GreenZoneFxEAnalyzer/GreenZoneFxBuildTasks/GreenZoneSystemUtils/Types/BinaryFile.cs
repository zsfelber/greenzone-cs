using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GreenZoneFxEngine.Types
{
    public class BinaryFileR : GeneralFile
    {
        private bool readMode;
        private BinaryReader reader;
        protected FileMode fileMode;
        protected FileAccess fileAccess;
        protected long position;

        public BinaryFileR(string file)
            : this(file, false, true)
        {
        }

        protected internal BinaryFileR(string file, bool append, bool readMode)
        {
            this.readMode = readMode;
            if (readMode)
            {
                if (append)
                {
                    fileMode = FileMode.OpenOrCreate;
                    fileAccess = FileAccess.ReadWrite;
                    reader = new BinaryReader(System.IO.File.Open(file, fileMode, fileAccess), Encoding.ASCII);
                }
                else
                {
                    fileMode = FileMode.Open;
                    fileAccess = FileAccess.Read;
                    reader = new BinaryReader(System.IO.File.Open(file, fileMode, fileAccess), Encoding.ASCII);
                }
                position = append ? reader.BaseStream.Length : 0L;
            }
            else
            {
                position = 0L;
            }
        }

        public BinaryReader Reader
        {
            get { return reader; }
        }

        public override Stream Stream
        {
            get { return reader.BaseStream; }
        }

        public override FileMode FileMode
        {
            get { return fileMode; }
        }

        public override FileAccess FileAccess
        {
            get { return fileAccess; }
        }

        public override long Tell()
        {
            return position;
        }

        public override void Seek(long offset, SeekOrigin origin)
        {
            Stream.Seek(offset, origin);
            position = Stream.Position;
        }

        public override string ReadString(int count, bool defaultMode = true)
        {
            if (defaultMode && count < 0)
            {
                throw new NotSupportedException("Negative length not supported in default mode");
            }
            else
            {
                if (!readMode)
                {
                    throw new NotSupportedException("File is opened in WRITE mode");
                }
                else if (Tell() >= Size())
                {
                    throw new EOFException("End of file reached.  pos : " + Tell());
                }
                else if (count >= 0)
                {
                    byte[] buffer = new byte[count];
                    int v = Stream.Read(buffer, 0, count);
                    position += count;
                    string r = Encoding.ASCII.GetString(buffer);
                    r = r.TrimEnd('\0');
                    return r;
                }
                else
                {
                    StringBuilder r = new StringBuilder();
                    char ch;
                    while (true)
                    {
                        ch = reader.ReadChar();
                        position++;
                        if (ch == '\r')
                        {
                            ch = reader.ReadChar();
                            position++;
                            break;
                        }
                        r.Append(ch);
                    }
                    return r.ToString();
                }
            }
        }

        public override int ReadInteger(FileIntegerType bytes = FileIntegerType.LONG_VALUE)
        {
            if (!readMode)
            {
                throw new NotSupportedException("File is opened in WRITE mode");
            }
            else if (Tell() >= Size())
            {
                throw new EOFException("End of file reached.  pos : " + Tell());
            }
            else
            {
                int v;
                switch (bytes)
                {
                    case FileIntegerType.LONG_VALUE:
                        v = reader.ReadInt32();
                        position += sizeof(Int32);
                        break;
                    case FileIntegerType.SHORT_VALUE:
                        v = reader.ReadInt16();
                        position += sizeof(Int16);
                        break;
                    case FileIntegerType.CHAR_VALUE:
                        v = reader.ReadByte();
                        position += sizeof(byte);
                        break;
                    default:
                        throw new ArgumentException("bytes : " + bytes);
                }
                return v;
            }
        }

        public override double ReadDouble(FileDoubleType bytes = FileDoubleType.DOUBLE_VALUE)
        {
            if (!readMode)
            {
                throw new NotSupportedException("File is opened in WRITE mode");
            }
            else if (Tell() >= Size())
            {
                throw new EOFException("End of file reached.  pos : " + Tell());
            }
            else
            {
                double v;
                switch (bytes)
                {
                    case FileDoubleType.DOUBLE_VALUE:
                        v = reader.ReadDouble();
                        position += sizeof(double);
                        break;
                    case FileDoubleType.FLOAT_VALUE:
                        v = reader.ReadSingle();
                        position += sizeof(float);
                        break;
                    default:
                        throw new ArgumentException("bytes : " + bytes);
                }
                return v;
            }
        }

        public override double ReadNumber()
        {
            throw new NotSupportedException("Not supported for binary file");
        }

        public override int ReadArray<T>(T[] _array, int start, int count)
        {
            int bytes = 0;

            if (_array is string[])
            {
                string[] array = (string[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    string s = ReadString(-1, false);
                    array[i] = s;
                    bytes += s.Length + 2;
                }
            }
            else if (_array is double[])
            {
                double[] array = (double[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    double d = ReadDouble();
                    array[i] = d;
                    bytes += sizeof(double);
                }
            }
            else if (_array is int[])
            {
                int[] array = (int[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    int n = ReadInteger();
                    array[i] = n;
                    bytes += sizeof(int);
                }
            }
            return bytes;
        }
    }

    public class BinaryFileW : BinaryFileR
    {
        private BinaryWriter writer;

        public BinaryFileW(string file, bool append)
            : base(file, append, append)
        {
            if (append)
            {
                writer = new BinaryWriter(Reader.BaseStream);
            }
            else
            {
                fileMode = FileMode.Create;
                fileAccess = FileAccess.Write;
                writer = new BinaryWriter(System.IO.File.Open(file, fileMode, fileAccess), Encoding.ASCII);
            }
        }

        public BinaryWriter Writer
        {
            get { return writer; }
        }

        public override Stream Stream
        {
            get { return writer.BaseStream; }
        }

        public override void Flush()
        {
            writer.Flush();
            Stream.Flush();
        }

        public override int WriteString(string str, bool defaultMode = true, int length = -1)
        {
            if (defaultMode && length < 0)
            {
                throw new NotSupportedException("Negative length not supported in default mode");
            }
            else
            {
                byte[] buffer;
                if (length < 0)
                {
                    buffer = Encoding.ASCII.GetBytes(str);
                }
                else if (length < str.Length)
                {
                    buffer = Encoding.ASCII.GetBytes(str.Substring(0, length));
                }
                else
                {
                    buffer = Encoding.ASCII.GetBytes(str);
                }
                Stream.Write(buffer, 0, buffer.Length);
                position += buffer.Length;

                for (int i = str.Length; i < length; i++)
                {
                    Stream.WriteByte(0);
                    position++;
                }
                return buffer.Length;
            }
        }

        public override void WriteInteger(int v, FileIntegerType bytes = FileIntegerType.LONG_VALUE)
        {
            switch (bytes)
            {
                case FileIntegerType.LONG_VALUE:
                    writer.Write(v);
                    position += sizeof(Int32);
                    break;
                case FileIntegerType.SHORT_VALUE:
                    writer.Write((Int16)v);
                    position += sizeof(Int16);
                    break;
                case FileIntegerType.CHAR_VALUE:
                    writer.Write((byte)v);
                    position += sizeof(byte);
                    break;
                default:
                    throw new ArgumentException("bytes : " + bytes);
            }
        }

        public override void WriteDouble(double v, FileDoubleType bytes = FileDoubleType.DOUBLE_VALUE)
        {
            switch (bytes)
            {
                case FileDoubleType.DOUBLE_VALUE:
                    writer.Write(v);
                    position += sizeof(double);
                    break;
                case FileDoubleType.FLOAT_VALUE:
                    writer.Write((float)v);
                    position += sizeof(float);
                    break;
                default:
                    throw new ArgumentException("bytes : " + bytes);
            }
        }


        public override int WriteArray<T>(T[] _array, int start, int count, bool defaultMode)
        {
            int bytes = 0;
            if (_array is string[])
            {
                string[] array = (string[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    var v = array[i];
                    bytes += WriteString(v + "\r\n", false);
                }
            }
            else if (_array is double[])
            {
                double[] array = (double[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    var v = array[i];
                    WriteDouble(v);
                    bytes += sizeof(double);
                }
            }
            else if (_array is int[])
            {
                int[] array = (int[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    var v = array[i];
                    WriteInteger(v);
                    bytes += sizeof(int);
                }
            }
            else if (!defaultMode)
            {
                object[] array = (object[])(object)_array;
                for (int i = start, j = 0; j < count; i++, j++)
                {
                    var v = array[i];
                    try
                    {
                        double d = Convert.ToDouble(v);
                        WriteDouble(d);
                        bytes += sizeof(double);
                    }
                    catch (FormatException)
                    {
                        bytes += WriteString(v + "\r\n", false);
                    }
                }
            }

            return bytes;
        }

        public override int Write(params object[] array)
        {
            throw new NotSupportedException("Not supported for binary file");
        }
    }
}
