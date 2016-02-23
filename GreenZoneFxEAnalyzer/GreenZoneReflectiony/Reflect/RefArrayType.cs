using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflArrayType : ReflType, TypeNameItem
    {
        internal ReflArrayType(Resolver resolver, IParser parser, INode parseNode, int dims, ReflType elementType)
            : base(resolver, parser, parseNode, -1, (string)null, "array", 0)
        {
            this.dims = dims;
            this.elementType = elementType;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.ArrayType;
            }
        }

        readonly int dims;
        public int Dims
        {
            get
            {
                return dims;
            }
        }

        readonly ReflType elementType;
        public ReflType ElementType
        {
            get
            {
                return elementType;
            }
        }

        bool TypeNameItem.IsArray
        {
            get { return true; }
        }

        TypeNameItem TypeNameItem.ElementType
        {
            get { return elementType; }
        }

        int TypeNameItem.Dims
        {
            get { return dims; }
        }
    }
}
