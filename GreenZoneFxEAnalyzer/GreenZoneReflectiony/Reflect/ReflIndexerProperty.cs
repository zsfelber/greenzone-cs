using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflIndexerProperty : ReflMember
    {
        internal ReflIndexerProperty(Resolver resolver, IParser parser, INode parseNode, int id, ReflObjType declaringType, ReflType propertyType, ReflPropertyMethodType propertyMethodType, string propertyName, ReflModifier modifiers, ReflModifier setModifiers, ReflAttribute[] attributes, params ReflMethodArgDefinition[] indexerArgs)
            : base(resolver, parser, parseNode, id, declaringType, propertyName, modifiers, attributes)
        {
            this.propertyType = propertyType;
            this.propertyMethodType = propertyMethodType;
            this.setModifiers = setModifiers;
            this.indexerArgs = indexerArgs;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.IndexerProperty;
            }
        }

        protected readonly ReflType propertyType;
        public ReflType PropertyType
        {
            get
            {
                return propertyType;
            }
        }

        readonly ReflPropertyMethodType propertyMethodType;
        public ReflPropertyMethodType PropertyMethodType
        {
            get
            {
                return propertyMethodType;
            }
        }

        readonly ReflModifier setModifiers;
        public ReflModifier SetModifiers
        {
            get
            {
                return setModifiers;
            }
        }

        protected readonly ReflMethodArgDefinition[] indexerArgs;
        public ReflMethodArgDefinition[] IndexerArgs
        {
            get
            {
                return indexerArgs;
            }
        }
    }
}
