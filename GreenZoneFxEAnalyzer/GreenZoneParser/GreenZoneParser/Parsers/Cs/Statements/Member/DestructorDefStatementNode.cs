using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Parsers.Cs
{
    public class DestructorDefStatementNode : BaseMethodStatementNode
    {
        public DestructorDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token identifierToken, TokenOpenClose parens, List<VarDeclArgumentNode> argumentList, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.DestructorDef, parser, parent, startToken, parens.CloseToken, null, GreenZoneSysUtilsBase.AsList(identifierToken), parens, argumentList, false, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            parser.RaiseNodeCreated(this);
        }
    }
}
