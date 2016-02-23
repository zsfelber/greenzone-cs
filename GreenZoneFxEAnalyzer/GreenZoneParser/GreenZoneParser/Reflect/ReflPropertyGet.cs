using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflPropertyGet : ReflMethodBase
    {
        internal ReflPropertyGet(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflProperty parent, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, "get", modifiers, attributes)
        {
            this.parent = parent;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.PropertyGet;
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
