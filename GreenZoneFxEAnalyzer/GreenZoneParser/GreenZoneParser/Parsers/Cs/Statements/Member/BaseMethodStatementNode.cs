using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public abstract class BaseMethodStatementNode : StatementNode
    {
        public BaseMethodStatementNode(StatementId statementId, CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeExpression, List<Token> identifierTokens, TokenOpenClose parens, List<VarDeclArgumentNode> argumentList, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(statementId, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.typeExpression = typeExpression;
            this.parens = parens;
            this.argumentList = argumentList;

            this.identifierTokens = identifierTokens;
            if (identifierTokens != null)
            {
                this.identifierTokensUm = identifierTokens.AsReadOnly();
                methodId = parser.GetContent(identifierTokens[0].TokenStartPos, identifierTokens[identifierTokens.Count - 1].TokenEndPos);
            }
            if (argumentList != null)
            {
                this.argumentListUm = argumentList.AsReadOnly();
            }
        }

        readonly TypeSpecifierNode typeExpression;
        public TypeSpecifierNode TypeExpression
        {
            get
            {
                return typeExpression;
            }
        }

        readonly string methodId;
        public string MethodId
        {
            get
            {
                return methodId;
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

        protected readonly TokenOpenClose parens;
        public TokenOpenClose Parens
        {
            get
            {
                return parens;
            }
        }

        readonly List<VarDeclArgumentNode> argumentList;
        readonly IList<VarDeclArgumentNode> argumentListUm;
        public IList<VarDeclArgumentNode> ArgumentList
        {
            get
            {
                return argumentListUm;
            }
        }
    }
}
