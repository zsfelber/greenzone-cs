﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ThrowStatementNode : StatementNode
    {
        public ThrowStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, ExpressionNode right, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.Throw, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.right = right;
            parser.RaiseNodeCreated(this);
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
