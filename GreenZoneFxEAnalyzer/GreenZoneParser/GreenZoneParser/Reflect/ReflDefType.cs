using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public abstract class ReflDefType : ReflObjType
    {
        internal ReflDefType(Resolver resolver, Parser parser, BaseNode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
        }

        internal ReflDefType(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
        }

        internal override void AddMember(ReflMember member)
        {
            members.Add(member);
        }
    }
}
