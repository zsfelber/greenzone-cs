using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using GreenZoneParser.Parsers.Cs;
using System.Windows.Forms;
using GreenZoneParser.Parsers;
using System.Threading;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;


namespace GreenZoneParser
{

    public abstract class Parser
    {

        public delegate void TokenEventHandler(Token token, bool sync);
        public delegate void TokenPosEventHandler(int position, bool sync);
        public delegate void ErrorEventHandler(string info, CompilationErrorEnty err, bool sync);
        public delegate void NodeEventHandler(BaseNode child, bool sync);


        public event TokenPosEventHandler TokenRead;
        public event TokenEventHandler TokenAdded;
        public event ErrorEventHandler ErrorAdded;
        public event TokenPosEventHandler NewLineAdded;
        public event ErrorEventHandler TmpErrorsAdded;
        public event NodeEventHandler NodeCreated;
        public event NodeEventHandler NodeAdded;

        protected Thread parserThread;
        protected readonly object threadLock = new object();
        protected int parseNextPosition;
        protected bool stepThroughTokenizer;

        public struct Pos
        {
            public readonly int Line;
            public readonly int Column;

            internal Pos(int line, int column)
            {
                this.Line = line;
                this.Column = column;
            }
        }

        public Parser(string fileName, string fileContent)
        {
            this.fileName = fileName;
            this.fileContent = fileContent;
            this.newLinePositions = new SortedSet<int>();
            this.compilationErrors = new List<CompilationErrorEnty>();
        }

        public abstract Tokenizer Tokenizer
        {
            get;
        }

        protected readonly string fileContent;
        public string FileContent
        {
            get
            {
                return fileContent;
            }
        }

        readonly string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        readonly SortedSet<int> newLinePositions;
        public IList<int> NewLinePositions
        {
            get
            {
                return new List<int>(newLinePositions).AsReadOnly();
            }
        }

        readonly List<CompilationErrorEnty> compilationErrors;
        public IList<CompilationErrorEnty> CompilationErrors
        {
            get
            {
                return compilationErrors.AsReadOnly();
            }
        }

        protected int lastPosition;
        public int LastPosition
        {
            get
            {
                return lastPosition;
            }
        }

        public string GetContent(int startIndex, int endIndex)
        {
            string content = fileContent.Substring(startIndex, endIndex - startIndex + 1);
            return content;
        }

        internal void AddNewLine()
        {
            newLinePositions.Add(Tokenizer.Position);
        }

        internal CompilationErrorEnty CreateError(string error, int tokenStart, int tokenEnd)
        {
            Pos pos = FindPos(tokenStart);
            CompilationErrorEnty e = new CompilationErrorEnty(this, error, tokenStart, pos.Line, pos.Column, (int)(tokenEnd - tokenStart + 1));
            return e;
        }

        internal CompilationErrorEnty AddError(string error, int tokenStart, int tokenEnd)
        {
            CompilationErrorEnty e = CreateError(error, tokenStart, tokenEnd);
            compilationErrors.Add(e);
            RaiseErrorAdded(e);
            return e;
        }

        internal void AddError(CompilationErrorEnty e)
        {
            compilationErrors.Add(e);
            RaiseErrorAdded(e);
        }

        public abstract BaseNode Parse();

