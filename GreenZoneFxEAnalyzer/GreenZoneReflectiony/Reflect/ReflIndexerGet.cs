using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflIndexerGet : ReflMethodBase
    {
        internal ReflIndexerGet(Resolver resolver, IParser parser, INode parseNode, int id, ReflObjType declaringType, ReflIndexerProperty parent, ReflModifier modifiers, ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, id, declaringType, "get", modifiers, attributes, parent.IndexerArgs)
        {
            this.parent = parent;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.IndexerGet;
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
