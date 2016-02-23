using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqOrderByExpressionNode : ExpressionNode
    {
        public LinqOrderByExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, List<LinqOrderingExpressionNode> orderings, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqOrderBy, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.orderings = orderings;
            if (orderings != null)
            {
                orderingsUm = orderings.AsReadOnly();
            }
            parser.RaiseNodeCreated(this);
        }

        readonly List<LinqOrderingExpressionNode> orderings;
        readonly IList<LinqOrderingExpressionNode> orderingsUm;
        public IList<LinqOrderingExpressionNode> Orderings
        {
            get
            {
                return orderingsUm;
            }
        }
    }
}
