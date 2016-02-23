using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneParser.Xml;
using GreenZoneParser.Lexer;
using GreenZoneParser.Parsers;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Reflect
{
    public class Resolver
    {
        readonly SortedDictionary<string, ReflType> types;
        public readonly ReadOnlyDictionary<string, ReflType> Types;
        int types_Read_debug_k = 0;

        internal readonly ParseInfo rootParseInfo;
        readonly Dictionary<string, ParseInfo> defParseInfos;

        internal readonly PureReflResolver PureReflResolver;

        public static readonly ITypeNameBuilder TypeNameBuilder = new CsTypeNameBuilder();

        public Resolver(IEnumerable<string> xmlFiles)
        {
            types = new SortedDictionary<string, ReflType>();
            Types = new ReadOnlyDictionary<string, ReflType>(types);

            rootParseInfo = new ParseInfo(null, null);
            defParseInfos = new Dictionary<string, ParseInfo>();

            PureReflResolver = new PureReflResolver(this);

            foreach (string xmlFile in xmlFiles)
            {
                preLoadXml(xmlFile);
            }
        }

        public Resolver(IEnumerable<XmlParser> parsedXmlFiles)
        {
            types = new SortedDictionary<string, ReflType>();
            Types = new ReadOnlyDictionary<string, ReflType>(types);

            rootParseInfo = new ParseInfo(null, null);
            defParseInfos = new Dictionary<string, ParseInfo>();

            PureReflResolver = new PureReflResolver(this);

            foreach (var parser in parsedXmlFiles)
            {
                preLoadXml(parser);
            }
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

        void preLoadXml(string xmlFile)
        {
            string fileContent = File.ReadAllText(xmlFile);
            XmlParser parser = new XmlParser(xmlFile, fileContent.Replace("\r\n", "\n"));
            parser.Parse();

            preLoadXml(parser);
        }

        void preLoadXml(XmlParser parser)
        {
            string xmlFile = parser.FileName;
            foreach (var err in parser.CompilationErrors)
            {
                Console.WriteLine("ERROR in " + xmlFile + ":  " + err.Message + "  Line:" + err.Line + "  Column:" + err.Column + "  Length:" + err.Length);
                return;
            }

            if (parser.RootNodes.Count != 1)
            {
                Console.WriteLine("ERROR in " + xmlFile + ":  parser.RootNodes.Count != 1");
                return;
            }

            XmlNode assemblyNode = parser.RootNodes[0];
            if (assemblyNode.OpenTag.Name != "assembly")
            {
                Console.WriteLine("ERROR in " + xmlFile + ":  Root node is not 'assembly'");
                return;
            }

            ParseInfo parserInfo = new ParseInfo(parser, rootParseInfo);
            if (rootParseInfo.Children == null)
            {
                rootParseInfo.Children = new Dictionary<string, ParseInfo>();
            }
            rootParseInfo.Children[parser.FileName] = parserInfo;
            preLoadParseNode(assemblyNode, parserInfo);
        }

        void preLoadParseNode(XmlNode parentNode, ParseInfo parentParseInfo)
        {
            foreach (XmlNode typeNode in parentNode.Children)
            {
                XmlTag typeTag = typeNode.OpenTag;
                if (typeTag.Name == "type")
                {

                    int id = Convert.ToInt32(typeTag.attributes["id"]);
                    string name = (string)typeTag.attributes["n"];
                    string namespce = (string)typeTag.attributes["ns"];
                    string modifiers = (string)typeTag.attributes["mod"];
                    string tpType = (string)typeTag.attributes["t"];

                    bool hasBody;
                    switch (tpType)
                    {
                        case "cl":
                        case "de":
                        case "st":
                        case "in":
                            hasBody = true;
                            break;
                        case "en":
                            hasBody = false;
                            break;
                        default:
                            throw new NotSupportedException();
                    }

                    int pos = 0;
                    ParseInfo tpParseInfo = decodeTypeCode(parentParseInfo, name, ref pos, true);
                    tpParseInfo.ParseNode = typeNode;
                    tpParseInfo.TpType = tpType;
                    tpParseInfo.Namespace = namespce;
                    tpParseInfo.Id = id;
                    if (parentParseInfo.Children == null)
                    {
                        parentParseInfo.Children = new Dictionary<string, ParseInfo>();
                    }
                    if (parentParseInfo.Parent != rootParseInfo)
                    {
                        tpParseInfo.ParentType = parentParseInfo;
                    }
                    pos = 0;
                    decodeModifiers(tpParseInfo, modifiers, ref pos);
                    parentParseInfo.Children[tpParseInfo.GenericId] = tpParseInfo;
                    defParseInfos[tpParseInfo.GenericId] = tpParseInfo;

                    int ni0 = 0;
                    XmlNode node0 = null;
                    XmlTag t0 = null;

                    if (hasBody)
                    {
                        parseAttributes(tpParseInfo, typeNode, ref ni0, ref node0, ref t0);

                        if (ok_child(typeNode, ref ni0, ref node0, ref t0, "base-t"))
                        {
                            pos = 0;
                            tpParseInfo.BaseType = decodeTypeCode(tpParseInfo, (string)t0.attributes["n"], ref pos);
                            tpParseInfo.BaseType.Id = Convert.ToInt32(t0.attributes["id"]);
                            tpParseInfo.BaseType.ParseNode = node0;
                        }
                        for (; ok_child(typeNode, ref ni0, ref node0, ref t0, "base-i"); )
                        {
                            if (tpParseInfo.BaseInterfaces == null)
                            {
                                tpParseInfo.BaseInterfaces = new List<ParseInfo>();
                            }
                            pos = 0;
                            ParseInfo baseInterface = decodeTypeCode(tpParseInfo, (string)t0.attributes["n"], ref pos);
                            baseInterface.Id = Convert.ToInt32(t0.attributes["id"]);
                            baseInterface.ParseNode = node0;
                            tpParseInfo.BaseInterfaces.Add(baseInterface);
                        }

                        parseGenericConstraints(tpParseInfo, typeNode, ref ni0, ref node0, ref t0);

                        string[] _memberTags = { "type", "method", "constr", "prop", "field", "event" };
                        SortedSet<string> memberTags = new SortedSet<string>(_memberTags);
                        for (; ok_child(typeNode, ref ni0, ref node0, ref t0, memberTags); )
                        {
                            if (t0.Name == "type")
                            {
                                preLoadParseNode(node0, tpParseInfo);
                            }
                            else
                            {
                                if (tpParseInfo.UnparsedMembers == null)
                                {
                                    tpParseInfo.UnparsedMembers = new List<XmlNode>();
                                }
                                tpParseInfo.UnparsedMembers.Add(node0);
                            }
                        }

                        if (node0 != null)
                        {
                            throw new NotSupportedException();
                        }
                    }
                    else if (tpType == "en")
                    {
                        parseAttributes(tpParseInfo, typeNode, ref ni0, ref node0, ref t0);

                        if (ok_child(typeNode, ref ni0, ref node0, ref t0, "base"))
                        {
                            pos = 0;
                            tpParseInfo.BaseType = decodeTypeCode(tpParseInfo, (string)t0.attributes["n"], ref pos);
                            tpParseInfo.BaseType.Id = Convert.ToInt32(t0.attributes["id"]);
                            tpParseInfo.BaseType.ParseNode = node0;
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }

                        List<EnumConstant> es = new List<EnumConstant>();

                        for (; ok_child(typeNode, ref ni0, ref node0, ref t0, "item"); )
                        {
                            EnumConstant ec = new EnumConstant(tpParseInfo);
                            ec.EnumConstName = (string)t0.attributes["n"];
                            ec.EnumConstValue = Convert.ToDecimal(t0.attributes["v"]);
                            ec.ParseNode = node0;
                            es.Add(ec);
                        }

                        if (es.Count > 0)
                        {
                            tpParseInfo.EnumConstants = es.ToArray();
                        }

                        if (node0 != null)
                        {
                            throw new NotSupportedException();
                        }
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                }
            }
        }

        void parseGenericConstraints(ParseInfo tpParseInfo, XmlNode typeNode, ref int ni0, ref XmlNode node0, ref XmlTag t0)
        {
            for (; ok_child(typeNode, ref ni0, ref node0, ref t0, "gc"); )
            {
                int tpid = Convert.ToInt32(t0.attributes["type-is"]);
                ParseInfo ctype = tpParseInfo.GenericArgs[tpid];

                List<GenericConstraint> cs = new List<GenericConstraint>();

                int ni = 0;
                XmlNode node = null;
                XmlTag t = null;
                for (; ok_child(node0, ref ni, ref node, ref t, "rule"); )
                {
                    GenericConstraint gc = new GenericConstraint(tpParseInfo);
                    gc.ParseNode = node;
                    string n = (string)t.attributes["n"];
                    switch (n)
                    {
                        case "new()":
                            gc.Rule = ReflTypeArgSpecialRule.New;
                            break;
                        case "class":
                            gc.Rule = ReflTypeArgSpecialRule.Class;
                            break;
                        case "struct":
                            gc.Rule = ReflTypeArgSpecialRule.Struct;
                            break;
                        case "cov":
                            gc.Rule = ReflTypeArgSpecialRule.Cov;
                            break;
                        case "contr":
                            gc.Rule = ReflTypeArgSpecialRule.Contr;
                            break;
                        default:
                            gc.Rule = ReflTypeArgSpecialRule.Type;
                            int pos = 0;
                            gc.Type = decodeTypeCode(gc, n, ref pos);
                            break;
                    }
                    cs.Add(gc);
                }
                if (cs.Count > 0)
                {
                    ctype.GenericConstraints = cs.ToArray();
                }
            }
        }

        void parseAttributes(ParseInfo tpParseInfo, XmlNode typeNode, ref int ni0, ref XmlNode node0, ref XmlTag t0)
        {
            if (ok_child(typeNode, ref ni0, ref node0, ref t0, "attrs"))
            {
                tpParseInfo.Attributes = new List<ParseInfo>();

                int ni = 0;
                XmlNode node = null;
                XmlTag t = null;
                int pos;
                for (; ok_child(node0, ref ni, ref node, ref t, "attr"); )
                {
                    pos = 0;
                    ParseInfo attr = decodeTypeCode(tpParseInfo, (string)t.attributes["t"], ref pos);

                    tpParseInfo.Attributes.Add(attr);

                    attr.ParseNode = node;
                    attr.AttributeConstrArgs = new List<AttributeConstrArg>();
                    attr.AttributeNamedArgs = new List<AttributeConstrArg>();

                    int ni1 = 0;
                    XmlNode node1 = null;
                    XmlTag t1 = null;
                    attr.AttrConstrId = Convert.ToInt32(t.attributes["constr"]);

                    if (ok_child(node, ref ni1, ref node1, ref t1, "args"))
                    {
                        int ni2 = 0;
                        XmlNode node2 = null;
                        XmlTag t2 = null;
                        for (; ok_child(node1, ref ni2, ref node2, ref t2, "arg"); )
                        {
                            AttributeConstrArg arg = new AttributeConstrArg(attr);
                            pos = 0;
                            arg.Type = decodeTypeCode(arg, (string)t2.attributes["t"], ref pos);
                            arg.Value = (string)t2.attributes["v"];
                            attr.AttributeConstrArgs.Add(arg);
                            arg.ParseNode = node2;
                        }
                    }

                    ni1 = 0;
                    node1 = null;
                    t1 = null;

                    if (ok_child(node, ref ni1, ref node1, ref t1, "named-args"))
                    {
                        int ni2 = 0;
                        XmlNode node2 = null;
                        XmlTag t2 = null;
                        for (; ok_child(node1, ref ni2, ref node2, ref t2, "arg"); )
                        {
                            AttributeConstrArg arg = new AttributeConstrArg(attr);
                            pos = 0;
                            arg.Type = decodeTypeCode(arg, (string)t2.attributes["t"], ref pos);
                            arg.Value = (string)t2.attributes["v"];
                            arg.Property = (string)t2.attributes["p"];
                            attr.AttributeNamedArgs.Add(arg);
                            arg.ParseNode = node2;
                        }
                    }
                }
            }
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
                    foreach (var memberNode in defTypeInfo.UnparsedMembers)
                    {
                        object nmobj;
                        memberNode.OpenTag.attributes.TryGetValue("n", out nmobj);

                        if ("TInvoke".Equals(nmobj))
                        {
                            invoke = parseReflMethod(defTypeInfo, null, memberNode);
                            break;
                        }
                    }
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

                    ReflObjType objType = (ReflObjType)type;
                    if (defTypeInfo.UnparsedMembers != null)
                    {
                        foreach (var memberNode in defTypeInfo.UnparsedMembers)
                        {
                            ReflMember member = parseReflMember(defTypeInfo, objType, memberNode);
                            objType.AddMember(member);
                        }
                    }
                    break;
            }
        }

        ReflMember parseReflMember(ParseInfo typeDeclParseInfo, ReflObjType declaringType, XmlNode memberNode)
        {
            switch (memberNode.OpenTag.Name)
            {
                case "method":
                    return parseReflMethod(typeDeclParseInfo, declaringType, memberNode);
                case "constr":
                    return parseReflConstr(typeDeclParseInfo, declaringType, memberNode);
                case "prop":
                    return parseReflProp(typeDeclParseInfo, declaringType, memberNode);
                case "field":
                    return parseReflField(typeDeclParseInfo, declaringType, memberNode);
                case "event":
                    return parseReflEvent(typeDeclParseInfo, declaringType, memberNode);
                default:
                    throw new NotSupportedException();
            }
        }

        ReflMethod parseReflMethod(ParseInfo typeDeclParseInfo, ReflObjType declaringType, XmlNode memberNode)
        {
            int ni0 = 0;
            XmlNode node0 = null;
            XmlTag t0 = memberNode.OpenTag;
            int pos;
            ParseInfo nameInfo;
            ParseInfo memberTypeInfo;
            string modifiers;
            int id;

            pos = 0;
            nameInfo = decodeTypeCode(typeDeclParseInfo, (string)t0.attributes["n"], ref pos);
            pos = 0;
            memberTypeInfo = decodeTypeCode(typeDeclParseInfo, (string)t0.attributes["rt"], ref pos);
            memberTypeInfo.ParseNode = memberNode;
            modifiers = (string)t0.attributes["mod"];
            id = Convert.ToInt32(t0.attributes["id"]);
            pos = 0;
            decodeModifiers(nameInfo, modifiers, ref pos);
            ni0 = 0;

            parseAttributes(nameInfo, memberNode, ref ni0, ref node0, ref t0);
            ReflAttribute[] attributes = parseReflAttributes(nameInfo);

            ReflMethodArgDefinition[] args = parseReflMethodParameters(typeDeclParseInfo, memberNode, ref ni0, ref node0, ref t0);

            parseGenericConstraints(nameInfo, memberNode, ref ni0, ref node0, ref t0);

            ReflTypeArgDefinition[] targs = parseReflGenericArgDefs(nameInfo);

            ReflType memberType = Resolve(memberTypeInfo, false);
            ReflMethod result = new ReflMethod(this, declaringType == null ? null : declaringType.Parser, memberNode, id, declaringType, memberType, nameInfo.Name, nameInfo.Modifiers, attributes, args);
            if (targs != null)
            {
                foreach (var targ in targs)
                {
                    result.AddTypeArgDefinition(targ);
                }
            }
            return result;
        }

        ReflConstructor parseReflConstr(ParseInfo typeDeclParseInfo, ReflObjType declaringType, XmlNode memberNode)
        {
            int ni0 = 0;
            XmlNode node0 = null;
            XmlTag t0 = memberNode.OpenTag;
            int pos;
            ParseInfo nameInfo;
            string modifiers;
            int id;

            modifiers = (string)t0.attributes["mod"];
            id = Convert.ToInt32(t0.attributes["id"]);
            pos = 0;
            nameInfo = new ParseInfo(typeDeclParseInfo);
            decodeModifiers(nameInfo, modifiers, ref pos);
            ni0 = 0;

            parseAttributes(nameInfo, memberNode, ref ni0, ref node0, ref t0);
            ReflAttribute[] attributes = parseReflAttributes(nameInfo);

            ReflMethodArgDefinition[] args = parseReflMethodParameters(typeDeclParseInfo, memberNode, ref ni0, ref node0, ref t0);

            ReflConstructor result = new ReflConstructor(this, declaringType.Parser, memberNode, id, declaringType, nameInfo.Modifiers, attributes, args);
            return result;
        }

        ReflMember parseReflProp(ParseInfo typeDeclParseInfo, ReflObjType declaringType, XmlNode memberNode)
        {
            int ni0 = 0;
            XmlNode node0 = null;
            XmlTag t0 = memberNode.OpenTag;
            int pos;
            string name;
            ParseInfo memberTypeInfo;
            bool read, write;
            ParseInfo nameInfo, nameInfoSet;
            string modifiers, setModifiers;
            int id;

            pos = 0;
            name = (string)t0.attributes["n"];
            pos = 0;
            memberTypeInfo = decodeTypeCode(typeDeclParseInfo, (string)t0.attributes["t"], ref pos);
            memberTypeInfo.ParseNode = memberNode;
            read = "True".Equals(t0.attributes["r"]);
            write = "True".Equals(t0.attributes["w"]);
            modifiers = (string)t0.attributes["mod"];
            id = Convert.ToInt32(t0.attributes["id"]);
            setModifiers = null;
            object smod;
            t0.attributes.TryGetValue("set-mod", out smod);
            setModifiers = (string)smod;
            nameInfo = new ParseInfo(typeDeclParseInfo);
            nameInfoSet = new ParseInfo(typeDeclParseInfo);
            pos = 0;
            decodeModifiers(nameInfo, modifiers, ref pos);
            pos = 0;
            decodeModifiers(nameInfoSet, setModifiers, ref pos);
            ni0 = 0;
            if (!read && !write)
            {
                throw new NotSupportedException();
            }

            parseAttributes(nameInfo, memberNode, ref ni0, ref node0, ref t0);
            ReflAttribute[] attributes = parseReflAttributes(nameInfo);

            ReflMethodArgDefinition[] args = parseReflMethodParameters(typeDeclParseInfo, memberNode, ref ni0, ref node0, ref t0);
            ReflType memberType = Resolve(memberTypeInfo, false);
            ReflMember result;
            if (args.Length == 0)
            {
                result = new ReflProperty(this, declaringType.Parser, memberNode, id, declaringType, memberType, read ? write ? ReflPropertyMethodType.GetSet : ReflPropertyMethodType.Get : ReflPropertyMethodType.Set, name, nameInfo.Modifiers, nameInfoSet.Modifiers, attributes);
            }
            else
            {
                result = new ReflIndexerProperty(this, declaringType.Parser, memberNode, id, declaringType, memberType, read ? write ? ReflPropertyMethodType.GetSet : ReflPropertyMethodType.Get : ReflPropertyMethodType.Set, name, nameInfo.Modifiers, nameInfoSet.Modifiers, attributes, args);
            }
            return result;
        }

        ReflField parseReflField(ParseInfo typeDeclParseInfo, ReflObjType declaringType, XmlNode memberNode)
        {
            int ni0 = 0;
            XmlNode node0 = null;
            XmlTag t0 = memberNode.OpenTag;
            int pos;
            string name;
            ParseInfo memberTypeInfo;
            string constant;
            ParseInfo nameInfo;
            string modifiers;
            int id;

            name = (string)t0.attributes["n"];
            object cobj;
            t0.attributes.TryGetValue("const", out cobj);
            constant = (string)cobj;
            pos = 0;
            memberTypeInfo = decodeTypeCode(typeDeclParseInfo, (string)t0.attributes["t"], ref pos);
            memberTypeInfo.ParseNode = memberNode;
            modifiers = (string)t0.attributes["mod"];
            id = Convert.ToInt32(t0.attributes["id"]);
            pos = 0;
            nameInfo = new ParseInfo(typeDeclParseInfo);
            decodeModifiers(nameInfo, modifiers, ref pos);
            ni0 = 0;

            parseAttributes(nameInfo, memberNode, ref ni0, ref node0, ref t0);
            ReflAttribute[] attributes = parseReflAttributes(nameInfo);

            ReflType memberType = Resolve(memberTypeInfo, false);
            ReflField result;
            if (constant == null)
            {
                result = new ReflField(this, declaringType.Parser, memberNode, id, declaringType, memberType, name, nameInfo.Modifiers, attributes);
            }
            else
            {
                result = new ReflField(this, declaringType.Parser, memberNode, id, declaringType, memberType, name, constant, nameInfo.Modifiers, attributes);
            }
            return result;
        }

        ReflEvent parseReflEvent(ParseInfo typeDeclParseInfo, ReflObjType declaringType, XmlNode memberNode)
        {
            int ni0 = 0;
            XmlNode node0 = null;
            XmlTag t0 = memberNode.OpenTag;
            int pos;
            string name;
            ParseInfo memberTypeInfo;
            ParseInfo nameInfo;
            string modifiers;
            int id;

            pos = 0;
            name = (string)t0.attributes["n"];
            pos = 0;
            memberTypeInfo = decodeTypeCode(typeDeclParseInfo, (string)t0.attributes["t"], ref pos);
            memberTypeInfo.ParseNode = memberNode;
            modifiers = (string)t0.attributes["mod"];
            id = Convert.ToInt32(t0.attributes["id"]);
            pos = 0;
            nameInfo = new ParseInfo(typeDeclParseInfo);
            decodeModifiers(nameInfo, modifiers, ref pos);
            ni0 = 0;

            parseAttributes(nameInfo, memberNode, ref ni0, ref node0, ref t0);
            ReflAttribute[] attributes = parseReflAttributes(nameInfo);

            ReflType memberType = Resolve(memberTypeInfo, false);
            ReflEvent result = new ReflEvent(this, declaringType.Parser, memberNode, id, declaringType, (ReflDelegateType)memberType, name, nameInfo.Modifiers, attributes);
            return result;
        }

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

        ReflMethodArgDefinition[] parseReflMethodParameters(ParseInfo parentDef, XmlNode methodNode, ref int ni0, ref XmlNode node0, ref XmlTag t0)
        {
            int pos;
            List<ReflMethodArgDefinition> result = new List<ReflMethodArgDefinition>();
            for (; ok_child(methodNode, ref ni0, ref node0, ref t0, "param"); )
            {
                pos = 0;
                ParseInfo ptypeInfo = decodeTypeCode(parentDef, (string)t0.attributes["t"], ref pos);
                ReflType paramType = Resolve(ptypeInfo, false);
                string name = (string)t0.attributes["n"];

                object ptobj;
                t0.attributes.TryGetValue("p", out ptobj);
                string pt = (string)ptobj;
                if (pt == null)
                {
                    pt = "Value";
                }

                int ni = 0;
                XmlNode node = null;
                XmlTag t = null;
                parseAttributes(ptypeInfo, node0, ref ni, ref node, ref t);
                ReflAttribute[] attributes = parseReflAttributes(ptypeInfo);

                ReflMethodArgType argType = (ReflMethodArgType)Enum.Parse(typeof(ReflMethodArgType), pt);
                ReflMethodArgDefinition argDef = new ReflMethodArgDefinition(this, methodNode.Parser, node0, paramType, name, argType, attributes);
                result.Add(argDef);
            }
            return result.ToArray();
        }


        bool ok_child(XmlNode parentNode, ref int ni, ref XmlNode node, ref XmlTag tag, params string[] supportedTagNames)
        {
            return ok_child(parentNode, ref ni, ref node, ref tag, new SortedSet<string>(supportedTagNames));
        }

        bool ok_child(XmlNode parentNode, ref int ni, ref XmlNode node, ref XmlTag tag, ISet<string> supportedTagNames)
        {
            bool result;
            if (ni < parentNode.children.Count)
            {
                node = parentNode.children[ni];
                tag = node.OpenTag;
                result = supportedTagNames.Contains(tag.Name);
                if (result)
                {
                    ni++;
                }
            }
            else
            {
                node = null;
                tag = null;
                result = false;
            }
            return result;
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