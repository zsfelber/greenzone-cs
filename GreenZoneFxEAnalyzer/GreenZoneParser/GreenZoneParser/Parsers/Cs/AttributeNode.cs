using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class AttributeNode : NormalNode
    {
        public AttributeNode(CsParser parser, BlockNode parent, TokenOpenClose arrayTokens)
            : base(parser, parent, arrayTokens.OpenToken, arrayTokens.CloseToken)
        {
            parser.RaiseNodeCreated(this);
        }
    }
}
