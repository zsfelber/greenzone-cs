using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflEventRemove : ReflMethodBase
    {
        internal ReflEventRemove(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflEvent parent, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, "remove", modifiers, attributes, new ReflMethodArgDefinition(resolver, parser, parseNode, parent.EventType, "e", ReflMethodArgType.Value))
        {
            this.parent = parent;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.EventRemove;
            }
        }

        readonly new ReflEvent parent;
        public new ReflEvent Parent
        {
            get
            {
                return parent;
            }
        }
    }
}