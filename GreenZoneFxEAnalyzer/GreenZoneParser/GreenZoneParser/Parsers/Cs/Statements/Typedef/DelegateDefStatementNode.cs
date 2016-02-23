using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class DelegateDefStatementNode : TypeDefStatementNode
    {
        public DelegateDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, MethodDefStatementNode signature, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.DelegateDef, parser, parent, startToken, endToken, signature.identifierTokens, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.signature = signature;
            parser.RaiseNodeCreated(this);
        }

        readonly MethodDefStatementNode signature;
        public MethodDefStatementNode Signature
        {
            get
            {
                return signature;
            }
        }
    }
}
