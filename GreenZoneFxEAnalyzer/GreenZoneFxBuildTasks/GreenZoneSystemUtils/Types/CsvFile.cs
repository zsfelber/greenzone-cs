using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace GreenZoneFxEngine.Types
{
    public class CsvFileR : GeneralFile
    {
        private bool readMode;
        private StreamReader reader;
        protected FileMode fileMode;
        protected FileAccess fileAccess;
        protected string[] delimiters;
        protected int delimiterLen;
        protected string line;
        protected string[] row;
        protected long rowIndexFromLastPos;
        protected long columnIndex;
        protected long columnPos;
        protected long position;

        public CsvFileR(string file)
            : this(file, false, true, ";")
        {
        }

        public CsvFileR(string file, params string[] delimiters)
            : this(file, false, true, delimiters)
        {
        }

        protected internal CsvFileR(string file, bool append, bool readMode, params string[] delimiters)
        {
            this.readMode = readMode;
            if (readMode)
            {
                if (append)
                {
                    fileMode = FileMode.OpenOrCreate;
                    fileAccess = FileAccess.ReadWrite;
                    reader = new StreamReader(System.IO.File.Open(file, fileMode, fileAccess), Encoding.ASCII);
                }
                else
                {
                    fileMode = FileMode.Open;
                    fileAccess = FileAccess.Read;
                    reader = new StreamReader(System.IO.File.Open(file, fileMode, fileAccess), Encoding.ASCII);
                }
            }
            this.delimiters = delimiters;
            delimiterLen = delimiters[0].Length;
            foreach (string s in delimiters)
            {
                if (s.Length != delimiterLen)
                {
                    throw new NotSupportedException("delimiters must be equally sized : " + s.Length + " != " + delimiterLen);
                }
            }
            ResetRow();
            rowIndexFromLastPos = -1;
            position = 0;
        }

        public StreamReader Reader
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
            return position - line.Length + columnPos;
        }

        public override void Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Current)
            {
                Stream.Seek(Tell() + offset, SeekOrigin.Begin);
            }
            else
            {
                Stream.Seek(offset, origin);
            }
            ResetRow();
            rowIndexFromLastPos = -1;
            position = Stream.Position;
            reader.DiscardBufferedData();
        }

        public override bool IsLineEnding()
        {
            return columnIndex >= row.Length;
        }

        public override bool IsEnding()
        {
            switch (Math.Sign(Tell() - Size()))
            {
                case 1:     return true;
                case -1:    return false;
                default:
                    if (string.IsNullOrEmpty(line))
                    {
                        return true;
                    }
                    else if (line.EndsWith("\n") || line.EndsWith("\r"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                            
            }
        }

        private void ResetRow()
        {
            columnIndex = 0;
            columnPos = 0;
            line = "";
            row = new string[0];
        }

        private bool ReadLine()
        {
            string _line = reader.ReadLine();
            if (_line == null)
            {
                if (line.EndsWith("\n") || line.EndsWith("\r"))
                {
                    line = "";
                    row = new string[] { "" };
                    rowIndexFromLastPos++;
                    columnIndex = 0;
                    columnPos = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                line = _line;
                row = line.Split(delimiters, StringSplitOptions.None);
                rowIndexFromLastPos++;
                columnIndex = 0;
                columnPos = 0;
                position += line.Length;
                if (!IsEnding())
                {
                    position += 2;
                    line += "\r\n";
                }
                return true;
            }
        }

        public string ReadString()
        {
            if (!readMode)
            {
                throw new NotSupportedException("File is opened in WRITE mode");
            }
            else if (IsLineEnding())
            {
                if (!ReadLine())
                {
                    throw new EOFException("End of file reached.  pos   csv:" + Tell() + " stream:" + Stream.Position + "/" + position + " of " + Size() + "    column   index:" + columnIndex + "  pos:" + columnPos);
                }
            }

            string r = row[columnIndex];
            if (columnIndex > 0)
            {
                columnPos += delimiterLen;
            }
            columnPos += r.Length;
            columnIndex++;
            if (IsLineEnding() && !IsEnding())
            {
                columnPos += 2;
            }
            return r;
        }

        public override double ReadNumber()
        {
            return Convert.ToDouble(ReadString());
        }

        public int ReadInteger()
        {
            return Convert.ToInt32(ReadString());
        }

        public double ReadDouble()
        {
            return Convert.ToDouble(ReadString());
        }

        public override string ReadString(int count = 0, bool defaultMode = true)
        {
            return ReadString();
        }

        public override int ReadInteger(FileIntegerType bytes = FileIntegerType.LONG_VALUE)
        {
            throw new NotSupportedException("Not supported for CSV");
        }

        public override double ReadDouble(FileDoubleType bytes = FileDoubleType.DOUBLE_VALUE)
        {
            throw new NotSupportedException("Not supported for CSV");
        }

        public object[] ReadArrayCsv(bool format = true)
        {
            ArrayList r = new ArrayList();
            ReadArrayCsv(r, 0, -1, format);
            return r.ToArray();
        }

        public int ReadArrayCsv(IList array, int start, int count, bool format = true)
        {
            int aisz = array.Count;
            int bytes = 0;
            for (int i = start, j = 0; j < count || count < 0; i++, j++)
            {
                string s;
                if (count < 0 && j > 0 && IsLineEnding())
                {
                    count = j;
                    bytes += 2;
                    break;
                }
                s = ReadString();

                if (columnIndex > 1)
                {
                    bytes += delimiterLen;
                }

                if (array.IsFixedSize || aisz > 0)
                {
                    if (format)
                    {
                        try
                        {
                            array[i] = Convert.ToDouble(s);
                        }
                        catch (FormatException)
                        {
                            array[i] = s;
                        }
                    }
                    else
                    {
                        array[i] = s;
                    }
                }
                else
                {
                    if (format)
                    {
                        try
                        {
                            array.Add(Convert.ToDouble(s));
                        }
                        catch (FormatException)
                        {
                            array.Add(s);
                        }
                    }
                    else
                    {
                        array.Add(s);
                    }
                }
                bytes += s.Length;
            }

            return bytes;
        }

        public override int ReadArray<T>(T[] array, int start, int count)
        {
            throw new NotSupportedException("Not supported for CSV");
        }
    }

    public class CsvFileW : CsvFileR
    {
        private Stream stream;

        public CsvFileW(string file, bool append)
            : this(file, append, ";")
        {
        }

        public CsvFileW(string file, bool append, params string[] delimiters)
            : base(file, append, append, delimiters)
        {
            if (append)
            {
                stream = Reader.BaseStream;
            }
            else
            {
                fileMode = FileMode.Create;
                fileAccess = FileAccess.Write;
                stream = System.IO.File.Open(file, fileMode, fileAccess);
            }
        }

        public override Stream Stream
        {
            get { return stream; }
        }

        public int WriteStringCsv(string str, int length = -1)
        {
            int bytes = 0;
            byte[] buffer;
            if (columnIndex > 0)
            {
                buffer = Encoding.ASCII.GetBytes(delimiters[0]);
                Stream.Write(buffer, 0, delimiterLen);
                bytes += delimiterLen;
            }
            if (length < 0)
            {
                buffer = Encoding.ASCII.GetBytes(str);
            }
            else
            {
                buffer = Encoding.ASCII.GetBytes(str.Substring(0,length));
            }
            Stream.Write(buffer, 0, buffer.Length);
            bytes += buffer.Length;
            if (str.EndsWith("\n") || str.EndsWith("\r"))
            {
                columnPos += bytes;
                position += columnPos;
                rowIndexFromLastPos++;
                columnPos = 0;
                columnIndex = 0;
            }
            else
            {
                columnPos += bytes;
                columnIndex++;
            }
            if (length > str.Length)
            {
                for (int i = str.Length; i < length; i++)
                {
                    Stream.WriteByte(0);
                    columnPos ++;
                }
            }
            return bytes;
        }

        public override int WriteArray<T>(T[] array, int start, int count, bool defaultMode=true)
        {
            if (defaultMode || !(array is object[]))
            {
                throw new NotSupportedException("Not supported for CSV file");
            }
            else
            {
                return WriteArrayCsv((object[])(object)array, start, count);
            }
        }

        public override int Write(params object[] array)
        {
            return WriteArrayCsv(array, 0, array.Length);
        }

        public int WriteArrayCsv(object[] array, int start, int count)
        {
            int bytes = 0;
            for (int i = start, j = 0; j < count; i++, j++)
            {
                var v = array[i];
                if (v is string)
                {
                    bytes += WriteStringCsv((string)v);
                }
                else
                {
                    bytes += WriteNumberCsv((double)v);
                }
            }
            byte[] buffer = Encoding.ASCII.GetBytes("\r\n");
            Stream.Write(buffer, 0, 2);

            bytes += 2;
            position += bytes;
            rowIndexFromLastPos++;
            columnPos = 0;
            columnIndex = 0;
            return bytes;
        }

        public int WriteNumberCsv(double v)
        {
            int bytes = 0;
            byte[] buffer;
            if (columnIndex > 0)
            {
                buffer = Encoding.ASCII.GetBytes(delimiters[0]);
                Stream.Write(buffer, 0, delimiterLen);
                bytes += delimiterLen;
            }
            string s = Convert.ToString(v);

            buffer = Encoding.ASCII.GetBytes(s);
            Stream.Write(buffer, 0, s.Length);
            bytes += s.Length;
            
            columnPos += bytes;
            columnIndex++;
            return bytes;
        }

        public override int WriteString(string str, bool defaultMode = true, int length = -1)
        {
            if (defaultMode)
            {
                throw new NotSupportedException("Not supported for CSV");
            }
            else
            {
                return WriteStringCsv(str, length);
            }
        }

        public override void WriteInteger(int v, FileIntegerType bytes = FileIntegerType.LONG_VALUE)
        {
            throw new NotSupportedException("Not supported for CSV");
        }

        public override void WriteDouble(double v, FileDoubleType bytes = FileDoubleType.DOUBLE_VALUE)
        {
            throw new NotSupportedException("Not supported for CSV");
        }
    }
}
