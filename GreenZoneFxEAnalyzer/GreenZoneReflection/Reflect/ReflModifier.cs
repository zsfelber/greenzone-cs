using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    [Flags]
    public enum ReflModifier
    {
        Static = 1 << 0,
        Abstract = 1 << 1,
        Sealed = 1 << 2,
        Override = 1 << 3,
        Virtual = 1 << 4,

        Const = 1 << 5,
        Readonly = 1 << 6,
        Volatile = 1 << 7,

        Public = 1 << 8,
        Internal = 1 << 9,
        Protected = 1 << 10,
        Private = 1 << 11,
    }
}
