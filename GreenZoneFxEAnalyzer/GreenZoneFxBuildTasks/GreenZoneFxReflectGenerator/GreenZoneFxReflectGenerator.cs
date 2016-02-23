using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using GreenZoneUtil.Util;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace GreenZoneFxReflectGenerator
{
    public class GreenZoneFxReflectGenerator
    {
        const string NEWLINE = "\r\n";
        const string tab1 = "\t";
        const string tab2 = "\t\t";
        const string tab3 = "\t\t\t";
        const string tab4 = "\t\t\t\t";
        const string tab5 = "\t\t\t\t\t";
        const string tab6 = "\t\t\t\t\t\t";

        public static void Generate()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<xml>");

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                xml.Append(tab1);
                xml.Append("<assembly name=\"");
                xml.Append(asm.FullName);
                xml.Append("\">");
                xml.Append(NEWLINE);

                foreach (Type type in asm.GetTypes())
                {
                    GenerateType(xml, tab2, type);
                }

                xml.Append(tab1);
                xml.Append("</assembly>");
                xml.Append(NEWLINE);
            }
            xml.Append("</xml>");
            xml.Append(NEWLINE);
        }

        static void GenerateType(StringBuilder xml, string tabs0, Type type)
        {
            xml.Append(tabs0);
            xml.Append("<type name=\"");
            xml.Append(TypeCode(type));
            xml.Append("\" id=\"");
            xml.Append(type.MetadataToken);
            xml.Append("\" type=\"");
            bool hasBody;
            if (type.IsClass)
            {
                hasBody = true;
                xml.Append("class");
            }
            if (type.IsInterface)
            {
                hasBody = true;
                xml.Append("interface");
            }
            else if (type.IsEnum)
            {
                xml.Append("enum");
            }
            else if (type.IsValueType)
            {
                hasBody = true;
                xml.Append("struct");
            }
            else
            {
                throw new NotSupportedException();
            }
            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, type);

            if (hasBody)
            {
                foreach (var c in type.GetConstructors(BindingFlags.DeclaredOnly))
                {
                    GenerateConstructor(xml, tabs0 + tab1, c);
                }

                foreach (var m in type.GetMembers(BindingFlags.DeclaredOnly))
                {
                    GenerateMember(xml, tabs0 + tab1, m);
                }
            }

            xml.Append(tabs0);
            xml.Append("</type>");
            xml.Append(NEWLINE);
        }

        static void GenerateConstructor(StringBuilder xml, string tabs0, ConstructorInfo constructor)
        {
            xml.Append(tabs0);
            xml.Append("<constructor id=\"");
            xml.Append(constructor.MetadataToken);
            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, constructor);

            GenerateParameters(xml, tabs0 + tab1, IsExtension(constructor), constructor.GetParameters());

            xml.Append(tabs0);
            xml.Append("</constructor>");
            xml.Append(NEWLINE);
        }

        static void GenerateMember(StringBuilder xml, string tabs0, MemberInfo member)
        {
            if (member is MethodInfo)
            {
                GenerateMethod(xml, tabs0, (MethodInfo)member);
            }
            else if (member is FieldInfo)
            {
                GenerateField(xml, tabs0, (FieldInfo)member);
            }
            else if (member is EventInfo)
            {
                GenerateEvent(xml, tabs0, (EventInfo)member);
            }
            else if (member is Type)
            {
                GenerateType(xml, tabs0, (Type)member);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        static void GenerateMethod(StringBuilder xml, string tabs0, MethodInfo method)
        {
            xml.Append(tabs0);
            xml.Append("<method name=\"");
            xml.Append(method.Name);
            xml.Append("\" id=\"");
            xml.Append(method.MetadataToken);
            xml.Append("\" return-type=\"");
            xml.Append(TypeCode(method.ReturnType));
            if (method.GetGenericArguments().Length > 0)
            {
                xml.Append("\" generic=\"");

                xml.Append(method.GetGenericArguments().Length);
                foreach (Type ta in method.GetGenericArguments())
                {
                    xml.Append(TypeCode(ta));
                }
            }
            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, "return-attributes", new BridgeCollection<CustomAttributeData>(method.ReturnTypeCustomAttributes.GetCustomAttributes(false).ToList<object>()));

            GenerateAttributes(xml, tabs0 + tab1, method);

            GenerateParameters(xml, tabs0 + tab1, IsExtension(method), method.GetParameters());

            xml.Append(tabs0);
            xml.Append("</method>");
            xml.Append(NEWLINE);
        }

        static void GenerateField(StringBuilder xml, string tabs0, FieldInfo field)
        {
            xml.Append(tabs0);
            xml.Append("<field name=\"");
            xml.Append(field.Name);
            xml.Append("\" id=\"");
            xml.Append(field.MetadataToken);
            xml.Append("\" type=\"");
            xml.Append(TypeCode(field.FieldType));
            
            if (field.GetRawConstantValue() != null)
            {
                xml.Append("\" constant=\"");
                xml.Append(field.GetRawConstantValue());
            }

            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, field);

            xml.Append(tabs0);
            xml.Append("</field>");
            xml.Append(NEWLINE);
        }

        static void GenerateEvent(StringBuilder xml, string tabs0, EventInfo evt)
        {
            xml.Append(tabs0);
            xml.Append("<event name=\"");
            xml.Append(evt.Name);
            xml.Append("\" id=\"");
            xml.Append(evt.MetadataToken);
            xml.Append("\" type=\"");
            xml.Append(TypeCode(evt.EventHandlerType));
            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, evt);

            xml.Append(tabs0);
            xml.Append("</event>");
            xml.Append(NEWLINE);
        }

        static void GenerateAttributes(StringBuilder xml, string tabs0, MemberInfo member)
        {
            GenerateAttributes(xml, tabs0, "attributes", member.GetCustomAttributesData());
        }

        static void GenerateAttributes(StringBuilder xml, string tabs0, string tag, IList<CustomAttributeData> attrs)
        {
            if (attrs.Count > 0)
            {
                xml.Append(tabs0);
                xml.Append("<");
                xml.Append(tag);
                xml.Append(">");
                xml.Append(NEWLINE);

                foreach (CustomAttributeData attr in attrs)
                {
                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("<attribute type=\"");
                    xml.Append(TypeCode(attr.Constructor.DeclaringType));
                    xml.Append("\" constructor=\"");
                    xml.Append(attr.Constructor.MetadataToken);
                    xml.Append("\">");
                    xml.Append(NEWLINE);

                    if (attr.ConstructorArguments.Count > 0)
                    {
                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("<arguments>");
                        xml.Append(NEWLINE);

                        foreach (CustomAttributeTypedArgument arg in attr.ConstructorArguments)
                        {
                            xml.Append(tabs0);
                            xml.Append(tab3);
                            xml.Append("<argument type=\"");
                            xml.Append(TypeCode(arg.ArgumentType));
                            xml.Append("\" value=\"");
                            xml.Append(arg.Value);
                            xml.Append("\"/>");
                            xml.Append(NEWLINE);
                        }

                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("</arguments>");
                        xml.Append(NEWLINE);
                    }

                    if (attr.NamedArguments.Count > 0)
                    {
                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("<named-arguments>");
                        xml.Append(NEWLINE);

                        foreach (CustomAttributeNamedArgument arg in attr.NamedArguments)
                        {
                            xml.Append(tabs0);
                            xml.Append(tab3);
                            xml.Append("<argument type=\"");
                            xml.Append(TypeCode(arg.TypedValue.ArgumentType));
                            xml.Append("\" value=\"");
                            xml.Append(arg.TypedValue.Value);
                            xml.Append("\" property=\"");
                            xml.Append(arg.MemberInfo.Name);
                            xml.Append("\"/>");
                            xml.Append(NEWLINE);
                        }

                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("</named-arguments>");
                        xml.Append(NEWLINE);
                    }


                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("</attribute>");
                    xml.Append(NEWLINE);
                }

                xml.Append(tabs0);
                xml.Append("</");
                xml.Append(tag);
                xml.Append(">");
                xml.Append(NEWLINE);
            }
        }

        static void GenerateParameters(StringBuilder xml, string tabs0, bool isExtension, IList<ParameterInfo> parameters)
        {
            int i = 0;
            foreach (ParameterInfo pi in parameters)
            {
                xml.Append(tabs0);
                xml.Append(tab1);
                xml.Append("<parameter type=\"");
                xml.Append(TypeCode(pi.ParameterType));
                xml.Append("\" name=\"");
                xml.Append(pi.Name);
                if (HasDefault(pi))
                {
                    xml.Append("\" default=\"");
                    xml.Append(pi.RawDefaultValue);
                }
                
                if (IsParams(pi))
                {
                    xml.Append("\" type=\"params");
                }
                else if (pi.IsOut)
                {
                    xml.Append("\" type=\"out");
                }
                else if (pi.IsRetval)
                {
                    xml.Append("\" type=\"ref");
                }
                else if (isExtension && i == 0)
                {
                    xml.Append("\" type=\"this");
                }
                xml.Append("\">");
                xml.Append(NEWLINE);

                GenerateAttributes(xml, tabs0 + tab2, "attributes", pi.GetCustomAttributesData());

                xml.Append(tabs0);
                xml.Append(tab1);
                xml.Append("</parameter>");
                xml.Append(NEWLINE);

                i++;
            }
        }

        static string TypeCode(Type type)
        {
            switch (type.FullName)
            {
                case "System.Void":
                    return "v";
                case "System.Boolean":
                    return "o";
                case "System.Byte":
                    return "b";
                case "System.SByte":
                    return "B";
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
                default:
                    StringBuilder result = new StringBuilder();
                    if (type.IsArray)
                    {
                        result.Append("A");
                        result.Append(type.GetArrayRank());
                        result.Append(TypeCode(type.GetElementType()));
                    }
                    else if (type.GetGenericArguments().Length > 0)
                    {
                        result.Append("G");
                        result.Append(type.GetGenericArguments().Length);
                        foreach (Type ta in type.GetGenericArguments())
                        {
                            result.Append(TypeCode(ta));
                        }
                    }
                    else
                    {
                        var typeDefeninition = type.FullName;
                        if (typeDefeninition[typeDefeninition.Length - 1] == '&')
                        {
                            typeDefeninition = typeDefeninition.Substring(0, typeDefeninition.Length - 1);
                        }

                        result.Append("T");
                        result.Append(typeDefeninition);
                        result.Append(";");
                    }
                    return result.ToString();
            }
        }

        static bool IsParams(ParameterInfo param)
        {
            return Attribute.IsDefined(param, typeof(ParamArrayAttribute));
        }

        static bool IsExtension(MethodBase method)
        {
            return Attribute.IsDefined(method, typeof(ExtensionAttribute));
        }

        static bool HasDefault(ParameterInfo param)
        {
            return Attribute.IsDefined(param, typeof(DefaultParameterValueAttribute));
        }
    }
}
