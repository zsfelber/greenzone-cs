using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneParser.Parsers;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Xml
{
    public enum XmlTagType
    {
        Open,
        Close,
        Single
    }

    public class XmlTag : BaseNode
    {
        internal XmlTag(XmlParser parser, string name, XmlTagType type, Token startToken, Token endToken, IDictionary<string, object> attributes)
           : base(parser, null, startToken.TokenStartPos, endToken.TokenEndPos)
        {
            this.name = name;
            this.type = type;
            this.attributes = new ReadOnlyDictionary<string, object>(attributes);
            parser.RaiseNodeCreated(this);
            parser.RaiseNodeAdded(parent, this);
        }

        readonly string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        readonly XmlTagType type;
        public XmlTagType Type
        {
            get
            {
                return type;
            }
        }

        XmlTag pair;
        public XmlTag Pair
        {
            get
            {
                return pair;
            }
            internal set
            {
                switch (type)
                {
                    case XmlTagType.Open:
                        if (value.type != XmlTagType.Close)
                        {
                            throw new NotSupportedException();
                        }
                        if (!name.Equals(value.name))
                        {
                            throw new NotSupportedException();
                        }
                        break;
                    case XmlTagType.Close:
                        if (value.type != XmlTagType.Open)
                        {
                            throw new NotSupportedException();
                        }
                        if (!name.Equals(value.name))
                        {
                            throw new NotSupportedException();
                        }
                        break;
                    case XmlTagType.Single:
                        if (value != this)
                        {
                            throw new NotSupportedException();
                        }
                        break;
                }
                pair = value;
            }
        }

        internal readonly IDictionary<string, object> attributes;
        public IDictionary<string, object> Attributes
        {
            get
            {
                return attributes;
            }
        }
    }
}
