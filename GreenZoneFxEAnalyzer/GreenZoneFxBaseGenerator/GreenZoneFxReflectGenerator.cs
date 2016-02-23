using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using GreenZoneUtil.Util;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.IO;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Web;
using GreenZoneParser.Reflect;

namespace GreenZoneFxReflectGenerator
{
    public class GreenZoneFxReflectGenerator
    {
        const string GEN_FOLDER = "F:/workspaces/general_web/ForexRobots/windows_dll/GreenZoneFxEAnalyzer/GreenZoneFxBaseGEx/generated/";

        const string NEWLINE = "\r\n";
        const string tab1 = "\t";
        const string tab2 = "\t\t";
        const string tab3 = "\t\t\t";
        const string tab4 = "\t\t\t\t";
        const string tab5 = "\t\t\t\t\t";
        const string tab6 = "\t\t\t\t\t\t";

        public static void Generate()
        {

            List<Assembly> asms = new List<Assembly>();
            PureReflResolver.FindAssemblies(AppDomain.CurrentDomain, asms);

            bool nonpublic = true;

            int i = 0;
            foreach (Assembly asm in asms)
            {
                if (asm.GetName().Name.StartsWith("GreenZone"))
                {

                    StringBuilder xml = new StringBuilder(10000000);

                    i++;
                    Console.WriteLine(i + " of " + asms.Count + " : " + asm.FullName);
                    xml.Append("<assembly n=\"");
                    xml.Append(asm.FullName);
                    xml.Append("\">");
                    xml.Append(NEWLINE);

                    int j = 0;
                    int k = 0;
                    Type[] types = asm.GetTypes();
                    foreach (Type type in types)
                    {
                        j++;
                        if ((int)(10.0 * j / types.Length) > (int)(10.0 * k / types.Length))
                        {
                            Console.WriteLine(10 * (int)(10.0 * j / types.Length) + " %");
                            k = j;
                        }
                        GenerateType(xml, tab1, type, false, nonpublic);
                    }

                    xml.Append("</assembly>");
                    xml.Append(NEWLINE);
                    File.WriteAllText(GEN_FOLDER + "reflection-info-" + asm.GetName().Name + ".xml", xml.ToString(), Encoding.UTF8);
                }
            }

        }

