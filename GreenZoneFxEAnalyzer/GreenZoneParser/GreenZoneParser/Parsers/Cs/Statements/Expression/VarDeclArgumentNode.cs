using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class VarDeclArgumentNode : ExpressionNode
    {
        public VarDeclArgumentNode(CsParser parser, BlockNode parent, ArgumentType type, Token refToken, VarDeclStatementNode right)
            : base(StatementId.VarDeclArgument, parser, parent, right.StartToken, right.EndToken, false, null, null, null, null, StatementParseMode.Inside)
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

        readonly VarDeclStatementNode right;
        public VarDeclStatementNode Right
        {
            get
            {
                return right;
            }
        }
    }
}
