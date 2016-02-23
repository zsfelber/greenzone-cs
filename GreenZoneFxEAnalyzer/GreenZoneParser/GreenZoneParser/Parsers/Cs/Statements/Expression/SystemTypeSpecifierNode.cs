using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class SystemTypeSpecifierNode : TypeSpecifierNode
    {
        public SystemTypeSpecifierNode(CsParser parser, BlockNode parent, Token token, List<ExpressionNode> firstBracketExpressions, TokenOpenClose initializerRanks, int commaSeparatedRanks, int emptyRanks, TokenOpenClose emptyRanksFirst, TokenOpenClose emptyRanksLast, Token pointerStar)
            : base(StatementId.SystemType, parser, parent, token, token, firstBracketExpressions, initializerRanks, commaSeparatedRanks, emptyRanks, emptyRanksFirst, emptyRanksLast, pointerStar)
        {
            parser.RaiseNodeCreated(this);
        }

        public override string Name
        {
            get
            {
                return StartToken.ContentString;
            }
        }
    }
}