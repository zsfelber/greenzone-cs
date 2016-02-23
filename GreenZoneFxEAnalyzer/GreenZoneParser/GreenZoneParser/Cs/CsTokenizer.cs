using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneParser.Lexer;
using System.ComponentModel;

namespace GreenZoneParser
{
    public class CsTokenizer : Tokenizer
    {
        TokenOpenClose currentBlock;
        TokenOpenClose currentGeneric;

        internal CsTokenizer(CsParser parser, string fileContent)
            : base(parser, fileContent)
        {
            this.blockOpenClose = new TokenOpenClose(null, TokenOpenCloseType.Main);
            this.blockOpens = new List<Token>();
            this.arrayOpens = new List<Token>();
            this.parenOpens = new List<Token>();
            this.genericOpens = new List<Token>();

            blockOpensUm = blockOpens.AsReadOnly();
            arrayOpensUm = arrayOpens.AsReadOnly();
            parenOpensUm = parenOpens.AsReadOnly();
            genericOpensUm = genericOpens.AsReadOnly();

            currentBlock = blockOpenClose;
            currentGeneric = null;
            //currentGeneric = new TokenOpenClose(null, TokenOpenCloseType.Generic);

            rules.Add(new SimpleTokenRule(parser, TokenId.String, "string"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Char, "char"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Byte, "byte"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Short, "short"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Int, "int"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Long, "long"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Float, "float"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Double, "double"));
            rules.Add(new SimpleTokenRule(parser, TokenId.UShort, "ushort"));
            rules.Add(new SimpleTokenRule(parser, TokenId.UInt, "uint"));
            rules.Add(new SimpleTokenRule(parser, TokenId.ULong, "ulong"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Var, "var"));
            rules.Add(new SimpleTokenRule(parser, TokenId.This, "this"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Base, "base"));
            rules.Add(new SimpleTokenRule(parser, TokenId.True, "true"));
            rules.Add(new SimpleTokenRule(parser, TokenId.False, "false"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Null, "null"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Decimal, "decimal"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Bool, "bool"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Void, "void"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Object, "object"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Sbyte, "sbyte"));
            rules.Add(new SimpleTokenRule(parser, TokenId.From, "from"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Where, "where"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Let, "let"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Select, "select"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Join, "join"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Order, "order"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Group, "group"));
            rules.Add(new SimpleTokenRule(parser, TokenId.By, "by"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Ascending, "ascending"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Descending, "descending"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Into, "into"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Distinct, "distinct"));
            rules.Add(new SimpleTokenRule(parser, TokenId.On, "on"));
            rules.Add(new SimpleTokenRule(parser, TokenId.EqualsLinq, "equals"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Is, "is"));
            rules.Add(new SimpleTokenRule(parser, TokenId.As, "as"));
            rules.Add(new SimpleTokenRule(parser, TokenId.New, "new"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Typeof, "typeof"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Checked, "checked"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Unchecked, "unchecked"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Unsafe, "unsafe"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Lock, "lock"));
            rules.Add(new SimpleTokenRule(parser, TokenId.In, "in"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Fixed, "fixed"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Class, "class"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Interface, "interface"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Namespace, "namespace"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Struct, "struct"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Enum, "enum"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Delegate, "delegate"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Event, "event"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Using, "using"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Extern, "extern"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Alias, "alias"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Ref, "ref"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Out, "out"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Params, "params"));
            rules.Add(new SimpleTokenRule(parser, TokenId.If, "if"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Else, "else"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Switch, "switch"));
            rules.Add(new SimpleTokenRule(parser, TokenId.For, "for"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Foreach, "foreach"));
            rules.Add(new SimpleTokenRule(parser, TokenId.While, "while"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Do, "do"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Try, "try"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Catch, "catch"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Finally, "finally"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Throw, "throw"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Break, "break"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Continue, "continue"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Goto, "goto"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Case, "case"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Default, "default"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Return, "return"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Get, "get"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Set, "set"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Value, "value"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Private, "private"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Protected, "protected"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Public, "public"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Internal, "internal"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Abstract, "abstract"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Override, "override"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Partial, "partial"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Readonly, "readonly"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Sealed, "sealed"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Static, "static"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Virtual, "virtual"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Volatile, "volatile"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Const, "const"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Assembly, "assembly"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Field, "field"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Method, "method"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Module, "module"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Param, "param"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Property, "property"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Type, "type"));
            rules.Add(new SimpleTokenRule(parser, TokenId.BlockOpen, "{"));
            rules.Add(new SimpleTokenRule(parser, TokenId.BlockClose, "}"));
            rules.Add(new SimpleTokenRule(parser, TokenId.ArrayOrAttributeOpen, "["));
            rules.Add(new SimpleTokenRule(parser, TokenId.ArrayOrAttributeClose, "]"));
            rules.Add(new SimpleTokenRule(parser, TokenId.ParenOpen, "("));
            rules.Add(new SimpleTokenRule(parser, TokenId.ParenClose, ")"));
            rules.Add(new SimpleTokenRule(parser, TokenId.StatementSepScolon, ";"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Plus, "+"));
            rules.Add(new SimpleTokenRule(parser, TokenId.PlusPlus, "++"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Minus, "-"));
            rules.Add(new SimpleTokenRule(parser, TokenId.MinusMinus, "--"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Star, "*"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Slash, "/"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Modulo, "%"));
            rules.Add(new SimpleTokenRule(parser, TokenId.And, "&"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Or, "|"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Xor, "^"));
            rules.Add(new SimpleTokenRule(parser, TokenId.AndAnd, "&&"));
            rules.Add(new SimpleTokenRule(parser, TokenId.OrOr, "||"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Not, "!"));
            rules.Add(new SimpleTokenRule(parser, TokenId.NotBitws, "~"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Assign, "="));
            rules.Add(new SimpleTokenRule(parser, TokenId.PlusAssign, "+="));
            rules.Add(new SimpleTokenRule(parser, TokenId.MinusAssign, "-="));
            rules.Add(new SimpleTokenRule(parser, TokenId.MulAssign, "*="));
            rules.Add(new SimpleTokenRule(parser, TokenId.PerAssign, "/="));
            rules.Add(new SimpleTokenRule(parser, TokenId.XorAssign, "^="));
            rules.Add(new SimpleTokenRule(parser, TokenId.AndAssign, "&="));
            rules.Add(new SimpleTokenRule(parser, TokenId.OrAssign, "|="));
            rules.Add(new SimpleTokenRule(parser, TokenId.ModAssign, "%="));
            rules.Add(new SimpleTokenRule(parser, TokenId.LsAssign, "<<="));
            rules.Add(new SimpleTokenRule(parser, TokenId.RsAssign, ">>="));
            rules.Add(new SimpleTokenRule(parser, TokenId.Equal, "=="));
            rules.Add(new SimpleTokenRule(parser, TokenId.NonEqual, "!="));
            rules.Add(new SimpleTokenRule(parser, TokenId.Gt, ">"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Lt, "<"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Ge, ">="));
            rules.Add(new SimpleTokenRule(parser, TokenId.Le, "<="));
            rules.Add(new SimpleTokenRule(parser, TokenId.Point, "."));
            rules.Add(new SimpleTokenRule(parser, TokenId.Comma, ","));
            rules.Add(new SimpleTokenRule(parser, TokenId.Colon, ":"));
            rules.Add(new SimpleTokenRule(parser, TokenId.Qmark, "?"));


            rules.Add(new IdentifierTokenRule(parser, "_", "_"));

            rules.Add(new CharLiteralTokenRule(parser));
            rules.Add(new NumberLiteralTokenRule(parser));
            rules.Add(new HexLiteralTokenRule(parser));
            rules.Add(new StringLiteralTokenRule(parser));

            rules.Add(new SingleLineCommentTokenRule(parser));
            rules.Add(new MultiLineCommentTokenRule(parser));
            rules.Add(new WhiteSpaceTokenRule(parser));

            Init();
        }

