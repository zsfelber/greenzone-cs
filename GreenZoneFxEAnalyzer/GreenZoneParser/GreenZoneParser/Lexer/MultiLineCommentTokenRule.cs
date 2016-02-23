using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class MultiLineCommentTokenRule : TokenRule
    {
        enum MultiLineCommentMatchState
        {
            NORMAL,
            AFTER_BEGIN_1,
            AFTER_END_1,
        }

        MultiLineCommentMatchState state = MultiLineCommentMatchState.NORMAL;

        internal MultiLineCommentTokenRule(Parser parser)
            : base(parser, TokenId.Comment)
        {
        }


        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            state = MultiLineCommentMatchState.NORMAL;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (currentState == TokenMatchState.NO_MATCH)
            {
                if (ch == '/')
                {
                    currentState = TokenMatchState.STARTED;
                    state = MultiLineCommentMatchState.AFTER_BEGIN_1;
                    tokenStart = parser.Tokenizer.Position;
                }
            }
            else
            {
                switch (state)
                {
                    case MultiLineCommentMatchState.AFTER_BEGIN_1:
                        if (ch == '*')
                        {
                            state = MultiLineCommentMatchState.NORMAL;
                        }
                        else
                        {
                            currentState = TokenMatchState.NO_MATCH;
                        }
                        break;
                    case MultiLineCommentMatchState.AFTER_END_1:
                        if (ch == '/')
                        {
                            tokenEnd = parser.Tokenizer.Position;
                            currentState = TokenMatchState.MATCHED;
                        }
                        else
                        {
                            state = MultiLineCommentMatchState.NORMAL;
                        }
                        break;
                    default:
                        if (ch == '*')
                        {
                            state = MultiLineCommentMatchState.AFTER_END_1;
                        }
                        break;
                }
            }

            if (currentState == TokenMatchState.STARTED && state == MultiLineCommentMatchState.NORMAL &&
                next_ch == '\0')
            {
                CompilationErrorEnty err = parser.CreateError("Unclosed block comment.", tokenStart, parser.Tokenizer.Position);
                currentErrors.Add(err);
                currentState = TokenMatchState.ERROR_NO_BACKSTEP;
            }
        }
    }

}
