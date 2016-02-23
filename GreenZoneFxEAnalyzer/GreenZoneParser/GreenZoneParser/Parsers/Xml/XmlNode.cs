using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Xml
{
    public class XmlNode : BaseNode
    {
        internal XmlNode(XmlParser parser, XmlNode parent, XmlTag openTag)
            : base(parser, parent, openTag.StartPos) 
        {
            if (parent != null)
            {
                Parent.children.Add(this);
            }
            this.openTag = openTag;
            children = new List<XmlNode>();
            childrenUm = children.AsReadOnly();
            parser.RaiseNodeCreated(this);
            parser.RaiseNodeAdded(parent, this);
        }

        public new XmlNode Parent
        {
            get
            {
                return (XmlNode)parent;
            }
        }

        readonly XmlTag openTag;
        public XmlTag OpenTag
        {
            get
            {
                return openTag;
            }
        }

        XmlTag closeTag;
        public XmlTag CloseTag
        {
            get
            {
                return closeTag;
            }
            internal set
            {
                closeTag = value;
                openTag.Pair = value;
                EndPos = closeTag.EndPos;
            }
        }

        internal readonly List<XmlNode> children;
        readonly IList<XmlNode> childrenUm;
        public IList<XmlNode> Children
        {
            get
            {
                return childrenUm;
            }
        }
    }
}