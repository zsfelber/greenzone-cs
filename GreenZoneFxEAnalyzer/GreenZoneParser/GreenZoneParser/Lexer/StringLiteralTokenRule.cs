using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class StringLiteralTokenRule : TokenRule
    {
        enum StringLiteralMatchState
        {
            NORMAL,
            ESCAPED
        }

        StringLiteralMatchState state = StringLiteralMatchState.NORMAL;

        readonly char quote;

        internal StringLiteralTokenRule(Parser parser)
            : base(parser, TokenId.StringLiteral)
        {
            this.quote = '"';
        }

        internal StringLiteralTokenRule(Parser parser, char quote)
            : base(parser, TokenId.StringLiteral)
        {
            this.quote = quote;
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
            state = StringLiteralMatchState.NORMAL;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (state == StringLiteralMatchState.ESCAPED)
            {
                state = StringLiteralMatchState.NORMAL;
            }
            else
            {
                switch (ch)
                {
                    case '\\':
                        if (currentState == TokenMatchState.STARTED)
                        {
                            state = StringLiteralMatchState.ESCAPED;
                        }
                        break;
                    default:
                        if (ch == quote)
                        {
                            if (currentState == TokenMatchState.STARTED)
                            {
                                tokenEnd = parser.Tokenizer.Position;
                                value = parser.GetContent(tokenStart + 1, tokenEnd - 1);
                                currentState = TokenMatchState.MATCHED;
                            }
                            else
                            {
                                currentState = TokenMatchState.STARTED;
                                tokenStart = parser.Tokenizer.Position;
                            }
                        }
                        else if (currentState == TokenMatchState.STARTED)
                        {
                            if (ch == '\n')
                            {
                                CompilationErrorEnty err = parser.CreateError("String literal not terminated", tokenStart, parser.Tokenizer.Position);
                                currentErrors.Add(err);
                                currentState = TokenMatchState.ERROR_NO_BACKSTEP;
                            }
                        }
                        break;
                }
            }
        }

    }
}
