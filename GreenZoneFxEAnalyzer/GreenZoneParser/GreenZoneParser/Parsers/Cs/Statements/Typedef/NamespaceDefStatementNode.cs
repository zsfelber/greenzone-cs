using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class NamespaceDefStatementNode : StatementNode
    {
        public NamespaceDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, List<Token> identifierTokens, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.NamespaceDef, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.identifierTokens = identifierTokens;
            if (identifierTokens != null)
            {
                this.identifierTokensUm = identifierTokens.AsReadOnly();
                namespaceId = parser.GetContent(identifierTokens[0].TokenStartPos, identifierTokens[identifierTokens.Count - 1].TokenEndPos);
            }
            parser.RaiseNodeCreated(this);
        }

        readonly string namespaceId;
        public string NamespaceId
        {
            get
            {
                return namespaceId;
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
