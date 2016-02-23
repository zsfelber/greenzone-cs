using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneParser.Parsers;

namespace GreenZoneParser.Reflect
{
    public abstract class ReflBaseNode : BaseNode
    {
        public ReflBaseNode(Resolver resolver, Parser parser, BaseNode parseNode, BaseNode parent)
            : base(parser, parent, parseNode == null ? -1 : parseNode.StartPos, parseNode == null ? -1 : parseNode.EndPos)
        {
            this.resolver = resolver;
            this.parseNode = parseNode;
        }

        readonly Resolver resolver;
        public Resolver Resolver
        {
            get
            {
                return resolver;
            }
        }

        public abstract ReflTypeId ReflTypeId
        {
            get;
        }

        readonly BaseNode parseNode;
        public BaseNode ParseNode
        {
            get
            {
                return parseNode;
            }
        }
    }
}
