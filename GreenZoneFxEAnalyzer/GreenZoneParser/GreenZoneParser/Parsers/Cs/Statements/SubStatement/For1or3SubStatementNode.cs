using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class For1or3SubStatementNode : StatementNode
    {
        public For1or3SubStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.For1or3Sub, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            parser.RaiseNodeCreated(this);
        }
    }
}
