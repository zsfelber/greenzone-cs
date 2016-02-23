using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqLetExpressionNode : LinqHeadExpressionNode
    {
        public LinqLetExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, AtomicExpressionNode identifier, ExpressionNode expression, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqLet, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.identifier = identifier;
            this.expression = expression;
            parser.RaiseNodeCreated(this);
        }

        readonly AtomicExpressionNode identifier;
        public AtomicExpressionNode Identifier
        {
            get
            {
                return identifier;
            }
        }

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
