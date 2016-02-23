using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class WhiteSpaceTokenRule : TokenRule
    {

        internal WhiteSpaceTokenRule(Parser parser)
            : base(parser, TokenId.WhiteSpace)
        {
        }

        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (char.IsWhiteSpace(ch))
            {
                if (currentState == TokenMatchState.NO_MATCH)
                {
                    currentState = TokenMatchState.STARTED;
                    tokenStart = parser.Tokenizer.Position;
                }
            }

            if (currentState == TokenMatchState.STARTED && (!char.IsWhiteSpace(next_ch) || next_ch == '\0'))
            {
                tokenEnd = parser.Tokenizer.Position;
                currentState = TokenMatchState.MATCHED;
            }
        }

    }
}
