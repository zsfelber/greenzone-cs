using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GreenZoneParser.Reflect
{
    public class PureReflResolver
    {
        static readonly NCmpPiName ncmpinm = new NCmpPiName();
        static readonly NCmpPiMeta ncmpimtd = new NCmpPiMeta();

        List<Assembly> asms = new List<Assembly>();
        Resolver resolver;

        internal PureReflResolver(Resolver resolver)
        {
            FindAssemblies(AppDomain.CurrentDomain, asms);
            this.resolver = resolver;
        }

        public Type GetNativeType(object obj)
        {
            return obj.GetType();
        }

        public Type GetNativeType(string typeId)
        {
            foreach (var asm in asms)
            {
                var aa = asm.GetType(typeId);
                if (aa != null)
                {
                    return aa;
                }
            }
            throw new TypeAccessException();
        }

        public bool IsAssignableFrom(Type type, Type fromType)
        {
            return type.IsAssignableFrom(fromType);
        }

        public PropertyInfo GetNativeProperty(Type declaringType, string name)
        {
            return declaringType.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
        }

        public MethodInfo GetNativeMethod(Type declaringType, string name, Type[] args)
        {
            return declaringType.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly, null, args, null);
        }

        public ConstructorInfo GetNativeConstructor(Type declaringType, Type[] args)
        {
            return declaringType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly, null, args, null);
        }


        public object GetNativePropertyValue(PropertyInfo nativeProperty, object objectOfDeclaringType)
        {
            return nativeProperty.GetValue(objectOfDeclaringType, null);
        }

        public void SetNativePropertyValue(PropertyInfo nativeProperty, object objectOfDeclaringType, object value)
        {
            nativeProperty.SetValue(objectOfDeclaringType, value, null);
        }

        public object CreateObject(ConstructorInfo nativeConstructor, object[] args)
        {
            return nativeConstructor.Invoke(args);
        }

        public object Invoke(MethodInfo nativeMethod, object This, object[] args)
        {
            return nativeMethod.Invoke(This, args);
        }

        internal ReflType Resolve(ReflDefType parentType, ParseInfo typeInfo, out Type type)
        {
            foreach (var asm in asms)
            {
                var aa = asm.GetType(typeInfo.GenericId);
                if (aa != null)
                {
                    type = aa;
                    ReflType rtype = Resolve(parentType, aa);
                    return rtype;
                }
            }

            throw new NotSupportedException();
        }

        ReflType Resolve(ReflDefType parentType, Type type)
        {
            if (parentType == null)
            {
                if (type.DeclaringType != null)
                {
                    throw new NotSupportedException();
                }
            }
            else
            {
                if (type.DeclaringType == null)
                {
                    throw new NotSupportedException();
                }
            }

            ReflType baseType;
            if (type.BaseType == null)
            {
                baseType = null;
            }
            else
            {
                baseType = resolver.Resolve(TypeCode(type.BaseType), true);
            }

            string mod = GenerateModifiers(type);
            ReflModifier modifiers = resolver.ParseModifiers(mod);

            string rawName;
            int gmark = type.Name.IndexOf('`');
            if (gmark == -1)
            {
                rawName = type.Name;
            }
            else
            {
                rawName = type.Name.Substring(0, gmark);
            }

            ReflType result;
            if (IsDelegate(type))
            {
                result = null;
            }
            else if (type.IsClass)
            {
                if (parentType == null)
                {
                    result = new ReflClassType(resolver, null, null, type.MetadataToken, type.Namespace, rawName, (ReflClassType)baseType, modifiers);
                }
                else
                {
                    result = new ReflClassType(resolver, null, null, type.MetadataToken, parentType, rawName, (ReflClassType)baseType, modifiers);
                }
            }
            else if (IsStruct(type))
            {
                if (parentType == null)
                {
                    result = new ReflStructType(resolver, null, null, type.MetadataToken, type.Namespace, rawName, baseType, modifiers);
                }
                else
                {
                    result = new ReflStructType(resolver, null, null, type.MetadataToken, parentType, rawName, baseType, modifiers);
                }
            }
            else if (type.IsInterface)
            {
                if (parentType == null)
                {
                    result = new ReflInterfaceType(resolver, null, null, type.MetadataToken, type.Namespace, rawName, modifiers);
                }
                else
                {
                    result = new ReflInterfaceType(resolver, null, null, type.MetadataToken, parentType, rawName, modifiers);
                }
            }
            else if (type.IsEnum)
            {
                if (parentType == null)
                {
                    result = new ReflEnumType(resolver, null, null, type.MetadataToken, type.Namespace, rawName, baseType, modifiers);
                }
                else
                {
                    result = new ReflEnumType(resolver, null, null, type.MetadataToken, parentType, rawName, baseType, modifiers);
                }
            }
            else if (type.IsPrimitive)
            {
                if (parentType == null)
                {
                    result = new ReflPrimitiveType(resolver, null, null, type.MetadataToken, type.Namespace, rawName, modifiers);
                }
                else
                {
                    result = new ReflPrimitiveType(resolver, null, null, type.MetadataToken, parentType, rawName, modifiers);
                }
            }
            else
            {
                throw new NotSupportedException();
            }

            if (IsDelegate(type))
            {
                ReflMethod invoke = null;
                foreach (var memberNode in type.GetMethods())
                {
                    if (memberNode.Name == "Invoke")
                    {
                        invoke = parseReflMethod(null, memberNode);
                        break;
                    }
                }
                if (invoke == null)
                {
                    throw new NotSupportedException();
                }
                if (parentType == null)
                {
                    result = new ReflDelegateType(resolver, null, null, type.MetadataToken, type.Namespace, rawName, invoke, modifiers);
                }
                else
                {
                    result = new ReflDelegateType(resolver, null, null, type.MetadataToken, parentType, rawName, invoke, modifiers);
                }
                if (parentType != null)
                {
                    parentType.AddMember(result);
                }
            }
            else if (type.IsEnum)
            {
                if (parentType != null)
                {
                    parentType.AddMember(result);
                }

                string[] names = type.GetEnumNames();
                ReflEnumType enumType = (ReflEnumType)result;
                int i = 0;
                foreach (var ec in type.GetEnumValues())
                {
                    ReflEnumConstant enumConst = new ReflEnumConstant(resolver, null, null, enumType, names[i], Convert.ToDecimal((Enum)ec));
                    enumType.AddEnumConstant(enumConst);
                    i++;
                }
            }

            return result;
        }

        internal void FinishResolve(ReflDefType parentType, Type type, ReflType rtype)
        {
            if (rtype is ReflObjType)
            {
                var baseIs = GetBaseInterfaces(type).ToArray();
                if (baseIs.Length != 0)
                {
                    ReflObjType otype = (ReflObjType)rtype;
                    foreach (Type parg in baseIs)
                    {
                        otype.AddBaseInterface((ReflInterfaceType)resolver.Resolve(TypeCode(parg), true));
                    }
                }
            }

            ReflTypeArgDefinition[] targs = parseReflGenericArgDefs(type);
            if (targs != null && targs.Length > 0)
            {
                ReflCallableType ctype = (ReflCallableType)rtype;
                foreach (var targ in targs)
                {
                    ctype.AddTypeArgDefinition(targ);
                }
            }

            ReflAttribute[] attributes = parseReflAttributes(type);
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    rtype.AddAttribute(attribute);
                }
            }

            if (IsDelegate(type))
            {
            }
            else if (type.IsEnum)
            {
            }
            else if (type.IsPrimitive)
            {
            }
            else
            {
                if (parentType != null)
                {
                    parentType.AddMember(rtype);
                }

                ReflObjType objType = (ReflObjType)rtype;
                foreach (var memberNode in type.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                {
                    if (IsGenerated(memberNode))
                    {
                        Console.WriteLine("WARN Not generating   member:" + memberNode.Name);
                    }
                    else
                    {
                        ReflMember member = parseReflMember(objType, memberNode);
                        objType.AddMember(member);
                    }
                }
            }
        }

        ReflMember parseReflMember(ReflObjType declaringType, MemberInfo memberNode)
        {
            if (memberNode is Type)
                return resolver.Resolve(TypeCode((Type)memberNode), true);
            else if (memberNode is MethodInfo)
                return parseReflMethod(declaringType, (MethodInfo)memberNode);
            else if (memberNode is ConstructorInfo)
                return parseReflConstr(declaringType, (ConstructorInfo)memberNode);
            else if (memberNode is PropertyInfo)
                return parseReflProp(declaringType, (PropertyInfo)memberNode);
            else if (memberNode is FieldInfo)
                return parseReflField(declaringType, (FieldInfo)memberNode);
            else if (memberNode is EventInfo)
                return parseReflEvent(declaringType, (EventInfo)memberNode);
            else
                throw new NotSupportedException();
        }

        ReflMethod parseReflMethod(ReflObjType declaringType, MethodInfo memberNode)
        {
            string mod = GenerateModifiers(memberNode);
            ReflModifier modifiers = resolver.ParseModifiers(mod);

            ReflAttribute[] attributes = parseReflAttributes(memberNode);

            ReflMethodArgDefinition[] args = parseReflMethodParameters(memberNode);

            ReflTypeArgDefinition[] targs = parseReflGenericArgDefs(memberNode);

            ReflType memberType = resolver.Resolve(TypeCode(memberNode.ReturnType), true);
            ReflMethod result = new ReflMethod(resolver, null, null, memberNode.MetadataToken, declaringType, memberType, memberNode.Name, modifiers, attributes, args);
            if (targs != null)
            {
                foreach (var targ in targs)
                {
                    result.AddTypeArgDefinition(targ);
                }
            }
            return result;
        }

        ReflConstructor parseReflConstr(ReflObjType declaringType, ConstructorInfo memberNode)
        {
            string mod = GenerateModifiers(memberNode);
            ReflModifier modifiers = resolver.ParseModifiers(mod);

            ReflAttribute[] attributes = parseReflAttributes(memberNode);

            ReflMethodArgDefinition[] args = parseReflMethodParameters(memberNode);

            ReflConstructor result = new ReflConstructor(resolver, null, null, memberNode.MetadataToken, declaringType, modifiers, attributes, args);
            return result;
        }

        ReflMember parseReflProp(ReflObjType declaringType, PropertyInfo memberNode)
        {
            string mod = GenerateModifiers(memberNode);
            ReflModifier modifiers = resolver.ParseModifiers(mod);

            ReflModifier setModifiers = 0;
            if (memberNode.CanRead && memberNode.CanWrite)
            {
                string mod2 = GenerateModifiers(memberNode.GetSetMethod(true));
                setModifiers = resolver.ParseModifiers(mod2);
                if (modifiers == setModifiers)
                {
                    setModifiers = 0;
                }
            }

            ReflAttribute[] attributes = parseReflAttributes(memberNode);

            ReflMethodArgDefinition[] args = parseReflMethodParameters(memberNode);

            ReflType memberType = resolver.Resolve(TypeCode(memberNode.PropertyType), true);

            ReflMember result;
            if (args.Length == 0)
            {
                result = new ReflProperty(resolver, null, null, memberNode.MetadataToken, declaringType, memberType, memberNode.CanRead ? memberNode.CanWrite ? ReflPropertyMethodType.GetSet : ReflPropertyMethodType.Get : ReflPropertyMethodType.Set, memberNode.Name, modifiers, setModifiers, attributes);
            }
            else
            {
                result = new ReflIndexerProperty(resolver, null, null, memberNode.MetadataToken, declaringType, memberType, memberNode.CanRead ? memberNode.CanWrite ? ReflPropertyMethodType.GetSet : ReflPropertyMethodType.Get : ReflPropertyMethodType.Set, memberNode.Name, modifiers, setModifiers, attributes, args);
            }
            return result;
        }

        ReflField parseReflField(ReflObjType declaringType, FieldInfo memberNode)
        {
            string mod = GenerateModifiers(memberNode);
            ReflModifier modifiers = resolver.ParseModifiers(mod);

            ReflAttribute[] attributes = parseReflAttributes(memberNode);

            ReflType memberType = resolver.Resolve(TypeCode(memberNode.FieldType), true);
            ReflField result;
            string constant = null;
            try
            {
                if (memberNode.GetRawConstantValue() != null)
                {
                    constant = GreenZoneSysUtilsBase.EscapeXml(memberNode.GetRawConstantValue());
                }
            }
            catch (InvalidOperationException)
            {
            }

            if (constant == null)
            {
                result = new ReflField(resolver, null, null, memberNode.MetadataToken, declaringType, memberType, memberNode.Name, modifiers, attributes);
            }
            else
            {
                result = new ReflField(resolver, null, null, memberNode.MetadataToken, declaringType, memberType, memberNode.Name, constant, modifiers, attributes);
            }
            return result;
        }

        ReflEvent parseReflEvent(ReflObjType declaringType, EventInfo memberNode)
        {
            string mod = GenerateModifiers(memberNode);
            ReflModifier modifiers = resolver.ParseModifiers(mod);

            ReflAttribute[] attributes = parseReflAttributes(memberNode);

            ReflType memberType = resolver.Resolve(TypeCode(memberNode.EventHandlerType), true);
            ReflEvent result = new ReflEvent(resolver, null, null, memberNode.MetadataToken, declaringType, (ReflDelegateType)memberType, memberNode.Name, modifiers, attributes);
            return result;
        }

        ReflTypeArgDefinition[] parseReflGenericArgDefs(Type type)
        {
            Type[] gas = type.GetGenericArguments();
            return parseReflGenericArgDefs(gas);
        }

        ReflTypeArgDefinition[] parseReflGenericArgDefs(MethodInfo minf)
        {
            Type[] gas = minf.GetGenericArguments();
            return parseReflGenericArgDefs(gas);
        }

        ReflTypeArgDefinition[] parseReflGenericArgDefs(Type[] gas)
        {
            ReflTypeArgDefinition[] targs;
            if (gas.Length == 0)
            {
                targs = null;
            }
            else
            {
                targs = new ReflTypeArgDefinition[gas.Length];
                List<ReflTypeArgRule> rules = new List<ReflTypeArgRule>();
                for (int i = 0; i < targs.Length; i++)
                {
                    Type tparg = gas[i];

                    foreach (var tpc in tparg.GetGenericParameterConstraints())
                    {
                        GenericParameterAttributes gpa = tparg.GenericParameterAttributes;

                        if ((gpa & GenericParameterAttributes.DefaultConstructorConstraint) != 0)
                        {
                            rules.Add(new ReflTypeArgRule(resolver, null, null, ReflTypeArgSpecialRule.New));
                        }
                        else if ((gpa & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
                        {
                            rules.Add(new ReflTypeArgRule(resolver, null, null, ReflTypeArgSpecialRule.Class));
                        }
                        else if ((gpa & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
                        {
                            rules.Add(new ReflTypeArgRule(resolver, null, null, ReflTypeArgSpecialRule.Struct));
                        }
                        else if ((gpa & GenericParameterAttributes.Covariant) != 0)
                        {
                            rules.Add(new ReflTypeArgRule(resolver, null, null, ReflTypeArgSpecialRule.Cov));
                        }
                        else if ((gpa & GenericParameterAttributes.Contravariant) != 0)
                        {
                            rules.Add(new ReflTypeArgRule(resolver, null, null, ReflTypeArgSpecialRule.Contr));
                        }
                        else
                        {
                            rules.Add(new ReflTypeArgRule(resolver, null, null, resolver.Resolve(TypeCode(tpc), true)));
                        }
                    }

                    targs[i] = new ReflTypeArgDefinition(resolver, null, null, tparg.Name, rules.ToArray());
                }
            }
            return targs;
        }

        ReflAttribute[] parseReflAttributes(MemberInfo memberInfo)
        {
            return parseReflAttributes(memberInfo.GetCustomAttributesData());
        }

        ReflAttribute[] parseReflAttributes(IEnumerable<CustomAttributeData> attrs)
        {
            List<ReflAttribute> result = new List<ReflAttribute>();
            foreach (var attr in attrs)
            {
                ReflClassType declaringClass = (ReflClassType)resolver.Resolve(TypeCode(attr.Constructor.DeclaringType), true);
                ReflAttribute rattr = new ReflAttribute(resolver, null, null, declaringClass, attr.Constructor.MetadataToken);
                result.Add(rattr);

                int i = 0;
                foreach (var a in attr.ConstructorArguments)
                {
                    new ReflAttributeArg(resolver, null, null, rattr, i, GreenZoneSysUtilsBase.EscapeXml(a.Value));
                    i++;
                }
                foreach (var a in attr.NamedArguments)
                {
                    new ReflAttributeArg(resolver, null, null, rattr, a.MemberInfo.Name, GreenZoneSysUtilsBase.EscapeXml(a.TypedValue.Value));
                }

            }
            return result.ToArray();
        }

        ReflMethodArgDefinition[] parseReflMethodParameters(MethodBase methodNode)
        {
            bool isExtension = IsExtension(methodNode);
            return parseReflMethodParameters(methodNode.GetParameters(), isExtension);
        }

        ReflMethodArgDefinition[] parseReflMethodParameters(PropertyInfo propertyNode)
        {
            return parseReflMethodParameters(propertyNode.GetIndexParameters(), false);
        }

        ReflMethodArgDefinition[] parseReflMethodParameters(ParameterInfo[] parameters, bool isExtension)
        {
            List<ReflMethodArgDefinition> result = new List<ReflMethodArgDefinition>();
            int i = 0;
            foreach (ParameterInfo pi in parameters)
            {
                ReflType paramType = resolver.Resolve(TypeCode(pi.ParameterType), true);
                string name = pi.Name;
                string pt;
                if (IsParams(pi))
                {
                    pt = "Params";
                }
                else if (pi.IsOut)
                {
                    pt = "Out";
                }
                else if (pi.IsRetval)
                {
                    pt = "Ref";
                }
                else if (isExtension && i == 0)
                {
                    pt = "This";
                }
                else
                {
                    pt = "Value";
                }
                ReflAttribute[] attrs = parseReflAttributes(pi.GetCustomAttributesData());
                ReflMethodArgType argType = (ReflMethodArgType)Enum.Parse(typeof(ReflMethodArgType), pt);
                ReflMethodArgDefinition argDef = new ReflMethodArgDefinition(resolver, null, null, paramType, name, argType, attrs);
                result.Add(argDef);

                i++;
            }
            return result.ToArray();
        }


        public static string TypeCode(Type type, bool isClassDefinition = false, bool omitSemicolon = true)
        {
            if (type.IsByRef)
            {
                return TypeCode(type.GetElementType(), isClassDefinition, omitSemicolon);
            }
            else
            {
                if (!isClassDefinition)
                {
                    switch (type.FullName)
                    {
                        case "System.Void":
                            return "v";
                        case "System.Boolean":
                            return "b";
                        case "System.Byte":
                            return "y";
                        case "System.SByte":
                            return "Y";
                        case "System.Int16":
                            return "s";
                        case "System.UInt16":
                            return "S";
                        case "System.Int32":
                            return "n";
                        case "System.UInt32":
                            return "N";
                        case "System.Int64":
                            return "l";
                        case "System.UInt64":
                            return "L";
                        case "System.IntPtr":
                            return "p";
                        case "System.UIntPtr":
                            return "P";
                        case "System.Char":
                            return "c";
                        case "System.Double":
                            return "d";
                        case "System.Single":
                            return "f";
                        case "System.Decimal":
                            return "e";
                        case "System.Object":
                            return "O";
                        case "System.String":
                            return "X";
                        case "System.Type":
                            return "t";
                        default:
                            break;
                    }
                }

                StringBuilder result = new StringBuilder();

                if (type.IsArray)
                {
                    result.Append("A");
                    result.Append(type.GetArrayRank());
                    result.Append(TypeCode(type.GetElementType(), false, false));
                }
                else if (type.IsPointer)
                {
                    result.Append("*");
                    result.Append(TypeCode(type.GetElementType(), false, false));
                }
                else
                {
                    if (type.IsNested && !type.IsGenericParameter)
                    {
                        result.Append(TypeCode(type.DeclaringType, false, true)).Append("+");
                    }

                    if (type.GetGenericArguments().Length > 0)
                    {
                        result.Append("G");
                        result.Append(type.GetGenericArguments().Length);
                        foreach (Type ta in type.GetGenericArguments())
                        {
                            result.Append(TypeCode(ta, false, false));
                        }
                    }
                    else if (type.IsGenericParameter)
                    {
                        result.Append("g");
                    }
                    else if (typeof(Attribute).IsAssignableFrom(type))
                    {
                        result.Append("a");
                    }
                    else
                    {
                        result.Append("T");
                    }

                    string typeDefeninition;

                    if (isClassDefinition)
                    {
                        typeDefeninition = type.Name;
                    }
                    else if (type.Namespace != null)
                    {
                        if (type.IsGenericParameter || type.IsNested)
                        {
                            typeDefeninition = type.Name;
                        }
                        else
                        {
                            typeDefeninition = type.Namespace + "." + type.Name;
                        }
                    }
                    else
                    {
                        typeDefeninition = type.Name;
                    }

                    if (typeDefeninition[typeDefeninition.Length - 1] == '&')
                    {
                        typeDefeninition = typeDefeninition.Substring(0, typeDefeninition.Length - 1);
                    }
                    if (type.GetGenericArguments().Length > 0)
                    {
                        int gm = typeDefeninition.IndexOf("`");
                        if (gm != -1)
                        {
                            typeDefeninition = typeDefeninition.Substring(0, gm);
                        }
                    }
                    result.Append(typeDefeninition);
                    if (!omitSemicolon)
                    {
                        result.Append(";");
                    }
                }
                return result.ToString();
            }
        }


        // ----------------



        public static void FindAssemblies(AppDomain domain, List<Assembly> asms)
        {
            asms.AddRange(AppDomain.CurrentDomain.GetAssemblies());

            FindAssemblies(domain, AppDomain.CurrentDomain.GetAssemblies(), asms);
        }

        public static void FindAssemblies(AppDomain domain, IEnumerable<Assembly> asms0, List<Assembly> asms)
        {
            foreach (var a in asms0)
            {
                List<Assembly> asms00 = new List<Assembly>();
                foreach (var an in a.GetReferencedAssemblies())
                {
                    Assembly asm = Assembly.Load(an);
                    if (!asms.Contains(asm))
                    {
                        asms00.Add(asm);
                        asms.Add(asm);
                    }
                }
                FindAssemblies(domain, asms00, asms);
            }
        }

        public static IEnumerable<Type> GetBaseInterfaces(Type type)
        {
            var allInterfaces = type.GetInterfaces();
            var allBaseTypes = type.BaseType == null ? allInterfaces : GreenZoneSysUtilsBase.AddToArray(allInterfaces, type.BaseType);
            var result = allInterfaces.Except
                    (allBaseTypes.SelectMany(t => t.GetInterfaces()));
            return result;
        }

        public static void SortMembers<T>(T[] fields, bool sortByName) where T : MemberInfo
        {
            if (sortByName)
            {
                Array.Sort(fields, ncmpinm);
            }
            else
            {
                // sorts by declaration order :
                // info:
                // http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/ab8718e0-d029-4433-9908-02d3a252620c/
                Array.Sort(fields, ncmpimtd);
            }
        }

        public static MethodInfo[] GetTopLevelMethods(Type objType, bool sortByName = false)
        {
            MethodInfo[] fields = objType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static PropertyInfo[] GetTopLevelProperties(Type objType, bool sortByName = false)
        {
            PropertyInfo[] fields = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static bool IsDelegate(Type type)
        {
            return !type.IsAbstract && typeof(Delegate).IsAssignableFrom(type);
        }

        public static bool IsStruct(Type type)
        {
            return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
        }

        public static bool IsStatic(Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        public static bool IsParams(ParameterInfo param)
        {
            return Attribute.IsDefined(param, typeof(ParamArrayAttribute));
        }

        public static bool IsVolatile(FieldInfo field)
        {
            return field
                .GetRequiredCustomModifiers()
                .Any(x => x == typeof(IsVolatile));
        }

        public static bool IsExtension(MethodBase method)
        {
            return Attribute.IsDefined(method, typeof(ExtensionAttribute));
        }

        public static bool IsGenerated(MemberInfo member)
        {
            return member.IsDefined(typeof(CompilerGeneratedAttribute), false);
        }

        public static bool HasDefault(ParameterInfo param)
        {
            return Attribute.IsDefined(param, typeof(DefaultParameterValueAttribute));
        }

        public static string GenerateModifiers(Type type)
        {
            StringBuilder mod = new StringBuilder();
            if (IsStatic(type))
            {
                mod.Append("s");
            }
            else if (type.IsAbstract)
            {
                mod.Append("a");
            }
            else if (type.IsSealed)
            {
                mod.Append("e");
            }

            if (type.IsNested)
            {
                if (type.IsNestedPublic)
                {
                    mod.Append("P");
                }
                else if (type.IsNestedFamANDAssem)
                {
                    mod.Append("pi");
                }
                else if (type.IsNestedAssembly)
                {
                    mod.Append("i");
                }
                else if (type.IsNestedFamily)
                {
                    mod.Append("p");
                }
                else if (type.IsNestedPrivate)
                {
                    mod.Append("0");
                }
            }
            else
            {
                if (type.IsPublic)
                {
                    mod.Append("P");
                }
            }
            return mod.ToString();
        }

        public static string GenerateModifiers(MethodBase mb)
        {
            StringBuilder mod = new StringBuilder();
            if (mb.IsAbstract)
            {
                mod.Append("a");
            }
            else if (mb is MethodInfo && ((MethodInfo)mb).GetBaseDefinition() != mb)
            {
                mod.Append("o");
            }
            else if (mb.IsVirtual)
            {
                mod.Append("v");
            }
            else if (mb.IsStatic)
            {
                mod.Append("s");
            }

            if (mb.IsPublic)
            {
                mod.Append("P");
            }
            else if (mb.IsAssembly)
            {
                if (mb.IsFamily)
                {
                    mod.Append("pi");
                }
                else
                {
                    mod.Append("i");
                }
            }
            else if (mb.IsFamily)
            {
                mod.Append("p");
            }
            else if (mb.IsPrivate)
            {
                mod.Append("0");
            }
            return mod.ToString();
        }

        public static string GenerateModifiers(PropertyInfo pi)
        {
            if (pi.CanRead)
            {
                string mod = GenerateModifiers(pi.GetGetMethod(true));
                return mod;
            }
            else
            {
                string mod = GenerateModifiers(pi.GetSetMethod(true));
                return mod;
            }
        }

        public static string GenerateModifiers(FieldInfo field)
        {
            StringBuilder mod = new StringBuilder();
            if (field.IsStatic)
            {
                mod.Append("s");
            }
            else if (field.IsLiteral)
            {
                mod.Append("c");
            }

            if (field.IsInitOnly)
            {
                mod.Append("r");
            }
            else if (IsVolatile(field))
            {
                mod.Append("l");
            }

            if (field.IsPublic)
            {
                mod.Append("P");
            }
            else if (field.IsAssembly)
            {
                if (field.IsFamily)
                {
                    mod.Append("pi");
                }
                else
                {
                    mod.Append("i");
                }
            }
            else if (field.IsFamily)
            {
                mod.Append("p");
            }
            else if (field.IsPrivate)
            {
                mod.Append("0");
            }
            return mod.ToString();
        }

        public static string GenerateModifiers(EventInfo evt)
        {
            MethodInfo mi = evt.GetAddMethod(true);

            StringBuilder mod = new StringBuilder();
            if (mi.IsStatic)
            {
                mod.Append("s");
            }

            if (mi.IsPublic)
            {
                mod.Append("P");
            }
            else if (mi.IsAssembly)
            {
                if (mi.IsFamily)
                {
                    mod.Append("pi");
                }
                else
                {
                    mod.Append("i");
                }
            }
            else if (mi.IsFamily)
            {
                mod.Append("p");
            }
            else if (mi.IsPrivate)
            {
                mod.Append("0");
            }
            return mod.ToString();
        }
    }


    class NCmpPiName : IComparer<MemberInfo>
    {
        public int Compare(MemberInfo x, MemberInfo y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    class NCmpPiMeta : IComparer<MemberInfo>
    {
        public int Compare(MemberInfo x, MemberInfo y)
        {
            return x.MetadataToken.CompareTo(y.MetadataToken);
        }
    }
}