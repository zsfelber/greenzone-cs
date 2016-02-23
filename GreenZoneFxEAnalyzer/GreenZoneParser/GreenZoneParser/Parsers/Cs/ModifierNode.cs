using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ModifierNode : NormalNode
    {
        public ModifierNode(CsParser parser, BlockNode parent, Token token)
            : base(parser, parent, token, token)
        {
            if (!token.Id.IsModifier(parent.ParseMode))
            {
                throw new NotSupportedException();
            }
            parser.RaiseNodeCreated(this);
        }
    }
}
