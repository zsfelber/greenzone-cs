using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ClassDefStatementNode : RefTypeDefStatementNode
    {
        public ClassDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, UserTypeSpecifierNode typeDefinitionMain, List<TypeSpecifierNode> supertypes, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.ClassDef, parser, parent, startToken, endToken, typeDefinitionMain, supertypes, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            parser.RaiseNodeCreated(this);
        }
    }
}
