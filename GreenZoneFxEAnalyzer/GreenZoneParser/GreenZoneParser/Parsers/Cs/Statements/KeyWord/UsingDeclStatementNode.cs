using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class UsingDeclStatementNode : StatementNode
    {
        public UsingDeclStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, List<Token> identifierTokens, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.UsingDecl, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.identifierTokens = identifierTokens;
            if (identifierTokens != null)
            {
                this.identifierTokensUm = identifierTokens.AsReadOnly();
                name = parser.GetContent(identifierTokens[0].TokenStartPos, identifierTokens[identifierTokens.Count - 1].TokenEndPos);
            }
            parser.RaiseNodeCreated(this);
        }

        readonly string name;
        public string Name
        {
            get
            {
                return name;
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
    }
}
