using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public enum ReflTypeArgSpecialRule
    {
        Type,
        Class,
        Struct,
        New,
        Cov,
        Contr
    }

    public class ReflTypeArgRule : ReflBaseNode
    {
        internal ReflTypeArgRule(Resolver resolver, Parser parser, BaseNode parseNode, ReflType singleType)
            : base(resolver, parser, parseNode, null)
        {
            specialRule = ReflTypeArgSpecialRule.Type;
            this.singleType = singleType;
        }

        internal ReflTypeArgRule(Resolver resolver, Parser parser, BaseNode parseNode, ReflTypeArgSpecialRule specialRule)
            : base(resolver, parser, parseNode, null)
        {
            if (specialRule == ReflTypeArgSpecialRule.Type)
            {
                throw new ArgumentException("Missing type, use   new ReflTypeArgRule(ReflTypeArg, ReflType)");
            }
            this.specialRule = specialRule;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.TypeArgRule;
            }
        }

        readonly ReflType singleType;
        public ReflType SingleType
        {
            get
            {
                return singleType;
            }
        }


        readonly ReflTypeArgSpecialRule specialRule;
        public ReflTypeArgSpecialRule SpecialRule
        {
            get
            {
                return specialRule;
            }
        }
    }
}
