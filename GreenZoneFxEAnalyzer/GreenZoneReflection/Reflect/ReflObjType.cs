using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Reflection;

namespace GreenZoneParser.Reflect
{
    public abstract class ReflObjType : ReflCallableType
    {
        internal ReflObjType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            members = new List<ReflMember>();
            membersUm = members.AsReadOnly();
            this.baseInterfaces = new List<ReflInterfaceType>();
            this.baseInterfacesUm = this.baseInterfaces.AsReadOnly();
        }

        internal ReflObjType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            members = new List<ReflMember>();
            membersUm = members.AsReadOnly();
            this.baseInterfaces = new List<ReflInterfaceType>();
            this.baseInterfacesUm = this.baseInterfaces.AsReadOnly();
        }


        protected readonly List<ReflMember> members;
        protected readonly IList<ReflMember> membersUm;
        public IList<ReflMember> Members
        {
            get
            {
                lazyResolve();
                return membersUm;
            }
        }

        protected readonly List<ReflInterfaceType> baseInterfaces;
        readonly IList<ReflInterfaceType> baseInterfacesUm;
        public IList<ReflInterfaceType> BaseInterfaces
        {
            get
            {
                lazyResolve();
                return baseInterfacesUm;
            }
        }

        internal virtual void AddMember(ReflMember member)
        {
            if (member is ReflType)
            {
                throw new ArgumentException("Type definition is only possible in ReflDefType");
            }
            members.Add(member);
        }

        internal void AddBaseInterface(ReflInterfaceType baseIf)
        {
            baseInterfaces.Add(baseIf);
        }

        public ReflMember MemberById(int id)
        {
            foreach (var m in Members)
            {
                if (m.Id == id)
                {
                    return m;
                }
            }
            return null;
        }

        public M GetMember<M>(string name, BindingFlags flags = BindingFlags.Default) where M : ReflMember
        {
            List<M> members = GetMembers<M>(flags);
            foreach (M m in members)
            {
                if (m.Name.Equals(name))
                {
                    return m;
                }
            }
            return null;
        }

        public ReflConstructor GetConstructor(ReflType[] argTypes, BindingFlags flags = BindingFlags.Default)
        {
            return GetMethod<ReflConstructor>(null, argTypes, flags);
        }

        public M GetMethod<M>(string name, ReflType[] argTypes, BindingFlags flags = BindingFlags.Default) where M : ReflMethodBase
        {
            if (typeof(M) == typeof(ReflConstructor))
            {
                flags = flags | BindingFlags.DeclaredOnly;
            }

            List<M> members = GetMembers<M>(flags);
            foreach (M m in members)
            {
                if (name == null || name.Equals(m.Name))
                {
                    if (m.MethodArgs.Length == argTypes.Length)
                    {
                        bool ok = true;
                        for (int i = 0; i < argTypes.Length; i++)
                        {
                            if (m.MethodArgs[i].ParameterType != argTypes[i])
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok)
                        {
                            return m;
                        }
                    }
                }
            }
            return null;
        }

        public List<M> GetMembers<M>(BindingFlags flags = BindingFlags.Default) where M : ReflMember
        {
            if (flags == 0)
            {
                flags = BindingFlags.Public | BindingFlags.Instance;
            }
            List<M> result = new List<M>();
            foreach (ReflMember _m in members)
            {
                if (_m is M)
                {
                    M m = (M)_m;
                    if ((flags & BindingFlags.DeclaredOnly) == 0 && parentType != null)
                    {
                        result.AddRange(parentType.GetMembers<M>(flags));
                    }

                    bool ok = true;

                    if ((m.Modifiers & ReflModifier.Static) != 0)
                    {
                        if ((flags & BindingFlags.Static) == 0)
                        {
                            ok = false;
                        }
                    }
                    else
                    {
                        if ((flags & BindingFlags.Instance) == 0)
                        {
                            ok = false;
                        }
                    }

                    if (ok)
                    {
                        if ((m.Modifiers & ReflModifier.Public) != 0)
                        {
                            if ((flags & BindingFlags.Public) == 0)
                            {
                                ok = false;
                            }
                        }
                        else
                        {
                            if ((flags & BindingFlags.NonPublic) == 0)
                            {
                                ok = false;
                            }
                        }
                    }

                    if (ok)
                    {
                        result.Add(m);
                    }
                }
            }
            return result;
        }

        protected internal override bool isSubtypeOf(ReflType fromType)
        {
            if (base.isSubtypeOf(fromType))
            {
                return true;
            }
            else if (fromType.ReflTypeId == ReflTypeId.InterfaceType)
            {
                foreach (var i in baseInterfaces)
                {
                    if (i.isSubtypeOf(fromType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
