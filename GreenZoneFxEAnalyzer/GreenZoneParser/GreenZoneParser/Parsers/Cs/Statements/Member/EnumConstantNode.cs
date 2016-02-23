using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class EnumConstantNode : StatementNode
    {
        public EnumConstantNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, Token identifierToken, ExpressionNode right, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.EnumConstant, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.identifierToken = identifierToken;
            this.right = right;
            parser.RaiseNodeCreated(this);
        }

        readonly Token identifierToken;
        public Token IdentifierToken
        {
            get
            {
                return identifierToken;
            }
        }

        readonly ExpressionNode right;
        public ExpressionNode Right
        {
            get
            {
                return right;
            }
        }
    }
}
