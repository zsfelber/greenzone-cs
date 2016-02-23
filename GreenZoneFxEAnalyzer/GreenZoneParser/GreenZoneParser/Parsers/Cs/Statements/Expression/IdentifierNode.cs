using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public abstract class IdentifierNode : ExpressionNode
    {
        public IdentifierNode(StatementId expressionId, CsParser parser, BlockNode parent, Token startToken, Token endToken)
            : base(expressionId, parser, parent, startToken, endToken, false, null, null, null, null, StatementParseMode.Inside)
        {
        }
    }
}