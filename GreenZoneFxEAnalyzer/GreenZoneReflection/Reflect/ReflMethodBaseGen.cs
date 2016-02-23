using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public abstract class ReflMethodBaseGen : ReflMethodBase
    {
        internal ReflMethodBaseGen(Resolver resolver, IParser parser, INode parseNode, int id, ReflCallableType declaringType, string methodName, ReflModifier modifiers, ReflAttribute[] attributes, params ReflMethodArgDefinition[] methodArgs)
            : base(resolver, parser, parseNode, id, declaringType, methodName, modifiers, attributes, methodArgs)
        {
            this.genericTypeArgDefs = new List<ReflTypeArgDefinition>();
            this.genericTypeArgDefsUm = this.genericTypeArgDefs.AsReadOnly();
            this.genericTypeArgs = new List<ReflTypeArg>();
            this.genericTypeArgsUm = this.genericTypeArgs.AsReadOnly();
        }

        internal readonly List<ReflTypeArgDefinition> genericTypeArgDefs;
        protected readonly IList<ReflTypeArgDefinition> genericTypeArgDefsUm;
        public IList<ReflTypeArgDefinition> GenericTypeArgDefs
        {
            get
            {
                return genericTypeArgDefsUm;
            }
        }

        protected readonly List<ReflTypeArg> genericTypeArgs;
        protected readonly IList<ReflTypeArg> genericTypeArgsUm;
        public IList<ReflTypeArg> GenericTypeArgs
        {
            get
            {
                return genericTypeArgsUm;
            }
        }

        internal void AddTypeArgDefinition(ReflTypeArgDefinition genericTypeArgDef)
        {
            genericTypeArgDefs.Add(genericTypeArgDef);
        }
    }
}
