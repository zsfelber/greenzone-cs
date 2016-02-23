using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class SingleLineCommentTokenRule : TokenRule
    {
        enum SingleLineCommentMatchState
        {
            NORMAL,
            AFTER_BEGIN
        }

        SingleLineCommentMatchState state = SingleLineCommentMatchState.NORMAL;

        internal SingleLineCommentTokenRule(Parser parser)
            : base(parser, TokenId.Comment)
        {
        }

        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            state = SingleLineCommentMatchState.NORMAL;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (currentState == TokenMatchState.NO_MATCH)
            {
                if (ch == '/')
                {
                    currentState = TokenMatchState.STARTED;
                    state = SingleLineCommentMatchState.AFTER_BEGIN;
                    tokenStart = parser.Tokenizer.Position;
                }
            }
            else
            {
                if (state == SingleLineCommentMatchState.AFTER_BEGIN)
                {
                    if (ch == '/')
                    {
                        state = SingleLineCommentMatchState.NORMAL;
                    }
                    else
                    {
                        currentState = TokenMatchState.NO_MATCH;
                    }
                }
            }

            if (currentState == TokenMatchState.STARTED && state == SingleLineCommentMatchState.NORMAL &&
                (next_ch == '\n' || next_ch == '\0'))
            {
                tokenEnd = parser.Tokenizer.Position;
                currentState = TokenMatchState.MATCHED;
            }
        }
    }

}