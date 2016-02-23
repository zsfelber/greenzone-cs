using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public enum LinqOrderingType
    {
        Ascending,
        Descending
    }

    public class LinqOrderingExpressionNode : ExpressionNode
    {
        public LinqOrderingExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, ExpressionNode expression, LinqOrderingType type, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqOrdering, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.expression = expression;
            this.type = type;
            parser.RaiseNodeCreated(this);
        }

        readonly ExpressionNode expression;
        public ExpressionNode Expression
        {
            get
            {
                return expression;
            }
        }

        readonly LinqOrderingType type;
        public LinqOrderingType Type
        {
            get
            {
                return type;
            }
        }
    }
}
