using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class EventDeclStatementNode : StatementNode
    {
        public EventDeclStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, VarDeclStatementNode varDeclHeader, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.EventDecl, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.varDeclHeader = varDeclHeader;
            parser.RaiseNodeCreated(this);
        }

        readonly VarDeclStatementNode varDeclHeader;
        public VarDeclStatementNode VarDeclHeader
        {
            get
            {
                return varDeclHeader;
            }
        }
    }
}
