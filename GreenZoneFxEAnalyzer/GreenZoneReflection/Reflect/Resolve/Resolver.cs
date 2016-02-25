using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneUtil.Util;
using System.Reflection;



namespace GreenZoneParser.Reflect
{
    public abstract class Resolver
    {
        readonly SortedDictionary<string, ReflType> types;
        public readonly ReadOnlyDictionary<string, ReflType> Types;
        int types_Read_debug_k = 0;

        internal readonly ParseInfo rootParseInfo;
        readonly Dictionary<string, ParseInfo> defParseInfos;

        internal readonly PureReflResolver PureReflResolver;

        public static readonly ITypeNameBuilder TypeNameBuilder = new CsTypeNameBuilder();

        public Resolver()
        {
            types = new SortedDictionary<string, ReflType>();
            Types = new ReadOnlyDictionary<string, ReflType>(types);

            rootParseInfo = new ParseInfo(null, null);
            defParseInfos = new Dictionary<string, ParseInfo>();

            PureReflResolver = new PureReflResolver(this);
        }

        public void ResolveAll()
        {
            int all = 0;
            foreach (ParseInfo parserInfo in rootParseInfo.Children.Values)
            {
                all += parserInfo.Children.Count;
            }
            foreach (ParseInfo parserInfo in rootParseInfo.Children.Values)
            {
                foreach (ParseInfo typeInfo in parserInfo.Children.Values)
                {
                    Resolve(typeInfo, true);
                }
            }
        }

        public ReflType GetType(object obj)
        {
            string typeCode = PureReflResolver.TypeCode(PureReflResolver.GetNativeType(obj));
            return Resolve(typeCode, false);
        }

        public ReflType GetType(string sysTypeId)
        {
            string typeCode = PureReflResolver.TypeCode(PureReflResolver.GetNativeType(sysTypeId));
            return Resolve(typeCode, false);
        }

        public ReflType GetType(Type sysType)
        {
            string typeCode = PureReflResolver.TypeCode(sysType);
            return Resolve(typeCode, false);
        }

        internal ReflType Resolve(string typeCode, bool genericOnly)
        {
            int pos = 0;
            ParseInfo typeInfo = decodeTypeCode(null, typeCode, ref pos);
            ReflType result = Resolve(typeInfo, genericOnly);
            return result;
        }

