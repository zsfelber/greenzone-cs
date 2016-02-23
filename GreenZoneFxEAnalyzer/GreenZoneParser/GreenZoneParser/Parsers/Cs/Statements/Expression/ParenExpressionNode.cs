using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ParenExpressionNode : ExpressionNode
    {
        public ParenExpressionNode(CsParser parser, BlockNode parent, TokenOpenClose parens, ExpressionNode inner)
            : base(StatementId.Paren, parser, parent, parens.OpenToken, parens.CloseToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.parens = parens;
            this.inner = inner;
            parser.RaiseNodeCreated(this);
        }

        readonly TokenOpenClose parens;
        public TokenOpenClose Parens
        {
            get
            {
                return parens;
            }
        }

        readonly ExpressionNode inner;
        public ExpressionNode Inner
        {
            get
            {
                return inner;
            }
        }
    }
}