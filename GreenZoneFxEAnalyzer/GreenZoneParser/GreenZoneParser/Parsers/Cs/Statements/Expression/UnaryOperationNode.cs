using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class UnaryOperationNode : ExpressionNode
    {
        public UnaryOperationNode(CsParser parser, BlockNode parent, Token operatorToken, Token startToken, Token endToken, ExpressionNode right)
            : base(StatementId.UnaryOperation, parser, parent, startToken, endToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.operatorToken = operatorToken;
            this.right = right;
            switch (operatorToken.Id)
            {
                case TokenId.PlusPlus:
                case TokenId.MinusMinus:
                    validStatement = true;
                    break;
                default:
                    validStatement = false;
                    break;
            }
            parser.RaiseNodeCreated(this);
        }

        public bool LeftSideOperator
        {
            get
            {
                return operatorToken == StartToken;
            }
        }

        readonly Token operatorToken;
        public Token OperatorToken
        {
            get
            {
                return operatorToken;
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