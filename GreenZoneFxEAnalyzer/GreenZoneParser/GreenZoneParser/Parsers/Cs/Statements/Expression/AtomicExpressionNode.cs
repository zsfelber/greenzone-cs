using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class AtomicExpressionNode : IdentifierNode
    {
        public AtomicExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, Token pointToken, Token fieldIdentifier)
            : base(StatementId.AtomicExpression, parser, parent, startToken, endToken)
        {
            this.pointToken = pointToken;
            this.fieldIdentifier = fieldIdentifier;
            parser.RaiseNodeCreated(this);
        }

        readonly Token pointToken;
        public Token PointToken
        {
            get
            {
                return pointToken;
            }
        }

        readonly Token fieldIdentifier;
        public Token FieldIdentifier
        {
            get
            {
                return fieldIdentifier;
            }
        }
    }
}