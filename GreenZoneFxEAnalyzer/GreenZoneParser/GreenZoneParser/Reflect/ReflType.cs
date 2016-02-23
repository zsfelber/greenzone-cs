using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public abstract class ReflType : ReflMember, TypeNameItem
    {
        internal ReflType(Resolver resolver, Parser parser, BaseNode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            isInnerType = false;
        }

        internal ReflType(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            isInnerType = true;
            this.parentType = parentType;
        }

        protected readonly bool isInnerType;
        public bool IsInnerType
        {
            get
            {
                return isInnerType;
            }
        }

        protected readonly ReflDefType parentType;
        public ReflDefType ParentType
        {
            get
            {
                return parentType;
            }
        }

        public string FullName
        {
            get
            {
                return Resolver.TypeNameBuilder.GetFullName(this);
            }
        }

        public string GenericId
        {
            get
            {
                return Resolver.TypeNameBuilder.GetGenericId(this);
            }
        }

        public string TypeId
        {
            get
            {
                return Resolver.TypeNameBuilder.GetTypeId(this);
            }
        }

        Type nativeType;
        public Type NativeType
        {
            get
            {
                if (nativeType == null)
                {
                    nativeType = Resolver.PureReflResolver.GetNativeType(TypeId);
                }
                return nativeType;
            }
        }

        protected internal virtual bool isSubtypeOf(ReflType fromType)
        {
            if (fromType == this)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAssignableFrom(ReflType fromType)
        {
            return fromType != null && fromType.isSubtypeOf(this);
        }

        bool TypeNameItem.IsArray
        {
            get { return false; }
        }

        bool TypeNameItem.IsPointer
        {
            get { return false; }
        }

        TypeNameItem TypeNameItem.ElementType
        {
            get { return null; }
        }

        TypeNameItem TypeNameItem.ParentType
        {
            get { return parentType; }
        }

        string TypeNameItem.Namespace
        {
            get { return namespaceId; }
        }

        string TypeNameItem.Name
        {
            get { return Name; }
        }

        int TypeNameItem.Dims
        {
            get { return 0; }
        }

        IList<TypeNameItem> TypeNameItem.GenericArgs
        {
            get { return null; }
        }
    }
}
