using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Text;
using GreenZoneUtil.Util;
using System.ComponentModel;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{

    public class Mt4ExecutableInfo : RmiBase, IComparable
    {
        public Mt4ExecutableInfo(GreenRmiManager rmiManager, Mt4ExecutableType executableType, string systemTypeId, ExecutableLoadType loadType)
            : base(rmiManager)
        {
            if (systemTypeId == null)
            {
                throw new ArgumentNullException("systemTypeId");
            }
            ExecutableType = executableType;
            SystemTypeId = systemTypeId;
            LoadType = loadType;
        }

        public Mt4ExecutableInfo(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            SystemTypeId = (string)buffer.ChangedProps[0];
            if (SystemTypeId == null)
            {
                throw new ArgumentNullException("SystemTypeId");
            }
            ExecutableType = (Mt4ExecutableType)buffer.ChangedProps[2];
            LoadType = (ExecutableLoadType)buffer.ChangedProps[3];
        }

        protected Mt4ExecutableInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SystemTypeId = (string)info.GetValue("SystemTypeId", typeof(string));
            ExecutableType = (Mt4ExecutableType)info.GetValue("ExecutableType", typeof(Mt4ExecutableType));
            LoadType = (ExecutableLoadType)info.GetValue("LoadType", typeof(ExecutableLoadType));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SystemTypeId", SystemTypeId);
            info.AddValue("ExecutableType", ExecutableType);
            info.AddValue("LoadType", LoadType);
        }

        public Mt4ExecutableType ExecutableType
        {
            get;
            private set;
        }

        public string SystemTypeId
        {
            get;
            private set;
        }

        public string ShortTypeName
        {
            get
            {
                string[] stid = SystemTypeId.Split('.');
                return stid[stid.Length - 1];
            }
        }

        public ExecutableLoadType LoadType
        {
            get;
            private set;
        }

        ReflObjType systemType;
        public ReflObjType SystemType
        {
            get
            {
                if (systemType == null)
                {
                    systemType = resolveType();
                }

                return systemType;
            }
        }

        ExecAttribute attribute;
        ExecAttribute Attribute
        {
            get
            {
                if (attribute == null)
                {
                    foreach (var a in GreenZoneUtilsBase.GetCustomAttributes(rmiManager.Resolver.GetType(this), typeof(ExecAttribute)))
                    {
                        attribute = (ExecAttribute)a;
                        break;
                    }
                }
                return attribute;
            }
        }

        public string Name
        {
            get
            {
                ExecAttribute a = Attribute;
                if (a == null || a.Name == null)
                {
                    return ShortTypeName;
                }
                else
                {
                    return a.Name;
                }
            }
        }

        public string Description
        {
            get
            {
                ExecAttribute a = Attribute;
                if (a == null || a.Description == null)
                {
                    return null;
                }
                else
                {
                    return a.Description;
                }
            }
        }

        public string[] Categories
        {
            get
            {
                ExecAttribute a = Attribute;
                if (a == null || a.Categories == null)
                {
                    return null;
                }
                else
                {
                    return a.Categories;
                }
            }
        }

        public string FullName
        {
            get
            {
                StringBuilder b = new StringBuilder();
                b.Append(Name);
                string d = Description;
                if (d != null)
                {
                    b.Append(" - ");
                    b.Append(d);
                }
                return b.ToString();
            }
        }

        public override string ToString()
        {
            return SystemTypeId;
        }

        public override bool Equals(object obj)
        {
            Mt4ExecutableInfo o = (Mt4ExecutableInfo)obj;
            if (SystemTypeId == null)
            {
                return o == null || o.SystemTypeId == null;
            }
            else if (o == null || o.SystemTypeId == null)
            {
                return false;
            }
            else
            {
                return SystemTypeId.Equals(o.SystemTypeId);
            }
        }

        public override int GetHashCode()
        {
            return SystemTypeId == null ? 0 : SystemTypeId.GetHashCode();
        }

        public int CompareTo(object other)
        {
            Mt4ExecutableInfo o = (Mt4ExecutableInfo)other;
            return SystemTypeId == null ? (o == null || o.SystemTypeId == null ? 0 : -1) : SystemTypeId.CompareTo(o.SystemTypeId);
        }


        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case 0:
                    return SystemTypeId;
                case 1:
                    return Name;
                case 2:
                    return ExecutableType;
                case 3:
                    return LoadType;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    break;
                default:
                    throw new NotSupportedException();
            }
        }


        ReflObjType resolveType()
        {
            ReflObjType type = (ReflObjType)rmiManager.Resolver.GetType(SystemTypeId);

            return type;
        }
    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExecAttribute : Attribute
    {
        public readonly string Name;

        public readonly string[] Categories;

        public ExecAttribute()
        {
        }

        public ExecAttribute(string name)
        {
            this.Name = name;
        }

        public ExecAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public ExecAttribute(string name, string description, params string[] categories)
        {
            this.Name = name;
            this.Description = description;
            this.Categories = categories;
        }

        public string Description
        {
            get;
            set;
        }

        public bool System
        {
            get;
            set;
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptAttribute : ExecAttribute
    {
        public ScriptAttribute()
            : base()
        {
        }

        public ScriptAttribute(string name)
            : base(name)
        {
        }

        public ScriptAttribute(string name, string description)
            : base(name, description)
        {
        }

        public ScriptAttribute(string name, string description, params string[] categories)
            : base(name, description, categories)
        {
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExpertAttribute : ExecAttribute
    {
        public ExpertAttribute()
            : base()
        {
        }

        public ExpertAttribute(string name)
            : base(name)
        {
        }

        public ExpertAttribute(string name, string description)
            : base(name, description)
        {
        }

        public ExpertAttribute(string name, string description, params string[] categories)
            : base(name, description, categories)
        {
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class IndicatorAttribute : ExecAttribute
    {
        public IndicatorAttribute()
            : base()
        {
        }

        public IndicatorAttribute(string name)
            : base(name)
        {
        }

        public IndicatorAttribute(string name, string description)
            : base(name, description)
        {
        }

        public IndicatorAttribute(string name, string description, params string[] categories)
            : base(name, description, categories)
        {
        }
    }
}