        public void ParseNext()
        {
            if (parserThread == null)
            {
                lastPosition = -1;
                parseNextPosition = -1;
                stepThroughTokenizer = false;

                parserThread = new Thread(new ThreadStart(Parse0));
                parserThread.IsBackground = true;
                parserThread.Start();
            }
            else
            {
                lock (threadLock)
                {
                    parseNextPosition = -1;
                    Console.WriteLine("ParseNext()  Pulse   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                    Monitor.Pulse(threadLock);
                    Console.WriteLine("ParseNext()  Finished");
                }
            }
        }

        public void ParseToPosition(int parseNextPosition)
        {
            lock (threadLock)
            {
                this.parseNextPosition = parseNextPosition;
                stepThroughTokenizer = false;

                if (parserThread == null)
                {
                    parserThread = new Thread(new ThreadStart(Parse0));
                    parserThread.IsBackground = true;
                    parserThread.Start();
                }
                while (parseNextPosition == int.MaxValue ? parserThread.IsAlive : lastPosition < parseNextPosition)
                {
                    Console.WriteLine("ParseToPosition()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                    Monitor.Pulse(threadLock);
                    Monitor.Wait(threadLock);
                    Console.WriteLine("ParseToPosition()  Finished");
                }
                Console.WriteLine("ParseToPosition()  while finished too");
            }
        }

        public void TokenizeAllFirst()
        {
            lock (threadLock)
            {
                parseNextPosition = -1;
                stepThroughTokenizer = true;

                if (parserThread == null)
                {
                    parserThread = new Thread(new ThreadStart(Parse0));
                    parserThread.IsBackground = true;
                    parserThread.Start();
                }

                Console.WriteLine("TokenizeAllFirst()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                Monitor.Pulse(threadLock);
                Monitor.Wait(threadLock);
                Console.WriteLine("TokenizeAllFirst()  Finished");
            }
        }

        void Parse0()
        {
            lock (threadLock)
            {
                Parse();

                Console.WriteLine("Parse0()  Pulse   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                Monitor.Pulse(threadLock);
                Console.WriteLine("Parse0()  Finished");

                lastPosition = -1;
                parseNextPosition = -1;
                stepThroughTokenizer = false;
            }
        }

        public Pos FindPos(int filePosition)
        {
            int lastLine = 1;
            int lastLinePos = -1;
            int i = 2;
            foreach (int nlp in newLinePositions)
            {
                if (nlp < filePosition)
                {
                    lastLine = i;
                    lastLinePos = nlp;
                }
                else
                {
                    break;
                }
                i++;
            }
            int column = (int)(filePosition - lastLinePos);
            Pos pos = new Pos(lastLine, column);
            return pos;
        }


        internal void RaiseTokenRead()
        {
            lastPosition = Tokenizer.Position;

            if (TokenRead != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if (!stepThroughTokenizer && lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(TokenRead, Tokenizer.Position, true);
                        Console.WriteLine("RaiseTokenRead()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseTokenRead()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(TokenRead, Tokenizer.Position, false);
                    }
                }
                else
                {
                    TokenRead(Tokenizer.Position, true);
                }
            }
        }

        internal void RaiseTokenAdded(Token token)
        {
            lastPosition = token.TokenStartPos;

            if (TokenAdded != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if (!stepThroughTokenizer && lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(TokenAdded, token, true);
                        Console.WriteLine("RaiseTokenAdded()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseTokenAdded()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(TokenAdded, token, false);
                    }
                }
                else
                {
                    TokenAdded(token, true);
                }
            }
        }

        void RaiseErrorAdded(CompilationErrorEnty e)
        {
            lastPosition = e.Position;

            if (ErrorAdded != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if ((Tokenizer.Finished || !stepThroughTokenizer) && lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(ErrorAdded, null, e, true);
                        Console.WriteLine("RaiseErrorAdded()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseErrorAdded()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(ErrorAdded, null, e, false);
                    }
                }
                else
                {
                    ErrorAdded(null, e, true);
                }
            }
        }

        void RaiseNewLineAdded()
        {
            lastPosition = Tokenizer.Position;

            if (NewLineAdded != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if (!stepThroughTokenizer && lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(NewLineAdded, Tokenizer.Position, true);
                        Console.WriteLine("RaiseNewLineAdded()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseNewLineAdded()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(NewLineAdded, Tokenizer.Position, false);
                    }
                }
                else
                {
                    NewLineAdded(Tokenizer.Position, true);
                }
            }
        }

        internal void RaiseTmpErrorAdded(string info, CompilationErrorEnty err)
        {
            lastPosition = err.Position;

            if (TmpErrorsAdded != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if (lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(TmpErrorsAdded, info, err, true);
                        Console.WriteLine("RaiseTmpErrorAdded()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseTmpErrorAdded()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(TmpErrorsAdded, info, err, false);
                    }
                }
                else
                {
                    TmpErrorsAdded(info, err, true);
                }
            }
        }

        internal void RaiseNodeCreated(BaseNode child)
        {
            lastPosition = child.StartPos;

            if (NodeCreated != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if (lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(NodeCreated, child, true);
                        Console.WriteLine("RaiseNodeCreated()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseNodeCreated()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(NodeCreated, child, false);
                    }
                }
                else
                {
                    NodeCreated(child, true);
                }
            }
        }

        internal void RaiseNodeAdded(BaseNode parent, BaseNode node)
        {
            lastPosition = node.StartPos;

            if (NodeAdded != null)
            {
                if (Thread.CurrentThread == parserThread)
                {
                    Form appForm = Application.OpenForms[0];
                    if (lastPosition >= parseNextPosition)
                    {
                        appForm.BeginInvoke(NodeAdded, node, true);
                        Console.WriteLine("RaiseNodeAdded()  Pulse Wait   parseNextPosition:" + parseNextPosition + " stepThroughTokenizer:" + stepThroughTokenizer + " lastPosition:" + lastPosition);
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                        Console.WriteLine("RaiseNodeAdded()  Finished");
                    }
                    else
                    {
                        appForm.BeginInvoke(NodeAdded, node, false);
                    }
                }
                else
                {
                    NodeAdded(node, true);
                }
            }
        }
    }

    public class CompilationErrorEnty : IComparable, IComparable<CompilationErrorEnty>
    {
        internal CompilationErrorEnty(Parser parser, string message, int position, int line, int column, int length)
        {
            this.parser = parser;
            this.message = message;
            this.position = position;
            this.line = line;
            this.column = column;
            this.length = length;
        }

        readonly Parser parser;
        public Parser Parser
        {
            get
            {
                return parser;
            }
        }

        readonly string message;
        public string Message
        {
            get
            {
                return message;
            }
        }

        readonly int position;
        public int Position
        {
            get
            {
                return position;
            }
        }

        readonly int line;
        public int Line
        {
            get
            {
                return line;
            }
        }

        readonly int column;
        public int Column
        {
            get
            {
                return column;
            }
        }

        readonly int length;
        public int Length
        {
            get
            {
                return length;
            }
        }


        public int CompareTo(object obj)
        {
            return CompareTo((CompilationErrorEnty)obj);
        }

        public int CompareTo(CompilationErrorEnty other)
        {
            return position - other.position;
        }
    }
}