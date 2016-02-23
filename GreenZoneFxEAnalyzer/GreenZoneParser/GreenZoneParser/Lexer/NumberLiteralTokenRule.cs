using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GreenZoneParser.Lexer
{
    class NumberLiteralTokenRule : TokenRule
    {
        enum NumberLiteralMatchState
        {
            BEGINNING,
            AFTER_POINT,
            AFTER_POINT_OK,
            AFTER_FLP_E,
            AFTER_FLP_E_PM,
            AFTER_FLP_E_OK,
            AFTER_END_1,
            AFTER_END_2,
            AFTER_END_1_2,
        }

        NumberLiteralMatchState state = NumberLiteralMatchState.BEGINNING;
        string fpeChars = "eE";
        string endingByte = "bB";
        string endingShort = "sS";
        string endingInt = "iI";
        string endingLong = "lL";
        string endingFloat = "fF";
        string endingDouble = "dD";
        string endingUnsigned = "uU";
        string plusMinusChars = "+-";

        internal NumberLiteralTokenRule(Parser parser)
            : base(parser)
        {
            tokenId = TokenId.IntLiteral;
        }


        internal override void Reset()
        {
            currentState = TokenMatchState.NO_MATCH;
            state = NumberLiteralMatchState.BEGINNING;
            tokenId = TokenId.IntLiteral;
            currentErrors.Clear();
        }

        public string NumberContent
        {
            get
            {
                int end = tokenEnd;
                if (state == NumberLiteralMatchState.AFTER_END_1 || state == NumberLiteralMatchState.AFTER_END_2)
                {
                    end--;
                }
                else if (state == NumberLiteralMatchState.AFTER_END_1_2)
                {
                    end -= 2;
                }

                string ns = parser.GetContent(tokenStart, end);
                if (ns[0] == '.')
                {
                    ns = '0' + ns;
                }
                return ns;
            }
        }

        double value;
        public override object Value
        {
            get
            {
                return value;
            }
        }

        internal override void Apply(char ch, char next_ch)
        {
            if (currentState == TokenMatchState.NO_MATCH)
            {
                defaultParse(ch);
            }

            ch = next_ch;

            if (currentState == TokenMatchState.STARTED)
            {
                if (defaultParse(ch))
                {
                }
                else if (fpeChars.Contains(ch))
                {
                    if (state != NumberLiteralMatchState.BEGINNING &&
                        state != NumberLiteralMatchState.AFTER_POINT_OK)
                    {
                        CompilationErrorEnty err = parser.CreateError("Exponent marker is not valid here.", tokenStart, parser.Tokenizer.Position);
                        currentErrors.Add(err);
                        currentState = TokenMatchState.ERROR;
                    }
                    else
                    {
                        state = NumberLiteralMatchState.AFTER_FLP_E;
                        tokenId = TokenId.DoubleLiteral;
                    }
                }
                else if (state == NumberLiteralMatchState.AFTER_FLP_E && plusMinusChars.Contains(ch))
                {
                    state = NumberLiteralMatchState.AFTER_FLP_E_PM;
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
                else if (parseTypeEnding(endingFloat, ch, TokenId.FloatLiteral, true))
                {
                }
                else if (parseTypeEnding(endingDouble, ch, TokenId.DoubleLiteral, true))
                {
                }
                else if (endingUnsigned.Contains(ch))
                {
                    if (state == NumberLiteralMatchState.AFTER_END_2 || state == NumberLiteralMatchState.AFTER_END_1_2)
                    {
                        CompilationErrorEnty err = parser.CreateError("Multiple unsigned-mark", tokenStart, parser.Tokenizer.Position);
                        currentErrors.Add(err);
                        currentState = TokenMatchState.ERROR;
                    }
                    else
                    {
                        if (state == NumberLiteralMatchState.AFTER_END_1)
                        {
                            state = NumberLiteralMatchState.AFTER_END_1_2;
                        }
                        else
                        {
                            state = NumberLiteralMatchState.AFTER_END_2;
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
                    if (state == NumberLiteralMatchState.AFTER_FLP_E || state == NumberLiteralMatchState.AFTER_FLP_E_PM || state == NumberLiteralMatchState.AFTER_POINT)
                    {
                        CompilationErrorEnty err = parser.CreateError("Invalid number literal ending.", tokenStart, parser.Tokenizer.Position);
                        currentErrors.Add(err);
                        currentState = TokenMatchState.ERROR;
                    }
                    else
                    {
                        tokenEnd = parser.Tokenizer.Position;
                        try
                        {
                            string result = NumberContent;
                            switch (tokenId)
                            {
                                case TokenId.ByteLiteral: value = Convert.ToByte(result); break;
                                case TokenId.ShortLiteral: value = Convert.ToInt16(result); break;
                                case TokenId.UShortLiteral: value = Convert.ToUInt16(result); break;
                                case TokenId.IntLiteral: value = Convert.ToInt32(result); break;
                                case TokenId.UIntLiteral: value = Convert.ToUInt32(result); break;
                                case TokenId.LongLiteral: value = Convert.ToInt64(result); break;
                                case TokenId.ULongLiteral: value = Convert.ToUInt64(result); break;
                                case TokenId.FloatLiteral: value = Convert.ToSingle(result); break;
                                case TokenId.DoubleLiteral: value = Convert.ToDouble(result); break;
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
        }

        bool defaultParse(char ch)
        {
            if (appendNum(ch))
            {
                if (state == NumberLiteralMatchState.AFTER_FLP_E || state == NumberLiteralMatchState.AFTER_FLP_E_PM)
                {
                    state = NumberLiteralMatchState.AFTER_FLP_E_OK;
                }
                else if (state == NumberLiteralMatchState.AFTER_POINT)
                {
                    state = NumberLiteralMatchState.AFTER_POINT_OK;
                }
                return true;
            }
            else if (ch == '.')
            {
                if (state != NumberLiteralMatchState.BEGINNING)
                {
                    CompilationErrorEnty err = parser.CreateError("Decimal point is not valid here.", tokenStart, parser.Tokenizer.Position);
                    currentErrors.Add(err);
                    currentState = TokenMatchState.ERROR;
                }
                else
                {
                    if (currentState != TokenMatchState.STARTED)
                    {
                        currentState = TokenMatchState.STARTED;
                        tokenStart = parser.Tokenizer.Position;
                    }
                    state = NumberLiteralMatchState.AFTER_POINT;
                    tokenId = TokenId.DoubleLiteral;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        bool appendNum(char ch)
        {
            if ('0' <= ch && ch <= '9')
            {
                if (currentState != TokenMatchState.STARTED)
                {
                    currentState = TokenMatchState.STARTED;
                    tokenStart = parser.Tokenizer.Position;
                }
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
                if (state == NumberLiteralMatchState.AFTER_FLP_E || state == NumberLiteralMatchState.AFTER_FLP_E_PM || 
                    state == NumberLiteralMatchState.AFTER_POINT ||
                    state == NumberLiteralMatchState.AFTER_END_1 || state == NumberLiteralMatchState.AFTER_END_1_2)
                {
                    CompilationErrorEnty err = parser.CreateError("Type marker char is not valid here.", tokenStart, parser.Tokenizer.Position);
                    currentErrors.Add(err);
                    currentState = TokenMatchState.ERROR;
                    return true;
                }

                if (!isFp && tokenId == TokenId.DoubleLiteral)
                {
                    CompilationErrorEnty err = parser.CreateError("Invalid type marker char after floating point number literal : " + preferredToken, tokenStart, parser.Tokenizer.Position);
                    currentErrors.Add(err);
                    currentState = TokenMatchState.ERROR;
                    return true;
                }

                if (state == NumberLiteralMatchState.AFTER_END_2)
                {
                    try
                    {
                        state = NumberLiteralMatchState.AFTER_END_1_2;
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
                else
                {
                    state = NumberLiteralMatchState.AFTER_END_1;
                    tokenId = preferredToken;
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