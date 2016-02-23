using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqContinuationExpressionNode : ExpressionNode
    {
        public LinqContinuationExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, AtomicExpressionNode intoIdentifier, List<LinqJoinExpressionNode> joins, LinqExpressionNode linqExpression, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqContinuation, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.intoIdentifier = intoIdentifier;
            this.joins = joins;
            if (joins != null)
            {
                joinsUm = joins.AsReadOnly();
            }
            this.linqExpression = linqExpression;
            parser.RaiseNodeCreated(this);
        }


        readonly AtomicExpressionNode intoIdentifier;
        public AtomicExpressionNode IntoIdentifier
        {
            get
            {
                return intoIdentifier;
            }
        }

        readonly List<LinqJoinExpressionNode> joins;
        readonly IList<LinqJoinExpressionNode> joinsUm;
        public IList<LinqJoinExpressionNode> Joins
        {
            get
            {
                return joinsUm;
            }
        }

        readonly LinqExpressionNode linqExpression;
        public LinqExpressionNode LinqExpression
        {
            get
            {
                return linqExpression;
            }
        }
    }
}
