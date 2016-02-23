using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ObjectCreationNode : ExpressionNode
    {
        public ObjectCreationNode(CsParser parser, BlockNode parent, TypeSpecifierNode typeExpression, Token newToken, TokenOpenClose parens, List<ArgumentNode> argumentList)
            : base(StatementId.ObjectCreation, parser, parent, newToken, parens.CloseToken, false, null, null, null, null, StatementParseMode.Inside)
        {
            this.typeExpression = typeExpression;
            this.newToken = newToken;
            this.parens = parens;
            this.argumentList = argumentList;
            if (argumentList != null)
            {
                this.argumentListUm = argumentList.AsReadOnly();
            }
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

        readonly TokenOpenClose parens;
        public TokenOpenClose Parens
        {
            get
            {
                return parens;
            }
        }

        readonly List<ArgumentNode> argumentList;
        readonly IList<ArgumentNode> argumentListUm;
        public IList<ArgumentNode> ArgumentList
        {
            get
            {
                return argumentListUm;
            }
        }

        public override bool ValidStatement
        {
            get
            {
                return true;
            }
        }
    }
}