        static void GenerateMember(StringBuilder xml, string tabs0, PropertyInfo[] allProps, MemberInfo member, bool nonpublic)
        {
            if (PureReflResolver.IsGenerated(member))
            {
                Console.WriteLine("WARN Not generating   member:" + member.Name);
                return;
            }

            if (member is MethodInfo)
            {
                GenerateMethod(xml, tabs0, allProps, (MethodInfo)member);
            }
            else if (member is ConstructorInfo)
            {
                GenerateConstructor(xml, tabs0, (ConstructorInfo)member);
            }
            else if (member is PropertyInfo)
            {
                GenerateProperty(xml, tabs0, (PropertyInfo)member);
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
                GenerateType(xml, tabs0, (Type)member, true, nonpublic);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        static void GenerateType(StringBuilder xml, string tabs0, Type type, bool hasParent, bool nonpublic)
        {
            if (hasParent)
            {
                if (type.DeclaringType == null)
                {
                    throw new NotSupportedException();
                }
            }
            else
            {
                if (type.DeclaringType != null)
                {
                    return;
                }
            }
            if (PureReflResolver.IsGenerated(type) && type.Name != "Resources" && type.Name != "Settings")
            {
                Console.WriteLine("WARN Not generating   type:"+type.Name);
                return;
            }

            xml.Append(tabs0);
            xml.Append("<type n=\"");
            xml.Append(PureReflResolver.TypeCode(type, true));
            xml.Append("\" ns=\"");
            xml.Append(type.Namespace);
            xml.Append("\" id=\"");
            xml.Append(type.MetadataToken);

            string mod = PureReflResolver.GenerateModifiers(type);
            xml.Append("\" mod=\"");
            xml.Append(mod);

            xml.Append("\" t=\"");

            bool hasBody;
            if (type.IsClass)
            {
                hasBody = true;
                if (PureReflResolver.IsDelegate(type))
                {
                    xml.Append("de");
                }
                else
                {
                    xml.Append("cl");
                }
            }
            else if (type.IsInterface)
            {
                hasBody = true;
                xml.Append("in");
            }
            else if (type.IsEnum)
            {
                hasBody = false;
                xml.Append("en");
            }
            else if (PureReflResolver.IsStruct(type))
            {
                hasBody = true;
                xml.Append("st");
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
                if (type.BaseType != null && type.BaseType != typeof(object))
                {
                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("<base-t n=\"");
                    xml.Append(PureReflResolver.TypeCode(type.BaseType));
                    xml.Append("\" id=\"");
                    xml.Append(type.BaseType.MetadataToken);
                    xml.Append("\"/>");
                    xml.Append(NEWLINE);
                }

                var baseInterfaces = PureReflResolver.GetBaseInterfaces(type);

                foreach (Type basei in baseInterfaces)
                {
                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("<base-i n=\"");
                    xml.Append(PureReflResolver.TypeCode(basei));
                    xml.Append("\" id=\"");
                    xml.Append(basei.MetadataToken);
                    xml.Append("\"/>");
                    xml.Append(NEWLINE);
                }

                GenerateGenericConstraints(xml, tabs0 + tab1, type.GetGenericArguments());

                MemberInfo[] mis = type.GetMembers((nonpublic ? BindingFlags.NonPublic : 0) | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
                PureReflResolver.SortMembers(mis, false);

                PropertyInfo[] allProps = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

                foreach (var m in mis)
                {
                    GenerateMember(xml, tabs0 + tab1, allProps, m, nonpublic);
                }
            }
            else if (type.IsEnum)
            {
                xml.Append(tabs0);
                xml.Append(tab1);
                xml.Append("<base n=\"");
                xml.Append(PureReflResolver.TypeCode(type.GetEnumUnderlyingType()));
                xml.Append("\" id=\"");
                xml.Append(type.GetEnumUnderlyingType().MetadataToken);
                xml.Append("\"/>");
                xml.Append(NEWLINE);

                string[] names = type.GetEnumNames();

                Array values;
                try
                {
                    values = type.GetEnumValues();
                }
                catch (NotSupportedException)
                {
                    values = null;
                }
                for (int i = 0; i < names.Length; i++)
                {
                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("<item n=\"");
                    xml.Append(names[i]);
                    if (values != null)
                    {
                        xml.Append("\" v=\"");
                        xml.Append(Convert.ToDecimal((Enum)values.GetValue(i)));
                    }
                    xml.Append("\"/>");
                    xml.Append(NEWLINE);
                }
            }

            xml.Append(tabs0);
            xml.Append("</type>");
            xml.Append(NEWLINE);
        }

        static void GenerateConstructor(StringBuilder xml, string tabs0, ConstructorInfo constructor)
        {
            xml.Append(tabs0);
            xml.Append("<constr id=\"");
            xml.Append(constructor.MetadataToken);

            string mod = PureReflResolver.GenerateModifiers(constructor);
            xml.Append("\" mod=\"");
            xml.Append(mod);

            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, constructor);

            GenerateParameters(xml, tabs0 + tab1, PureReflResolver.IsExtension(constructor), constructor.GetParameters());

            xml.Append(tabs0);
            xml.Append("</constr>");
            xml.Append(NEWLINE);
        }

        static void GenerateMethod(StringBuilder xml, string tabs0, PropertyInfo[] allProps, MethodInfo method)
        {
            bool isPropertyAccessor = false;
            if (allProps != null)
            {
                var props = from p in allProps
                            where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
                            select p;

                foreach (var p in props)
                {
                    isPropertyAccessor = true;
                    break;
                }
            }

            if (!isPropertyAccessor)
            {
                xml.Append(tabs0);
                xml.Append("<method n=\"");

                string mn = GenerateMethodName(method);
                xml.Append(mn);

                xml.Append("\" id=\"");
                xml.Append(method.MetadataToken);
                xml.Append("\" rt=\"");
                xml.Append(PureReflResolver.TypeCode(method.ReturnType));

                string mod = PureReflResolver.GenerateModifiers(method);
                xml.Append("\" mod=\"");
                xml.Append(mod);

                xml.Append("\">");
                xml.Append(NEWLINE);

                GenerateAttributes(xml, tabs0 + tab1, method);

                GenerateParameters(xml, tabs0 + tab1, PureReflResolver.IsExtension(method), method.GetParameters());

                GenerateGenericConstraints(xml, tabs0 + tab1, method.GetGenericArguments());

                xml.Append(tabs0);
                xml.Append("</method>");
                xml.Append(NEWLINE);
            }
        }

        static void GenerateProperty(StringBuilder xml, string tabs0, PropertyInfo property)
        {
            if (!property.CanRead && !property.CanWrite)
            {
                throw new NotSupportedException();
            }
            xml.Append(tabs0);
            xml.Append("<prop n=\"");
            if (property.GetIndexParameters().Length > 0)
            {
                xml.Append("[]");
            }
            xml.Append(property.Name);
            xml.Append("\" id=\"");
            xml.Append(property.MetadataToken);
            xml.Append("\" t=\"");
            xml.Append(PureReflResolver.TypeCode(property.PropertyType));
            xml.Append("\" r=\"");
            xml.Append(property.CanRead);
            xml.Append("\" w=\"");
            xml.Append(property.CanWrite);

            string mod = PureReflResolver.GenerateModifiers(property);
            xml.Append("\" mod=\"");
            xml.Append(mod);

            if (property.CanRead && property.CanWrite)
            {
                string mod2 = PureReflResolver.GenerateModifiers(property.GetSetMethod(true));
                if (!mod.Equals(mod2))
                {
                    xml.Append("\" set-mod=\"");
                    xml.Append(mod2);
                }
            }

            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, property);

            if (property.GetIndexParameters().Length > 0)
            {
                GenerateParameters(xml, tabs0 + tab1, false, property.GetIndexParameters());
            }

            //if (property.CanRead)
            //{
            //    GenerateMethod(xml, tabs0 + tab1, null, property.GetGetMethod(true));
            //}
            //if (property.CanWrite)
            //{
            //    GenerateMethod(xml, tabs0 + tab1, null, property.GetSetMethod(true));
            //}

            xml.Append(tabs0);
            xml.Append("</prop>");
            xml.Append(NEWLINE);
        }

        static void GenerateField(StringBuilder xml, string tabs0, FieldInfo field)
        {
            xml.Append(tabs0);
            xml.Append("<field n=\"");
            xml.Append(field.Name);
            xml.Append("\" id=\"");
            xml.Append(field.MetadataToken);
            xml.Append("\" t=\"");
            xml.Append(PureReflResolver.TypeCode(field.FieldType));

            try
            {
                if (field.GetRawConstantValue() != null)
                {
                    xml.Append("\" const=\"");
                    xml.Append(GreenZoneSysUtilsBase.EscapeXml(field.GetRawConstantValue()));
                }
            }
            catch (InvalidOperationException)
            {
            }

            string mod = PureReflResolver.GenerateModifiers(field);
            xml.Append("\" mod=\"");
            xml.Append(mod);

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
            xml.Append("<event n=\"");
            xml.Append(evt.Name);
            xml.Append("\" id=\"");
            xml.Append(evt.MetadataToken);
            xml.Append("\" t=\"");
            xml.Append(PureReflResolver.TypeCode(evt.EventHandlerType));

            string mod = PureReflResolver.GenerateModifiers(evt);
            xml.Append("\" mod=\"");
            xml.Append(mod);

            xml.Append("\">");
            xml.Append(NEWLINE);

            GenerateAttributes(xml, tabs0 + tab1, evt);

            xml.Append(tabs0);
            xml.Append("</event>");
            xml.Append(NEWLINE);
        }

        static void GenerateAttributes(StringBuilder xml, string tabs0, MemberInfo member)
        {
            GenerateAttributes(xml, tabs0, "attrs", member.GetCustomAttributesData());
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
                    xml.Append("<attr t=\"");
                    xml.Append(PureReflResolver.TypeCode(attr.Constructor.DeclaringType));
                    xml.Append("\" constr=\"");
                    xml.Append(attr.Constructor.MetadataToken);
                    xml.Append("\">");
                    xml.Append(NEWLINE);

                    if (attr.ConstructorArguments.Count > 0)
                    {
                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("<args>");
                        xml.Append(NEWLINE);

                        foreach (CustomAttributeTypedArgument arg in attr.ConstructorArguments)
                        {
                            xml.Append(tabs0);
                            xml.Append(tab3);
                            xml.Append("<arg t=\"");
                            xml.Append(PureReflResolver.TypeCode(arg.ArgumentType));
                            xml.Append("\" v=\"");
                            xml.Append(GreenZoneSysUtilsBase.EscapeXml(arg.Value));
                            xml.Append("\"/>");
                            xml.Append(NEWLINE);
                        }

                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("</args>");
                        xml.Append(NEWLINE);
                    }

                    if (attr.NamedArguments.Count > 0)
                    {
                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("<named-args>");
                        xml.Append(NEWLINE);

                        foreach (CustomAttributeNamedArgument arg in attr.NamedArguments)
                        {
                            xml.Append(tabs0);
                            xml.Append(tab3);
                            xml.Append("<arg t=\"");
                            xml.Append(PureReflResolver.TypeCode(arg.TypedValue.ArgumentType));
                            xml.Append("\" v=\"");
                            xml.Append(GreenZoneSysUtilsBase.EscapeXml(arg.TypedValue.Value));
                            xml.Append("\" p=\"");
                            xml.Append(arg.MemberInfo.Name);
                            xml.Append("\"/>");
                            xml.Append(NEWLINE);
                        }

                        xml.Append(tabs0);
                        xml.Append(tab2);
                        xml.Append("</named-args>");
                        xml.Append(NEWLINE);
                    }


                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("</attr>");
                    xml.Append(NEWLINE);
                }

                xml.Append(tabs0);
                xml.Append("</");
                xml.Append(tag);
                xml.Append(">");
                xml.Append(NEWLINE);
            }
        }