        ReflType Resolve(ParseInfo typeInfo, bool genericOnly)
        {
            ReflType gtype = null;

            if (typeInfo.IsArray)
            {
                if (!types.TryGetValue(typeInfo.GenericId, out gtype))
                {
                    ReflType elementType = Resolve(typeInfo.ElementType, true);

                    gtype = new ReflArrayType(this, typeInfo.Parser, typeInfo.ParseNode, typeInfo.Dims, elementType);
                    types[typeInfo.GenericId] = gtype;
                    debugAddType();
                }

                ReflType type;

                if (!genericOnly && !types.TryGetValue(typeInfo.TypeId, out type))
                {
                    ReflType elementType = Resolve(typeInfo.ElementType, false);

                    type = new ReflArrayType(this, typeInfo.Parser, typeInfo.ParseNode, typeInfo.Dims, elementType);
                    types[typeInfo.TypeId] = type;
                    debugAddType();
                }
                else
                {
                    type = gtype;
                }

                return type;
            }
            else if (typeInfo.IsPointer)
            {
                if (!types.TryGetValue(typeInfo.GenericId, out gtype))
                {
                    ReflType elementType = Resolve(typeInfo.ElementType, true);

                    gtype = new ReflPointerType(this, typeInfo.Parser, typeInfo.ParseNode, elementType);
                    types[typeInfo.GenericId] = gtype;
                    debugAddType();
                }

                ReflType type;

                if (!genericOnly && !types.TryGetValue(typeInfo.TypeId, out type))
                {
                    ReflType elementType = Resolve(typeInfo.ElementType, false);

                    type = new ReflPointerType(this, typeInfo.Parser, typeInfo.ParseNode, elementType);
                    types[typeInfo.TypeId] = type;
                    debugAddType();
                }
                else
                {
                    type = gtype;
                }

                return type;
            }
            else if (typeInfo.IsGenericArg)
            {
                gtype = new ReflGenericTypeArg(this, typeInfo.Parser, typeInfo.ParseNode, typeInfo.Name);
                return gtype;
            }
            else
            {
                if (!types.TryGetValue(typeInfo.GenericId, out gtype))
                {
                    ReflDefType parentType;
                    if (typeInfo.ParentType != null)
                    {
                        parentType = (ReflDefType)Resolve(typeInfo.ParentType, false);
                    }
                    else
                    {
                        parentType = null;
                    }

                    ParseInfo defTypeInfo;
                    if (defParseInfos.TryGetValue(typeInfo.GenericId, out defTypeInfo))
                    {
                        gtype = Resolve(parentType, defTypeInfo);
                        types[typeInfo.GenericId] = gtype;
                        debugAddType();
                        gtype.LazyResolver = new XmlLazyTypeResolver(this, parentType, defTypeInfo, gtype);
                    }
                    else
                    {
                        Type stype;
                        gtype = PureReflResolver.Resolve(parentType, typeInfo, out stype);
                        types[typeInfo.GenericId] = gtype;
                        debugAddType();
                        gtype.LazyResolver = new PureReflLazyTypeResolver(this, parentType, stype, gtype);
                    }
                }

                ReflType type;

                if (!genericOnly && !types.TryGetValue(typeInfo.TypeId, out type))
                {
                    ReflDefType parentType;
                    if (typeInfo.ParentType != null)
                    {
                        parentType = (ReflDefType)Resolve(typeInfo.ParentType, false);
                    }
                    else
                    {
                        parentType = null;
                    }

                    ReflCallableType ctype = (ReflCallableType)gtype;
                    ReflTypeArg[] targs = parseReflGenericArgs(ctype, typeInfo);
                    type = ctype.ApplyGenericArgs(parentType, targs);
                    types[typeInfo.TypeId] = type;
                    debugAddType();
                }
                else
                {
                    type = gtype;
                }

                return type;
            }
        }

