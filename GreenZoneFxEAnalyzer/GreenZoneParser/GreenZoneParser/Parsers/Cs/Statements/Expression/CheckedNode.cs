using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class CheckedNode : ExpressionNode
    {
        public CheckedNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TokenOpenClose parens, Token keywordToken, ExpressionNode innerExpression)
            : base(StatementId.Checked, parser, parent, startToken, endToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.keywordToken = keywordToken;
            this.parens = parens;
            this.innerExpression = innerExpression;
            parser.RaiseNodeCreated(this);
        }

        readonly Token keywordToken;
        public Token KeywordToken
        {
            get
            {
                return keywordToken;
            }
        }

        readonly TokenOpenClose parens;
        public TokenOpenClose Parens
        {
            get
            {
                return parens;
            }
        }

        readonly ExpressionNode innerExpression;
        public ExpressionNode InnerExpression
        {
            get
            {
                return innerExpression;
            }
        }
    }
}