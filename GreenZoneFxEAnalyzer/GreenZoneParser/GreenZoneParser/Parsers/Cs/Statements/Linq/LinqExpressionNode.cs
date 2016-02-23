using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqExpressionNode : ExpressionNode
    {
        public LinqExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, List<LinqHeadExpressionNode> fromLetWheres, LinqOrderByExpressionNode orderBy, LinqFootExpressionNode selectGroup, LinqContinuationExpressionNode continuation, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.Linq, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.fromLetWheres = fromLetWheres;
            if (fromLetWheres != null)
            {
                fromLetWheresUm = fromLetWheres.AsReadOnly();
            }
            this.orderBy = orderBy;
            this.selectGroup = selectGroup;
            this.continuation = continuation;
            parser.RaiseNodeCreated(this);
        }

        readonly List<LinqHeadExpressionNode> fromLetWheres;
        readonly IList<LinqHeadExpressionNode> fromLetWheresUm;
        public IList<LinqHeadExpressionNode> FromLetWheres
        {
            get
            {
                return fromLetWheresUm;
            }
        }

        readonly LinqOrderByExpressionNode orderBy;
        public LinqOrderByExpressionNode OrderBy
        {
            get
            {
                return orderBy;
            }
        }

        readonly LinqFootExpressionNode selectGroup;
        public LinqFootExpressionNode SelectGroup
        {
            get
            {
                return selectGroup;
            }
        }

        readonly LinqContinuationExpressionNode continuation;
        public LinqContinuationExpressionNode Continuation
        {
            get
            {
                return continuation;
            }
        }
    }
}
