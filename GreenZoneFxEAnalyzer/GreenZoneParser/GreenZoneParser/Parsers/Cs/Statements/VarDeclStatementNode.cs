using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class VarDeclStatementNode : StatementNode
    {
        public VarDeclStatementNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeSpecifier, List<ExpressionNode> variables, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(StatementId.VarDecl, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.typeSpecifier = typeSpecifier;
            this.variables = variables;
            if (variables != null)
            {
                variablesUm = variables.AsReadOnly();
            }
            parser.RaiseNodeCreated(this);
        }

        readonly TypeSpecifierNode typeSpecifier;
        public TypeSpecifierNode TypeSpecifier
        {
            get
            {
                return typeSpecifier;
            }
        }

        readonly List<ExpressionNode> variables;
        readonly IList<ExpressionNode> variablesUm;
        public IList<ExpressionNode> Variables
        {
            get
            {
                return variablesUm;
            }
        }
    }
}
