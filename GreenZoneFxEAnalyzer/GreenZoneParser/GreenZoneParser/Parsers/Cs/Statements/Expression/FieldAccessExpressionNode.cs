using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class FieldAccessOrIdNode : IdentifierNode
    {
        public FieldAccessOrIdNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, Token pointToken, ExpressionNode left, Token fieldIdentifier)
            : base(StatementId.FieldAccessOrId, parser, parent, startToken, endToken)
        {
            this.pointToken = pointToken;
            this.left = left;
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

        readonly ExpressionNode left;
        public ExpressionNode Left
        {
            get
            {
                return left;
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