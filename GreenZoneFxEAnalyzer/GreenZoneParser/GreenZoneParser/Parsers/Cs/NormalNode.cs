using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class NormalNode : BaseCsNode
    {
        public NormalNode(CsParser parser, BlockNode parent, Token startToken, Token endToken)
            : base(parser, parent, startToken, endToken)
        {
        }

    }
}
