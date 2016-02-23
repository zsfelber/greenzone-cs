using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneParser.Reflect
{
    public enum ReflTypeId
    {
        ArrayType,
        Attribute,
        AttributeArg,
        ClassType,
        Constructor,
        DelegateType,
        EnumConstant,
        EnumType,
        Event,
        EventAdd,
        EventRemove,
        Field,
        GenericTypeArg,
        IndexerGet,
        IndexerProperty,
        IndexerSet,
        InterfaceType,
        Method,
        MethodArg,
        MethodArgDefinition,
        PointerType,
        PrimitiveType,
        Property,
        PropertyGet,
        PropertySet,
        StructType,
        TypeArg,
        TypeArgDefinition,
        TypeArgRule,
    }
}
