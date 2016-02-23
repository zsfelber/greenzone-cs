using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class TernaryOperationNode : ExpressionNode
    {
        public TernaryOperationNode(CsParser parser, BlockNode parent, Token operatorToken1, Token operatorToken2, ExpressionNode left, ExpressionNode middle, ExpressionNode right)
            : base(StatementId.TernaryOperation, parser, parent, left.StartToken, right.EndToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.operatorToken1 = operatorToken1;
            this.operatorToken2 = operatorToken2;
            this.left = left;
            this.middle = middle;
            this.right = right;
            parser.RaiseNodeCreated(this);
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

        readonly ExpressionNode middle;
        public ExpressionNode Middle
        {
            get
            {
                return middle;
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