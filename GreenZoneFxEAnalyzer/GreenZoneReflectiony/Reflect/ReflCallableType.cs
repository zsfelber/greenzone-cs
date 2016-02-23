using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace GreenZoneParser.Reflect
{
    public abstract class ReflCallableType : ReflType, TypeNameItem
    {
        internal ReflCallableType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            this.genericTypeArgDefs = new List<ReflTypeArgDefinition>();
            this.genericTypeArgDefsUm = this.genericTypeArgDefs.AsReadOnly();
            this.genericTypeArgDefsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgDefs);
            this.genericTypeArgs = new List<ReflTypeArg>();
            this.genericTypeArgsUm = this.genericTypeArgs.AsReadOnly();
            this.genericTypeArgsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgs);
        }

        internal ReflCallableType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            this.genericTypeArgDefs = new List<ReflTypeArgDefinition>();
            this.genericTypeArgDefsUm = this.genericTypeArgDefs.AsReadOnly();
            this.genericTypeArgDefsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgDefs);
            this.genericTypeArgs = new List<ReflTypeArg>();
            this.genericTypeArgsUm = this.genericTypeArgs.AsReadOnly();
            this.genericTypeArgsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgs);
        }

        internal ReflCallableType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers, List<ReflTypeArgDefinition> genericTypeArgDefs)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            this.genericTypeArgDefs = genericTypeArgDefs;
            this.genericTypeArgDefsUm = this.genericTypeArgDefs.AsReadOnly();
            this.genericTypeArgDefsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgDefs);
            this.genericTypeArgs = new List<ReflTypeArg>();
            this.genericTypeArgsUm = this.genericTypeArgs.AsReadOnly();
            this.genericTypeArgsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgs);
        }

        internal ReflCallableType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers, List<ReflTypeArgDefinition> genericTypeArgDefs)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            this.genericTypeArgDefs = genericTypeArgDefs;
            this.genericTypeArgDefsUm = this.genericTypeArgDefs.AsReadOnly();
            this.genericTypeArgDefsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgDefs);
            this.genericTypeArgs = new List<ReflTypeArg>();
            this.genericTypeArgsUm = this.genericTypeArgs.AsReadOnly();
            this.genericTypeArgsB = new BridgeCollection<TypeNameItem>(this.genericTypeArgs);
        }

        protected readonly List<ReflTypeArgDefinition> genericTypeArgDefs;
        protected readonly IList<ReflTypeArgDefinition> genericTypeArgDefsUm;
        protected readonly IList<TypeNameItem> genericTypeArgDefsB;
        public IList<ReflTypeArgDefinition> GenericTypeArgDefs
        {
            get
            {
                lazyResolve();
                return genericTypeArgDefsUm;
            }
        }

        protected readonly List<ReflTypeArg> genericTypeArgs;
        protected readonly IList<ReflTypeArg> genericTypeArgsUm;
        protected readonly IList<TypeNameItem> genericTypeArgsB;
        public IList<ReflTypeArg> GenericTypeArgs
        {
            get
            {
                lazyResolve();
                return genericTypeArgsUm;
            }
        }


        internal void AddTypeArgDefinition(ReflTypeArgDefinition genericTypeArgDef)
        {
            genericTypeArgDefs.Add(genericTypeArgDef);
        }


        internal abstract ReflCallableType ApplyGenericArgs(ReflDefType parentType, ReflTypeArg[] genericTypeArgs);


        IList<TypeNameItem> TypeNameItem.GenericArgs
        {
            get { return genericTypeArgs == null || genericTypeArgs.Count == 0 ? genericTypeArgDefsB : genericTypeArgsB; }
        }
    }
}
