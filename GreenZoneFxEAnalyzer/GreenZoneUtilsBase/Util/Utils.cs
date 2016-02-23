using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using GreenZoneUtil.ViewController;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using GreenZoneParser.Reflect;

namespace GreenZoneUtil.Util
{

    public class GreenZoneUtilsBase : GreenZoneSysUtilsBase
    {
        static readonly CmpPiName cmpinm = new CmpPiName();
        static readonly CmpPiMeta cmpimtd = new CmpPiMeta();

        static readonly MyCmpPiName mycmpinm = new MyCmpPiName();
        static readonly MyCmpPiMeta mycmpimtd = new MyCmpPiMeta();

        public static List<ReflType> GetNamespaceClasses(Resolver resolver, string nameSpace, params ReflType[] attributeTypes)
        {
            List<ReflType> classlist = new List<ReflType>();

            foreach (ReflType type in resolver.Types.Values)
            {
                bool allFnd = true;
                foreach (ReflType at in attributeTypes)
                {
                    List<Attribute> attrs = GetCustomAttributes(type, at);
                    allFnd = attrs != null && attrs.Count > 0;
                    if (!allFnd)
                    {
                        break;
                    }
                }
                if (allFnd && (nameSpace == null || type.NamespaceId.Equals(nameSpace)))
                {
                    classlist.Add(type);
                }
            }

            return classlist;
        }

        public static List<ReflType> GetNamespaceClasses(Resolver resolver, string nameSpace, params Type[] attributeTypes)
        {
            List<ReflType> classlist = new List<ReflType>();

            foreach (ReflType type in resolver.Types.Values)
            {
                bool allFnd = true;
                foreach (Type at in attributeTypes)
                {
                    List<Attribute> attrs = GetCustomAttributes(type, at);
                    allFnd = attrs != null && attrs.Count > 0;
                    if (!allFnd)
                    {
                        break;
                    }
                }
                if (allFnd && (nameSpace == null || type.NamespaceId.Equals(nameSpace)))
                {
                    classlist.Add(type);
                }
            }

            return classlist;
        }

