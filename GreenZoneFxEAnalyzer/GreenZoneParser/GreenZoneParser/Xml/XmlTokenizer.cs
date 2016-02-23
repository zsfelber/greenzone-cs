using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneParser.Lexer;
using System.ComponentModel;

namespace GreenZoneParser.Xml
{
    public class XmlTokenizer : Tokenizer
    {
        TokenOpenClose currentGeneric;

        internal XmlTokenizer(XmlParser parser, string fileContent)
            : base(parser, fileContent)
        {
            this.currentGeneric = new TokenOpenClose(null, TokenOpenCloseType.Generic);
            this.genericOpens = new List<Token>();

            genericOpensUm = genericOpens.AsReadOnly();

            rules.Add(new SimpleTokenRule(parser, TokenId.Gt, ">"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Lt, "<"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Slash, "/"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Assign, "="));

            rules.Add(new IdentifierTokenRule(parser, "_", "_-"));
            rules.Add(new NumberLiteralTokenRule(parser));
            rules.Add(new StringLiteralTokenRule(parser));
            rules.Add(new StringLiteralTokenRule(parser, '\''));

            rules.Add(new WhiteSpaceTokenRule(parser));

            Init();
        }

        public XmlParser XmlParser
        {
            get
            {
                return (XmlParser)parser;
            }
        }

        readonly List<Token> genericOpens;
        readonly IList<Token> genericOpensUm;
        public IList<Token> GenericOpens
        {
            get
            {
                return genericOpensUm;
            }
        }

        public override void ClearTokens()
        {
            base.ClearTokens();
            genericOpens.Clear();
            currentGeneric = null;
        }


        public TokenOpenClose Generic(Token token)
        {
            TokenOpenClose result;
            if (token.GenericIndex < genericOpens.Count)
            {
                token = genericOpens[token.GenericIndex];
                // !
                result = token.GenericBlock;
                if (result.Type != TokenOpenCloseType.Generic)
                {
                    throw new NotSupportedException();
                }
            }
            else
            {
                result = null;
            }

            return result;
        }

        protected override Token processRule(TokenRule rule)
        {
            TokenOpenClose toc = null;
            switch (rule.TokenId)
            {
                case TokenId.Lt:
                    toc = push(ref currentGeneric, TokenOpenCloseType.Generic);
                    break;
                case TokenId.Gt:
                    toc = pop(ref currentGeneric, TokenOpenCloseType.Generic);
                    break;
            }

            Token token = rule.GenerateToken(totalResult.Count, result.Count, 0, 0, 0, genericOpens.Count, null, currentGeneric);
            totalResult.Add(token);
            switch (token.Id)
            {
                case TokenId.WhiteSpace:
                    break;
                default:
                    result.Add(token);
                    break;
            }

            switch (token.Id)
            {
                case TokenId.Lt:
                    genericOpens.Add(token);
                    toc.OpenToken = token;
                    break;
                case TokenId.Gt:
                    toc.CloseToken = token;
                    break;
            }

            return token;
        }

        TokenOpenClose push(ref TokenOpenClose currentToc, TokenOpenCloseType type)
        {
            TokenOpenClose toc = new TokenOpenClose(currentToc, type);
            currentToc = toc;
            return toc;
        }

        TokenOpenClose pop(ref TokenOpenClose currentToc, TokenOpenCloseType type)
        {
            if (currentToc.Type != type)
            {
                return null;
            }
            else
            {
                TokenOpenClose toc = currentToc;
                currentToc = currentToc.Parent;
                return toc;
            }
        }
    }
}