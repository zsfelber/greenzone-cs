using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class BinaryShiftOperationNode : ExpressionNode
    {
        public BinaryShiftOperationNode(CsParser parser, BlockNode parent, Token operatorToken1, Token operatorToken2, ExpressionNode left, ExpressionNode right)
            : base(StatementId.BinaryShiftOperation, parser, parent, left.StartToken, right.EndToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.operatorToken1 = operatorToken1;
            this.operatorToken2 = operatorToken2;
            this.left = left;
            this.right = right;
            switch (operatorToken1.Id)
            {
                case TokenId.Lt:
                    leftShift = true;
                    break;
                case TokenId.Gt:
                    leftShift = false;
                    break;
                default:
                    throw new NotSupportedException();
            }
            if (operatorToken1.Id != operatorToken2.Id)
            {
                throw new NotSupportedException();
            }
            if (operatorToken2.TotalIndex - operatorToken1.TotalIndex != 1)
            {
                throw new NotSupportedException();
            }
            parser.RaiseNodeCreated(this);
        }

        readonly bool leftShift;
        public bool LeftShift
        {
            get
            {
                return leftShift;
            }
        }

        readonly Token operatorToken1;
        public Token OperatorToken1
        {
            get
            {
                return operatorToken1;
            }
        }

        readonly Token operatorToken2;
        public Token OperatorToken2
        {
            get
            {
                return operatorToken2;
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
    }
}