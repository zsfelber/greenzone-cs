using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflGenericTypeArg : ReflType
    {
        internal ReflGenericTypeArg(Resolver resolver, Parser parser, BaseNode parseNode, string name)
            : base(resolver, parser, parseNode, -1, (string)null, name, 0)
        {
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.GenericTypeArg;
            }
        }
    }
}
