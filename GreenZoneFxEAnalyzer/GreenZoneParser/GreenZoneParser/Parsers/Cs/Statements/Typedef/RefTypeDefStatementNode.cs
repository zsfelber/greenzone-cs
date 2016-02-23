using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public abstract class RefTypeDefStatementNode : TypeDefStatementNode
    {
        public RefTypeDefStatementNode(StatementId id, CsParser parser, BlockNode parent, Token startToken, Token endToken, UserTypeSpecifierNode typeDefinitionMain, List<TypeSpecifierNode> supertypes, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(id, parser, parent, startToken, endToken, typeDefinitionMain.identifierTokens, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.supertypes = supertypes;
            if (supertypes != null)
            {
                supertypesUm = supertypes.AsReadOnly();
            }

            this.typeDefinitionMain = typeDefinitionMain;
        }

        readonly UserTypeSpecifierNode typeDefinitionMain;
        public UserTypeSpecifierNode TypeDefinitionMain
        {
            get
            {
                return typeDefinitionMain;
            }
        }

        readonly List<TypeSpecifierNode> supertypes;
        readonly IList<TypeSpecifierNode> supertypesUm;
        public IList<TypeSpecifierNode> Supertypes
        {
            get
            {
                return supertypes;
            }
        }
    }
}