        static void GenerateGenericConstraints(StringBuilder xml, string tabs0, Type[] genericArguments)
        {
            for (int i = 0; i < genericArguments.Length; i++)
            {
                Type tparg = genericArguments[i];
                GenericParameterAttributes gpa = tparg.GenericParameterAttributes;
                Type[] tpConstraints = tparg.GetGenericParameterConstraints();
                if (tpConstraints.Length > 0)
                {
                    xml.Append(tabs0);
                    xml.Append("<gc type-is=\"");
                    xml.Append(i);
                    xml.Append("\">");
                    xml.Append(NEWLINE);
                }

                for (int j = 0; j < tpConstraints.Length; j++)
                {
                    xml.Append(tabs0);
                    xml.Append(tab1);
                    xml.Append("<rule n=\"");


                    if ((gpa & GenericParameterAttributes.DefaultConstructorConstraint) != 0)
                    {
                        xml.Append("new()");
                    }
                    else if ((gpa & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
                    {
                        xml.Append("class");
                    }
                    else if ((gpa & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
                    {
                        xml.Append("struct");
                    }
                    else if ((gpa & GenericParameterAttributes.Covariant) != 0)
                    {
                        xml.Append("cov");
                    }
                    else if ((gpa & GenericParameterAttributes.Contravariant) != 0)
                    {
                        xml.Append("contr");
                    }
                    else
                    {
                        Type tpc = tpConstraints[j];
                        xml.Append(PureReflResolver.TypeCode(tpc));
                    }

                    xml.Append("\"/>");

                    xml.Append(NEWLINE);
                }

                if (tpConstraints.Length > 0)
                {
                    xml.Append(tabs0);
                    xml.Append("</gc>");
                    xml.Append(NEWLINE);
                }
            }
        }

        static void GenerateParameters(StringBuilder xml, string tabs0, bool isExtension, IList<ParameterInfo> parameters)
        {
            int i = 0;
            foreach (ParameterInfo pi in parameters)
            {
                xml.Append(tabs0);
                xml.Append("<param t=\"");
                xml.Append(PureReflResolver.TypeCode(pi.ParameterType));
                xml.Append("\" n=\"");
                xml.Append(pi.Name);
                if (PureReflResolver.HasDefault(pi))
                {
                    xml.Append("\" default=\"");
                    xml.Append(pi.RawDefaultValue);
                }

                if (PureReflResolver.IsParams(pi))
                {
                    xml.Append("\" p=\"Params");
                }
                else if (pi.IsOut)
                {
                    xml.Append("\" p=\"Out");
                }
                else if (pi.IsRetval)
                {
                    xml.Append("\" p=\"Ref");
                }
                else if (isExtension && i == 0)
                {
                    xml.Append("\" p=\"This");
                }
                xml.Append("\">");
                xml.Append(NEWLINE);

                GenerateAttributes(xml, tabs0 + tab1, "attrs", pi.GetCustomAttributesData());

                xml.Append(tabs0);
                xml.Append("</param>");
                xml.Append(NEWLINE);

                i++;
            }
        }

        static string GenerateMethodName(MethodInfo method)
        {
            StringBuilder mn = new StringBuilder();
            if (method.GetGenericArguments().Length > 0)
            {
                mn.Append("G");
                mn.Append(method.GetGenericArguments().Length);
                foreach (Type ta in method.GetGenericArguments())
                {
                    mn.Append(PureReflResolver.TypeCode(ta, false, false));
                }
            }
            else
            {
                mn.Append("T");
            }
            mn.Append(method.Name);
            return mn.ToString();
        }



        

    }
}