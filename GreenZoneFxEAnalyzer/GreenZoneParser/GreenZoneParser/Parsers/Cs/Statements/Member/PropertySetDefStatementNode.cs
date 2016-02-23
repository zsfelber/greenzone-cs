﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Parsers.Cs
{
    public class PropertySetDefStatementNode : BaseMethodStatementNode
    {
        public PropertySetDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token identifierToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.PropertySet, parser, parent, startToken, startToken, null, GreenZoneSysUtilsBase.AsList(identifierToken), null, null, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            parser.RaiseNodeCreated(this);
        }
    }
}
