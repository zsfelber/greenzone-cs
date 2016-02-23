using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public abstract class ReflMember : ReflBaseNode
    {
        internal ReflMember(Resolver resolver, Parser parser, BaseNode parseNode, int id, string namespaceId, string name, ReflModifier modifiers)
            : base(resolver, parser, parseNode, null)
        {
            isTopLevel = true;
            this.id = id;
            this.namespaceId = namespaceId;
            this.name = name;
            this.modifiers = modifiers;
            this.attributes = new List<ReflAttribute>();
            this.attributesUm = this.attributes.AsReadOnly();
        }

        internal ReflMember(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflCallableType declaringType, string name, ReflModifier modifiers)
            : base(resolver, parser, parseNode, declaringType)
        {
            isTopLevel = false;
            this.id = id;
            this.declaringType = declaringType;
            this.name = name;
            this.modifiers = modifiers;
            this.attributes = new List<ReflAttribute>();
            this.attributesUm = this.attributes.AsReadOnly();
        }

        internal ReflMember(Resolver resolver, Parser parser, BaseNode parseNode, int id, string namespaceId, string name, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, null)
        {
            isTopLevel = true;
            this.id = id;
            this.namespaceId = namespaceId;
            this.name = name;
            this.modifiers = modifiers;
            this.attributes = new List<ReflAttribute>();
            this.attributesUm = this.attributes.AsReadOnly();
            if (attributes != null)
                this.attributes.AddRange(attributes);
        }

        internal ReflMember(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflCallableType declaringType, string name, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, declaringType)
        {
            isTopLevel = false;
            this.id = id;
            this.declaringType = declaringType;
            this.name = name;
            this.modifiers = modifiers;
            this.attributes = new List<ReflAttribute>();
            this.attributesUm = this.attributes.AsReadOnly();
            if (attributes != null)
                this.attributes.AddRange(attributes);
        }

        protected readonly bool isTopLevel;
        public bool IsTopLevel
        {
            get
            {
                return isTopLevel;
            }
        }

        protected readonly int id;
        public int Id
        {
            get
            {
                return id;
            }
        }

        protected readonly string namespaceId;
        public string NamespaceId
        {
            get
            {
                return namespaceId;
            }
        }

        protected readonly ReflCallableType declaringType;
        public ReflCallableType DeclaringType
        {
            get
            {
                return declaringType;
            }
        }

        protected readonly string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        readonly ReflModifier modifiers;
        public ReflModifier Modifiers
        {
            get
            {
                return modifiers;
            }
        }

        internal readonly List<ReflAttribute> attributes;
        readonly IList<ReflAttribute> attributesUm;
        public IList<ReflAttribute> Attributes
        {
            get
            {
                lazyResolve();
                return attributesUm;
            }
        }

        internal void AddAttribute(ReflAttribute attribute)
        {
            attributes.Add(attribute);
        }


        ILazyTypeResolver lazyResolver;
        internal ILazyTypeResolver LazyResolver
        {
            get
            {
                return lazyResolver;
            }
            set
            {
                lazyResolver = value;
            }
        }

        protected internal void lazyResolve()
        {
            if (lazyResolver != null && !lazyResolver.Resolved)
            {
                lazyResolver.FinishResolve();
            }
        }
    }
}