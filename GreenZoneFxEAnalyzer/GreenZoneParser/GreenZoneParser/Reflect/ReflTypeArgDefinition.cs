using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{

    public class ReflTypeArgDefinition : ReflBaseNode, TypeNameItem
    {
        internal ReflTypeArgDefinition(Resolver resolver, Parser parser, BaseNode parseNode, string typeParameterName, params ReflTypeArgRule[] genericTypeArgRules)
            : base(resolver, parser, parseNode, null)
        {
            this.typeParameterName = typeParameterName;
            this.genericTypeArgRules = genericTypeArgRules;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.TypeArgDefinition;
            }
        }

        readonly string typeParameterName;
        public string TypeParameterName
        {
            get
            {
                return typeParameterName;
            }
        }

        readonly ReflTypeArgRule[] genericTypeArgRules;
        public ReflTypeArgRule[] GenericTypeArgRules
        {
            get
            {
                return genericTypeArgRules;
            }
        }

        bool TypeNameItem.IsArray
        {
            get { return false; }
        }

        bool TypeNameItem.IsPointer
        {
            get { return false; }
        }

        TypeNameItem TypeNameItem.ElementType
        {
            get { return null; }
        }

        TypeNameItem TypeNameItem.ParentType
        {
            get { return null; }
        }

        string TypeNameItem.Namespace
        {
            get { return null; }
        }

        string TypeNameItem.Name
        {
            get { return typeParameterName; }
        }

        int TypeNameItem.Dims
        {
            get { return 0; }
        }

        IList<TypeNameItem> TypeNameItem.GenericArgs
        {
            get { return null; }
        }
    }
}
