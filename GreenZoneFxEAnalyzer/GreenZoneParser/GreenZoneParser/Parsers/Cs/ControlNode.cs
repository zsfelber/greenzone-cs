using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class ControlNode : NormalNode
    {
        public ControlNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, Token firstHeaderToken)
            : base(parser, parent, startToken, endToken)
        {
            if (firstHeaderToken != null)
            {
                if (firstHeaderToken.Index >= startToken.Index)
                {
                    throw new NotSupportedException();
                }
                IList<Token> tokens = parser.Tokenizer.Result;

                attributes = ParseAttributes(parent, firstHeaderToken, startToken);
                if (attributes != null)
                {
                    attributesUm = attributes.AsReadOnly();
                    firstHeaderToken = tokens[attributes[attributes.Count - 1].EndToken.Index + 1];
                }
                modifiers = ParseModifiers(parent, firstHeaderToken, startToken);
                if (modifiers != null)
                {
                    modifiersUm = modifiers.AsReadOnly();
                    startToken = tokens[modifiers[modifiers.Count - 1].EndToken.Index + 1];
                }
            }
        }

        public ControlNode(CsParser parser, BlockNode parent, Token startToken, Token endToken, List<AttributeNode> attributes, List<ModifierNode> modifiers)
            : base(parser, parent, startToken, endToken)
        {
            this.attributes = attributes;
            if (attributes != null)
            {
                attributesUm = attributes.AsReadOnly();
            }
            this.modifiers = modifiers;
            if (modifiers != null)
            {
                modifiersUm = modifiers.AsReadOnly();
            }
        }

        readonly List<AttributeNode> attributes;
        protected readonly IList<AttributeNode> attributesUm;
        public IList<AttributeNode> Attributes
        {
            get
            {
                return attributesUm;
            }
        }

        readonly List<ModifierNode> modifiers;
        protected readonly IList<ModifierNode> modifiersUm;
        public IList<ModifierNode> Modifiers
        {
            get
            {
                return modifiersUm;
            }
        }
    }
}
