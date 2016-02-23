using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GreenZoneParser.Reflect
{
    internal interface ILazyTypeResolver
    {
        bool Resolved
        {
            get;
        }

        void FinishResolve();
    }

    internal class LazyTypeResolver
    {
        protected readonly Resolver resolver;
        protected readonly ReflDefType parentType;
        protected readonly ReflType gtype;

        internal LazyTypeResolver(Resolver resolver, ReflDefType parentType, ReflType gtype)
        {
            this.resolver = resolver;
            this.parentType = parentType;
            this.gtype = gtype;
        }

        protected bool resolved = false;
        public bool Resolved
        {
            get
            {
                return resolved;
            }
        }
    }

    internal class XmlLazyTypeResolver : LazyTypeResolver, ILazyTypeResolver
    {
        readonly ParseInfo defTypeInfo;

        internal XmlLazyTypeResolver(Resolver resolver, ReflDefType parentType, ParseInfo defTypeInfo, ReflType gtype)
            : base(resolver, parentType, gtype)
        {
            this.defTypeInfo = defTypeInfo;
        }

        public void FinishResolve()
        {
            Console.WriteLine("DEBUG   XmlLazyTypeResolver.FinishResolve()   " + gtype.FullName);
            resolved = true;
            resolver.FinishResolve(parentType, defTypeInfo, gtype);
        }
    }

    internal class PureReflLazyTypeResolver : LazyTypeResolver, ILazyTypeResolver
    {
        readonly Type stype;

        internal PureReflLazyTypeResolver(Resolver resolver, ReflDefType parentType, Type stype, ReflType gtype)
            : base(resolver, parentType, gtype)
        {
            this.stype = stype;
        }

        public void FinishResolve()
        {
            Console.WriteLine("DEBUG   PureReflLazyTypeResolver.FinishResolve()   " + gtype.FullName);
            resolved = true;
            resolver.PureReflResolver.FinishResolve(parentType, stype, gtype);
        }
    }
}
