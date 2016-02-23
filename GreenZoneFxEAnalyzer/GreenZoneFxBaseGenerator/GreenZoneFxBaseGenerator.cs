using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Reflection;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.ViewController;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using GreenZoneFxEngine.Trading;
using GreenZoneParser;
using GreenZoneParser.Parsers.Cs;
using GreenZoneParser.Reflect;
using GreenZoneFxEngine.Types;

namespace GreenZoneFxBaseGenerator
{
    public class GreenZoneFxBaseGenerator
    {
        const string NEWLINE = "\r\n";
        const string GEN_FOLDER = "F:/workspaces/general_web/ForexRobots/windows_dll/GreenZoneFxEAnalyzer/GreenZoneFxBaseGEx/generated/";
        const string EX_BASE_FOLDER = "F:/workspaces/general_web/ForexRobots/windows_dll/GreenZoneFxEAnalyzer/GreenZoneFxBaseGEx/GreenZoneFxEngine/";

        const string tab1 = "\t";
        const string tab2 = "\t\t";
        const string tab3 = "\t\t\t";
        const string tab4 = "\t\t\t\t";
        const string tab5 = "\t\t\t\t\t";
        const string tab6 = "\t\t\t\t\t\t";

        class BuildInfo
        {
            internal Type rmiInterface;

            internal StringBuilder
                Props_header = new StringBuilder(),
                Props_propertyConstants = new StringBuilder(),
                Props_rmiGet = new StringBuilder(),
                Props_rmiSet = new StringBuilder(),
                Props_init = new StringBuilder(),
                Props_deps = new StringBuilder(),
                Props_serial_r = new StringBuilder(),
                Props_serial_w = new StringBuilder(),
                Bean_header = new StringBuilder(),
                Bean_constructors = new StringBuilder(),
                Bean_methods = new StringBuilder(),
                Bean_events = new StringBuilder(),
                Bean_serial_r = new StringBuilder(),
                Bean_serial_w = new StringBuilder(),
                Bean_rmiGet = new StringBuilder(),
                Bean_rmiSet = new StringBuilder()
                ;
            internal Dictionary<string, StringBuilder> Bean_properties = new Dictionary<string, StringBuilder>();
            internal Dictionary<string, StringBuilder> Bean_properties_Qname = new Dictionary<string, StringBuilder>();

            internal Dictionary<string, StringBuilder> Bean_properties_Gen;
            internal Dictionary<string, StringBuilder> Bean_properties_Qname_Gen;
            internal Dictionary<string, string> Bean_properties_Map_Gen;

            internal SortedDictionary<int, string> Props_propertyConstants_All = new SortedDictionary<int, string>();
            internal SortedSet<string> Overridden_Parent_Properties = new SortedSet<string>();
            internal int lastPropertyId;
            internal SortedSet<string> usedNamespaces = new SortedSet<string>();
            internal Type parentType;
            internal BuildInfo parent;
            internal List<BuildInfo> additionalParents;
            internal List<BuildInfo> allParents;
            internal ConstructorInfo[] baseConstructors;
            internal SourceInfo customBaseEx;
        }

        class SourceInfo
        {
            internal BlockNode classDefBlock;
        }

        public static void Generate()
        {
            Assembly asm = Assembly.GetAssembly(typeof(IMainWindowController));
            List<Type> interfaces = GreenZoneUtils.GetNamespaceClasses(asm, null, typeof(GreenRmiAttribute));

            Dictionary<string, BuildInfo> buildInfos = new Dictionary<string, BuildInfo>();
            Dictionary<string, SourceInfo> sourceInfos = new Dictionary<string, SourceInfo>();

            foreach (var i in interfaces)
            {
                build(asm, i, buildInfos, sourceInfos);
            }

        }


