using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Parsers.Cs
{
    public class ConstructorDefStatementNode : BaseMethodStatementNode
    {
        public ConstructorDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, Token identifierToken, TokenOpenClose parens, List<VarDeclArgumentNode> argumentList, MethodExpressionNode baseOrThisCall, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.ConstructorDef, parser, parent, startToken, endToken, null, GreenZoneSysUtilsBase.AsList(identifierToken), parens, argumentList, false, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.baseOrThisCall = baseOrThisCall;
            parser.RaiseNodeCreated(this);
        }

        readonly MethodExpressionNode baseOrThisCall;
        public MethodExpressionNode BaseOrThisCall
        {
            get
            {
                return baseOrThisCall;
            }
        }
    }
}
