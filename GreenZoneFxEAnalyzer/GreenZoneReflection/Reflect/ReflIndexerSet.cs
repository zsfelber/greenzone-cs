using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.Util;




namespace GreenZoneParser.Reflect
{
    public class ReflIndexerSet : ReflMethodBase
    {
        internal ReflIndexerSet(Resolver resolver, IParser parser, INode parseNode, int id, ReflObjType declaringType, ReflIndexerProperty parent, ReflModifier modifiers, ReflAttribute[] attributes)
			: base(resolver, parser, parseNode, id, declaringType, "set", modifiers, attributes, GreenZoneSysUtilsBase.AddToArray(parent.IndexerArgs, new ReflMethodArgDefinition(resolver, parser, parseNode, parent.PropertyType, "value", ReflMethodArgType.Value)))
        {
            this.parent = parent;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.IndexerSet;
            }
        }

        readonly new ReflIndexerProperty parent;
        public new ReflIndexerProperty Parent
        {
            get
            {
                return parent;
            }
        }
    }
}