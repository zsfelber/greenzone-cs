using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflEvent : ReflField
    {
        internal ReflEvent(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflDelegateType eventType, string eventName, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, eventType, eventName, modifiers, attributes)
        {
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.Event;
            }
        }

        public ReflDelegateType EventType
        {
            get
            {
                return (ReflDelegateType)fieldType;
            }
        }
    }
}
