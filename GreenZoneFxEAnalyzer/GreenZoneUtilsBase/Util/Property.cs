using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneUtil.Util
{
    public class Property : ICloneable
    {
        public Property(string name, object value, string typeId, int metadataToken)
        {
            this.name = name;
            this.value = value;
            this.typeId = typeId;
            this.metadataToken = metadataToken;
        }

        public Property(string name, object value, object defaultValue, string typeId, int metadataToken)
        {
            this.name = name;
            this.value = value;
            this.defaultValue = defaultValue;
            this.typeId = typeId;
            this.metadataToken = metadataToken;
        }

        public Property(string category, string name, string description, object value, object defaultValue, string typeId, int metadataToken)
        {
            this.category = category;
            this.name = name;
            this.description = description;
            this.value = value;
            this.defaultValue = defaultValue;
            this.typeId = typeId;
            this.metadataToken = metadataToken;
        }

        readonly string category;
        public string Category
        {
            get
            {
                return category;
            }
        }

        readonly string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        readonly string description;
        public string Description
        {
            get
            {
                return description;
            }
        }

        object value;
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        object defaultValue;
        public object DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                this.defaultValue = value;
            }
        }

        readonly string typeId;
        public string TypeId
        {
            get
            {
                return typeId;
            }
        }

        readonly int metadataToken;
        public int MetadataToken
        {
            get
            {
                return metadataToken;
            }
        }

        public override bool Equals(object obj)
        {
            Property p2 = (Property)obj;
            return metadataToken == p2.metadataToken;
        }

        public override int GetHashCode()
        {
            return metadataToken;
        }

        public override string ToString()
        {
            return typeId + " " + name + " = " + value;
        }

        public object Clone()
        {
            Property result = new Property(name, value, defaultValue, typeId, metadataToken);
            return result;
        }
    }
}
