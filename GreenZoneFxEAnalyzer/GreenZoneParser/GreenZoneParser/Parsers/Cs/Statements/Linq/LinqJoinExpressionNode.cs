using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqJoinExpressionNode : ExpressionNode
    {
        public LinqJoinExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeSpecifier, AtomicExpressionNode identifier, ExpressionNode inExpression, ExpressionNode onExpression, ExpressionNode onEqualsExpression, AtomicExpressionNode intoIdentifier, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqJoin, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.typeSpecifier = typeSpecifier;
            this.identifier = identifier;
            this.inExpression = inExpression;
            this.onExpression = onExpression;
            this.onEqualsExpression = onEqualsExpression;
            this.intoIdentifier = intoIdentifier;
        }

        readonly TypeSpecifierNode typeSpecifier;
        public TypeSpecifierNode TypeSpecifier
        {
            get
            {
                return typeSpecifier;
            }
        }

        readonly AtomicExpressionNode identifier;
        public AtomicExpressionNode Identifier
        {
            get
            {
                return identifier;
            }
        }

        readonly ExpressionNode inExpression;
        public ExpressionNode InExpression
        {
            get
            {
                return inExpression;
            }
        }

        readonly ExpressionNode onExpression;
        public ExpressionNode OnExpression
        {
            get
            {
                return onExpression;
            }
        }

        readonly ExpressionNode onEqualsExpression;
        public ExpressionNode OnEqualsExpression
        {
            get
            {
                return onEqualsExpression;
            }
        }

        readonly AtomicExpressionNode intoIdentifier;
        public AtomicExpressionNode IntoIdentifier
        {
            get
            {
                return intoIdentifier;
            }
        }

    }
}
