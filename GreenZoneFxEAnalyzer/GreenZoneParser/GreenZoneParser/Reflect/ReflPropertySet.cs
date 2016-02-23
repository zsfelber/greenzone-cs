using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflPropertySet : ReflMethodBase
    {
        internal ReflPropertySet(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflProperty parent, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, "set", modifiers, attributes, new ReflMethodArgDefinition(resolver, parser, parseNode, parent.PropertyType, "value", ReflMethodArgType.Value))
        {
            this.parent = parent;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.PropertySet;
            }
        }

        readonly new ReflProperty parent;
        public new ReflProperty Parent
        {
            get
            {
                return parent;
            }
        }
    }
}