        static void build(Assembly asm, Type rmiInterface, Dictionary<string, BuildInfo> buildInfos, Dictionary<string, SourceInfo> sourceInfos)
        {
            if (buildInfos.ContainsKey(PrettyFullName(rmiInterface)))
            {
                return;
            }

            Console.WriteLine("Processing " + PrettyFullName(rmiInterface));
            if (rmiInterface.IsInterface)
            {
                Type[] rmiGenArgs = rmiInterface.GetGenericArguments();
                if (rmiGenArgs != null && rmiGenArgs.Length > 0)
                {
                    Console.WriteLine("ERROR Generic interface is not supported!");
                }
                else
                {
                    int parentLastPropertyId;

                    string iName = PrettyName(rmiInterface);

                    var baseInterfaces = PureReflResolver.GetBaseInterfaces(rmiInterface);

                    BuildInfo bi = new BuildInfo();
                    bi.rmiInterface = rmiInterface;

                    Type[] baseis = baseInterfaces.ToArray();
                    if (baseis.Length < 1)
                    {
                        throw new NotSupportedException("Unsupported number of base interfaces : " + baseis.Length + " of type:" + PrettyFullName(rmiInterface));
                    }
                    Type baseInterface = baseis[0];
                    AddTypeNamespace(bi, baseInterface);
                    string baseIName = PrettyName(baseInterface);
                    string baseCName;

                    parentLastPropertyId = 0;

                    bi.additionalParents = new List<BuildInfo>();
                    int bii = 0;
                    foreach (var basei in baseis)
                    {
                        if (basei.Assembly == asm && typeof(IRmiBase).IsAssignableFrom(basei) && basei != typeof(ITradingConst))
                        {
                            build(asm, basei, buildInfos, sourceInfos);
                            BuildInfo pbi = buildInfos[PrettyFullName(basei)];
                            parentLastPropertyId = Math.Max(parentLastPropertyId, pbi.lastPropertyId);
                            if (bii != 0)
                            {
                                bi.additionalParents.Add(pbi);
                            }
                        }
                        bii++;
                    }

                    if (baseInterface.Assembly == asm && baseInterface != typeof(ITradingConst))
                    {
                        baseCName = baseIName.Substring(1) + "Base";

                        bi.parent = buildInfos[PrettyFullName(baseInterface)];
                        bi.parentType = baseInterface;
                        bi.baseConstructors = bi.parent.baseConstructors;
                        bi.customBaseEx = bi.parent.customBaseEx;
                    }
                    else
                    {
                        bi.parent = null;
                        baseCName = baseIName.Substring(1);

                        IEnumerable<Type> types0 =
                            from a in AppDomain.CurrentDomain.GetAssemblies()
                            from t in a.GetTypes()
                            where t.FullName == baseInterface.Namespace + "." + baseCName
                            select t;
                        Type[] types = types0.ToArray();
                        if (types.Length != 1)
                        {
                            throw new NotSupportedException();
                        }

                        bi.parentType = types[0];
                        bi.baseConstructors = bi.parentType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                        switch (baseIName)
                        {
                            case "ITabPageController":
                            case "ILabelledController":
                                parentLastPropertyId = 15;
                                break;
                            case "IController":
                                parentLastPropertyId = 13;
                                break;
                            case "IClientController":
                                parentLastPropertyId = 14;
                                break;
                            case "IFormController":
                            case "IDialogController":
                                parentLastPropertyId = 21;
                                break;
                            case "IAssistantPageController":
                                parentLastPropertyId = 16;
                                break;
                            case "IAssistantFormController":
                                parentLastPropertyId = 26;
                                break;
                            case "ITabController":
                                parentLastPropertyId = 14;
                                break;
                            case "IGridController":
                                parentLastPropertyId = 19;
                                break;
                            case "IPropertyPanelController":
                                parentLastPropertyId = 14;
                                break;
                            case "IRmiBase":
                                parentLastPropertyId = 0;
                                break;
                            case "ITradingConst":
                                parentLastPropertyId = 0;
                                break;

                            default:
                                throw new NotSupportedException(baseIName);
                        }
                    }

                    object[] attrs0 = rmiInterface.GetCustomAttributes(typeof(GreenRmiAttribute), false);
                    GreenRmiAttribute attr0 = (GreenRmiAttribute)attrs0[0];
                    if (attr0.BaseClass != null)
                    {
                        baseCName = attr0.BaseClass;
                        SourceInfo si;
                        if (!sourceInfos.TryGetValue(baseCName, out si))
                        {
                            string baseCFile;
                            if (rmiInterface.Namespace == "GreenZoneFxEngine.ViewController.Chart")
                            {
                                if (baseCName == "ChartPaneControllerEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "ViewController/ChartPaneController.cs";
                                }
                                else
                                {
                                    if (!baseCName.EndsWith("Ex"))
                                    {
                                        throw new NotSupportedException();
                                    }
                                    if (rmiInterface.Name.StartsWith("IClient"))
                                    {
                                        baseCFile = EX_BASE_FOLDER + "Client/ViewController/Chart/" + baseCName.Substring("Client".Length, baseCName.Length - "Client".Length - 2) + ".cs";
                                    }
                                    else if (rmiInterface.Name.StartsWith("IServer"))
                                    {
                                        baseCFile = EX_BASE_FOLDER + "Server/ViewController/Chart/" + baseCName.Substring("Server".Length, baseCName.Length - "Server".Length - 2) + ".cs";
                                    }
                                    else
                                    {
                                        throw new NotSupportedException();
                                    }
                                }
                            }
                            else if (rmiInterface.Namespace == "GreenZoneFxEngine.Trading")
                            {
                                if (baseCName == "ChartGroupRuntimeEx" || baseCName == "ChartRuntimeEx" || baseCName == "ChartCursorRuntimeEx" || baseCName == "EnvironmentRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/RuntimeEx.cs";
                                }
                                else if (baseCName == "ExecRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/ExecRuntimeEx.cs";
                                }
                                else if (baseCName == "UserRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/UserRuntimeEx.cs";
                                }
                                else if (baseCName == "SymbolRuntimeEx" || baseCName == "SymbolContextEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/SymbolRuntimeEx.cs";
                                }
                                else if (baseCName == "OrderEx" || baseCName == "HistoryOrderEx" || baseCName == "OrdersHistoryViewEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/SymbolSessionEx.cs";
                                }
                                else if (baseCName == "SeriesManagerRuntimeEx" || baseCName == "SeriesManagerCacheEx" || baseCName == "NormalSeriesManagerCacheEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/SeriesManagerEx.cs";
                                }
                                else if (baseCName == "TimeSeriesRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Trading/TimeSeriesRuntimeEx.cs";
                                }
                                else if (baseCName == "ServerTimeSeriesRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Server/Trading/TimeSeriesRuntime.cs";
                                }
                                else if (baseCName == "ServerNormalSeriesManagerCacheEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Server/Trading/SeriesManagerRuntime.cs";
                                }
                                else if (baseCName == "ServerUserRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Server/Trading/UserRuntimeEx.cs";
                                }
                                else if (baseCName == "ServerExecRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Server/Trading/ExecRuntimeEx.cs";
                                }
                                else if (baseCName == "ClientUserRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Client/Trading/UserRuntimeEx.cs";
                                }
                                else if (baseCName == "ClientExecRuntimeEx")
                                {
                                    baseCFile = EX_BASE_FOLDER + "Client/Trading/ExecRuntimeEx.cs";
                                }
                                else
                                {
                                    throw new NotSupportedException();
                                }
                            }
                            else
                            {
                                throw new NotSupportedException();
                            }

                            Console.WriteLine("Parsing Ex source : " + baseCFile);
                            CsParser parser = new CsParser(baseCFile, File.ReadAllText(baseCFile));

                            si = new SourceInfo();

                            //bi.customBaseEx.source.

                            MainBlockNode source = (MainBlockNode)parser.Parse();
                            foreach (var controlNode0 in source.ControlNodes)
                            {
                                if (controlNode0 is BlockNode)
                                {
                                    BlockNode blockNode0 = (BlockNode)controlNode0;
                                    if (blockNode0.HeadStatement is NamespaceDefStatementNode)
                                    {
                                        NamespaceDefStatementNode nsDefNode = (NamespaceDefStatementNode)blockNode0.HeadStatement;
                                        if (rmiInterface.Namespace.Equals(nsDefNode.NamespaceId))
                                        {
                                            foreach (var controlNode in blockNode0.ControlNodes)
                                            {
                                                if (controlNode is BlockNode)
                                                {
                                                    BlockNode blockNode = (BlockNode)controlNode;
                                                    if (blockNode.HeadStatement is ClassDefStatementNode)
                                                    {
                                                        ClassDefStatementNode classDefNode = (ClassDefStatementNode)blockNode.HeadStatement;
                                                        if (classDefNode.TypeName.Equals(baseCName))
                                                        {
                                                            si.classDefBlock = blockNode;
                                                            bi.customBaseEx = si;
                                                            Console.WriteLine("Found type :: " + baseCName);
                                                            goto breakall;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            throw new NotSupportedException();
                                        }
                                    }
                                }
                            }

                        breakall: foreach (var err in parser.CompilationErrors)
                            {
                                Console.WriteLine("ERROR in " + baseCName + ":  " + err.Message + "  Line:" + err.Line + "  Column:" + err.Column + "  Length:" + err.Length);
                            }

                        }
                    }


                    string cName = iName.Substring(1);
                    StringBuilder bi_Bean_properties_All;
                    StringBuilder bi_Bean_properties;
                    StringBuilder bi_Bean_properties_Qname;


                    bi.Props_header.Append(NEWLINE);
                    bi.Props_header.Append("namespace ");
                    bi.Props_header.Append(rmiInterface.Namespace);
                    bi.Props_header.Append(NEWLINE);
                    bi.Props_header.Append("{");
                    bi.Props_header.Append(NEWLINE);
                    bi.Props_header.Append(tab1);
                    bi.Props_header.Append("public static class ");
                    bi.Props_header.Append(cName);
                    bi.Props_header.Append("Props");
                    bi.Props_header.Append(NEWLINE);
                    bi.Props_header.Append(tab1);
                    bi.Props_header.Append("{");
                    bi.Props_header.Append(NEWLINE);

                    bi.Bean_header.Append(tab1);
                    bi.Bean_header.Append("public abstract class ");
                    bi.Bean_header.Append(cName);
                    bi.Bean_header.Append("Base : ");
                    bi.Bean_header.Append(baseCName);
                    bi.Bean_header.Append(", ");
                    bi.Bean_header.Append(iName);
                    bi.Bean_header.Append(NEWLINE);
                    bi.Bean_header.Append(tab1);
                    bi.Bean_header.Append("{");
                    bi.Bean_header.Append(NEWLINE);
                    bi.Bean_header.Append(NEWLINE);
                    bi.Bean_header.Append(tab2);
                    bi.Bean_header.Append("bool ___initialized = false;");
                    bi.Bean_header.Append(NEWLINE);
                    bi.Bean_header.Append(NEWLINE);

                    object[] custinss = rmiInterface.GetCustomAttributes(typeof(GreenRmiInsertBodyAttribute), false);
                    foreach (GreenRmiInsertBodyAttribute ci in custinss)
                    {
                        bi.Bean_header.Append(tab2);
                        bi.Bean_header.Append(ci.Definition);
                        bi.Bean_header.Append(NEWLINE);
                    }

                    if (custinss.Length > 0)
                    {
                        bi.Bean_header.Append(NEWLINE);
                    }


                    if (bi.customBaseEx != null)
                    {
                        foreach (var controlNode in bi.customBaseEx.classDefBlock.ControlNodes)
                        {
                            if (controlNode is BlockNode)
                            {
                                BlockNode blockNode = (BlockNode)controlNode;
                                if (blockNode.HeadStatement is ConstructorDefStatementNode)
                                {
                                    ConstructorDefStatementNode consDefNode = (ConstructorDefStatementNode)blockNode.HeadStatement;
                                    if (consDefNode.ArgumentList.Count != 2)
                                    {
                                        GenerateCosntructor(bi, bi.Bean_constructors, blockNode, consDefNode, cName);
                                    }
                                    else
                                    {
                                        var arg1 = consDefNode.ArgumentList[0];
                                        var arg2 = consDefNode.ArgumentList[1];
                                        string type1 = arg1.Right.TypeSpecifier.Name;
                                        string type2 = arg2.Right.TypeSpecifier.Name;
                                        if (type1.Equals("GreenRmiManager") &&
                                            type2.Equals("GreenRmiObjectBuffer"))
                                        {
                                            // Nothing to do
                                        }
                                        else if (type1.Equals("SerializationInfo") &&
                                            type2.Equals("StreamingContext"))
                                        {
                                            // Nothing to do
                                        }
                                        else
                                        {
                                            GenerateCosntructor(bi, bi.Bean_constructors, blockNode, consDefNode, cName);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ConstructorInfo ciBuf = null, ciSer = null;
                    foreach (var c in bi.baseConstructors)
                    {
                        ParameterInfo[] args = c.GetParameters();
                        if (c.IsPublic || c.IsFamily)
                        {
                            if (args.Length == 2)
                            {
                                if (args[0].ParameterType.Equals(typeof(GreenRmiManager)) &&
                                    args[1].ParameterType.Equals(typeof(GreenRmiObjectBuffer)))
                                {
                                    ciBuf = c;
                                }
                                else if (args[0].ParameterType.Equals(typeof(SerializationInfo)) &&
                                    args[1].ParameterType.Equals(typeof(StreamingContext)))
                                {
                                    ciSer = c;
                                }
                                else if (bi.customBaseEx == null)
                                {
                                    GenerateCosntructor(bi, bi.Bean_constructors, c, cName);
                                }
                            }
                            else if (bi.customBaseEx == null)
                            {
                                GenerateCosntructor(bi, bi.Bean_constructors, c, cName);
                            }
                        }
                    }
                    if (ciBuf == null)
                    {
                        throw new NotSupportedException();
                    }
                    if (ciSer == null)
                    {
                        throw new NotSupportedException();
                    }
                    GenerateCosntructorHeader(bi, bi.Bean_constructors, ciBuf, cName);
                    GenerateCosntructorHeader(bi, bi.Bean_serial_r, ciSer, cName);

                    bi.Bean_constructors.Append(tab3);
                    bi.Bean_constructors.Append(cName);
                    bi.Bean_constructors.Append("Props.Initialize(this, buffer, false);");
                    bi.Bean_constructors.Append(NEWLINE);

                    bi.Bean_serial_r.Append(tab3);
                    bi.Bean_serial_r.Append(cName);
                    bi.Bean_serial_r.Append("Props.SerializationRead(this, info, context, false);");
                    bi.Bean_serial_r.Append(NEWLINE);

                    bi.Bean_serial_w.Append(tab2);
                    bi.Bean_serial_w.Append("public override void GetObjectData(SerializationInfo info, StreamingContext context)");
                    bi.Bean_serial_w.Append(NEWLINE);
                    bi.Bean_serial_w.Append(tab2);
                    bi.Bean_serial_w.Append("{");
                    bi.Bean_serial_w.Append(NEWLINE);
                    bi.Bean_serial_w.Append(tab3);
                    bi.Bean_serial_w.Append("base.GetObjectData(info, context);");
                    bi.Bean_serial_w.Append(NEWLINE);

                    bi.Bean_serial_w.Append(tab3);
                    bi.Bean_serial_w.Append(cName);
                    bi.Bean_serial_w.Append("Props.SerializationWrite(this, info, context, false);");
                    bi.Bean_serial_w.Append(NEWLINE);

                    bi.Bean_serial_w.Append(tab2);
                    bi.Bean_serial_w.Append("}");
                    bi.Bean_serial_w.Append(NEWLINE);
                    bi.Bean_serial_w.Append(NEWLINE);

                    bi.Props_serial_r.Append(tab2);
                    bi.Props_serial_r.Append("public static void SerializationRead(");
                    bi.Props_serial_r.Append(iName);
                    bi.Props_serial_r.Append(" controller, SerializationInfo info, StreamingContext context, bool goToParent)");
                    bi.Props_serial_r.Append(NEWLINE);
                    bi.Props_serial_r.Append(tab2);
                    bi.Props_serial_r.Append("{");
                    bi.Props_serial_r.Append(NEWLINE);

                    if (bi.parent != null)
                    {
                        bi.Props_serial_r.Append(tab3);
                        bi.Props_serial_r.Append("if (goToParent) {");
                        bi.Props_serial_r.Append(NEWLINE);
                        GenerateSerializationRead(bi, bi.parent, tab4);
                        bi.Props_serial_r.Append(tab3);
                        bi.Props_serial_r.Append("}");
                        bi.Props_serial_r.Append(NEWLINE);
                    }

                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        GenerateSerializationRead(bi, apbi, tab3);
                    }

                    bi.Props_serial_w.Append(tab2);
                    bi.Props_serial_w.Append("public static void SerializationWrite(");
                    bi.Props_serial_w.Append(iName);
                    bi.Props_serial_w.Append(" controller, SerializationInfo info, StreamingContext context, bool goToParent)");
                    bi.Props_serial_w.Append(NEWLINE);
                    bi.Props_serial_w.Append(tab2);
                    bi.Props_serial_w.Append("{");
                    bi.Props_serial_w.Append(NEWLINE);

                    if (bi.parent != null)
                    {
                        bi.Props_serial_w.Append(tab3);
                        bi.Props_serial_w.Append("if (goToParent) {");
                        bi.Props_serial_w.Append(NEWLINE);
                        GenerateSerializationWrite(bi, bi.parent, tab4);
                        bi.Props_serial_w.Append(tab3);
                        bi.Props_serial_w.Append("}");
                        bi.Props_serial_w.Append(NEWLINE);
                    }

                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        GenerateSerializationWrite(bi, apbi, tab3);
                    }

                    AddTypeNamespace(bi, typeof(GreenRmiManager));
                    AddTypeNamespace(bi, typeof(SerializationInfo));
                    AddTypeNamespace(bi, typeof(StreamingContext));


                    bi.Props_rmiGet.Append(tab2);
                    bi.Props_rmiGet.Append("public static bool RmiGetProperty(");
                    bi.Props_rmiGet.Append(iName);
                    bi.Props_rmiGet.Append(" controller, int propertyId, out object value, bool goToParent)");
                    bi.Props_rmiGet.Append(NEWLINE);
                    bi.Props_rmiGet.Append(tab2);
                    bi.Props_rmiGet.Append("{");
                    bi.Props_rmiGet.Append(NEWLINE);

                    if (bi.parent != null)
                    {
                        bi.Props_rmiGet.Append(tab3);
                        bi.Props_rmiGet.Append("if (goToParent) {");
                        bi.Props_rmiGet.Append(NEWLINE);
                        GenerateRmiGet(bi, bi.parent, tab4);
                        bi.Props_rmiGet.Append(tab3);
                        bi.Props_rmiGet.Append("}");
                        bi.Props_rmiGet.Append(NEWLINE);
                    }

                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        GenerateRmiGet(bi, apbi, tab3);
                    }
                    bi.Props_rmiGet.Append(tab3);
                    bi.Props_rmiGet.Append("switch (propertyId)");
                    bi.Props_rmiGet.Append(NEWLINE);
                    bi.Props_rmiGet.Append(tab3);
                    bi.Props_rmiGet.Append("{");
                    bi.Props_rmiGet.Append(NEWLINE);

                    bi.Props_rmiSet.Append(tab2);
                    bi.Props_rmiSet.Append("public static bool RmiSetProperty(");
                    bi.Props_rmiSet.Append(iName);
                    bi.Props_rmiSet.Append(" controller, int propertyId, object value, bool goToParent)");
                    bi.Props_rmiSet.Append(NEWLINE);
                    bi.Props_rmiSet.Append(tab2);
                    bi.Props_rmiSet.Append("{");
                    bi.Props_rmiSet.Append(NEWLINE);

                    if (bi.parent != null)
                    {
                        bi.Props_rmiSet.Append(tab3);
                        bi.Props_rmiSet.Append("if (goToParent) {");
                        bi.Props_rmiSet.Append(NEWLINE);
                        GenerateRmiSet(bi, bi.parent, tab4);
                        bi.Props_rmiSet.Append(tab3);
                        bi.Props_rmiSet.Append("}");
                        bi.Props_rmiSet.Append(NEWLINE);
                    }

                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        GenerateRmiSet(bi, apbi, tab3);
                    }
                    bi.Props_rmiSet.Append(tab3);
                    bi.Props_rmiSet.Append("switch (propertyId)");
                    bi.Props_rmiSet.Append(NEWLINE);
                    bi.Props_rmiSet.Append(tab3);
                    bi.Props_rmiSet.Append("{");
                    bi.Props_rmiSet.Append(NEWLINE);

                    bi.Props_init.Append(tab2);
                    bi.Props_init.Append("public static void Initialize(");
                    bi.Props_init.Append(iName);
                    bi.Props_init.Append(" controller, GreenRmiObjectBuffer buffer, bool goToParent)");
                    bi.Props_init.Append(NEWLINE);
                    bi.Props_init.Append(tab2);
                    bi.Props_init.Append("{");
                    bi.Props_init.Append(NEWLINE);

                    if (bi.parent != null)
                    {
                        bi.Props_init.Append(tab3);
                        bi.Props_init.Append("if (goToParent) {");
                        bi.Props_init.Append(NEWLINE);
                        GenerateInitialize(bi, bi.parent, tab4);
                        bi.Props_init.Append(tab3);
                        bi.Props_init.Append("}");
                        bi.Props_init.Append(NEWLINE);
                    }

                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        GenerateInitialize(bi, apbi, tab3);
                    }

                    bi.Props_deps.Append(tab2);
                    bi.Props_deps.Append("public static void AddDependencies(");
                    bi.Props_deps.Append(iName);
                    bi.Props_deps.Append(" controller, bool goToParent)");
                    bi.Props_deps.Append(NEWLINE);
                    bi.Props_deps.Append(tab2);
                    bi.Props_deps.Append("{");
                    bi.Props_deps.Append(NEWLINE);

                    if (bi.parent != null)
                    {
                        bi.Props_deps.Append(tab3);
                        bi.Props_deps.Append("if (goToParent) {");
                        bi.Props_deps.Append(NEWLINE);
                        GenerateAddDependencies(bi, bi.parent, tab4);
                        bi.Props_deps.Append(tab3);
                        bi.Props_deps.Append("}");
                        bi.Props_deps.Append(NEWLINE);
                    }

                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        GenerateAddDependencies(bi, apbi, tab3);
                    }

                    bi.Bean_rmiGet.Append(tab2);
                    bi.Bean_rmiGet.Append("public override object RmiGetProperty(int propertyId)");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab2);
                    bi.Bean_rmiGet.Append("{");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab3);
                    bi.Bean_rmiGet.Append("object value;");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab3);
                    bi.Bean_rmiGet.Append("if (");
                    bi.Bean_rmiGet.Append(cName);
                    bi.Bean_rmiGet.Append("Props.RmiGetProperty(this, propertyId, out value, false))");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab4);
                    bi.Bean_rmiGet.Append("return value;");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab3);
                    bi.Bean_rmiGet.Append("else");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab4);
                    bi.Bean_rmiGet.Append("return base.RmiGetProperty(propertyId);");
                    bi.Bean_rmiGet.Append(NEWLINE);
                    bi.Bean_rmiGet.Append(tab2);
                    bi.Bean_rmiGet.Append("}");
                    bi.Bean_rmiGet.Append(NEWLINE);

                    bi.Bean_rmiSet.Append(tab2);
                    bi.Bean_rmiSet.Append("public override void RmiSetProperty(int propertyId, object value)");
                    bi.Bean_rmiSet.Append(NEWLINE);
                    bi.Bean_rmiSet.Append(tab2);
                    bi.Bean_rmiSet.Append("{");
                    bi.Bean_rmiSet.Append(NEWLINE);
                    bi.Bean_rmiSet.Append(tab3);
                    bi.Bean_rmiSet.Append("if (");
                    bi.Bean_rmiSet.Append(cName);
                    bi.Bean_rmiSet.Append("Props.RmiSetProperty(this, propertyId, value, false))");
                    bi.Bean_rmiSet.Append(NEWLINE);
                    bi.Bean_rmiSet.Append(tab4);
                    bi.Bean_rmiSet.Append("return;");
                    bi.Bean_rmiSet.Append(NEWLINE);
                    bi.Bean_rmiSet.Append(tab3);
                    bi.Bean_rmiSet.Append("else ");
                    bi.Bean_rmiSet.Append(NEWLINE);
                    bi.Bean_rmiSet.Append(tab4);
                    bi.Bean_rmiSet.Append("base.RmiSetProperty(propertyId, value);");
                    bi.Bean_rmiSet.Append(NEWLINE);
                    bi.Bean_rmiSet.Append(tab2);
                    bi.Bean_rmiSet.Append("}");
                    bi.Bean_rmiSet.Append(NEWLINE);

                    MethodInfo[] ms = PureReflResolver.GetTopLevelMethods(rmiInterface);
                    foreach (var m in ms)
                    {
                        bool isPropertyAccessor;
                        if (m.IsSpecialName && (m.Name.StartsWith("set_") || m.Name.StartsWith("get_")))
                        {
                            try
                            {
                                var prop = rmiInterface.GetProperty(m.Name.Substring(4),
                                       BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                                var accessor_get = prop.GetGetMethod(true);
                                var accessor_set = prop.GetSetMethod(true);
                                isPropertyAccessor = accessor_get == m || accessor_set == m;
                            }
                            catch (AmbiguousMatchException)
                            {
                                isPropertyAccessor = true;
                            }
                        }
                        else
                        {
                            isPropertyAccessor = false;
                        }
                        if (!isPropertyAccessor)
                        {
                            object[] defAttrs = m.GetCustomAttributes(typeof(GreenRmiMethodAttribute), false);
                            string def;
                            if (defAttrs.Length > 0)
                            {
                                def = ((GreenRmiMethodAttribute)defAttrs[0]).Definition;
                            }
                            else
                            {
                                def = null;
                            }

                            bi.Bean_methods.Append(tab2);
                            bi.Bean_methods.Append("public ");
                            if (def == null)
                            {
                                bi.Bean_methods.Append("abstract ");
                            }
                            bi.Bean_methods.Append(PrettyName(m.ReturnType));
                            bi.Bean_methods.Append(" ");
                            bi.Bean_methods.Append(m.Name);

                            Type[] gs = m.GetGenericArguments();
                            if (gs.Length > 0)
                            {
                                bi.Bean_methods.Append("<");
                            }
                            for (int i = 0; i < gs.Length - 1; i++)
                            {
                                bi.Bean_methods.Append(PrettyName(gs[i]));
                                bi.Bean_methods.Append(", ");
                            }
                            if (gs.Length > 0)
                            {
                                bi.Bean_methods.Append(PrettyName(gs[gs.Length - 1]));
                                bi.Bean_methods.Append(">");
                            }

                            bi.Bean_methods.Append("(");

                            AddTypeNamespace(bi, m.ReturnType);

                            ParameterInfo[] parameters = m.GetParameters();
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                var p = parameters[i];
                                if (IsParams(p))
                                {
                                    bi.Bean_methods.Append("params ");
                                }
                                bi.Bean_methods.Append(PrettyName(p.ParameterType, p.IsOut));
                                bi.Bean_methods.Append(" ");
                                bi.Bean_methods.Append(p.Name);
                                if (i < parameters.Length - 1)
                                {
                                    bi.Bean_methods.Append(", ");
                                }

                                AddTypeNamespace(bi, p.ParameterType);
                            }
                            bi.Bean_methods.Append(")");
                            for (int i = 0; i < gs.Length; i++)
                            {
                                Type[] tpConstraints = gs[i].GetGenericParameterConstraints();
                                if (tpConstraints.Length > 0)
                                {
                                    bi.Bean_methods.Append(NEWLINE);
                                    bi.Bean_methods.Append(tab3);
                                    bi.Bean_methods.Append("where ");
                                    bi.Bean_methods.Append(PrettyName(gs[i]));
                                    bi.Bean_methods.Append(" : ");
                                }
                                for (int j = 0; j < tpConstraints.Length; j++)
                                {
                                    bi.Bean_methods.Append(PrettyName(tpConstraints[j]));
                                    if (j < tpConstraints.Length - 1)
                                    {
                                        bi.Bean_methods.Append(", ");
                                    }
                                }
                            }
                            if (def == null)
                            {
                                bi.Bean_methods.Append(";");
                                bi.Bean_methods.Append(NEWLINE);
                            }
                            else
                            {
                                bi.Bean_methods.Append(NEWLINE);
                                bi.Bean_methods.Append(tab2);
                                bi.Bean_methods.Append("{");
                                bi.Bean_methods.Append(NEWLINE);
                                bi.Bean_methods.Append(tab3);
                                bi.Bean_methods.Append(def);
                                bi.Bean_methods.Append(NEWLINE);
                                bi.Bean_methods.Append(tab2);
                                bi.Bean_methods.Append("}");
                                bi.Bean_methods.Append(NEWLINE);
                            }
                            bi.Bean_methods.Append(NEWLINE);
                        }
                    }

                    bool hasReadonly = false;
                    int propertyId = parentLastPropertyId + 1;
                    PropertyInfo[] properties = PureReflResolver.GetTopLevelProperties(rmiInterface);
                    foreach (var p in properties)
                    {
                        AddTypeNamespace(bi, p.PropertyType);

                        object[] attrs = p.GetCustomAttributes(typeof(GreenRmiFieldAttribute), false);
                        GreenRmiFieldAttribute attr;
                        if (attrs.Length == 1)
                        {
                            attr = (GreenRmiFieldAttribute)attrs[0];
                        }
                        else
                        {
                            attr = new GreenRmiFieldAttribute(GreenRmiFieldType.Normal);
                        }

                        if (p.GetIndexParameters().Length > 0)
                        {
                            // Indexed property...
                            bi_Bean_properties_All = new StringBuilder();
                            bi_Bean_properties = new StringBuilder();
                            bi_Bean_properties_Qname = new StringBuilder();

                            int id = bi.Bean_properties.Count;
                            bi.Bean_properties[rmiInterface.Name + ".Indexer#" + id] = bi_Bean_properties;
                            bi.Bean_properties_Qname[rmiInterface.Name + ".Indexer#" + id] = bi_Bean_properties_Qname;

                            bi_Bean_properties.Append(tab2);
                            bi_Bean_properties.Append("public ");
                            switch (attr.Type)
                            {
                                case GreenRmiFieldType.Abstract:
                                case GreenRmiFieldType.Normal:
                                    bi_Bean_properties.Append("abstract ");
                                    break;
                                case GreenRmiFieldType.Simple:
                                case GreenRmiFieldType.Virtual:
                                    bi_Bean_properties.Append("virtual ");
                                    break;
                                default:
                                    throw new NotSupportedException();
                            }
                            bi_Bean_properties.Append(PrettyName(p.PropertyType));
                            bi_Bean_properties.Append(" ");

                            bi_Bean_properties_Qname.Append(tab2);
                            bi_Bean_properties_Qname.Append(rmiInterface.Name);
                            bi_Bean_properties_Qname.Append(".");

                            bi_Bean_properties_All.Append(" this[");

                            ParameterInfo[] parameters = p.GetIndexParameters();
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                var param = parameters[i];
                                if (IsParams(param))
                                {
                                    bi_Bean_properties_All.Append("params ");
                                }
                                bi_Bean_properties_All.Append(PrettyName(param.ParameterType, param.IsOut));
                                bi_Bean_properties_All.Append(" ");
                                bi_Bean_properties_All.Append(param.Name);
                                if (i < parameters.Length - 1)
                                {
                                    bi_Bean_properties_All.Append(", ");
                                }

                                AddTypeNamespace(bi, param.ParameterType);
                            }

                            object[] getDefAttrs = p.CanRead ? p.GetGetMethod(true).GetCustomAttributes(typeof(GreenRmiMethodAttribute), false) : null;
                            object[] setDefAttrs = p.CanWrite ? p.GetSetMethod(true).GetCustomAttributes(typeof(GreenRmiMethodAttribute), false) : null;
                            string gdef, sdef;
                            if (attr.Definition != null)
                            {
                                if (p.CanRead && p.CanWrite)
                                {
                                    throw new NotSupportedException();
                                }
                                if (getDefAttrs != null && getDefAttrs.Length != 0)
                                {
                                    throw new NotSupportedException();
                                }
                                if (setDefAttrs != null && setDefAttrs.Length != 0)
                                {
                                    throw new NotSupportedException();
                                }
                                gdef = sdef = attr.Definition;
                            }
                            else
                            {
                                if (getDefAttrs != null && getDefAttrs.Length != 0)
                                {
                                    gdef = ((GreenRmiMethodAttribute)getDefAttrs[0]).Definition;
                                }
                                else
                                {
                                    gdef = null;
                                }
                                if (setDefAttrs != null && setDefAttrs.Length != 0)
                                {
                                    sdef = ((GreenRmiMethodAttribute)setDefAttrs[0]).Definition;
                                }
                                else
                                {
                                    sdef = null;
                                }
                            }

                            bi_Bean_properties_All.Append("]");
                            bi_Bean_properties_All.Append(NEWLINE);
                            bi_Bean_properties_All.Append(tab2);
                            bi_Bean_properties_All.Append("{");
                            bi_Bean_properties_All.Append(NEWLINE);
                            if (p.CanRead)
                            {
                                bi_Bean_properties_All.Append(tab3);
                                bi_Bean_properties_All.Append("get");
                                if (gdef == null)
                                {
                                    bi_Bean_properties_All.Append(";");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                }
                                else
                                {
                                    bi_Bean_properties_All.Append(" {");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab4);
                                    bi_Bean_properties_All.Append(gdef);
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab3);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                }
                            }
                            if (p.CanWrite)
                            {
                                bi_Bean_properties_All.Append(tab3);
                                bi_Bean_properties_All.Append("set");
                                if (sdef == null)
                                {
                                    bi_Bean_properties_All.Append(";");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                }
                                else
                                {
                                    bi_Bean_properties_All.Append(" {");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab4);
                                    bi_Bean_properties_All.Append(sdef);
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab3);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                }
                            }
                            bi_Bean_properties_All.Append(tab2);
                            bi_Bean_properties_All.Append("}");
                            bi_Bean_properties_All.Append(NEWLINE);
                            bi_Bean_properties_All.Append(NEWLINE);

                            bi_Bean_properties_Qname.Append(tab2);
                            bi_Bean_properties_Qname.Append("}");

                            flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);
                        }
                        else
                        {
                            StringBuilder propId = new StringBuilder();
                            propId.Append("PROPERTY_");
                            propId.Append(propertyId);
                            propId.Append("_");
                            propId.Append(p.Name.ToUpper());
                            propId.Append("_ID");

                            string varId = "_" + rmiInterface.Name + "_" + p.Name;
                            string absPropId = rmiInterface.Name + "_" + p.Name;

                            bool isMainBranchParentProp, defaultIsMainBranchParentProp;
                            Type parentPropType, defaultParentPropType;
                            PropertyInfo api, defaultApi;
                            StringBuilder parenttps = new StringBuilder();
                            if (attr.Type == GreenRmiFieldType.New && attr.ParentType != null)
                            {
                                isMainBranchParentProp = attr.ParentType.IsAssignableFrom(bi.parent.rmiInterface);
                                api = attr.ParentType.GetProperty(p.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                                parentPropType = api.PropertyType;
                                
                                defaultIsMainBranchParentProp = FindParentProperty(bi, p.Name, true, parenttps, out defaultParentPropType, out defaultApi);
                            }
                            else
                            {
                                isMainBranchParentProp = FindParentProperty(bi, p.Name, true, parenttps, out parentPropType, out api);
                                defaultIsMainBranchParentProp = isMainBranchParentProp;
                                defaultParentPropType = parentPropType;
                                defaultApi = api;
                            }

                            switch (attr.Type)
                            {
                                case GreenRmiFieldType.Normal:
                                case GreenRmiFieldType.Virtual:
                                case GreenRmiFieldType.Readonly:
                                    if (bi.Props_propertyConstants_All.ContainsKey(propertyId))
                                    {
                                        throw new NotSupportedException("ERROR Property constant duplication : " + rmiInterface.Name + "." + propertyId);
                                    }
                                    else
                                    {
                                        bi.Props_propertyConstants_All[propertyId] = rmiInterface.Name + "." + propId;
                                    }

                                    if (parentPropType != null && isMainBranchParentProp)
                                    {
                                        bi.Overridden_Parent_Properties.Add(api.DeclaringType.Name + "." + p.Name);
                                    }

                                    bi.Props_propertyConstants.Append(tab2);
                                    bi.Props_propertyConstants.Append("public const int ");
                                    bi.Props_propertyConstants.Append(propId);
                                    bi.Props_propertyConstants.Append(" = ");
                                    bi.Props_propertyConstants.Append(propertyId);
                                    bi.Props_propertyConstants.Append(";");
                                    bi.Props_propertyConstants.Append(NEWLINE);

                                    bi.Props_rmiGet.Append(tab4);
                                    bi.Props_rmiGet.Append("case ");
                                    bi.Props_rmiGet.Append(cName);
                                    bi.Props_rmiGet.Append("Props.");
                                    bi.Props_rmiGet.Append(propId);
                                    bi.Props_rmiGet.Append(":");
                                    bi.Props_rmiGet.Append(NEWLINE);
                                    bi.Props_rmiGet.Append(tab5);
                                    bi.Props_rmiGet.Append("value = controller.");
                                    bi.Props_rmiGet.Append(p.Name);
                                    bi.Props_rmiGet.Append(";");
                                    bi.Props_rmiGet.Append(NEWLINE);
                                    bi.Props_rmiGet.Append(tab5);
                                    bi.Props_rmiGet.Append("return true;");
                                    bi.Props_rmiGet.Append(NEWLINE);


                                    bi_Bean_properties_All = new StringBuilder();
                                    bi_Bean_properties = new StringBuilder();
                                    bi_Bean_properties_Qname = new StringBuilder();

                                    bi.Bean_properties[rmiInterface.Name + "." + p.Name] = bi_Bean_properties;
                                    bi.Bean_properties_Qname[rmiInterface.Name + "." + p.Name] = bi_Bean_properties_Qname;

                                    bi_Bean_properties_All.Append(tab2);
                                    if (attr.FieldModifiers != null)
                                    {
                                        bi_Bean_properties_All.Append(attr.FieldModifiers);
                                        bi_Bean_properties_All.Append(" ");
                                    }
                                    bi_Bean_properties_All.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties_All.Append(" ");
                                    bi_Bean_properties_All.Append(varId);
                                    if (attr.FieldInitialization != null)
                                    {
                                        bi_Bean_properties_All.Append(" = ");
                                        bi_Bean_properties_All.Append(attr.FieldInitialization);
                                    }
                                    bi_Bean_properties_All.Append(";");
                                    bi_Bean_properties_All.Append(NEWLINE);

                                    flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);

                                    bi_Bean_properties.Append(tab2);
                                    bi_Bean_properties.Append("public ");

                                    if (isMainBranchParentProp)
                                    {
                                        if (parentPropType == p.PropertyType)
                                        {
                                            bi_Bean_properties.Append("override ");
                                        }
                                        else
                                        {
                                            if (parentPropType != null)
                                            {
                                                bi_Bean_properties.Append("new ");
                                            }
                                            if (attr.Type == GreenRmiFieldType.Virtual)
                                            {
                                                bi_Bean_properties.Append("virtual ");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (attr.Type == GreenRmiFieldType.Virtual)
                                        {
                                            bi_Bean_properties.Append("virtual ");
                                        }
                                    }

                                    if (attr.Modifiers != null)
                                    {
                                        bi_Bean_properties.Append(attr.Modifiers);
                                        bi_Bean_properties.Append(" ");
                                    }
                                    bi_Bean_properties.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties.Append(" ");

                                    bi_Bean_properties_Qname.Append(tab2);
                                    bi_Bean_properties_Qname.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties_Qname.Append(" ");
                                    bi_Bean_properties_Qname.Append(rmiInterface.Name);
                                    bi_Bean_properties_Qname.Append(".");


                                    bi_Bean_properties_All.Append(p.Name);
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab2);
                                    bi_Bean_properties_All.Append("{");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab3);
                                    bi_Bean_properties_All.Append("get {");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab4);
                                    bi_Bean_properties_All.Append("return ");
                                    bi_Bean_properties_All.Append(varId);
                                    bi_Bean_properties_All.Append(";");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab3);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab3);
                                    bi_Bean_properties_All.Append("set {");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab4);
                                    if (attr.Type == GreenRmiFieldType.Readonly)
                                    {
                                        bi_Bean_properties_All.Append("if (!___initialized) {");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                    }
                                    else
                                    {
                                        bi_Bean_properties_All.Append("if (");
                                        bi_Bean_properties_All.Append(varId);
                                        bi_Bean_properties_All.Append(" != value) {");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                    }
                                    bi_Bean_properties_All.Append(tab5);
                                    bi_Bean_properties_All.Append(varId);
                                    bi_Bean_properties_All.Append("= value;");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab5);
                                    bi_Bean_properties_All.Append("changed[");
                                    bi_Bean_properties_All.Append(cName);
                                    bi_Bean_properties_All.Append("Props.");
                                    bi_Bean_properties_All.Append(propId);
                                    bi_Bean_properties_All.Append("] = true;");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    if (attr.Type == GreenRmiFieldType.Readonly)
                                    {
                                        bi_Bean_properties_All.Append(tab4);
                                        bi_Bean_properties_All.Append("}");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab4);
                                        bi_Bean_properties_All.Append("else");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab4);
                                        bi_Bean_properties_All.Append("{");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab5);
                                        bi_Bean_properties_All.Append("throw new NotSupportedException(\"Readonly property already initialized.\");");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        AddTypeNamespace(bi, typeof(NotSupportedException));
                                    }
                                    else
                                    {
                                        bi_Bean_properties_All.Append(tab5);
                                        bi_Bean_properties_All.Append("if (");
                                        bi_Bean_properties_All.Append(absPropId);
                                        bi_Bean_properties_All.Append("_Changed != null)");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab6);
                                        bi_Bean_properties_All.Append(absPropId);
                                        bi_Bean_properties_All.Append("_Changed(this, new PropertyChangedEventArgs(\"");
                                        bi_Bean_properties_All.Append(p.Name);
                                        bi_Bean_properties_All.Append("\", value));");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                    }
                                    bi_Bean_properties_All.Append(tab4);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab3);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab2);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(NEWLINE);

                                    object[] nsattrs = p.GetCustomAttributes(typeof(GreenRmiNonSerialAttribute), false);
                                    if (nsattrs.Length == 0)
                                    {
                                        bi.Props_serial_r.Append(tab3);
                                        bi.Props_serial_r.Append("controller.");
                                        bi.Props_serial_r.Append(p.Name);
                                        bi.Props_serial_r.Append(" = (");
                                        bi.Props_serial_r.Append(PrettyName(p.PropertyType));
                                        bi.Props_serial_r.Append(") info.GetValue(\"");
                                        bi.Props_serial_r.Append(p.Name);
                                        bi.Props_serial_r.Append("\", typeof(");
                                        bi.Props_serial_r.Append(PrettyName(p.PropertyType));
                                        bi.Props_serial_r.Append("));");
                                        bi.Props_serial_r.Append(NEWLINE);

                                        bi.Props_serial_w.Append(tab3);
                                        bi.Props_serial_w.Append("info.AddValue(\"");
                                        bi.Props_serial_w.Append(p.Name);
                                        bi.Props_serial_w.Append("\", controller.");
                                        bi.Props_serial_w.Append(p.Name);
                                        bi.Props_serial_w.Append(");");
                                        bi.Props_serial_w.Append(NEWLINE);
                                    }

                                    flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);
                                    break;

                                case GreenRmiFieldType.New:

                                    if (parentPropType != null)
                                    {
                                        if (isMainBranchParentProp && parentPropType == p.PropertyType)
                                        {
                                            bi_Bean_properties_All = new StringBuilder();
                                            bi_Bean_properties = new StringBuilder();
                                            bi_Bean_properties_Qname = new StringBuilder();

                                            bi.Bean_properties[rmiInterface.Name + "." + p.Name] = bi_Bean_properties;
                                            bi.Bean_properties_Qname[rmiInterface.Name + "." + p.Name] = bi_Bean_properties_Qname;

                                            bi_Bean_properties_All.Append(tab2);
                                            bi_Bean_properties_All.Append("// parent property type is the same, no property generated : ");
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(tab2);
                                            bi_Bean_properties_All.Append("// ");
                                            bi_Bean_properties_All.Append(PrettyName(p.PropertyType) + " ");
                                            bi_Bean_properties_All.Append(" ");
                                            bi_Bean_properties_All.Append(p.Name);
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(tab2);
                                            bi_Bean_properties_All.Append("// in parents : ");
                                            bi_Bean_properties_All.Append(parenttps);
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(NEWLINE);

                                            flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);
                                        }
                                        else
                                        {
                                            AddTypeNamespace(bi, parentPropType);

                                            bi_Bean_properties_All = new StringBuilder();
                                            bi_Bean_properties = new StringBuilder();
                                            bi_Bean_properties_Qname = new StringBuilder();

                                            bi.Bean_properties[rmiInterface.Name + "." + p.Name] = bi_Bean_properties;
                                            bi.Bean_properties_Qname[rmiInterface.Name + "." + p.Name] = bi_Bean_properties_Qname;

                                            bi_Bean_properties.Append(tab2);
                                            bi_Bean_properties.Append("public ");
                                            
                                            if (defaultIsMainBranchParentProp || isMainBranchParentProp)
                                            {
                                                if (defaultIsMainBranchParentProp ? (defaultParentPropType == p.PropertyType) : (parentPropType == p.PropertyType))
                                                {
                                                    bi_Bean_properties.Append("override ");
                                                }
                                                else
                                                {
                                                    bi_Bean_properties.Append("new virtual ");
                                                }
                                            }
                                            else
                                            {
                                                bi_Bean_properties.Append("virtual ");
                                            }

                                            bi_Bean_properties.Append(PrettyName(p.PropertyType));
                                            bi_Bean_properties.Append(" ");

                                            bi_Bean_properties_Qname.Append(tab2);
                                            bi_Bean_properties_Qname.Append(PrettyName(p.PropertyType));
                                            bi_Bean_properties_Qname.Append(" ");
                                            bi_Bean_properties_Qname.Append(rmiInterface.Name);
                                            bi_Bean_properties_Qname.Append(".");

                                            bi_Bean_properties_All.Append(p.Name);
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(tab2);
                                            bi_Bean_properties_All.Append("{");
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(tab3);
                                            bi_Bean_properties_All.Append("get {");
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(tab4);
                                            bi_Bean_properties_All.Append("return ");
                                            if (parentPropType != p.PropertyType)
                                            {
                                                bi_Bean_properties_All.Append("(");
                                                bi_Bean_properties_All.Append(PrettyName(p.PropertyType));
                                                bi_Bean_properties_All.Append(") ");
                                            }
                                            bi_Bean_properties_All.Append("((");
                                            bi_Bean_properties_All.Append(api.DeclaringType.Name);
                                            bi_Bean_properties_All.Append(")this).");
                                            bi_Bean_properties_All.Append(p.Name);
                                            bi_Bean_properties_All.Append(";");
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(tab3);
                                            bi_Bean_properties_All.Append("}");
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            if (p.GetSetMethod(true) != null && api.GetSetMethod(true) != null)
                                            {
                                                bi_Bean_properties_All.Append(tab3);
                                                if (api.GetSetMethod(true).IsFamily)
                                                {
                                                    bi_Bean_properties_All.Append("protected ");
                                                }
                                                bi_Bean_properties_All.Append("set {");
                                                bi_Bean_properties_All.Append(NEWLINE);
                                                bi_Bean_properties_All.Append(tab4);
                                                bi_Bean_properties_All.Append("((");
                                                bi_Bean_properties_All.Append(api.DeclaringType.Name);
                                                bi_Bean_properties_All.Append(")this).");
                                                bi_Bean_properties_All.Append(p.Name);
                                                bi_Bean_properties_All.Append(" = value;");
                                                bi_Bean_properties_All.Append(NEWLINE);
                                                bi_Bean_properties_All.Append(tab3);
                                                bi_Bean_properties_All.Append("}");
                                                bi_Bean_properties_All.Append(NEWLINE);
                                            }
                                            bi_Bean_properties_All.Append(tab2);
                                            bi_Bean_properties_All.Append("}");
                                            bi_Bean_properties_All.Append(NEWLINE);
                                            bi_Bean_properties_All.Append(NEWLINE);

                                            flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);
                                        }
                                    }
                                    else
                                    {
                                        bi_Bean_properties_All = new StringBuilder();
                                        bi_Bean_properties = new StringBuilder();
                                        bi_Bean_properties_Qname = new StringBuilder();

                                        bi.Bean_properties[rmiInterface.Name + "." + p.Name] = bi_Bean_properties;
                                        bi.Bean_properties_Qname[rmiInterface.Name + "." + p.Name] = bi_Bean_properties_Qname;

                                        bi_Bean_properties_All.Append(tab2);
                                        bi_Bean_properties_All.Append("// FAILED to determine parent property type : ");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab2);
                                        bi_Bean_properties_All.Append("// ");
                                        bi_Bean_properties_All.Append(PrettyName(p.PropertyType) + " ");
                                        bi_Bean_properties_All.Append(" ");
                                        bi_Bean_properties_All.Append(p.Name);
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab2);
                                        bi_Bean_properties_All.Append("// in parents : ");
                                        bi_Bean_properties_All.Append(parenttps);
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(NEWLINE);

                                        flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);
                                    }
                                    break;

                                case GreenRmiFieldType.Abstract:

                                    if (parentPropType != null && isMainBranchParentProp)
                                    {
                                        bi.Overridden_Parent_Properties.Add(api.DeclaringType.Name + "." + p.Name);
                                    }

                                    bi_Bean_properties_All = new StringBuilder();
                                    bi_Bean_properties = new StringBuilder();
                                    bi_Bean_properties_Qname = new StringBuilder();

                                    bi.Bean_properties[rmiInterface.Name + "." + p.Name] = bi_Bean_properties;
                                    bi.Bean_properties_Qname[rmiInterface.Name + "." + p.Name] = bi_Bean_properties_Qname;

                                    bi_Bean_properties.Append(tab2);
                                    bi_Bean_properties.Append("public abstract ");
                                    bi_Bean_properties.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties.Append(" ");

                                    bi_Bean_properties_Qname.Append(tab2);
                                    bi_Bean_properties_Qname.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties_Qname.Append(" ");
                                    bi_Bean_properties_Qname.Append(rmiInterface.Name);
                                    bi_Bean_properties_Qname.Append(".");

                                    bi_Bean_properties_All.Append(p.Name);
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab2);
                                    bi_Bean_properties_All.Append("{");
                                    bi_Bean_properties_All.Append(NEWLINE);

                                    flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);

                                    if (p.CanRead)
                                    {
                                        bi_Bean_properties.Append(tab3);
                                        bi_Bean_properties.Append("get ;");
                                        bi_Bean_properties.Append(NEWLINE);

                                        bi_Bean_properties_Qname.Append(tab3);
                                        bi_Bean_properties_Qname.Append("get {");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                        bi_Bean_properties_Qname.Append(tab4);
                                        bi_Bean_properties_Qname.Append("return ");
                                        bi_Bean_properties_Qname.Append(absPropId);
                                        bi_Bean_properties_Qname.Append(";");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                        bi_Bean_properties_Qname.Append(tab3);
                                        bi_Bean_properties_Qname.Append("}");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                    }
                                    if (p.CanWrite)
                                    {
                                        bi_Bean_properties.Append(tab3);
                                        bi_Bean_properties.Append("set ;");
                                        bi_Bean_properties.Append(NEWLINE);

                                        bi_Bean_properties_Qname.Append(tab3);
                                        bi_Bean_properties_Qname.Append("set {");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                        bi_Bean_properties_Qname.Append(tab4);
                                        bi_Bean_properties_Qname.Append(absPropId);
                                        bi_Bean_properties_Qname.Append(" = value;");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                        bi_Bean_properties_Qname.Append(tab3);
                                        bi_Bean_properties_Qname.Append("}");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                    }

                                    bi_Bean_properties_All.Append(tab2);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(NEWLINE);

                                    flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);

                                    bi_Bean_properties_Qname.Append(tab2);
                                    bi_Bean_properties_Qname.Append("public abstract ");
                                    bi_Bean_properties_Qname.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties_Qname.Append(" ");
                                    bi_Bean_properties_Qname.Append(absPropId);
                                    bi_Bean_properties_Qname.Append(NEWLINE);
                                    bi_Bean_properties_Qname.Append(tab2);
                                    bi_Bean_properties_Qname.Append("{");
                                    bi_Bean_properties_Qname.Append(NEWLINE);
                                    if (p.CanRead)
                                    {
                                        bi_Bean_properties_Qname.Append(tab3);
                                        bi_Bean_properties_Qname.Append("get ;");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                    }
                                    if (p.CanWrite)
                                    {
                                        bi_Bean_properties_Qname.Append(tab3);
                                        bi_Bean_properties_Qname.Append("set ;");
                                        bi_Bean_properties_Qname.Append(NEWLINE);
                                    }
                                    bi_Bean_properties_Qname.Append(tab2);
                                    bi_Bean_properties_Qname.Append("}");
                                    bi_Bean_properties_Qname.Append(NEWLINE);
                                    bi_Bean_properties_Qname.Append(NEWLINE);



                                    break;

                                case GreenRmiFieldType.Simple:
                                    if (!p.CanRead && !p.CanWrite)
                                    {
                                        throw new NotSupportedException();
                                    }
                                    if (parentPropType != null && isMainBranchParentProp)
                                    {
                                        bi.Overridden_Parent_Properties.Add(api.DeclaringType.Name + "." + p.Name);
                                    }

                                    object[] getDefAttrs = p.CanRead ? p.GetGetMethod(true).GetCustomAttributes(typeof(GreenRmiMethodAttribute), false) : null;
                                    object[] setDefAttrs = p.CanWrite ? p.GetSetMethod(true).GetCustomAttributes(typeof(GreenRmiMethodAttribute), false) : null;

                                    string gdef, sdef;
                                    if (attr.Definition != null)
                                    {
                                        if (p.CanRead && p.CanWrite)
                                        {
                                            throw new NotSupportedException();
                                        }
                                        if (getDefAttrs != null && getDefAttrs.Length != 0)
                                        {
                                            throw new NotSupportedException();
                                        }
                                        if (setDefAttrs != null && setDefAttrs.Length != 0)
                                        {
                                            throw new NotSupportedException();
                                        }
                                        gdef = sdef = attr.Definition;
                                    }
                                    else
                                    {
                                        if (getDefAttrs != null && getDefAttrs.Length != 0)
                                        {
                                            gdef = ((GreenRmiMethodAttribute)getDefAttrs[0]).Definition;
                                        }
                                        else
                                        {
                                            gdef = null;
                                        }
                                        if (setDefAttrs != null && setDefAttrs.Length != 0)
                                        {
                                            sdef = ((GreenRmiMethodAttribute)setDefAttrs[0]).Definition;
                                        }
                                        else
                                        {
                                            sdef = null;
                                        }
                                    }


                                    bi_Bean_properties_All = new StringBuilder();
                                    bi_Bean_properties = new StringBuilder();
                                    bi_Bean_properties_Qname = new StringBuilder();

                                    bi.Bean_properties[rmiInterface.Name + "." + p.Name] = bi_Bean_properties;
                                    bi.Bean_properties_Qname[rmiInterface.Name + "." + p.Name] = bi_Bean_properties_Qname;

                                    bi_Bean_properties.Append(tab2);
                                    bi_Bean_properties.Append("public ");
                                    if (isMainBranchParentProp)
                                    {
                                        if (parentPropType == p.PropertyType)
                                        {
                                            bi_Bean_properties.Append("override ");
                                        }
                                        else
                                        {
                                            if (parentPropType != null)
                                            {
                                                bi_Bean_properties.Append("new ");
                                            }
                                            bi_Bean_properties.Append("virtual ");
                                        }
                                    }
                                    else
                                    {
                                        bi_Bean_properties.Append("virtual ");
                                    }
                                    bi_Bean_properties.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties.Append(" ");

                                    bi_Bean_properties_Qname.Append(tab2);
                                    bi_Bean_properties_Qname.Append(PrettyName(p.PropertyType));
                                    bi_Bean_properties_Qname.Append(" ");
                                    bi_Bean_properties_Qname.Append(rmiInterface.Name);
                                    bi_Bean_properties_Qname.Append(".");

                                    bi_Bean_properties_All.Append(p.Name);
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(tab2);
                                    bi_Bean_properties_All.Append("{");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    if (p.CanRead)
                                    {
                                        bi_Bean_properties_All.Append(tab3);
                                        bi_Bean_properties_All.Append("get {");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab4);
                                        bi_Bean_properties_All.Append(gdef);
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab3);
                                        bi_Bean_properties_All.Append("}");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                    }
                                    if (p.CanWrite)
                                    {
                                        bi_Bean_properties_All.Append(tab3);
                                        bi_Bean_properties_All.Append("set {");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab4);
                                        bi_Bean_properties_All.Append(sdef);
                                        bi_Bean_properties_All.Append(NEWLINE);
                                        bi_Bean_properties_All.Append(tab3);
                                        bi_Bean_properties_All.Append("}");
                                        bi_Bean_properties_All.Append(NEWLINE);
                                    }
                                    bi_Bean_properties_All.Append(tab2);
                                    bi_Bean_properties_All.Append("}");
                                    bi_Bean_properties_All.Append(NEWLINE);
                                    bi_Bean_properties_All.Append(NEWLINE);

                                    flush_props(bi_Bean_properties_All, bi_Bean_properties, bi_Bean_properties_Qname);
                                    break;
                            }

                            switch (attr.Type)
                            {
                                case GreenRmiFieldType.Normal:
                                case GreenRmiFieldType.Virtual:
                                    bi.Props_rmiSet.Append(tab4);
                                    bi.Props_rmiSet.Append("case ");
                                    bi.Props_rmiSet.Append(cName);
                                    bi.Props_rmiSet.Append("Props.");
                                    bi.Props_rmiSet.Append(propId);
                                    bi.Props_rmiSet.Append(":");
                                    bi.Props_rmiSet.Append(NEWLINE);
                                    bi.Props_rmiSet.Append(tab5);
                                    bi.Props_rmiSet.Append("controller.");
                                    bi.Props_rmiSet.Append(p.Name);
                                    bi.Props_rmiSet.Append(" = ");
                                    bi.Props_rmiSet.Append("(");
                                    bi.Props_rmiSet.Append(PrettyName(p.PropertyType));
                                    bi.Props_rmiSet.Append(") value;");
                                    bi.Props_rmiSet.Append(NEWLINE);
                                    bi.Props_rmiSet.Append(tab5);
                                    bi.Props_rmiSet.Append("return true;");
                                    bi.Props_rmiSet.Append(NEWLINE);

                                    bi.Bean_events.Append(tab2);
                                    bi.Bean_events.Append("public event PropertyChangedEventHandler ");
                                    bi.Bean_events.Append(absPropId);
                                    bi.Bean_events.Append("_Changed;");
                                    bi.Bean_events.Append(NEWLINE);
                                    AddTypeNamespace(bi, typeof(PropertyChangedEventHandler));

                                    propertyId++;
                                    break;
                                case GreenRmiFieldType.Readonly:
                                    hasReadonly = true;

                                    bi.Props_init.Append(tab3);
                                    bi.Props_init.Append("controller.");
                                    bi.Props_init.Append(p.Name);
                                    bi.Props_init.Append(" = ");
                                    bi.Props_init.Append("(");
                                    bi.Props_init.Append(PrettyName(p.PropertyType));
                                    bi.Props_init.Append(") buffer.ChangedProps[");
                                    bi.Props_init.Append(cName);
                                    bi.Props_init.Append("Props.");
                                    bi.Props_init.Append(propId);
                                    bi.Props_init.Append("];");
                                    bi.Props_init.Append(NEWLINE);

                                    if (typeof(GreenRmiBound).IsAssignableFrom(p.PropertyType))
                                    {
                                        bi.Props_deps.Append(tab3);
                                        bi.Props_deps.Append("controller.Dependencies.Add(controller.");
                                        bi.Props_deps.Append(p.Name);
                                        bi.Props_deps.Append(");");
                                        bi.Props_deps.Append(NEWLINE);
                                    }

                                    if (typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                                    {
                                        bool rmiBoundEnumerable = false;
                                        var genericArguments = p.PropertyType.GetGenericArguments();
                                        if (genericArguments.Length > 0)
                                        {
                                            foreach (var genericArgument in genericArguments)
                                            {
                                                if (typeof(GreenRmiBound).IsAssignableFrom(genericArgument))
                                                {
                                                    rmiBoundEnumerable = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (rmiBoundEnumerable)
                                        {
                                            if (typeof(IDictionary).IsAssignableFrom(p.PropertyType))
                                            {
                                                bi.Props_deps.Append(tab3);
                                                bi.Props_deps.Append("controller.Dependencies.AddRange(controller.");
                                                bi.Props_deps.Append(p.Name);
                                                bi.Props_deps.Append(".Values);");
                                                bi.Props_deps.Append(NEWLINE);
                                            }
                                            else
                                            {
                                                bi.Props_deps.Append(tab3);
                                                bi.Props_deps.Append("controller.Dependencies.AddRange(controller.");
                                                bi.Props_deps.Append(p.Name);
                                                bi.Props_deps.Append(");");
                                                bi.Props_deps.Append(NEWLINE);
                                            }
                                        }
                                    }

                                    propertyId++;
                                    break;
                                case GreenRmiFieldType.New:
                                    break;
                            }
                        }
                    }

                    browse_parent_members(bi);

                    bi.Props_rmiGet.Append(tab4);
                    bi.Props_rmiGet.Append("default:");
                    bi.Props_rmiGet.Append(NEWLINE);
                    bi.Props_rmiGet.Append(tab5);
                    bi.Props_rmiGet.Append("value = null;");
                    bi.Props_rmiGet.Append(NEWLINE);
                    bi.Props_rmiGet.Append(tab5);
                    bi.Props_rmiGet.Append("return false;");
                    bi.Props_rmiGet.Append(NEWLINE);
                    bi.Props_rmiGet.Append(tab3);
                    bi.Props_rmiGet.Append("}");
                    bi.Props_rmiGet.Append(NEWLINE);
                    bi.Props_rmiGet.Append(tab2);
                    bi.Props_rmiGet.Append("}");
                    bi.Props_rmiGet.Append(NEWLINE);

                    bi.Props_rmiSet.Append(tab4);
                    bi.Props_rmiSet.Append("default:");
                    bi.Props_rmiSet.Append(NEWLINE);
                    bi.Props_rmiSet.Append(tab5);
                    bi.Props_rmiSet.Append("return false;");
                    bi.Props_rmiSet.Append(NEWLINE);
                    bi.Props_rmiSet.Append(tab3);
                    bi.Props_rmiSet.Append("}");
                    bi.Props_rmiSet.Append(NEWLINE);
                    bi.Props_rmiSet.Append(tab2);
                    bi.Props_rmiSet.Append("}");
                    bi.Props_rmiSet.Append(NEWLINE);

                    bi.Props_init.Append(tab2);
                    bi.Props_init.Append("}");
                    bi.Props_init.Append(NEWLINE);
                    bi.Props_init.Append(NEWLINE);

                    bi.Props_deps.Append(tab2);
                    bi.Props_deps.Append("}");
                    bi.Props_deps.Append(NEWLINE);
                    bi.Props_deps.Append(NEWLINE);

                    bi.Props_serial_r.Append(tab2);
                    bi.Props_serial_r.Append("}");
                    bi.Props_serial_r.Append(NEWLINE);
                    bi.Props_serial_r.Append(NEWLINE);

                    bi.Props_serial_w.Append(tab2);
                    bi.Props_serial_w.Append("}");
                    bi.Props_serial_w.Append(NEWLINE);
                    bi.Props_serial_w.Append(NEWLINE);

                    GenerateCosntructorFooter(bi, bi.Bean_constructors, cName, !hasReadonly);
                    GenerateCosntructorFooter(bi, bi.Bean_serial_r, cName);


                    bi.lastPropertyId = propertyId - 1;
                    buildInfos[PrettyFullName(rmiInterface)] = bi;

                    StringBuilder content = new StringBuilder();
                    foreach (BuildInfo apbi in bi.additionalParents)
                    {
                        foreach (var ns in apbi.usedNamespaces)
                        {
                            bi.usedNamespaces.Add(ns);
                        }
                    }
                    foreach (var ns in bi.usedNamespaces)
                    {
                        content.Append("using ");
                        content.Append(ns);
                        content.Append(";");
                        content.Append(NEWLINE);
                    }
                    content.Append(bi.Props_header);
                    content.Append(bi.Props_propertyConstants);
                    content.Append(bi.Props_rmiGet);
                    content.Append(bi.Props_rmiSet);
                    content.Append(bi.Props_init);
                    content.Append(bi.Props_deps);
                    content.Append(bi.Props_serial_r);
                    content.Append(bi.Props_serial_w);
                    content.Append(tab1);
                    content.Append("}");
                    content.Append(NEWLINE);
                    content.Append(bi.Bean_header);
                    content.Append(bi.Bean_events);
                    content.Append(NEWLINE);
                    content.Append(bi.Bean_constructors);
                    content.Append(bi.Bean_serial_r);
                    content.Append(bi.Bean_serial_w);
                    content.Append(bi.Bean_methods);
                    content.Append(NEWLINE);
                    foreach (var sb in bi.Bean_properties_Gen.Values)
                    {
                        content.Append(sb);
                    }
                    content.Append(NEWLINE);
                    content.Append(bi.Bean_rmiGet);
                    content.Append(bi.Bean_rmiSet);
                    content.Append(tab1);
                    content.Append("}");
                    content.Append(NEWLINE);
                    content.Append("}");
                    content.Append(NEWLINE);

                    string folder = GEN_FOLDER + rmiInterface.Namespace.Replace('.', '/');
                    Directory.CreateDirectory(folder);
                    string file = folder + "/" + cName + "Base.cs";
                    //File.Delete(file);
                    File.WriteAllText(file, content.ToString());
                }
            }
            else
            {
                Console.WriteLine("ERROR Not an interface");
            }
        }

        private static void flush_props(StringBuilder bi_Bean_properties_All, StringBuilder bi_Bean_properties, StringBuilder bi_Bean_properties_Qname)
        {
            bi_Bean_properties.Append(bi_Bean_properties_All);
            bi_Bean_properties_Qname.Append(bi_Bean_properties_All);
            bi_Bean_properties_All.Clear();
        }

        private static void browse_parent_members(BuildInfo leafBi)
        {

            leafBi.allParents = new List<BuildInfo>();
            if (leafBi.parent != null)
            {
                leafBi.allParents.Add(leafBi.parent);
            }
            leafBi.allParents.AddRange(leafBi.additionalParents);

            leafBi.Bean_properties_Map_Gen = new Dictionary<string, string>();

            foreach (var k in leafBi.Bean_properties_Qname.Keys)
            {
                string[] sls = k.Split('.');
                string name = sls[sls.Length - 1];
                leafBi.Bean_properties_Map_Gen.Add(name, k);
            }


            browse_parent_members_req(leafBi, leafBi, false);
        }

        private static void browse_parent_members_req(BuildInfo leafBi, BuildInfo currentBi, bool parseBaseClass)
        {
            List<BuildInfo> parents;

            if (parseBaseClass)
            {
                parents = currentBi.allParents;
            }
            else
            {
                parents = currentBi.additionalParents;
            }

            currentBi.Bean_properties_Gen = new Dictionary<string, StringBuilder>(currentBi.Bean_properties);
            currentBi.Bean_properties_Qname_Gen = new Dictionary<string, StringBuilder>(currentBi.Bean_properties_Qname);

            foreach (BuildInfo parentBi in parents)
            {
                if (leafBi.parent != null &&
                    leafBi.parent.rmiInterface != null &&
                    parentBi.rmiInterface.IsAssignableFrom(leafBi.parent.rmiInterface))
                {
                    continue;
                }

                browse_parent_members_req(leafBi, parentBi, true);

                foreach (var e in parentBi.Props_propertyConstants_All)
                {
                    if (leafBi.Props_propertyConstants_All.ContainsKey(e.Key))
                    {
                        throw new NotSupportedException("ERROR Property constant duplication : " + e.Key + "  :  " + leafBi.Props_propertyConstants_All[e.Key] + "  VS  " + parentBi.Props_propertyConstants_All[e.Key]);
                    }
                    else
                    {
                        leafBi.Props_propertyConstants_All[e.Key] = e.Value;
                    }
                }

                List<string> keys = new List<string>(parentBi.Bean_properties_Qname_Gen.Keys);
                keys.Reverse();

                foreach (var k in keys)
                {
                    browse_gen_props(leafBi, parentBi, k);
                }

                leafBi.Bean_methods.Append(parentBi.Bean_methods);
                leafBi.Bean_methods.Append(NEWLINE);

                leafBi.Bean_events.Append(parentBi.Bean_events);
                leafBi.Bean_events.Append(NEWLINE);
            }

            foreach (var prop in currentBi.Overridden_Parent_Properties)
            {
                currentBi.Bean_properties_Gen.Remove(prop);
                currentBi.Bean_properties_Qname_Gen.Remove(prop);
            }
        }

        private static void browse_gen_props(BuildInfo leafBi, BuildInfo parentBi, string k)
        {
            string[] sls = k.Split('.');
            string name = sls[sls.Length - 1];

            Dictionary<string, StringBuilder> ps;

            StringBuilder b;
            if (!leafBi.Bean_properties_Gen.TryGetValue(k, out b))
            {
                leafBi.Bean_properties_Gen[k] = b = new StringBuilder();
            }

            if (leafBi.Bean_properties_Map_Gen.ContainsKey(name))
            {
                ps = parentBi.Bean_properties_Qname_Gen;

                b.Append(tab2);
                b.Append("// WARNING Property duplication : ");
                b.Append(name);
                b.Append(NEWLINE);
                b.Append(NEWLINE);
            }
            else
            {
                ps = parentBi.Bean_properties_Gen;
                leafBi.Bean_properties_Map_Gen[name] = k;
            }

            b.Append(ps[k]);
            leafBi.Bean_properties_Qname_Gen[k] = new StringBuilder(parentBi.Bean_properties_Qname_Gen[k].ToString());
        }


        private static void GenerateCosntructor(BuildInfo bi, StringBuilder buffer, ConstructorInfo ci, string cName)
        {
            GenerateCosntructorHeader(bi, buffer, ci, cName);
            GenerateCosntructorFooter(bi, buffer, cName);
        }

        private static void GenerateCosntructor(BuildInfo bi, StringBuilder buffer, BlockNode consBlock, ConstructorDefStatementNode ci, string cName)
        {
            GenerateCosntructorHeader(bi, buffer, consBlock, ci, cName);
            GenerateCosntructorFooter(bi, buffer, cName);
        }

        private static void GenerateCosntructorHeader(BuildInfo bi, StringBuilder buffer, ConstructorInfo ci, string cName)
        {
            ParameterInfo[] ps = ci.GetParameters();

            buffer.Append(tab2);
            if (ci.IsFamily)
            {
                buffer.Append("protected ");
            }
            else if (ci.IsPublic)
            {
                buffer.Append("public ");
            }
            else
            {
                throw new NotSupportedException();
            }
            buffer.Append(cName);
            buffer.Append("Base(");
            for (int i = 0; i < ps.Length; i++)
            {
                var pi = ps[i];
                AddTypeNamespace(bi, pi.ParameterType);
                if (IsParams(pi))
                {
                    buffer.Append("params ");
                }
                buffer.Append(PrettyName(pi.ParameterType));
                buffer.Append(" ");
                buffer.Append(pi.Name);
                if (i < ps.Length - 1)
                {
                    buffer.Append(", ");
                }
            }
            buffer.Append(")");
            buffer.Append(NEWLINE);
            buffer.Append(tab3);
            buffer.Append(": base(");
            for (int i = 0; i < ps.Length; i++)
            {
                var pi = ps[i];
                buffer.Append(pi.Name);
                if (i < ps.Length - 1)
                {
                    buffer.Append(", ");
                }
            }
            buffer.Append(")");
            buffer.Append(NEWLINE);
            buffer.Append(tab2);
            buffer.Append("{");
            buffer.Append(NEWLINE);
        }

        private static void GenerateCosntructorHeader(BuildInfo bi, StringBuilder buffer, BlockNode consBlock, ConstructorDefStatementNode consDefNode, string cName)
        {
            StringBuilder mods = new StringBuilder();
            foreach (var m in consBlock.Modifiers)
            {
                mods.Append(m.Content);
                mods.Append(" ");
            }

            IList<VarDeclArgumentNode> args = consDefNode.ArgumentList;

            buffer.Append(tab2);
            buffer.Append(mods);

            buffer.Append(cName);
            buffer.Append("Base(");
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                switch (arg.Type)
                {
                    case ArgumentType.Out:
                        buffer.Append("out ");
                        break;
                    case ArgumentType.Ref:
                        buffer.Append("ref ");
                        break;
                    case ArgumentType.Params:
                        buffer.Append("params ");
                        break;
                }
                string type = arg.Right.TypeSpecifier.Name;
                AtomicExpressionNode varNode = (AtomicExpressionNode)arg.Right.Variables[0];
                string name = varNode.FieldIdentifier.ContentString;
                //?
                //AddTypeNamespace(bi, pi.ParameterType);
                // TODO workaround
                switch (type)
                {
                    case "Controller":
                        AddTypeNamespace(bi, typeof(Controller));
                        break;
                    case "IChartRuntime":
                        AddTypeNamespace(bi, typeof(IChartRuntime));
                        break;
                    case "datetime":
                        AddTypeNamespace(bi, typeof(datetime));
                        break;
                    case "symbol":
                        AddTypeNamespace(bi, typeof(symbol));
                        break;
                }
                buffer.Append(type);
                buffer.Append(" ");
                buffer.Append(name);
                if (i < args.Count - 1)
                {
                    buffer.Append(", ");
                }
            }
            buffer.Append(")");
            buffer.Append(NEWLINE);
            buffer.Append(tab3);
            buffer.Append(": base(");
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                AtomicExpressionNode varNode = (AtomicExpressionNode)arg.Right.Variables[0];
                string name = varNode.FieldIdentifier.ContentString;
                buffer.Append(name);
                if (i < args.Count - 1)
                {
                    buffer.Append(", ");
                }
            }
            buffer.Append(")");
            buffer.Append(NEWLINE);
            buffer.Append(tab2);
            buffer.Append("{");
            buffer.Append(NEWLINE);
        }

        private static void GenerateCosntructorFooter(BuildInfo bi, StringBuilder buffer, string cName, bool nowarn = false)
        {
            buffer.Append(tab3);
            buffer.Append("___initialized = true;");
            buffer.Append(NEWLINE);

            buffer.Append(tab3);
            buffer.Append(cName);
            buffer.Append("Props.AddDependencies(this, false);");
            buffer.Append(NEWLINE);

            if (nowarn)
            {
                buffer.Append(tab3);
                buffer.Append("if (!___initialized) Console.Write(0);  // Omit compiler warnings...");
                AddTypeNamespace(bi, typeof(Console));
                buffer.Append(NEWLINE);
            }
            buffer.Append(tab2);
            buffer.Append("}");
            buffer.Append(NEWLINE);
            buffer.Append(NEWLINE);
        }


        static void GenerateSerializationRead(BuildInfo bi, BuildInfo apbi, string tab)
        {
            string apIName = PrettyName(apbi.rmiInterface);
            string apCName = apIName.Substring(1);
            bi.Props_serial_r.Append(tab);
            bi.Props_serial_r.Append(apCName);
            bi.Props_serial_r.Append("Props.SerializationRead(controller, info, context, true);");
            bi.Props_serial_r.Append(NEWLINE);
        }

        static void GenerateSerializationWrite(BuildInfo bi, BuildInfo apbi, string tab)
        {
            string apIName = PrettyName(apbi.rmiInterface);
            string apCName = apIName.Substring(1);
            bi.Props_serial_w.Append(tab);
            bi.Props_serial_w.Append(apCName);
            bi.Props_serial_w.Append("Props.SerializationWrite(controller, info, context, true);");
            bi.Props_serial_w.Append(NEWLINE);
        }

        static void GenerateRmiGet(BuildInfo bi, BuildInfo apbi, string tab)
        {
            string apIName = PrettyName(apbi.rmiInterface);
            string apCName = apIName.Substring(1);
            bi.Props_rmiGet.Append(tab);
            bi.Props_rmiGet.Append("if (");
            bi.Props_rmiGet.Append(apCName);
            bi.Props_rmiGet.Append("Props.RmiGetProperty(controller, propertyId, out value, true)) {");
            bi.Props_rmiGet.Append(NEWLINE);
            bi.Props_rmiGet.Append(tab);
            bi.Props_rmiGet.Append(tab1);
            bi.Props_rmiGet.Append("return true;");
            bi.Props_rmiGet.Append(NEWLINE);
            bi.Props_rmiGet.Append(tab);
            bi.Props_rmiGet.Append("}");
            bi.Props_rmiGet.Append(NEWLINE);
        }
        static void GenerateRmiSet(BuildInfo bi, BuildInfo apbi, string tab)
        {
            string apIName = PrettyName(apbi.rmiInterface);
            string apCName = apIName.Substring(1);
            bi.Props_rmiSet.Append(tab);
            bi.Props_rmiSet.Append("if (");
            bi.Props_rmiSet.Append(apCName);
            bi.Props_rmiSet.Append("Props.RmiSetProperty(controller, propertyId, value, true)) {");
            bi.Props_rmiSet.Append(NEWLINE);
            bi.Props_rmiSet.Append(tab);
            bi.Props_rmiSet.Append(tab1);
            bi.Props_rmiSet.Append("return true;");
            bi.Props_rmiSet.Append(NEWLINE);
            bi.Props_rmiSet.Append(tab);
            bi.Props_rmiSet.Append("}");
            bi.Props_rmiSet.Append(NEWLINE);
        }
        static void GenerateInitialize(BuildInfo bi, BuildInfo apbi, string tab)
        {
            string apIName = PrettyName(apbi.rmiInterface);
            string apCName = apIName.Substring(1);
            bi.Props_init.Append(tab);
            bi.Props_init.Append(apCName);
            bi.Props_init.Append("Props.Initialize(controller, buffer, true);");
            bi.Props_init.Append(NEWLINE);
        }

        static void GenerateAddDependencies(BuildInfo bi, BuildInfo apbi, string tab)
        {
            string apIName = PrettyName(apbi.rmiInterface);
            string apCName = apIName.Substring(1);
            bi.Props_deps.Append(tab);
            bi.Props_deps.Append(apCName);
            bi.Props_deps.Append("Props.AddDependencies(controller, true);");
            bi.Props_deps.Append(NEWLINE);
        }



        private static bool FindParentProperty(BuildInfo bi, string name, bool fromParentType, StringBuilder parenttps, out Type parentPropType, out PropertyInfo api)
        {
            parentPropType = null;
            BuildInfo abi = bi;
            api = null;

            StringBuilder b = new StringBuilder();
            while (abi != null)
            {
                Type thisLevelType;
                if (fromParentType)
                {
                    thisLevelType = abi.parentType;
                }
                else
                {
                    thisLevelType = abi.rmiInterface;
                }

                b.Insert(0, PrettyName(thisLevelType));
                b.Insert(0, ",");
                api = thisLevelType.GetProperty(name);
                if (api != null)
                {
                    parentPropType = api.PropertyType;
                    break;
                }
                else
                {
                    abi = abi.parent;
                }
            }
            if (b.Length > 0)
            {
                b.Remove(0, 1);
            }
            if (parenttps.Length > 0)
            {
                parenttps.Append(";");
            }
            parenttps.Append(b);

            if (fromParentType && parentPropType == null)
            {
                foreach (BuildInfo apbi in bi.additionalParents)
                {
                    FindParentProperty(apbi, name, false, parenttps, out parentPropType, out api);
                    if (parentPropType != null)
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void AddTypeNamespace(BuildInfo bi, Type type)
        {
            bi.usedNamespaces.Add(type.Namespace);
            foreach (var ga in type.GetGenericArguments())
            {
                AddTypeNamespace(bi, ga);
            }
        }

        private static string PrettyFullName(Type type)
        {
            if (type == typeof(void))
            {
                return "void";
            }
            else
            {
                return type.Namespace + "." + PrettyName(type);
            }
        }

        private static string PrettyName(Type type)
        {
            string result = PrettyName(type, false);
            return result;
        }

        private static string PrettyName(Type type, bool isOut)
        {
            if (type == typeof(void))
            {
                return "void";
            }
            else
            {
                var typeDefeninition = type.Name;
                if (typeDefeninition[typeDefeninition.Length - 1] == '&')
                {
                    typeDefeninition = (isOut ? "out " : "ref ") + typeDefeninition.Substring(0, typeDefeninition.Length - 1);
                }


                var genericArguments = type.GetGenericArguments();
                if (genericArguments.Length == 0)
                {
                    return typeDefeninition;
                }
                else
                {
                    var unmangledName = typeDefeninition.Substring(0, typeDefeninition.IndexOf("`"));
                    return unmangledName + "<" + String.Join(",", genericArguments.Select(PrettyName)) + ">";
                }
            }
        }

        static bool IsParams(ParameterInfo param)
        {
            return Attribute.IsDefined(param, typeof(ParamArrayAttribute));
        }
    }
}