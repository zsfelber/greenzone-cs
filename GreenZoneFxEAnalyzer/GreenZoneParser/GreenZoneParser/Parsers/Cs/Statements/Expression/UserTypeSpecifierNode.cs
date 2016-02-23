using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class UserTypeSpecifierNode : TypeSpecifierNode
    {
        public UserTypeSpecifierNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, List<Token> identifierTokens, List<ExpressionNode> firstBracketExpressions, List<TypeSpecifierNode> typeParameters, TokenOpenClose initializerRanks, int commaSeparatedRanks, int emptyRanks, TokenOpenClose emptyRanksFirst, TokenOpenClose emptyRanksLast, Token pointerStar)
            : base(StatementId.UserType, parser, parent, startToken, endToken, firstBracketExpressions, initializerRanks, commaSeparatedRanks, emptyRanks, emptyRanksFirst, emptyRanksLast, pointerStar)
        {
            this.identifierTokens = identifierTokens;
            if (identifierTokens != null)
            {
                this.identifierTokensUm = identifierTokens.AsReadOnly();
                name = parser.GetContent(identifierTokens[0].TokenStartPos, identifierTokens[identifierTokens.Count - 1].TokenEndPos);
            }
            this.typeParameters = typeParameters;
            if (typeParameters != null)
            {
                typeParametersUm = typeParameters.AsReadOnly();
            }
            parser.RaiseNodeCreated(this);
        }

        readonly string name;
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override bool IsGeneric
        {
            get
            {
                return typeParameters != null;
            }
        }

        internal readonly List<Token> identifierTokens;
        readonly IList<Token> identifierTokensUm;
        public IList<Token> IdentifierTokens
        {
            get
            {
                return identifierTokensUm;
            }
        }

        internal readonly List<TypeSpecifierNode> typeParameters;
        readonly IList<TypeSpecifierNode> typeParametersUm;
        public IList<TypeSpecifierNode> TypeParameters
        {
            get
            {
                return typeParameters;
            }
        }

    }
}