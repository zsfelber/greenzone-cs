using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class IdentifierTokenRule : TokenRule
    {
        readonly string specialCharsStart, specialCharsMiddle;

        internal IdentifierTokenRule(Parser parser, string specialCharsStart, string specialCharsMiddle)
            : base(parser, TokenId.Identifier)
        {
            this.specialCharsStart = specialCharsStart;
            this.specialCharsMiddle = specialCharsMiddle;
        }

        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (char.IsLetter(ch) || specialCharsStart.Contains(ch))
            {
                if (currentState == TokenMatchState.NO_MATCH)
                {
                    currentState = TokenMatchState.STARTED;
                    tokenStart = parser.Tokenizer.Position;
                }
            }
            else if (char.IsDigit(ch) || specialCharsMiddle.Contains(ch))
            {
            }

            if (currentState == TokenMatchState.STARTED && !char.IsLetterOrDigit(next_ch) && !specialCharsMiddle.Contains(next_ch))
            {
                tokenEnd = parser.Tokenizer.Position;
                currentState = TokenMatchState.MATCHED;
            }
        }

    }
}
