using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneParser.Reflect
{
    public interface TypeNameItem
    {
        bool IsArray
        {
            get;
        }
        
        bool IsPointer
        {
            get;
        }

        TypeNameItem ElementType
        {
            get;
        }

        TypeNameItem ParentType
        {
            get;
        }

        string Namespace
        {
            get;
        }

        string Name
        {
            get;
        }

        int Dims
        {
            get;
        }

        IList<TypeNameItem> GenericArgs
        {
            get;
        }
    }

    public interface ITypeNameBuilder
    {
        string GetFullName(TypeNameItem type, bool isLeaf = true);
        string GetGenericId(TypeNameItem type, bool isLeaf = true);
        string GetTypeId(TypeNameItem type, bool isLeaf = true);
    }

    internal class CsTypeNameBuilder : ITypeNameBuilder
    {
        public string GetFullName(TypeNameItem type, bool isLeaf = true)
        {
            StringBuilder result = new StringBuilder();
            if (type.IsArray)
            {
                result.Append(GetFullName(type.ElementType));
                result.Append('[').Append(type.Dims).Append(']');
            }
            else if (type.IsPointer)
            {
                result.Append(GetFullName(type.ElementType));
                result.Append('*');
            }
            else
            {
                if (type.ParentType != null)
                {
                    result.Append(GetFullName(type.ParentType,false)).Append("+").Append(type.Name);
                }
                else if (type.Namespace != null)
                {
                    result.Append(type.Namespace).Append(".").Append(type.Name);
                }
                else
                {
                    result.Append(type.Name);
                }

                if (type.GenericArgs != null && type.GenericArgs.Count > 0)
                {
                    if (isLeaf)
                    {
                        var gargs = from ga in type.GenericArgs
                                    select GetFullName(ga);
                        result.Append("<").Append(string.Join(",", gargs)).Append(">");
                    }
                }
            }
            return result.ToString();
        }

        public string GetGenericId(TypeNameItem type, bool isLeaf = true)
        {
            StringBuilder result = new StringBuilder();
            if (type.IsArray)
            {
                result.Append(GetGenericId(type.ElementType));
                result.Append('[').Append(type.Dims).Append(']');
            }
            else if (type.IsPointer)
            {
                result.Append(GetGenericId(type.ElementType));
                result.Append('*');
            }
            else
            {
                if (type.ParentType != null)
                {
                    result.Append(GetGenericId(type.ParentType,false)).Append("+").Append(type.Name);
                }
                else if (type.Namespace != null)
                {
                    result.Append(type.Namespace).Append(".").Append(type.Name);
                }
                else
                {
                    result.Append(type.Name);
                }

                if (type.GenericArgs != null && type.GenericArgs.Count > 0)
                {
                    if (type.ParentType == null)
                    {
                        result.Append("`").Append(type.GenericArgs.Count);
                    }
                    else
                    {
                        int galen = type.ParentType.GenericArgs == null ? type.GenericArgs.Count : type.GenericArgs.Count - type.ParentType.GenericArgs.Count;
                        switch (Math.Sign(galen))
                        {
                            case 1:
                                result.Append("`").Append(galen);
                                break;
                            case 0:
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                }
            }
            return result.ToString();
        }

        public string GetTypeId(TypeNameItem type, bool isLeaf = true)
        {
            StringBuilder result = new StringBuilder();
            if (type.IsArray)
            {
                result.Append(GetTypeId(type.ElementType));
                result.Append('[').Append(type.Dims).Append(']');
            }
            else if (type.IsPointer)
            {
                result.Append(GetTypeId(type.ElementType));
                result.Append('*');
            }
            else
            {
                if (type.ParentType != null)
                {
                    result.Append(GetTypeId(type.ParentType,false)).Append("+").Append(type.Name);
                }
                else if (type.Namespace != null)
                {
                    result.Append(type.Namespace).Append(".").Append(type.Name);
                }
                else
                {
                    result.Append(type.Name);
                }

                if (type.GenericArgs != null && type.GenericArgs.Count > 0)
                {
                    if (type.ParentType == null)
                    {
                        result.Append("`").Append(type.GenericArgs.Count);
                    }
                    else
                    {
                        int galen = type.ParentType.GenericArgs == null ? type.GenericArgs.Count : type.GenericArgs.Count - type.ParentType.GenericArgs.Count;
                        switch (Math.Sign(galen))
                        {
                            case 1:
                                result.Append("`").Append(galen);
                                break;
                            case 0:
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }

                    if (isLeaf)
                    {
                        var gargs = from ga in type.GenericArgs
                                    select GetTypeId(ga);
                        result.Append("[").Append(string.Join(",", gargs)).Append("]");
                    }
                }
            }
            return result.ToString();
        }
    }
}
