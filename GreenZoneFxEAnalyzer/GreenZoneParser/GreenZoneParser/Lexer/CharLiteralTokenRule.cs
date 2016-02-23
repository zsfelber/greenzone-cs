using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class CharLiteralTokenRule : TokenRule
    {
        enum CharLiteralMatchState
        {
            NORMAL,
            FILLED,
            ESCAPED
        }

        CharLiteralMatchState state = CharLiteralMatchState.NORMAL;

        internal CharLiteralTokenRule(Parser parser)
            : base(parser, TokenId.CharLiteral)
        {
        }

        string value;
        public override object Value
        {
            get
            {
                return value;
            }
        }

        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            state = CharLiteralMatchState.NORMAL;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (state == CharLiteralMatchState.ESCAPED)
            {
                state = CharLiteralMatchState.FILLED;
            }
            else
            {
                switch (ch)
                {
                    case '\'':
                        if (currentState == TokenMatchState.STARTED)
                        {
                            if (state != CharLiteralMatchState.FILLED)
                            {
                                CompilationErrorEnty err = parser.CreateError("Character constant should consist of one character.", tokenStart, parser.Tokenizer.Position);
                                currentErrors.Add(err);
                                currentState = TokenMatchState.ERROR;
                            }
                            else
                            {
                                currentState = TokenMatchState.MATCHED;
                                tokenEnd = parser.Tokenizer.Position;
                                value = parser.GetContent(tokenStart + 1, tokenEnd - 1);
                            }
                        }
                        else
                        {
                            currentState = TokenMatchState.STARTED;
                            tokenStart = parser.Tokenizer.Position;
                        }
                        break;
                    case '\\':
                        if (currentState == TokenMatchState.STARTED)
                        {
                            if (state == CharLiteralMatchState.FILLED)
                            {
                                CompilationErrorEnty err = parser.CreateError("Character constant should consist of one character.", tokenStart, parser.Tokenizer.Position);
                                currentErrors.Add(err);
                                currentState = TokenMatchState.ERROR;
                            }
                            else
                            {
                                state = CharLiteralMatchState.ESCAPED;
                            }
                        }
                        break;
                    default:
                        if (currentState == TokenMatchState.STARTED)
                        {
                            if (state == CharLiteralMatchState.FILLED)
                            {
                                CompilationErrorEnty err = parser.CreateError("Character constant should consist of one character.", tokenStart, parser.Tokenizer.Position);
                                currentErrors.Add(err);
                                currentState = TokenMatchState.ERROR;
                            }
                            else
                            {
                                state = CharLiteralMatchState.FILLED;
                            }
                        }
                        break;
                }
            }
        }

    }
}
