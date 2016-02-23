using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class BinaryOperationNode : ExpressionNode
    {
        public BinaryOperationNode(CsParser parser, BlockNode parent, Token operatorToken, ExpressionNode left, ExpressionNode right)
            : base(StatementId.BinaryOperation, parser, parent, left.StartToken, right.EndToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.operatorToken = operatorToken;
            this.left = left;
            this.right = right;

            switch (operatorToken.Id)
            {
                case TokenId.Assign:
                case TokenId.PlusAssign:
                case TokenId.MinusAssign:
                case TokenId.MulAssign:
                case TokenId.PerAssign:
                case TokenId.ModAssign:
                case TokenId.AndAssign:
                case TokenId.OrAssign:
                case TokenId.XorAssign:
                case TokenId.LsAssign:
                case TokenId.RsAssign:
                    validStatement = true;
                    break;
                default:
                    validStatement = false;
                    break;
            }
            parser.RaiseNodeCreated(this);
        }

        readonly Token operatorToken;
        public Token OperatorToken
        {
            get
            {
                return operatorToken;
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

        readonly ExpressionNode right;
        public ExpressionNode Right
        {
            get
            {
                return right;
            }
        }

        bool validStatement;
        public override bool ValidStatement
        {
            get
            {
                return validStatement;
            }
        }
    }
}