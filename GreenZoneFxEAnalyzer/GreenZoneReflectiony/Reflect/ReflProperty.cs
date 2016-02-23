using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Reflection;

namespace GreenZoneParser.Reflect
{
    public enum ReflPropertyMethodType
    {
        Get,
        Set,
        GetSet
    }

    public class ReflProperty : ReflField
    {
        internal ReflProperty(Resolver resolver, IParser parser, INode parseNode, int id, ReflObjType declaringType, ReflType propertyType, ReflPropertyMethodType propertyMethodType, string propertyName, ReflModifier modifiers, ReflModifier setModifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, propertyType, propertyName, modifiers, attributes)
        {
            this.propertyMethodType = propertyMethodType;
            this.setModifiers = setModifiers;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.Property;
            }
        }

        public ReflType PropertyType
        {
            get
            {
                return fieldType;
            }
        }

        readonly ReflPropertyMethodType propertyMethodType;
        public ReflPropertyMethodType PropertyMethodType
        {
            get
            {
                return propertyMethodType;
            }
        }

        readonly ReflModifier setModifiers;
        public ReflModifier SetModifiers
        {
            get
            {
                return setModifiers;
            }
        }

        PropertyInfo nativeProperty;
        public PropertyInfo NativeProperty
        {
            get
            {
                if (nativeProperty == null)
                {
                    nativeProperty = Resolver.PureReflResolver.GetNativeProperty(declaringType.NativeType, Name);
                }
                return nativeProperty;
            }
        }

        public object GetValue(object objectOfDeclaringType)
        {
            return Resolver.PureReflResolver.GetNativePropertyValue(NativeProperty, objectOfDeclaringType);
        }

        public void SetValue(object objectOfDeclaringType, object value)
        {
            Resolver.PureReflResolver.SetNativePropertyValue(NativeProperty, objectOfDeclaringType, value);
        }
    }
}
