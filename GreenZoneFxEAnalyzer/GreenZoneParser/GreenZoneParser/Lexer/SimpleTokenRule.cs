using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneParser.Lexer
{
    class SimpleTokenRule : TokenRule
    {
        int cursor = 0;
        readonly string token;
        internal SimpleTokenRule(Parser parser, TokenId tokenId, string token)
            : base(parser, tokenId)
        {
            this.token = token;
        }

        public override string CurrentTokenContent
        {
            get
            {
                return token;
            }
        }

        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            cursor = 0;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (token[cursor] != ch)
            {
                currentState = TokenMatchState.NO_MATCH;
            }
            else
            {
                cursor++;
                if (cursor == token.Length)
                {
                    currentState = TokenMatchState.MATCHED;
                    tokenEnd = parser.Tokenizer.Position;
                }
                else
                {
                    currentState = TokenMatchState.STARTED;
                }
                if (cursor == 1)
                {
                    tokenStart = parser.Tokenizer.Position;
                }
            }
        }
    }
}
