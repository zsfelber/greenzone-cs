using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
	public abstract class ReflBaseNode : INode
    {
        public ReflBaseNode(Resolver resolver, IParser parser, INode parseNode, INode parent)
        {
			this.resolver = resolver;
			this.parser = parser;
			this.parseNode = parseNode;
			this.parent = parent;
        }

		readonly Resolver resolver;
		public Resolver Resolver
		{
			get
			{
				return resolver;
			}
		}

		readonly IParser parser;
		public IParser Parser
		{
			get
			{
				return parser;
			}
		}

        public abstract ReflTypeId ReflTypeId
        {
            get;
        }

		readonly INode parseNode;
		public INode ParseNode
		{
			get
			{
				return parseNode;
			}
		}

		readonly INode parent;
		public INode Parent
		{
			get
			{
				return parent;
			}
		}
    }
}
