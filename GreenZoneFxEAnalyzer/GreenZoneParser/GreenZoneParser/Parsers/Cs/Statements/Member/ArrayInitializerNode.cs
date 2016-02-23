using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ArrayInitializerNode : StatementNode
    {
        public ArrayInitializerNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, ExpressionNode initializerExpression, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.ArrayInitializer, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.initializerExpression = initializerExpression;
            this.initializerBlock = subStatementsBlock;
            parser.RaiseNodeCreated(this);
        }

        readonly ExpressionNode initializerExpression;
        public ExpressionNode InitializerExpression
        {
            get
            {
                return initializerExpression;
            }
        }

        readonly BlockNode initializerBlock;
        public BlockNode InitializerBlock
        {
            get
            {
                return SubStatementsBlock;
            }
        }
    }
}
