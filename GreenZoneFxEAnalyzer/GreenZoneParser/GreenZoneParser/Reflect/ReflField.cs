using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflField : ReflMember
    {
        internal ReflField(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflType fieldType, string fieldName, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, fieldName, modifiers, attributes)
        {
            this.fieldType = fieldType;
            this.isConstant = false;
        }

        internal ReflField(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflType fieldType, string fieldName, string constantValue, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, fieldName, modifiers, attributes)
        {
            this.fieldType = fieldType;
            this.isConstant = true;
            this.constantValue = constantValue;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.Field;
            }
        }

        protected readonly ReflType fieldType;
        public ReflType FieldType
        {
            get
            {
                return fieldType;
            }
        }

        readonly bool isConstant;
        public bool IsConstant
        {
            get
            {
                return isConstant;
            }
        }

        readonly string constantValue;
        public string ConstantValue
        {
            get
            {
                return constantValue;
            }
        }
    }
}
