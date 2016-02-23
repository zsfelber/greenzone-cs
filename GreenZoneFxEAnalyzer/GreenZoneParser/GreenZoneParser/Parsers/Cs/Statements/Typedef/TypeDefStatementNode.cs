using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public abstract class TypeDefStatementNode : StatementNode
    {
        public TypeDefStatementNode(StatementId id, CsParser parser, BlockNode parent, Token startToken, Token endToken, List<Token> identifierTokens, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(id, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.identifierTokens = identifierTokens;
            if (identifierTokens != null)
            {
                this.identifierTokensUm = identifierTokens.AsReadOnly();
                typeName = parser.GetContent(identifierTokens[0].TokenStartPos, identifierTokens[identifierTokens.Count - 1].TokenEndPos);
            }
        }

        readonly string typeName;
        public string TypeName
        {
            get
            {
                return typeName;
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
