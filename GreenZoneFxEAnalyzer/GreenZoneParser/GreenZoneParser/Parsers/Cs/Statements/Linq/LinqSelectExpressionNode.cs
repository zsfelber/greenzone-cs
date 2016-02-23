using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqSelectExpressionNode : LinqFootExpressionNode
    {
        public LinqSelectExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, ExpressionNode selectExpression, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqSelect, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.selectExpression = selectExpression;
            parser.RaiseNodeCreated(this);
        }

        readonly ExpressionNode selectExpression;
        public ExpressionNode SelectExpression
        {
            get
            {
                return selectExpression;
            }
        }
    }
}