        ReflType Resolve(ReflDefType parentType, ParseInfo defTypeInfo)
        {
            ReflType baseType;
            if (defTypeInfo.BaseType == null)
            {
                baseType = null;
            }
            else
            {
                baseType = Resolve(defTypeInfo.BaseType, false);
            }

            ReflType result;
            switch (defTypeInfo.TpType)
            {
                case "cl":
                    if (parentType == null)
                    {
                        result = new ReflClassType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, defTypeInfo.Namespace, defTypeInfo.Name, (ReflClassType)baseType, defTypeInfo.Modifiers);
                    }
                    else
                    {
                        result = new ReflClassType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, parentType, defTypeInfo.Name, (ReflClassType)baseType, defTypeInfo.Modifiers);
                    }
                    break;
                case "de":
                    result = null;
                    break;
                case "st":
                    if (parentType == null)
                    {
                        result = new ReflStructType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, defTypeInfo.Namespace, defTypeInfo.Name, baseType, defTypeInfo.Modifiers);
                    }
                    else
                    {
                        result = new ReflStructType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, parentType, defTypeInfo.Name, baseType, defTypeInfo.Modifiers);
                    }
                    break;
                case "in":
                    if (parentType == null)
                    {
                        result = new ReflInterfaceType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, defTypeInfo.Namespace, defTypeInfo.Name, defTypeInfo.Modifiers);
                    }
                    else
                    {
                        result = new ReflInterfaceType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, parentType, defTypeInfo.Name, defTypeInfo.Modifiers);
                    }
                    break;
                case "en":
                    if (parentType == null)
                    {
                        result = new ReflEnumType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, defTypeInfo.Namespace, defTypeInfo.Name, baseType, defTypeInfo.Modifiers);
                    }
                    else
                    {
                        result = new ReflEnumType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, parentType, defTypeInfo.Name, baseType, defTypeInfo.Modifiers);
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }

            switch (defTypeInfo.TpType)
            {
                case "de":
                    ReflMethod invoke = null;
					// TODO defTypeInfo.UnparsedMembers removed  -> invoke = parseReflMethod(defTypeInfo, memberNode)break
                    if (invoke == null)
                    {
                        throw new NotSupportedException();
                    }
                    if (parentType == null)
                    {
                        result = new ReflDelegateType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, defTypeInfo.Namespace, defTypeInfo.Name, invoke, defTypeInfo.Modifiers);
                    }
                    else
                    {
                        result = new ReflDelegateType(this, defTypeInfo.Parser, defTypeInfo.ParseNode, defTypeInfo.Id, parentType, defTypeInfo.Name, invoke, defTypeInfo.Modifiers);
                    }
                    if (parentType != null)
                    {
                        parentType.AddMember(result);
                    }
                    break;
                case "en":
                    if (parentType != null)
                    {
                        parentType.AddMember(result);
                    }

                    ReflEnumType enumType = (ReflEnumType)result;
                    foreach (var ec in defTypeInfo.EnumConstants)
                    {
                        ReflEnumConstant enumConst = new ReflEnumConstant(this, defTypeInfo.Parser, defTypeInfo.ParseNode, enumType, ec.EnumConstName, ec.EnumConstValue);
                        enumType.AddEnumConstant(enumConst);
                    }
                    break;
            }

            return result;
        }

        internal void FinishResolve(ReflDefType parentType, ParseInfo defTypeInfo, ReflType type)
        {
            if (defTypeInfo.BaseInterfaces != null)
            {
                ReflObjType otype = (ReflObjType)type;
                foreach (ParseInfo parg in defTypeInfo.BaseInterfaces)
                {
                    otype.AddBaseInterface((ReflInterfaceType)Resolve(parg, false));
                }
            }

            ReflTypeArgDefinition[] targs = parseReflGenericArgDefs(defTypeInfo);
            if (targs != null)
            {
                ReflCallableType ctype = (ReflCallableType)type;
                foreach (var targ in targs)
                {
                    ctype.AddTypeArgDefinition(targ);
                }
            }
            
            ReflAttribute[] attributes = parseReflAttributes(defTypeInfo);
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    type.AddAttribute(attribute);
                }
            }

            switch (defTypeInfo.TpType)
            {
                case "de":
                case "en":
                    break;
                default:
                    if (parentType != null)
                    {
                        parentType.AddMember(type);
                    }

					// TODO defTypeInfo.UnparsedMembers removed  -> objType.AddMember(parseReflMember...)
                    break;
            }
        }












		protected abstract ReflMember parseReflMember (ReflObjType declaringType, MemberInfo memberNode);

		protected abstract ReflMethod parseReflMethod (ReflObjType declaringType, MethodInfo memberNode);

		protected abstract ReflConstructor parseReflConstr (ReflObjType declaringType, ConstructorInfo memberNode);

		protected abstract ReflMember parseReflProp (ReflObjType declaringType, PropertyInfo memberNode);

		protected abstract ReflField parseReflField (ReflObjType declaringType, FieldInfo memberNode);

		protected abstract ReflEvent parseReflEvent (ReflObjType declaringType, EventInfo memberNode);

		protected abstract ReflTypeArgDefinition[] parseReflGenericArgDefs (Type type);

		protected abstract ReflTypeArgDefinition[] parseReflGenericArgDefs (MethodInfo minf);

		protected abstract ReflTypeArgDefinition[] parseReflGenericArgDefs (Type[] gas);

		protected abstract ReflAttribute[] parseReflAttributes (MemberInfo memberInfo);

		protected abstract ReflAttribute[] parseReflAttributes (IEnumerable<CustomAttributeData> attrs);

		protected abstract ReflMethodArgDefinition[] parseReflMethodParameters (MethodBase methodNode);

		protected abstract ReflMethodArgDefinition[] parseReflMethodParameters (PropertyInfo propertyNode);

		protected abstract ReflMethodArgDefinition[] parseReflMethodParameters (ParameterInfo[] parameters, bool isExtension);





























        ReflTypeArgDefinition[] parseReflGenericArgDefs(ParseInfo parseInfo)
        {
            ReflTypeArgDefinition[] targs;
            if (parseInfo.GenericArgs == null)
            {
                targs = null;
            }
            else
            {
                targs = new ReflTypeArgDefinition[parseInfo.GenericArgs.Length];
                for (int i = 0; i < targs.Length; i++)
                {
                    ParseInfo parg = parseInfo.GenericArgs[i];
                    ReflTypeArgRule[] rules = { };
                    if (parg.GenericConstraints != null)
                    {
                        rules = new ReflTypeArgRule[parg.GenericConstraints.Length];
                        for (int j = 0; j < rules.Length; j++)
                        {
                            GenericConstraint gc = parg.GenericConstraints[j];
                            if (gc.Rule == ReflTypeArgSpecialRule.Type)
                            {
                                ReflType gcType = Resolve(gc.Type, false);
                                rules[j] = new ReflTypeArgRule(this, gc.Parser, gc.ParseNode, gcType);
                            }
                            else
                            {
                                rules[j] = new ReflTypeArgRule(this, gc.Parser, gc.ParseNode, gc.Rule);
                            }
                        }
                    }
                    targs[i] = new ReflTypeArgDefinition(this, parg.Parser, parg.ParseNode, parg.Name, rules);
                }
            }
            return targs;
        }

        ReflTypeArg[] parseReflGenericArgs(ReflCallableType type, ParseInfo parseInfo)
        {
            ReflTypeArg[] targs;
            if (parseInfo.GenericArgs == null)
            {
                targs = null;
            }
            else
            {
                targs = new ReflTypeArg[parseInfo.GenericArgs.Length];
                for (int i = 0; i < targs.Length; i++)
                {
                    ParseInfo parg = parseInfo.GenericArgs[i];
                    ReflType actualType = Resolve(parg, false);
                    targs[i] = new ReflTypeArg(this, parg.Parser, parg.ParseNode, type, i, actualType);
                }
            }
            return targs;
        }

        ReflAttribute[] parseReflAttributes(ParseInfo parseInfo)
        {
            if (parseInfo.Attributes == null)
            {
                return null;
            }
            else
            {
                List<ReflAttribute> result = new List<ReflAttribute>();
                foreach (var ai in parseInfo.Attributes)
                {
                    ReflClassType declaringClass = (ReflClassType)Resolve(ai, false);
                    ReflAttribute rattr = new ReflAttribute(this, null, null, declaringClass, ai.AttrConstrId);
                    result.Add(rattr);

                    int i = 0;
                    foreach (var a in ai.AttributeConstrArgs)
                    {
                        new ReflAttributeArg(this, a.Parser, a.ParseNode, rattr, i, a.Value);
                        i++;
                    }
                    foreach (var a in ai.AttributeNamedArgs)
                    {
                        new ReflAttributeArg(this, a.Parser, a.ParseNode, rattr, a.Property, a.Value);
                    }

                }
                return result.ToArray();
            }
        }
			
        ParseInfo decodeTypeCode(ParserBuf parentParseInfo, string typeCode, ref int pos, bool isClassDefinition = false, bool isRoot = true)
        {
            ParseInfo result = new ParseInfo(parentParseInfo);

            if (!isClassDefinition)
            {
                switch (typeCode[pos])
                {
                    case 'v':
                        pos++;
                        result.Name = "Void";
                        result.Namespace = "System";
                        return result;
                    case 'b':
                        pos++;
                        result.Name = "Boolean";
                        result.Namespace = "System";
                        return result;
                    case 'y':
                        pos++;
                        result.Name = "Byte";
                        result.Namespace = "System";
                        return result;
                    case 'Y':
                        pos++;
                        result.Name = "SByte";
                        result.Namespace = "System";
                        return result;
                    case 's':
                        pos++;
                        result.Name = "Int16";
                        result.Namespace = "System";
                        return result;
                    case 'S':
                        pos++;
                        result.Name = "UInt16";
                        result.Namespace = "System";
                        return result;
                    case 'n':
                        pos++;
                        result.Name = "Int32";
                        result.Namespace = "System";
                        return result;
                    case 'N':
                        pos++;
                        result.Name = "UInt32";
                        result.Namespace = "System";
                        return result;
                    case 'l':
                        pos++;
                        result.Name = "Int64";
                        result.Namespace = "System";
                        return result;
                    case 'L':
                        pos++;
                        result.Name = "UInt64";
                        result.Namespace = "System";
                        return result;
                    case 'p':
                        pos++;
                        result.Name = "IntPtr";
                        result.Namespace = "System";
                        return result;
                    case 'P':
                        pos++;
                        result.Name = "UIntPtr";
                        result.Namespace = "System";
                        return result;
                    case 'c':
                        pos++;
                        result.Name = "Char";
                        result.Namespace = "System";
                        return result;
                    case 'd':
                        pos++;
                        result.Name = "Double";
                        result.Namespace = "System";
                        return result;
                    case 'f':
                        pos++;
                        result.Name = "Single";
                        result.Namespace = "System";
                        return result;
                    case 'e':
                        pos++;
                        result.Name = "Decimal";
                        result.Namespace = "System";
                        return result;
                    case 'O':
                        pos++;
                        result.Name = "Object";
                        result.Namespace = "System";
                        return result;
                    case 'X':
                        pos++;
                        result.Name = "String";
                        result.Namespace = "System";
                        return result;
                    case 't':
                        pos++;
                        result.Name = "Type";
                        result.Namespace = "System";
                        return result;
                    default:
                        break;
                }
            }

            int startPos, length;
            int num;
            switch (typeCode[pos])
            {
                case 'A':
                    startPos = pos + 1;
                    length = -1;
                    num = GreenZoneSysUtilsBase.FindNumberBegin(typeCode, ref startPos, ref length);
                    if (startPos == -1)
                    {
                        throw new NotSupportedException();
                    }
                    result.IsArray = true;
                    result.Dims = num;
                    pos = startPos + length;
                    result.ElementType = decodeTypeCode(parentParseInfo, typeCode, ref pos, false, false);
                    return result;
                case '*':
                    result.IsPointer = true;
                    pos++;
                    result.ElementType = decodeTypeCode(parentParseInfo, typeCode, ref pos, false, false);
                    return result;
                case 'G':
                    startPos = pos + 1;
                    length = -1;
                    num = GreenZoneSysUtilsBase.FindNumberBegin(typeCode, ref startPos, ref length);
                    if (startPos == -1)
                    {
                        throw new NotSupportedException();
                    }
                    result.IsGeneric = true;
                    result.Dims = num;
                    result.GenericArgs = new ParseInfo[num];
                    pos = startPos + length;
                    for (int i = 0; i < num; i++)
                    {
                        ParseInfo ga = decodeTypeCode(parentParseInfo, typeCode, ref pos, false, false);
                        ga.ParseNode = parentParseInfo == null ? null : parentParseInfo.ParseNode;
                        result.GenericArgs[i] = ga;
                        if (pos >= typeCode.Length)
                        {
                            throw new NotSupportedException();
                        }
                    }
                    break;
                case 'g':
                    pos++;
                    result.IsGenericArg = true;
                    break;
                case 'T':
                    pos++;
                    break;
                case 'a':
                    pos++;
                    result.IsAttribute = true;
                    break;
                default:
                    throw new NotSupportedException();
            }

            int plus = typeCode.IndexOf('+', pos);
            int colind = typeCode.IndexOf(';', pos);

            if (plus == -1)
            {
                if (colind == -1)
                {
                    if (!isRoot)
                    {
                        throw new NotSupportedException();
                    }
                    result.Name = typeCode.Substring(pos);
                    pos = typeCode.Length;
                    splitNamespace(result);
                }
                else
                {
                    if (isRoot)
                    {
                        throw new NotSupportedException();
                    }
                    result.Name = typeCode.Substring(pos, colind - pos);
                    pos = colind + 1;
                    splitNamespace(result);
                }
            }
            else
            {
                if (colind == -1 || plus < colind)
                {
                    result.Name = typeCode.Substring(pos, plus - pos);
                    pos = plus + 1;

                    splitNamespace(result);

                    ParseInfo nestedtpres = new ParseInfo(parentParseInfo);
                    nestedtpres = decodeTypeCode(parentParseInfo, typeCode, ref pos, false, isRoot);

                    ParseInfo firstEmpty = nestedtpres;
                    while (firstEmpty.ParentType != null)
                    {
                        firstEmpty = firstEmpty.ParentType;
                    }
                    firstEmpty.ParentType = result;

                    result = nestedtpres;
                }
                else
                {
                    if (isRoot)
                    {
                        throw new NotSupportedException();
                    }
                    result.Name = typeCode.Substring(pos, colind - pos);
                    pos = colind + 1;
                    splitNamespace(result);
                }
            }

            return result;
        }

        void splitNamespace(ParseInfo result)
        {
            int nspt = result.Name.LastIndexOf('.');
            if (nspt >= 0)
            {
                result.Namespace = result.Name.Substring(0, nspt);
                result.Name = result.Name.Substring(nspt + 1);
            }
        }


        internal ReflModifier ParseModifiers(string modCode)
        {
            ParseInfo parseInfo = new ParseInfo(null);
            int pos = 0;
            decodeModifiers(parseInfo, modCode, ref pos);
            return parseInfo.Modifiers;
        }

        void decodeModifiers(ParseInfo parseInfo, string modCode, ref int pos)
        {
            if (modCode == null || pos == modCode.Length)
            {
                return;
            }

            switch (modCode[pos])
            {
                case 's':
                    parseInfo.Modifiers |= ReflModifier.Static;
                    break;
                case 'a':
                    parseInfo.Modifiers |= ReflModifier.Abstract;
                    break;
                case 'e':
                    parseInfo.Modifiers |= ReflModifier.Sealed;
                    break;
                case 'o':
                    parseInfo.Modifiers |= ReflModifier.Override;
                    break;
                case 'v':
                    parseInfo.Modifiers |= ReflModifier.Virtual;
                    break;

                case 'c':
                    parseInfo.Modifiers |= ReflModifier.Const;
                    break;
                case 'r':
                    parseInfo.Modifiers |= ReflModifier.Readonly;
                    break;
                case 'l':
                    parseInfo.Modifiers |= ReflModifier.Volatile;
                    break;

                case 'P':
                    parseInfo.Modifiers |= ReflModifier.Public;
                    break;
                case 'i':
                    parseInfo.Modifiers |= ReflModifier.Internal;
                    break;
                case 'p':
                    parseInfo.Modifiers |= ReflModifier.Protected;
                    break;
                case '0':
                    parseInfo.Modifiers |= ReflModifier.Private;
                    break;

                default:
                    throw new NotSupportedException();
            }
            pos++;
            decodeModifiers(parseInfo, modCode, ref pos);
        }

        void debugAddType()
        {
            if (types.Count / 50 > types_Read_debug_k / 50)
            {
                Console.WriteLine("INFO  Resolver.Resolve   types in buffer : " + types.Count);
                types_Read_debug_k = types.Count;
            }
        }
    }

}