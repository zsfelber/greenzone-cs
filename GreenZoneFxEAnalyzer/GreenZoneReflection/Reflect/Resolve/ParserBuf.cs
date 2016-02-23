﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GreenZoneParser.Reflect
{
    internal abstract class ParserBuf
    {
		internal ParserBuf(ParserBuf parent, INode node = null)
		{
			this.parent = parent;
			parser = parent.parser;
			parseNode = node;
		}

		internal ParserBuf(IParser parser)
		{
			this.parser = parser;
		}

		readonly ParserBuf parent;
		public ParserBuf Parent
		{
			get
			{
				return parent;
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

        INode parseNode;
        internal INode ParseNode
        {
            get
            {
                return parseNode;
            }
            set
            {
                if (parseNode != null)
                {
                    throw new NotSupportedException();
                }
                if (value != null)
                {
                    parseNode = value;
                }
            }
        }
    }

    internal class ParseInfo : ParserBuf, TypeNameItem
    {
		internal ParseInfo(ParserBuf parent, INode node = null)
			: base(parent, node)
		{
		}

		internal ParseInfo(IParser parser)
			: base(parser)
		{
		}

        internal Dictionary<string, ParseInfo> Children = null;

        internal ParseInfo ParentType = null;
        internal ParseInfo BaseType = null;
        internal List<ParseInfo> BaseInterfaces = null;

        internal ReflModifier Modifiers = 0;

        internal ParseInfo ElementType;
        internal ParseInfo[] GenericArgs;
        internal GenericConstraint[] GenericConstraints;
        internal EnumConstant[] EnumConstants;
        internal List<ParseInfo> Attributes = null;
        internal List<AttributeConstrArg> AttributeConstrArgs = null;
        internal List<AttributeConstrArg> AttributeNamedArgs = null;

        internal bool IsArray;
        internal bool IsPointer;
        internal bool IsGeneric;
        internal bool IsGenericArg;
        internal bool IsAttribute;

        internal string TpType;
        internal string Namespace;
        internal string Name;
        internal int Id;
        internal int Dims;
        internal int AttrConstrId;

        internal string FullName
        {
            get
            {
                return Resolver.TypeNameBuilder.GetFullName(this);
            }
        }

        internal string GenericId
        {
            get
            {
                return Resolver.TypeNameBuilder.GetGenericId(this);
            }
        }

        internal string TypeId
        {
            get
            {
                return Resolver.TypeNameBuilder.GetTypeId(this);
            }
        }


        bool TypeNameItem.IsArray
        {
            get { return IsArray; }
        }

        bool TypeNameItem.IsPointer
        {
            get { return IsPointer; }
        }

        TypeNameItem TypeNameItem.ElementType
        {
            get { return ElementType; }
        }

        TypeNameItem TypeNameItem.ParentType
        {
            get { return ParentType; }
        }

        string TypeNameItem.Namespace
        {
            get { return Namespace; }
        }

        string TypeNameItem.Name
        {
            get { return Name; }
        }

        int TypeNameItem.Dims
        {
            get { return Dims; }
        }

        IList<TypeNameItem> TypeNameItem.GenericArgs
        {
            get { return GenericArgs; }
        }
    }

    internal class AttributeConstrArg : ParserBuf
    {
        internal AttributeConstrArg(ParseInfo parent)
            : base(parent)
        {
        }

        internal ParseInfo Type;

        internal string Property;

        internal string Value;
    }

    internal class GenericConstraint : ParserBuf
    {
        internal GenericConstraint(ParseInfo parent)
            : base(parent)
        {
        }

        internal ReflTypeArgSpecialRule Rule;

        internal ParseInfo Type;
    }

    internal class EnumConstant : ParserBuf
    { 
        internal EnumConstant(ParseInfo parent)
            : base(parent)
        {
        }

        internal string EnumConstName;

        internal decimal EnumConstValue;
    }
}