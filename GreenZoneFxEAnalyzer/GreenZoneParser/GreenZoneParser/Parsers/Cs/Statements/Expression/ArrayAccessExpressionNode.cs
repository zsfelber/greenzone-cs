using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ArrayAccessExpressionNode : ExpressionNode
    {
        public ArrayAccessExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, ExpressionNode left, TokenOpenClose brackets, List<ExpressionNode> parameterList)
            : base(StatementId.ArrayAccess, parser, parent, startToken, endToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.left = left;
            this.brackets = brackets;
            this.parameterList = parameterList;
            if (parameterList != null)
            {
                this.parameterListUm = parameterList.AsReadOnly();
            }
            parser.RaiseNodeCreated(this);
        }

        readonly ExpressionNode left;
        public ExpressionNode Left
        {
            get
            {
                return left;
            }
        }

        readonly TokenOpenClose brackets;
        public TokenOpenClose Brackets
        {
            get
            {
                return brackets;
            }
        }

        readonly List<ExpressionNode> parameterList;
        readonly IList<ExpressionNode> parameterListUm;
        public IList<ExpressionNode> ParameterList
        {
            get
            {
                return parameterListUm;
            }
        }
    }
}