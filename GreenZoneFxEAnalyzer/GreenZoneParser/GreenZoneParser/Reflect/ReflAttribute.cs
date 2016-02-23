using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflAttribute : ReflBaseNode
    {
        internal ReflAttribute(Resolver resolver, Parser parser, BaseNode parseNode, ReflClassType declaringType, int attributeConstrId)
            : base(resolver, parser, parseNode, null)
        {
            this.declaringType = declaringType;
            this.attributeConstrId = attributeConstrId;
            this.args = new List<ReflAttributeArg>();
            this.argsUm = args.AsReadOnly();
            this.namedArgs = new List<ReflAttributeArg>();
            this.namedArgsUm = namedArgs.AsReadOnly();
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.Attribute;
            }
        }

        public string Name
        {
            get
            {
                return declaringType.Name;
            }
        }

        readonly ReflClassType declaringType;
        public ReflClassType DeclaringType
        {
            get
            {
                return declaringType;
            }
        }

        readonly int attributeConstrId;
        public int AttributeConstrId
        {
            get
            {
                return attributeConstrId;
            }
        }

        ReflConstructor attributeConstr;
        public ReflConstructor AttributeConstr
        {
            get
            {
                if (attributeConstr == null)
                {
                    attributeConstr = (ReflConstructor)declaringType.MemberById(attributeConstrId);
                }
                return attributeConstr;
            }
        }

        readonly List<ReflAttributeArg> args;
        readonly IList<ReflAttributeArg> argsUm;
        public IList<ReflAttributeArg> Args
        {
            get
            {
                return argsUm;
            }
        }

        readonly List<ReflAttributeArg> namedArgs;
        readonly IList<ReflAttributeArg> namedArgsUm;
        public IList<ReflAttributeArg> NamedArgs
        {
            get
            {
                return namedArgsUm;
            }
        }

        Attribute nativeAttribute;
        public Attribute NativeAttribute
        {
            get
            {
                if (nativeAttribute == null)
                {
                    object[] args = new object[this.Args.Count];
                    for (int i = 0; i < args.Length; i++)
                    {
                        ReflAttributeArg aarg = this.Args[i];
                        object v = GzEngineFormatter.ToObject(aarg.MethodArg.ParameterType.NativeType, aarg.Value);
                        args[i] = v;
                    }

                    nativeAttribute = (Attribute)this.AttributeConstr.CreateObject(args);

                    ReflObjType type = (ReflObjType)Resolver.GetType(nativeAttribute);
                    foreach (var aarg in this.NamedArgs)
                    {
                        ReflProperty prop = type.GetMember<ReflProperty>(aarg.ArgProperty);
                        object v = GzEngineFormatter.ToObject(prop.PropertyType.NativeType, aarg.Value);
                        prop.SetValue(nativeAttribute, v);
                    }
                }

                return nativeAttribute;
            }
        }


        internal void AddArg(ReflAttributeArg arg)
        {
            args.Add(arg);
        }

        internal void AddNamedArg(ReflAttributeArg arg)
        {
            namedArgs.Add(arg);
        }
    }
}