        public static List<ReflProperty> GetProperties(Resolver resolver, object obj, BindingFlags flags = BindingFlags.Default, bool sortByName = false)
        {
            ReflObjType type = (ReflObjType)resolver.GetType(obj);
            List<ReflProperty> fields = type.GetMembers<ReflProperty>(flags);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static List<ReflProperty> GetProperties(ReflObjType objType, BindingFlags flags = BindingFlags.Default, bool sortByName = false)
        {
            List<ReflProperty> fields = objType.GetMembers<ReflProperty>(flags);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static void CopyTopLevelProperties(Resolver resolver, object Object1, object Object2)
        {
            List<ReflProperty> fields = GetTopLevelProperties(resolver, Object1);
            foreach (ReflProperty pi in fields)
                pi.SetValue(Object2, pi.GetValue(Object1));
        }


        public static void CopyProperties(Resolver resolver, object Object1, object Object2, List<ReflProperty> fields)
        {
            foreach (ReflProperty pi in fields)
                pi.SetValue(Object2, pi.GetValue(Object1));
        }

        public static List<ReflProperty> GetTopLevelProperties(Resolver resolver, object obj, bool sortByName = false)
        {
            ReflObjType type = (ReflObjType)resolver.GetType(obj);
            List<ReflProperty> fields = type.GetMembers<ReflProperty>(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static List<ReflProperty> GetTopLevelProperties(ReflObjType objType, bool sortByName = false)
        {
            List<ReflProperty> fields = objType.GetMembers<ReflProperty>(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static List<ReflField> GetTopLevelFields(ReflObjType objType, bool sortByName = false)
        {
            List<ReflField> fields = objType.GetMembers<ReflField>(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static List<ReflMethod> GetTopLevelMethods(ReflObjType objType, bool sortByName = false)
        {
            List<ReflMethod> fields = objType.GetMembers<ReflMethod>(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SortMembers(fields, sortByName);
            return fields;
        }

        public static List<Property> GetMyProperties(Resolver resolver, object obj, BindingFlags flags = BindingFlags.Default, bool browsable = false, bool sortByName = false)
        {
            ReflObjType type = (ReflObjType)resolver.GetType(obj);
            List<ReflProperty> fields = type.GetMembers<ReflProperty>(flags);
            SortMembers(fields, sortByName);
            List<Property> result = new List<Property>();
            foreach (var pi in fields)
            {
                if (!browsable || IsBrowsable(pi))
                {
                    Property p = new Property(GetCategory(pi), pi.Name, GetDescription(pi), pi.GetValue(obj), GetDefaultValue(pi), pi.PropertyType.TypeId, pi.Id);
                    result.Add(p);
                }
            }
            return result;
        }

        public static List<Property> CloneProperties(List<Property> properties)
        {
            List<Property> result = new List<Property>();
            foreach (Property p in properties)
            {
                result.Add((Property)p.Clone());
            }
            return result;
        }


        public static bool PropertiesEqual(List<Property> properties1, List<Property> properties2)
        {
            int cnt = properties1.Count;
            if (cnt != properties2.Count)
            {
                return false;
            }
            List<Property> ps1 = new List<Property>(properties1);
            List<Property> ps2 = new List<Property>(properties2);
            SortMembers(ps1, false);
            SortMembers(ps2, false);
            for (int i = 0; i < cnt; i++)
            {
                Property p1 = ps1[i];
                Property p2 = ps2[i];
                if (!p1.Equals(p2))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool PropertiesEqual<A>(Resolver resolver, A Object1, A Object2, bool browsable = false)
        {
            List<ReflProperty> fields = GetProperties(resolver, Object1, BindingFlags.Public | BindingFlags.Instance);
            foreach (ReflProperty pi in fields)
            {
                if (!browsable || IsBrowsable(pi))
                {
                    object v1 = pi.GetValue(Object1);
                    object v2 = pi.GetValue(Object2);
                    bool eq = v1 == null ? v2 == null : v1.Equals(v2);
                    if (!eq)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void CopyProperties<A>(Resolver resolver, A Object1, A Object2, bool browsable = false)
        {
            List<ReflProperty> fields = GetProperties(resolver, Object1, BindingFlags.Public | BindingFlags.Instance);
            foreach (ReflProperty pi in fields)
                pi.SetValue(Object2, pi.GetValue(Object1));
        }

        public static void CopyProperties<A>(Resolver resolver, List<Property> properties, A Object2, bool browsable = false)
        {
            List<ReflProperty> fields = GetProperties(resolver, Object2, BindingFlags.Public | BindingFlags.Instance);
            SortMembers(properties, false);
            for (int i=0; i<fields.Count; i++) {
                Property p = properties[i];
                ReflProperty pi = fields[i];
                pi.SetValue(Object2, p.Value);
            }
        }

        public static object GetProperty(Resolver resolver, object target, string propertyName)
        {
            ReflObjType type = (ReflObjType)resolver.GetType(target);
            ReflProperty prop = type.GetMember<ReflProperty>(propertyName);

            object v = prop.GetValue(target);
            return v;
        }

        public static void SetProperty(Resolver resolver, object target, string propertyName, object propertyValue)
        {
            ReflObjType type = (ReflObjType)resolver.GetType(target);
            ReflProperty prop = type.GetMember<ReflProperty>(propertyName);

            prop.SetValue(target, propertyValue);
        }

        public static bool IsBrowsable(ReflProperty pi)
        {
            ReflType atp = pi.Resolver.GetType(typeof(BrowsableAttribute));

            var attributes = GetCustomAttributes(pi, atp);
            if (attributes == null || attributes.Count == 0)
            {
                return true;
            }
            else
            {
                return ((BrowsableAttribute)attributes[0]).Browsable;
            }
        }

        public static string GetCategory(ReflProperty pi)
        {
            ReflType atp = pi.Resolver.GetType(typeof(CategoryAttribute));

            var attributes = GetCustomAttributes(pi, atp);
            if (attributes == null || attributes.Count == 0)
            {
                return null;
            }
            else
            {
                return ((CategoryAttribute)attributes[0]).Category;
            }
        }

        public static string GetDescription(ReflProperty pi)
        {
            ReflType atp = pi.Resolver.GetType(typeof(DescriptionAttribute));

            var attributes = GetCustomAttributes(pi, atp);
            if (attributes == null || attributes.Count == 0)
            {
                return null;
            }
            else
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
        }

        public static object GetDefaultValue(ReflProperty pi)
        {
            ReflType atp = pi.Resolver.GetType(typeof(DefaultValueAttribute));

            var attributes = GetCustomAttributes(pi, atp);
            if (attributes == null || attributes.Count == 0)
            {
                return null;
            }
            else
            {
                return ((DefaultValueAttribute)attributes[0]).Value;
            }
        }

        public static List<Attribute> GetCustomAttributes(ReflMember m, ReflType attrType)
        {
            var attrs = from a in m.Attributes
                        where attrType.IsAssignableFrom(a.DeclaringType)
                        select a;
            List<Attribute> result = new List<Attribute>();
            foreach (var a in attrs)
            {
                result.Add(a.NativeAttribute);
            }
            return result;
        }

        public static List<Attribute> GetCustomAttributes(ReflMember m, Type attrType)
        {
            var attrs = from a in m.Attributes
                        where attrType.IsAssignableFrom(a.DeclaringType.NativeType)
                        select a;
            List<Attribute> result = new List<Attribute>();
            foreach (var a in attrs)
            {
                result.Add(a.NativeAttribute);
            }
            return result;
        }
        
        // ----------------

        protected static void SortMembers(List<Property> fields, bool sortByName)
        {
            if (sortByName)
            {
                fields.Sort(mycmpinm);
            }
            else
            {
                // sorts by declaration order :
                // info:
                // http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/ab8718e0-d029-4433-9908-02d3a252620c/
                fields.Sort(mycmpimtd);
            }
        }


        public static void SortMembers<T>(List<T> fields, bool sortByName) where T : ReflMember
        {
            if (sortByName)
            {
                fields.Sort(cmpinm);
            }
            else
            {
                // sorts by declaration order :
                // info:
                // http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/ab8718e0-d029-4433-9908-02d3a252620c/
                fields.Sort(cmpimtd);
            }
        }

    }

    class CmpPiName : IComparer<ReflMember>
    {
        public int Compare(ReflMember x, ReflMember y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    class CmpPiMeta : IComparer<ReflMember>
    {
        public int Compare(ReflMember x, ReflMember y)
        {
            return x.Id.CompareTo(y.Id);
        }
    }

    class MyCmpPiName : IComparer<Property>
    {
        public int Compare(Property x, Property y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    class MyCmpPiMeta : IComparer<Property>
    {
        public int Compare(Property x, Property y)
        {
            return x.MetadataToken.CompareTo(y.MetadataToken);
        }
    }

}