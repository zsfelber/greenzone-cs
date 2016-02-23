using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class LinqFromExpressionNode : LinqHeadExpressionNode
    {
        public LinqFromExpressionNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeSpecifier, AtomicExpressionNode identifier, ExpressionNode inExpression, List<LinqJoinExpressionNode> joins, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.LinqFrom, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.identifier = identifier;
            this.typeSpecifier = typeSpecifier;
            this.inExpression = inExpression;
            this.joins = joins;
            if (joins != null)
            {
                joinsUm = joins.AsReadOnly();
            }
            parser.RaiseNodeCreated(this);
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

        readonly List<LinqJoinExpressionNode> joins;
        readonly IList<LinqJoinExpressionNode> joinsUm;
        public IList<LinqJoinExpressionNode> Joins
        {
            get
            {
                return joinsUm;
            }
        }
    }
}
