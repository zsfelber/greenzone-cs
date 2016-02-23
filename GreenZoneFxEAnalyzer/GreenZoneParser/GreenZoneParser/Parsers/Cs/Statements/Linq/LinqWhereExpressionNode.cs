using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqWhereExpressionNode : LinqHeadExpressionNode
    {
        public LinqWhereExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, ExpressionNode expression, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqWhere, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.expression = expression;
            parser.RaiseNodeCreated(this);
        }

        // NOTE logical
        readonly ExpressionNode expression;
        public ExpressionNode Expression
        {
            get
            {
                return expression;
            }
        }
    }
}
