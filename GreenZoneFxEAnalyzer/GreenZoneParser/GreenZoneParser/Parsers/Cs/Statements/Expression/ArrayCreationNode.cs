using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ArrayCreationNode : ExpressionNode
    {
        public ArrayCreationNode(CsParser parser, BlockNode parent, TypeSpecifierNode typeExpression, Token newToken, Token lastToken, BlockNode arrayInitializerBlock)
            : base(StatementId.ArrayCreation, parser, parent, newToken, lastToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.typeExpression = typeExpression;
            this.newToken = newToken;
            this.arrayInitializerBlock = arrayInitializerBlock;
            parser.RaiseNodeCreated(this);
        }

        readonly TypeSpecifierNode typeExpression;
        public TypeSpecifierNode TypeExpression
        {
            get
            {
                return typeExpression;
            }
        }

        readonly Token newToken;
        public Token NewToken
        {
            get
            {
                return newToken;
            }
        }

        readonly BlockNode arrayInitializerBlock;
        public BlockNode ArrayInitializerBlock
        {
            get
            {
                return arrayInitializerBlock;
            }
        }
    }
}