        public CsParser CsParser
        {
            get
            {
                return (CsParser)parser;
            }
        }

        readonly TokenOpenClose blockOpenClose;
        public TokenOpenClose BlockOpenClose
        {
            get
            {
                return blockOpenClose;
            }
        }

        readonly List<Token> blockOpens;
        readonly IList<Token> blockOpensUm;
        public IList<Token> BlockOpens
        {
            get
            {
                return blockOpensUm;
            }
        }

        readonly List<Token> arrayOpens;
        readonly IList<Token> arrayOpensUm;
        public IList<Token> ArrayOpens
        {
            get
            {
                return arrayOpensUm;
            }
        }

        readonly List<Token> parenOpens;
        readonly IList<Token> parenOpensUm;
        public IList<Token> ParenOpens
        {
            get
            {
                return parenOpensUm;
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


        public TokenOpenClose Block(Token token)
        {
            TokenOpenClose result;
            if (token.BlockIndex < blockOpens.Count)
            {
                token = blockOpens[token.BlockIndex];
                result = token.Block;
                if (result.Type != TokenOpenCloseType.Block)
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

        public TokenOpenClose Array(Token token)
        {
            TokenOpenClose result;
            if (token.ArrayIndex < arrayOpens.Count)
            {
                token = arrayOpens[token.ArrayIndex];
                result = token.Block;
                if (result.Type != TokenOpenCloseType.Array)
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

        public TokenOpenClose ArrayByClose(Token token)
        {
            TokenOpenClose result;
            int arrInd = token.ArrayIndex - 1;
            if (0 <= arrInd && arrInd < arrayOpens.Count)
            {
                token = arrayOpens[arrInd];
                result = token.Block;
                if (result.Type != TokenOpenCloseType.Array)
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

        public TokenOpenClose Paren(Token token)
        {
            TokenOpenClose result;
            if (token.ParenIndex < parenOpens.Count)
            {
                token = parenOpens[token.ParenIndex];
                result = token.Block;
                if (result.Type != TokenOpenCloseType.Paren)
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
            TokenOpenClose toc2 = null;
            switch (rule.TokenId)
            {
                case TokenId.BlockOpen:
                    toc = push(ref currentBlock, TokenOpenCloseType.Block);
                    break;
                case TokenId.BlockClose:
                    toc = pop(ref currentBlock, TokenOpenCloseType.Block);
                    break;
                case TokenId.ArrayOrAttributeOpen:
                    toc = push(ref currentBlock, TokenOpenCloseType.Array);
                    if (currentGeneric != null)
                    {
                        if (currentGeneric == currentBlock)
                        {
                            throw new NotSupportedException();
                        }
                        toc2 = push(ref currentGeneric, TokenOpenCloseType.Array);
                    }
                    break;
                case TokenId.ArrayOrAttributeClose:
                    toc = pop(ref currentBlock, TokenOpenCloseType.Array);
                    if (currentGeneric != null)
                    {
                        toc2 = pop(ref currentGeneric, TokenOpenCloseType.Array);
                    }
                    break;
                case TokenId.ParenOpen:
                    toc = push(ref currentBlock, TokenOpenCloseType.Paren);
                    break;
                case TokenId.ParenClose:
                    toc = pop(ref currentBlock, TokenOpenCloseType.Paren);
                    break;
                case TokenId.Lt:
                    if (currentGeneric == null)
                    {
                        currentGeneric = currentBlock;
                    }
                    toc = push(ref currentGeneric, TokenOpenCloseType.Generic);
                    break;
                case TokenId.Gt:
                    if (currentGeneric != null)
                    {
                        toc = pop(ref currentGeneric, TokenOpenCloseType.Generic);
                        if (currentGeneric.Type != TokenOpenCloseType.Generic)
                        {
                            // !
                            currentGeneric = null;
                        }
                    }
                    break;
            }

            Token token = rule.GenerateToken(totalResult.Count, result.Count, blockOpens.Count, arrayOpens.Count, parenOpens.Count, genericOpens.Count, currentBlock, currentGeneric);
            totalResult.Add(token);
            switch (token.Id)
            {
                case TokenId.WhiteSpace:
                case TokenId.Comment:
                    break;
                default:
                    result.Add(token);
                    if (result.Count == 1)
                    {
                        blockOpenClose.OpenToken = token;
                    }
                    blockOpenClose.CloseToken = token;
                    break;
            }

            switch (token.Id)
            {
                case TokenId.BlockOpen:
                    blockOpens.Add(token);
                    toc.OpenToken = token;
                    break;
                case TokenId.BlockClose:
                    if (toc != null)
                    {
                        toc.CloseToken = token;
                    }
                    break;
                case TokenId.ArrayOrAttributeOpen:
                    arrayOpens.Add(token);
                    toc.OpenToken = token;
                    if (toc2 != null)
                    {
                        toc2.OpenToken = token;
                    }
                    break;
                case TokenId.ArrayOrAttributeClose:
                    if (toc != null)
                    {
                        toc.CloseToken = token;
                    }
                    if (toc2 != null)
                    {
                        toc2.CloseToken = token;
                    }
                    break;
                case TokenId.ParenOpen:
                    parenOpens.Add(token);
                    toc.OpenToken = token;
                    break;
                case TokenId.ParenClose:
                    if (toc != null)
                    {
                        toc.CloseToken = token;
                    }
                    break;
                case TokenId.Lt:
                    genericOpens.Add(token);
                    toc.OpenToken = token;
                    break;
                case TokenId.Gt:
                    if (toc != null)
                    {
                        toc.CloseToken = token;
                    }
                    break;
            }

            switch (token.Id)
            {
                case TokenId.StatementSepScolon:
                case TokenId.Comma:
                case TokenId.Colon:
                case TokenId.Assign:
                case TokenId.PlusAssign:
                case TokenId.MinusAssign:
                case TokenId.MulAssign:
                case TokenId.PerAssign:
                case TokenId.XorAssign:
                case TokenId.AndAssign:
                case TokenId.OrAssign:
                case TokenId.ModAssign:
                case TokenId.LsAssign:
                case TokenId.RsAssign:
                case TokenId.Qmark:
                case TokenId.OrOr:
                case TokenId.AndAnd:
                case TokenId.Or:
                case TokenId.Xor:
                case TokenId.And:
                case TokenId.Equal:
                case TokenId.NonEqual:
                case TokenId.Lt:
                case TokenId.Gt:
                case TokenId.Le:
                case TokenId.Ge:
                case TokenId.Is:
                case TokenId.As:
                //case TokenId.Ls :
                //case TokenId.rs :
                case TokenId.Plus:
                case TokenId.Minus:
                case TokenId.Star:
                case TokenId.Slash:
                case TokenId.Modulo:
                case TokenId.Not:
                case TokenId.NotBitws:
                case TokenId.PlusPlus:
                case TokenId.MinusMinus:
                case TokenId.Where:
                case TokenId.In:
                case TokenId.Point:
                    currentBlock.AddOperatorToken(token);
                    if (currentGeneric != null)
                    {
                        currentGeneric.AddOperatorToken(token);
                    }
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
                if (type != TokenOpenCloseType.Generic)
                {
                    parser.AddError("Invalid closing mark, " + type.GetTokenInfo() + " '" + type.GetTokenId().ClosingPair() + "'.  Expected:" + currentToc.Type.GetTokenInfo(), currentToc.OpenToken.TokenStartPos, currentToc.OpenToken.TokenEndPos);
                }

                if (type == TokenOpenCloseType.Block)
                {
                    TokenOpenClose curr = currentToc;
                    TokenOpenClose toc = null;
                    do
                    {
                        toc = curr;
                        curr = curr.Parent;
                    } while (toc != null && toc.Type != type && toc.Type != TokenOpenCloseType.Main);

                    if (toc != null && toc.Type == type)
                    {
                        currentToc = curr;
                    }

                    return toc;
                }
                else
                {
                    return null;
                }
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