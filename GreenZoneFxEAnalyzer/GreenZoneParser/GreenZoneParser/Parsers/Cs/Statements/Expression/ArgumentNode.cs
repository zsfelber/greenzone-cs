using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public enum ArgumentType
    {
        Value,
        Ref,
        Out,
        Params,
        This
    }

    public class ArgumentNode : ExpressionNode
    {
        public ArgumentNode(CsParser parser, BlockNode parent, ArgumentType type, Token refToken, ExpressionNode right)
            : base(StatementId.Argument, parser, parent, right.StartToken, right.EndToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.type = type;
            this.refToken = refToken;
            this.right = right;
            parser.RaiseNodeCreated(this);
        }

        readonly ArgumentType type;
        public ArgumentType Type
        {
            get
            {
                return type;
            }
        }

        readonly Token refToken;
        public Token RefToken
        {
            get
            {
                return refToken;
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
