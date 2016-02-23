using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Parsers.Cs
{
    public class IndexerDefStatementNode : BaseMethodStatementNode
    {
        public IndexerDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeExpression, Token identifierToken, TokenOpenClose brackets, List<VarDeclArgumentNode> argumentList, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.IndexerDef, parser, parent, startToken, endToken, typeExpression, GreenZoneSysUtilsBase.AsList(identifierToken), brackets, argumentList, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            parser.RaiseNodeCreated(this);
        }

        public TokenOpenClose Brackets
        {
            get
            {
                return parens;
            }
        }
    }
}
