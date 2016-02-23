using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class CastExpressionNode : ExpressionNode
    {
        public CastExpressionNode(CsParser parser, BlockNode parent, TokenOpenClose castParen, TypeSpecifierNode typeSpecifier, ExpressionNode right)
            : base(StatementId.Cast, parser, parent, castParen.OpenToken, right.EndToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.castParen = castParen;
            this.typeSpecifier = typeSpecifier;
            this.right = right;
            right.AddAcceptedType(ResolvedType.Create(typeSpecifier));
            right.AddAcceptedType(SystemResolvedType.OBJECT);
            parser.RaiseNodeCreated(this);
        }

        readonly TokenOpenClose castParen;
        public TokenOpenClose CastParen
        {
            get
            {
                return castParen;
            }
        }

        readonly TypeSpecifierNode typeSpecifier;
        public TypeSpecifierNode TypeSpecifier
        {
            get
            {
                return typeSpecifier;
            }
        }

        readonly ExpressionNode right;
        public ExpressionNode Right
        {
            get
            {
                return right;
            }
        }
    }
}