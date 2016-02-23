using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers
{
    public class BaseNode : IComparable, IComparable<BaseNode>
    {
        public BaseNode(Parser parser, BaseNode parent, int startPos, int endPos)
        {
            this.parser = parser;
            this.parent = parent;
            this.startPos = startPos;
            this.endPos = endPos;
            if (startPos > endPos)
            {
                throw new NotSupportedException();
            }
        }

        public BaseNode(Parser parser, BaseNode parent, int startPos)
        {
            this.parser = parser;
            this.parent = parent;
            this.startPos = startPos;
        }

        public BaseNode(Parser parser, BaseNode parent)
        {
            this.parser = parser;
            this.parent = parent;
        }

        protected readonly Parser parser;
        public Parser Parser
        {
            get
            {
                return parser;
            }
        }

        protected readonly BaseNode parent;
        public BaseNode Parent
        {
            get
            {
                return parent;
            }
        }

        int startPos = -1;
        public int StartPos
        {
            get
            {
                return startPos;
            }
            internal set
            {
                if (startPos != -1)
                {
                    throw new NotSupportedException();
                }
                startPos = value;
            }
        }

        int endPos = -1;
        public int EndPos
        {
            get
            {
                return endPos;
            }
            internal set
            {
                if (endPos != -1)
                {
                    throw new NotSupportedException();
                }
                if (startPos > value)
                {
                    throw new NotSupportedException();
                }
                endPos = value;
            }
        }

        public int Length
        {
            get
            {
                return endPos - startPos + 1;
            }
        }

        public string Content
        {
            get
            {
                return parser.GetContent(startPos, endPos);
            }
        }

        public override string ToString()
        {
            if (startPos == -1)
            {
                return GetType().Name;
            }
            else
            {
                int ed = Math.Max(startPos, Math.Min(startPos + 16, endPos));
                string result = parser.GetContent(startPos, ed) + (ed < endPos ? "..." : "");
                return result;
            }
        }


        public int CompareTo(object obj)
        {
            return CompareTo((BaseNode)obj);
        }

        public int CompareTo(BaseNode other)
        {
            return startPos - other.startPos;
        }
    }
}
