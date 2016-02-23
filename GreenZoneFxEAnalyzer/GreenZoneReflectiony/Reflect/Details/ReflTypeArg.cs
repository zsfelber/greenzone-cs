using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflTypeArg : ReflBaseNode, TypeNameItem
    {
        internal ReflTypeArg(Resolver resolver, IParser parser, INode parseNode, ReflCallableType parent, int argIndex, ReflType actualType)
            : base(resolver, parser, parseNode, null)
        {
            this.parent = parent;
            this.argIndex = argIndex;
            this.actualType = actualType;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.TypeArg;
            }
        }

        readonly new ReflCallableType parent;
        public new ReflCallableType Parent
        {
            get
            {
                return parent;
            }
        }

        readonly int argIndex;
        public int ArgIndex
        {
            get
            {
                return argIndex;
            }
        }

        public ReflTypeArgDefinition Definition
        {
            get
            {
                return parent.GenericTypeArgDefs[argIndex];
            }
        }

        public string TypeParameterName
        {
            get
            {
                return Definition.TypeParameterName;
            }
        }

        readonly ReflType actualType;
        public ReflType ActualType
        {
            get
            {
                return actualType;
            }
        }


        bool TypeNameItem.IsArray
        {
            get { return ((TypeNameItem)actualType).IsArray; }
        }

        bool TypeNameItem.IsPointer
        {
            get { return ((TypeNameItem)actualType).IsPointer; }
        }

        TypeNameItem TypeNameItem.ElementType
        {
            get { return ((TypeNameItem)actualType).ElementType; }
        }

        TypeNameItem TypeNameItem.ParentType
        {
            get { return ((TypeNameItem)actualType).ParentType; }
        }

        string TypeNameItem.Namespace
        {
            get { return ((TypeNameItem)actualType).Namespace; }
        }

        string TypeNameItem.Name
        {
            get { return ((TypeNameItem)actualType).Name; }
        }

        int TypeNameItem.Dims
        {
            get { return ((TypeNameItem)actualType).Dims; }
        }

        IList<TypeNameItem> TypeNameItem.GenericArgs
        {
            get { return ((TypeNameItem)actualType).GenericArgs; }
        }
    }
}
