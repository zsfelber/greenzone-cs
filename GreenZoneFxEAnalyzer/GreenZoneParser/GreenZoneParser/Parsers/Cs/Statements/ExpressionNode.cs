using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public abstract class ExpressionNode : StatementNode
    {
        public ExpressionNode(StatementId expressionId, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(expressionId, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode)
        {
            this.acceptedTypes = new List<ResolvedType>();
            if (acceptedTypes != null)
            {
                acceptedTypesUm = acceptedTypes.AsReadOnly();
            }
        }

        readonly List<ResolvedType> acceptedTypes;
        readonly IList<ResolvedType> acceptedTypesUm;
        public IList<ResolvedType> AcceptedTypes
        {
            get
            {
                return acceptedTypesUm;
            }
        }

        public virtual bool ValidStatement
        {
            get
            {
                return false;
            }
        }

        internal void AddAcceptedType(ResolvedType type)
        {
            acceptedTypes.Add(type);
        }
    }
}
