using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class HexLiteralTokenRule : TokenRule
    {
        enum HexLiteralMatchState
        {
            NOT_STARTED,
            BEGINNING,
            AFTER_END_1,
            AFTER_END_2,
            AFTER_END_1_2,
        }

        HexLiteralMatchState state = HexLiteralMatchState.NOT_STARTED;
        string hexChars = "xX";
        string endingByte = "bB";
        string endingShort = "sS";
        string endingInt = "iI";
        string endingLong = "lL";
        string endingUnsigned = "uU";

        internal HexLiteralTokenRule(Parser parser)
            : base(parser)
        {
            tokenId = TokenId.IntLiteral;
        }

        decimal value;
        public override object Value
        {
            get
            {
                return value;
            }
        }

        public string NumberContent
        {
            get
            {
                int end = tokenEnd;
                if (state == HexLiteralMatchState.AFTER_END_1 || state == HexLiteralMatchState.AFTER_END_2)
                {
                    end--;
                }
                else if (state == HexLiteralMatchState.AFTER_END_1_2)
                {
                    end -= 2;
                }

                return parser.GetContent(tokenStart, end);
            }
        }

        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            state = HexLiteralMatchState.NOT_STARTED;
            tokenId = TokenId.IntLiteral;
            currentErrors.Clear();
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (state == HexLiteralMatchState.NOT_STARTED)
            {
                if (currentState == TokenMatchState.STARTED)
                {
                    if (hexChars.Contains(ch))
                    {
                        tokenStart = parser.Tokenizer.Position - 1;
                        state = HexLiteralMatchState.BEGINNING;
                    }
                    else if (ch != '0')
                    {
                        currentState = TokenMatchState.NO_MATCH;
                    }
                }
                else
                {
                    if (ch == '0')
                    {
                        currentState = TokenMatchState.STARTED;
                    }
                }
            }

            ch = next_ch;

            if (state != HexLiteralMatchState.NOT_STARTED)
            {
                if (appendNum(ch))
                {
                }
                else if (parseTypeEnding(endingByte, ch, TokenId.ByteLiteral, false))
                {
                }
                else if (parseTypeEnding(endingShort, ch, TokenId.ShortLiteral, false))
                {
                }
                else if (parseTypeEnding(endingInt, ch, TokenId.IntLiteral, false))
                {
                }
                else if (parseTypeEnding(endingLong, ch, TokenId.LongLiteral, false))
                {
                }
                else if (endingUnsigned.Contains(ch))
                {
                    if (state == HexLiteralMatchState.AFTER_END_1_2 || state == HexLiteralMatchState.AFTER_END_2)
                    {
                        CompilationErrorEnty err = parser.CreateError("Unsigned marker char is not valid here.", tokenStart, parser.Tokenizer.Position);
                        currentErrors.Add(err);
                        currentState = TokenMatchState.ERROR;
                    }
                    else
                    {

                        if (state == HexLiteralMatchState.AFTER_END_1)
                        {
                            state = HexLiteralMatchState.AFTER_END_1_2;
                        }
                        else
                        {
                            state = HexLiteralMatchState.AFTER_END_2;
                        }

                        try
                        {
                            tokenId = tokenId.UnsignedPair();
                        }
                        catch (ArgumentException)
                        {
                            CompilationErrorEnty err = parser.CreateError("Number type can not be unsigned : " + tokenId, tokenStart, parser.Tokenizer.Position);
                            currentErrors.Add(err);
                            currentState = TokenMatchState.ERROR;
                        }
                    }
                }
                else
                {
                    tokenEnd = parser.Tokenizer.Position;
                    try
                    {
                        string result = NumberContent;
                        switch (tokenId)
                        {
                            case TokenId.ByteLiteral: value = Convert.ToByte(result, 16); break;
                            case TokenId.ShortLiteral: value = Convert.ToInt16(result, 16); break;
                            case TokenId.UShortLiteral: value = Convert.ToUInt16(result, 16); break;
                            case TokenId.IntLiteral: value = Convert.ToInt32(result, 16); break;
                            case TokenId.UIntLiteral: value = Convert.ToUInt32(result, 16); break;
                            case TokenId.LongLiteral: value = Convert.ToInt64(result, 16); break;
                            case TokenId.ULongLiteral: value = Convert.ToUInt64(result, 16); break;
                            default: throw new NotSupportedException();
                        }
                        currentState = TokenMatchState.MATCHED;
                    }
                    catch (FormatException e)
                    {
                        CompilationErrorEnty err = parser.CreateError("Number is invalid : " + e.Message, tokenStart, tokenEnd);
                        currentErrors.Add(err);
                        currentState = TokenMatchState.ERROR;
                    }
                }
            }
        }

        bool appendNum(char ch)
        {
            if (('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'f') || ('A' <= ch && ch <= 'F'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool parseTypeEnding(string typeEnding, char ch, TokenId preferredToken, bool isFp)
        {
            if (typeEnding.Contains(ch))
            {
                if (state == HexLiteralMatchState.AFTER_END_1_2)
                {
                    CompilationErrorEnty err = parser.CreateError("Type marker char is not valid here.", tokenStart, parser.Tokenizer.Position);
                    currentErrors.Add(err);
                    currentState = TokenMatchState.ERROR;
                }
                else
                {

                    if (state == HexLiteralMatchState.AFTER_END_2)
                    {
                        try
                        {
                            state = HexLiteralMatchState.AFTER_END_1_2;
                            tokenId = preferredToken.UnsignedPair();
                        }
                        catch (ArgumentException)
                        {
                            CompilationErrorEnty err = parser.CreateError("Number type can not be unsigned : " + tokenId, tokenStart, parser.Tokenizer.Position);
                            currentErrors.Add(err);
                            currentState = TokenMatchState.ERROR;
                            return true;
                        }
                    }
                    else if (state == HexLiteralMatchState.AFTER_END_1)
                    {
                        CompilationErrorEnty err = parser.CreateError("Type marker char is not valid here.", tokenStart, parser.Tokenizer.Position);
                        currentErrors.Add(err);
                        currentState = TokenMatchState.ERROR;
                    }
                    else
                    {
                        state = HexLiteralMatchState.AFTER_END_1;
                        tokenId = preferredToken;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}