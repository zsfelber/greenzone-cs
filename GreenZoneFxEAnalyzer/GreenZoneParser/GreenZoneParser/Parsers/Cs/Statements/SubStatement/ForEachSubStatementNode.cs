using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ForEachSubStatementNode : StatementNode
    {
        public ForEachSubStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeSpecifier, List<Token> identifierTokens, ExpressionNode right, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.ForEachSub, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.typeSpecifier = typeSpecifier;
            this.identifierTokens = identifierTokens;
            if (identifierTokens != null)
            {
                this.identifierTokensUm = identifierTokens.AsReadOnly();
            }
            this.right = right;
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

        readonly List<Token> identifierTokens;
        readonly IList<Token> identifierTokensUm;
        public IList<Token> IdentifierTokens
        {
            get
            {
                return identifierTokensUm;
            }
        }

        readonly ExpressionNode right;
        public ExpressionNode Right
        {
            get
            {
                return right;
            }
        }
    }
}
