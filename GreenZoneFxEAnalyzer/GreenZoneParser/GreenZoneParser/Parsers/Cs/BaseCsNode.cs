using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public class BaseCsNode : BaseNode
    {
        public BaseCsNode(CsParser parser, BlockNode parent, Token startToken, Token endToken)
            : base(parser, parent, startToken.TokenStartPos, endToken.TokenEndPos)
        {
            this.startToken = startToken;
            this.endToken = endToken;
        }

        public CsParser CSParser
        {
            get
            {
                return (CsParser)parser;
            }
        }

        public new BlockNode Parent
        {
            get
            {
                return (BlockNode)parent;
            }
        }

        readonly Token startToken;
        public Token StartToken
        {
            get
            {
                return startToken;
            }
        }

        readonly Token endToken;
        public Token EndToken
        {
            get
            {
                return endToken;
            }
        }

        protected List<AttributeNode> ParseAttributes(BlockNode parent, Token from, Token to)
        {
            if (from.Index > to.Index)
            {
                throw new NotSupportedException();
            }
            List<AttributeNode> result = null;

            TokenOpenClose toc;
            if (from.Id == TokenId.ArrayOrAttributeOpen)
            {
                toc = CSParser.CsTokenizer.Array(from);

                for (; toc != null && toc.OpenToken.Index < to.Index; toc = toc.NextArray())
                {
                    if (toc.Children.Count > toc.ChildrenParen.Count)
                    {
                        parser.AddError("Nested attribute token is unsupported.", toc.OpenToken.TokenStartPos, toc.OpenToken.TokenEndPos);
                        break;
                    }
                    if (toc.CloseToken.Index > to.Index)
                    {
                        parser.AddError("Missing attribute open token.", toc.OpenToken.TokenStartPos, to.TokenStartPos - 1);
                        break;
                    }
                    if (result == null)
                    {
                        result = new List<AttributeNode>();
                    }
                    AttributeNode attribute = new AttributeNode(CSParser, parent, toc);
                    result.Add(attribute);
                }
            }
            return result;
        }

        protected List<ModifierNode> ParseModifiers(BlockNode parent, Token from, Token to)
        {
            if (from.Index > to.Index)
            {
                throw new NotSupportedException();
            }
            List<ModifierNode> result = null;

            for (Token token = from; token.Index<=to.Index; token=parser.Tokenizer.Next(token))
            {
                if (!token.Id.IsModifier(parent.ParseMode))
                {
                    break;
                }
                if (result == null)
                {
                    result = new List<ModifierNode>();
                }
                ModifierNode modifier = new ModifierNode(CSParser, parent, token);
                result.Add(modifier);
            }
            return result;
        }

        public override string ToString()
        {
            int ed = Math.Max(StartPos, Math.Min(StartPos + 16, EndPos));
            string result = parser.GetContent(StartPos, ed) + (ed < EndPos ? "..." : "");
            return result;
        }

    }
}
