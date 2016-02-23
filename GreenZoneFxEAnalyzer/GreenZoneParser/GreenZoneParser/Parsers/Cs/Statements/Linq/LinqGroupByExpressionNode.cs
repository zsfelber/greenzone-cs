using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqGroupByExpressionNode : LinqFootExpressionNode
    {
        public LinqGroupByExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, ExpressionNode groupExpression, ExpressionNode byExpression, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqGroupBy, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.groupExpression = groupExpression;
            this.byExpression = byExpression;
            parser.RaiseNodeCreated(this);
        }

        readonly ExpressionNode groupExpression;
        public ExpressionNode GroupExpression
        {
            get
            {
                return groupExpression;
            }
        }

        readonly ExpressionNode byExpression;
        public ExpressionNode ByExpression
        {
            get
            {
                return byExpression;
            }
        }
    }
}
