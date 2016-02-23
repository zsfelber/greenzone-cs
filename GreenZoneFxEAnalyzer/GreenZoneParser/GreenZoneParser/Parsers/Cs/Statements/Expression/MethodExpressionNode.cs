using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class MethodExpressionNode : ExpressionNode
    {
        public MethodExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, Token pointToken, ExpressionNode left, List<TypeSpecifierNode> typeParameters, TokenOpenClose parens, Token methodIdentifier, List<ArgumentNode> argumentList)
            : base(StatementId.MethodExpression, parser, parent, startToken, endToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.pointToken = pointToken;
            this.left = left;
            this.methodIdentifier = methodIdentifier;
            this.parens = parens;
            this.typeParameters = typeParameters;
            if (typeParameters != null)
            {
                typeParametersUm = typeParameters.AsReadOnly();
            }
            this.argumentList = argumentList;
            if (argumentList != null)
            {
                this.argumentListUm = argumentList.AsReadOnly();
            }
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

        readonly Token methodIdentifier;
        public Token MethodIdentifier
        {
            get
            {
                return methodIdentifier;
            }
        }

        readonly List<TypeSpecifierNode> typeParameters;
        readonly IList<TypeSpecifierNode> typeParametersUm;
        public IList<TypeSpecifierNode> TypeParameters
        {
            get
            {
                return typeParameters;
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

        readonly List<ArgumentNode> argumentList;
        readonly IList<ArgumentNode> argumentListUm;
        public IList<ArgumentNode> ArgumentList
        {
            get
            {
                return argumentListUm;
            }
        }

        public override bool ValidStatement
        {
            get
            {
                return true;
            }
        }
    }
}