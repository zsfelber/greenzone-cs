using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class CatchStatementNode : StatementNode
    {
        public CatchStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.Catch, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            parser.RaiseNodeCreated(this);
        }
    }
}
