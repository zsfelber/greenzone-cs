using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class MethodDefStatementNode : BaseMethodStatementNode
    {
        public MethodDefStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeExpression, List<Token> identifierTokens, TokenOpenClose parens, List<VarDeclArgumentNode> argumentList, List<TypeSpecifierNode> typeParameters, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.MethodDef, parser, parent, startToken, endToken, typeExpression, identifierTokens, parens, argumentList, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.typeParameters = typeParameters;
            if (typeParameters != null)
            {
                typeParametersUm = typeParameters.AsReadOnly();
            }
            parser.RaiseNodeCreated(this);
        }

        readonly List<TypeSpecifierNode> typeParameters;
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
