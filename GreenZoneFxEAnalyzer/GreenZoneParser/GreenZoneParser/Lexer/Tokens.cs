using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers.Cs;

namespace GreenZoneParser.Lexer
{
    public enum TokenId
    {
        StringLiteral,
        CharLiteral,
        ByteLiteral,
        ShortLiteral,
        IntLiteral,
        LongLiteral,
        FloatLiteral,
        DoubleLiteral,
        UShortLiteral,
        UIntLiteral,
        ULongLiteral,
        Identifier,
        WhiteSpace,
        Comment,
        Var,
        String,
        Char,
        Byte,
        Short,
        Int,
        Long,
        Float,
        Double,
        UShort,
        UInt,
        ULong,
        This,
        Base,
        True,
        False,
        Null,
        Decimal,
        Bool,
        Void,
        Object,
        Sbyte,
        From,
        Where,
        Let,
        Select,
        Join,
        Group,
        By,
        Order,
        Ascending,
        Descending,
        Into,
        Distinct,
        On,
        EqualsLinq,
        Is,
        As,
        New,
        Typeof,
        Checked,
        Unchecked,
        Unsafe,
        Lock,
        In,
        Fixed,
        Namespace,
        Class,
        Interface,
        Struct,
        Enum,
        Delegate,
        Event,
        Using,
        Extern,
        Alias,
        Ref,
        Out,
        Params,
        Default,
        If,
        Else,
        Switch,
        For,
        Foreach,
        While,
        Do,
        Try,
        Catch,
        Finally,
        Throw,
        Break,
        Continue,
        Goto,
        Case,
        Return,
        Get,
        Set,
        Value,
        Private,
        Protected,
        Public,
        Internal,
        Abstract,
        Override,
        Partial,
        Readonly,
        Sealed,
        Static,
        Virtual,
        Volatile,
        Const,
        Assembly,
        Field,
        Method,
        Module,
        Param,
        Property,
        Type,
        BlockOpen,
        BlockClose,
        ArrayOrAttributeOpen,
        ArrayOrAttributeClose,
        ParenOpen,
        ParenClose,
        StatementSepScolon,
        Plus,
        PlusPlus,
        Minus,
        MinusMinus,
        Star,
        Slash,
        Modulo,
        And,
        Or,
        Xor,
        AndAnd,
        OrOr,
        Not,
        NotBitws,
        Assign,
        PlusAssign,
        MinusAssign,
        MulAssign,
        PerAssign,
        XorAssign,
        AndAssign,
        OrAssign,
        ModAssign,
        LsAssign,
        RsAssign,
        Equal,
        NonEqual,
        Gt,
        Lt,
        Ge,
        Le,
        Point,
        Comma,
        Colon,
        Qmark
    }

    public static class TokenIdEx
    {
        public static TokenId UnsignedPair(this TokenId tokenId)
        {
            switch (tokenId)
            {
                case TokenId.ShortLiteral: return TokenId.UShortLiteral;
                case TokenId.IntLiteral: return TokenId.UIntLiteral;
                case TokenId.LongLiteral: return TokenId.ULongLiteral;
                default: throw new ArgumentException();
            }
        }
        public static TokenId ClosingPair(this TokenId tokenId)
        {
            switch (tokenId)
            {
                case TokenId.BlockOpen: return TokenId.BlockClose;
                case TokenId.ArrayOrAttributeOpen: return TokenId.ArrayOrAttributeClose;
                case TokenId.ParenOpen: return TokenId.ParenClose;
                case TokenId.Lt: return TokenId.Gt;
                default: throw new ArgumentException();
            }
        }
        public static bool IsModifier(this TokenId tokenId, BlockParseMode blockParseMode)
        {
            switch (blockParseMode)
            {
                case BlockParseMode.MainBlock:
                case BlockParseMode.Namespace:
                case BlockParseMode.ClassOrStruct:
                case BlockParseMode.Interface:
                case BlockParseMode.Property:
                    switch (tokenId)
                    {
                        case TokenId.Public:
                        case TokenId.Protected:
                        case TokenId.Internal:
                        case TokenId.Private:
                        case TokenId.Volatile:
                        case TokenId.Static:
                        case TokenId.Const:
                        case TokenId.Readonly:
                        case TokenId.Sealed:
                        case TokenId.Abstract:
                        case TokenId.Virtual:
                        case TokenId.Override:
                        case TokenId.New:
                            return true;
                        default:
                            return false;
                    }
                case BlockParseMode.Declaration_Substatement:
                case BlockParseMode.DeclarationWithAssign_Substatement:
                case BlockParseMode.For1or3_Substatement:
                case BlockParseMode.Foreach_Substatement:
                case BlockParseMode.Method:
                case BlockParseMode.Switch:
                    switch (tokenId)
                    {
                        case TokenId.Const:
                            return true;
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }

        public static bool IsIdentifier(this TokenId tokenId)
        {
            switch (tokenId)
            {
                case TokenId.Identifier:
                case TokenId.Where:
                case TokenId.From:
                case TokenId.Let:
                case TokenId.Select:
                case TokenId.Join:
                case TokenId.Ascending:
                case TokenId.Descending:
                case TokenId.Into:
                case TokenId.Distinct:
                case TokenId.On:
                case TokenId.Order:
                case TokenId.Group:
                case TokenId.By:
                case TokenId.EqualsLinq:
                case TokenId.Value:
                case TokenId.Get:
                case TokenId.Set:
                case TokenId.Assembly:
                case TokenId.Field:
                case TokenId.Method:
                case TokenId.Module:
                case TokenId.Param:
                case TokenId.Property:
                case TokenId.Type:
                    return true;
                default:
                    return false;
            }
        }
    }

    public class Token : IComparable, IComparable<Token>
    {
        internal Token(TokenId id, string contentString, int tokenStartPos, int tokenEndPos, int totalIndex, int index, int blockIndex, int arrayIndex, int parenIndex, int genericIndex, TokenOpenClose block, TokenOpenClose genericBlock, object value)
        {
            this.id = id;
            this.contentString = contentString;
            this.tokenStartPos = tokenStartPos;
            this.tokenEndPos = tokenEndPos;
            this.totalIndex = totalIndex;
            this.index = index;
            this.blockIndex = blockIndex;
            this.arrayIndex = arrayIndex;
            this.parenIndex = parenIndex;
            this.genericIndex = genericIndex;
            this.block = block;
            this.genericBlock = genericBlock;

            if (block != null)
            {
                this.statementIndex = block.Semicolons.Count;
                this.commaIndex = block.Commas.Count;
                this.colonIndex = block.Colons.Count;

                assignIndex = block.Assigns.Count;
                plusAssignIndex = block.PlusAssigns.Count;
                minusAssignIndex = block.MinusAssigns.Count;
                mulAssignIndex = block.MulAssigns.Count;
                perAssignIndex = block.PerAssigns.Count;
                xorAssignIndex = block.XorAssigns.Count;
                andAssignIndex = block.AndAssigns.Count;
                orAssignIndex = block.OrAssigns.Count;
                modAssignIndex = block.ModAssigns.Count;
                lsAssignIndex = block.LsAssigns.Count;
                rsAssignIndex = block.RsAssigns.Count;
                qmarkIndex = block.Qmarks.Count;
                orOrIndex = block.OrOrs.Count;
                andAndIndex = block.AndAnds.Count;
                orIndex = block.Ors.Count;
                xorIndex = block.Xors.Count;
                andIndex = block.Ands.Count;
                equalsIndex = block.Equalss.Count;
                nonEqualsIndex = block.NonEqualss.Count;
                ltIndex = block.Lts.Count;
                gtIndex = block.Gts.Count;
                leIndex = block.Les.Count;
                geIndex = block.Ges.Count;
                isIndex = block.Iss.Count;
                asIndex = block.Ass.Count;
                lsIndex = block.Lss.Count;
                rsIndex = block.Rss.Count;
                plusIndex = block.Pluss.Count;
                minusIndex = block.Minuss.Count;
                mulIndex = block.Muls.Count;
                perIndex = block.Pers.Count;
                modIndex = block.Mods.Count;
                notIndex = block.Nots.Count;
                bwnotIndex = block.Bwnots.Count;
                plusPlusIndex = block.PlusPluss.Count;
                minusMinusIndex = block.MinusMinuss.Count;
                whereIndex = block.Wheres.Count;
                inIndex = block.Ins.Count;
                pointIndex = block.Points.Count;
            }

            this.value = value;
        }

        readonly TokenId id;
        public TokenId Id
        {
            get
            {
                return id;
            }
        }

        readonly string contentString;
        public string ContentString
        {
            get
            {
                return contentString;
            }
        }

        readonly int tokenStartPos;
        public int TokenStartPos
        {
            get
            {
                return tokenStartPos;
            }
        }

        readonly int tokenEndPos;
        public int TokenEndPos
        {
            get
            {
                return tokenEndPos;
            }
        }

        readonly int totalIndex;
        public int TotalIndex
        {
            get
            {
                return totalIndex;
            }
        }

        readonly int index;
        public int Index
        {
            get
            {
                return index;
            }
        }

        readonly int blockIndex;
        public int BlockIndex
        {
            get
            {
                return BlockIndex;
            }
        }

        readonly int arrayIndex;
        public int ArrayIndex
        {
            get
            {
                return arrayIndex;
            }
        }

        readonly int parenIndex;
        public int ParenIndex
        {
            get
            {
                return parenIndex;
            }
        }

        readonly int genericIndex;
        public int GenericIndex
        {
            get
            {
                return genericIndex;
            }
        }

        readonly TokenOpenClose block;
        public TokenOpenClose Block
        {
            get
            {
                return block;
            }
        }

        readonly TokenOpenClose genericBlock;
        public TokenOpenClose GenericBlock
        {
            get
            {
                return genericBlock;
            }
        }

        readonly int statementIndex;
        public int StatementIndex
        {
            get
            {
                return statementIndex;
            }
        }

        readonly int commaIndex;
        public int CommaIndex
        {
            get
            {
                return commaIndex;
            }
        }

        readonly int colonIndex;
        public int ColonIndex
        {
            get
            {
                return colonIndex;
            }
        }

        readonly object value;
        public object Value
        {
            get
            {
                return value;
            }
        }

        readonly int assignIndex; public int AssignIndex { get { return assignIndex; } }
        readonly int plusAssignIndex; public int PlusAssignIndex { get { return plusAssignIndex; } }
        readonly int minusAssignIndex; public int MinusAssignIndex { get { return minusAssignIndex; } }
        readonly int mulAssignIndex; public int MulAssignIndex { get { return mulAssignIndex; } }
        readonly int perAssignIndex; public int PerAssignIndex { get { return perAssignIndex; } }
        readonly int xorAssignIndex; public int XorAssignIndex { get { return xorAssignIndex; } }
        readonly int andAssignIndex; public int AndAssignIndex { get { return andAssignIndex; } }
        readonly int orAssignIndex; public int OrAssignIndex { get { return orAssignIndex; } }
        readonly int modAssignIndex; public int ModAssignIndex { get { return modAssignIndex; } }
        readonly int lsAssignIndex; public int LsAssignIndex { get { return lsAssignIndex; } }
        readonly int rsAssignIndex; public int RsAssignIndex { get { return rsAssignIndex; } }
        readonly int qmarkIndex; public int QmarkIndex { get { return qmarkIndex; } }
        readonly int orOrIndex; public int OrOrIndex { get { return orOrIndex; } }
        readonly int andAndIndex; public int AndAndIndex { get { return andAndIndex; } }
        readonly int orIndex; public int OrIndex { get { return orIndex; } }
        readonly int xorIndex; public int XorIndex { get { return xorIndex; } }
        readonly int andIndex; public int AndIndex { get { return andIndex; } }
        readonly int equalsIndex; public int EqualsIndex { get { return equalsIndex; } }
        readonly int nonEqualsIndex; public int NonEqualsIndex { get { return nonEqualsIndex; } }
        readonly int ltIndex; public int LtIndex { get { return ltIndex; } }
        readonly int gtIndex; public int GtIndex { get { return gtIndex; } }
        readonly int leIndex; public int LeIndex { get { return leIndex; } }
        readonly int geIndex; public int GeIndex { get { return geIndex; } }
        readonly int isIndex; public int IsIndex { get { return isIndex; } }
        readonly int asIndex; public int AsIndex { get { return asIndex; } }
        readonly int lsIndex; public int LsIndex { get { return lsIndex; } }
        readonly int rsIndex; public int RsIndex { get { return rsIndex; } }
        readonly int plusIndex; public int PlusIndex { get { return plusIndex; } }
        readonly int minusIndex; public int MinusIndex { get { return minusIndex; } }
        readonly int mulIndex; public int MulIndex { get { return mulIndex; } }
        readonly int perIndex; public int PerIndex { get { return perIndex; } }
        readonly int modIndex; public int ModIndex { get { return modIndex; } }
        readonly int notIndex; public int NotIndex { get { return notIndex; } }
        readonly int bwnotIndex; public int BwnotIndex { get { return bwnotIndex; } }
        readonly int plusPlusIndex; public int PlusPlusIndex { get { return plusPlusIndex; } }
        readonly int minusMinusIndex; public int MinusMinusIndex { get { return minusMinusIndex; } }
        readonly int whereIndex; public int WhereIndex { get { return whereIndex; } }
        readonly int inIndex; public int InIndex { get { return inIndex; } }
        readonly int pointIndex; public int PointIndex { get { return pointIndex; } }

        public int Length
        {
            get
            {
                return tokenEndPos - tokenStartPos + 1;
            }
        }

        public int OperatorTokenIndex(TokenId operatorTokenId)
        {
            switch (operatorTokenId)
            {
                case TokenId.StatementSepScolon: return statementIndex;
                case TokenId.Comma: return commaIndex;
                case TokenId.Colon: return colonIndex;
                case TokenId.Assign: return assignIndex;
                case TokenId.PlusAssign: return plusAssignIndex;
                case TokenId.MinusAssign: return minusAssignIndex;
                case TokenId.MulAssign: return mulAssignIndex;
                case TokenId.PerAssign: return perAssignIndex;
                case TokenId.XorAssign: return xorAssignIndex;
                case TokenId.AndAssign: return andAssignIndex;
                case TokenId.OrAssign: return orAssignIndex;
                case TokenId.ModAssign: return modAssignIndex;
                case TokenId.LsAssign: return lsAssignIndex;
                case TokenId.RsAssign: return rsAssignIndex;
                case TokenId.Qmark: return qmarkIndex;
                case TokenId.OrOr: return orOrIndex;
                case TokenId.AndAnd: return andAndIndex;
                case TokenId.Or: return orIndex;
                case TokenId.Xor: return xorIndex;
                case TokenId.And: return andIndex;
                case TokenId.Equal: return equalsIndex;
                case TokenId.NonEqual: return nonEqualsIndex;
                case TokenId.Lt: return ltIndex;
                case TokenId.Gt: return gtIndex;
                case TokenId.Le: return leIndex;
                case TokenId.Ge: return geIndex;
                case TokenId.Is: return isIndex;
                case TokenId.As: return asIndex;
                //case TokenId.Ls : return lsIndex;
                //case TokenId.rs : return rsIndex;
                case TokenId.Plus: return plusIndex;
                case TokenId.Minus: return minusIndex;
                case TokenId.Star: return mulIndex;
                case TokenId.Slash: return perIndex;
                case TokenId.Modulo: return modIndex;
                case TokenId.Not: return notIndex;
                case TokenId.NotBitws: return bwnotIndex;
                case TokenId.PlusPlus: return plusPlusIndex;
                case TokenId.MinusMinus: return minusMinusIndex;
                case TokenId.Where: return whereIndex;
                case TokenId.In: return inIndex;
                case TokenId.Point: return pointIndex;
                default: throw new ArgumentException("operatorTokenId:" + operatorTokenId);
            }
        }

        public override string ToString()
        {
            switch (Id)
            {
                case TokenId.WhiteSpace:
                case TokenId.Comment: return Id + ":" + tokenStartPos + ".." + tokenEndPos;
                default: return Id + ":tk" + index + " " + tokenStartPos + ".." + tokenEndPos + ":" + contentString;
            }
        }

        string mxs(string s, int len)
        {
            if (s.Length > len + 3)
            {
                return s.Substring(0, len) + "...";
            }
            else
            {
                return s;
            }
        }


        public int CompareTo(object obj)
        {
            return CompareTo((Token)obj);
        }

        public int CompareTo(Token other)
        {
            return tokenStartPos - other.tokenStartPos;
        }
    }

    public enum TokenOpenCloseType
    {
        Main,
        Block,
        Array,
        Paren,
        Generic
    }

    public static class TokenOpenCloseTypeEx
    {
        public static TokenId GetTokenId(this TokenOpenCloseType toc)
        {
            switch (toc)
            {
                case TokenOpenCloseType.Main:
                    return TokenId.BlockOpen;
                case TokenOpenCloseType.Block:
                    return TokenId.BlockOpen;
                case TokenOpenCloseType.Array:
                    return TokenId.ArrayOrAttributeOpen;
                case TokenOpenCloseType.Paren:
                    return TokenId.ParenOpen;
                case TokenOpenCloseType.Generic:
                    return TokenId.Lt;
                default:
                    throw new NotSupportedException();
            }
        }
        public static string GetTokenInfo(this TokenOpenCloseType toc)
        {
            switch (toc)
            {
                case TokenOpenCloseType.Block:
                    return "brace";
                case TokenOpenCloseType.Array:
                    return "bracket";
                case TokenOpenCloseType.Paren:
                    return "parenthesis";
                case TokenOpenCloseType.Generic:
                    return "chevron";
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public class TokenOpenClose
    {
        internal TokenOpenClose(TokenOpenClose parent, TokenOpenCloseType type)
        {
            this.parent = parent;
            this.type = type;
            this.children = new List<TokenOpenClose>();
            this.childrenUm = children.AsReadOnly();
            this.childrenBlock = new List<TokenOpenClose>();
            this.childrenBlockUm = childrenBlock.AsReadOnly();
            this.childrenArray = new List<TokenOpenClose>();
            this.childrenArrayUm = childrenArray.AsReadOnly();
            this.childrenParen = new List<TokenOpenClose>();
            this.childrenParenUm = childrenParen.AsReadOnly();
            this.childrenGeneric = new List<TokenOpenClose>();
            this.childrenGenericUm = childrenGeneric.AsReadOnly();
            this.semicolons = new List<Token>();
            this.semicolonsUm = semicolons.AsReadOnly();
            this.commas = new List<Token>();
            this.commasUm = commas.AsReadOnly();
            this.colons = new List<Token>();
            this.colonsUm = colons.AsReadOnly();
            assigns = new List<Token>();
            assignsUm = assigns.AsReadOnly();
            plusAssigns = new List<Token>();
            plusAssignsUm = plusAssigns.AsReadOnly();
            minusAssigns = new List<Token>();
            minusAssignsUm = minusAssigns.AsReadOnly();
            mulAssigns = new List<Token>();
            mulAssignsUm = mulAssigns.AsReadOnly();
            perAssigns = new List<Token>();
            perAssignsUm = perAssigns.AsReadOnly();
            xorAssigns = new List<Token>();
            xorAssignsUm = xorAssigns.AsReadOnly();
            andAssigns = new List<Token>();
            andAssignsUm = andAssigns.AsReadOnly();
            orAssigns = new List<Token>();
            orAssignsUm = orAssigns.AsReadOnly();
            modAssigns = new List<Token>();
            modAssignsUm = modAssigns.AsReadOnly();
            lsAssigns = new List<Token>();
            lsAssignsUm = lsAssigns.AsReadOnly();
            rsAssigns = new List<Token>();
            rsAssignsUm = rsAssigns.AsReadOnly();
            qmarks = new List<Token>();
            qmarksUm = qmarks.AsReadOnly();
            colons = new List<Token>();
            colonsUm = colons.AsReadOnly();
            orOrs = new List<Token>();
            orOrsUm = orOrs.AsReadOnly();
            andAnds = new List<Token>();
            andAndsUm = andAnds.AsReadOnly();
            ors = new List<Token>();
            orsUm = ors.AsReadOnly();
            xors = new List<Token>();
            xorsUm = xors.AsReadOnly();
            ands = new List<Token>();
            andsUm = ands.AsReadOnly();
            equalss = new List<Token>();
            equalssUm = equalss.AsReadOnly();
            nonEqualss = new List<Token>();
            nonEqualssUm = nonEqualss.AsReadOnly();
            lts = new List<Token>();
            ltsUm = lts.AsReadOnly();
            gts = new List<Token>();
            gtsUm = gts.AsReadOnly();
            les = new List<Token>();
            lesUm = les.AsReadOnly();
            ges = new List<Token>();
            gesUm = ges.AsReadOnly();
            iss = new List<Token>();
            issUm = iss.AsReadOnly();
            ass = new List<Token>();
            assUm = ass.AsReadOnly();
            lss = new List<Token>();
            lssUm = lss.AsReadOnly();
            rss = new List<Token>();
            rssUm = rss.AsReadOnly();
            pluss = new List<Token>();
            plussUm = pluss.AsReadOnly();
            minuss = new List<Token>();
            minussUm = minuss.AsReadOnly();
            muls = new List<Token>();
            mulsUm = muls.AsReadOnly();
            pers = new List<Token>();
            persUm = pers.AsReadOnly();
            mods = new List<Token>();
            modsUm = mods.AsReadOnly();
            nots = new List<Token>();
            notsUm = nots.AsReadOnly();
            bwnots = new List<Token>();
            bwnotsUm = bwnots.AsReadOnly();
            plusPluss = new List<Token>();
            plusPlussUm = plusPluss.AsReadOnly();
            minusMinuss = new List<Token>();
            minusMinussUm = minusMinuss.AsReadOnly();
            wheres = new List<Token>();
            wheresUm = wheres.AsReadOnly();
            ins = new List<Token>();
            insUm = ins.AsReadOnly();
            points = new List<Token>();
            pointsUm = points.AsReadOnly();

            if (parent != null)
            {
                parent.Add(this);
            }
        }

        readonly TokenOpenCloseType type;
        public TokenOpenCloseType Type
        {
            get
            {
                return type;
            }
        }

        Token openToken;
        public Token OpenToken
        {
            get
            {
                return openToken;
            }
            internal set
            {
                openToken = value;
                if (type != TokenOpenCloseType.Main && type.GetTokenId() != openToken.Id)
                {
                    throw new NotSupportedException();
                }
            }
        }

        Token closeToken;
        public Token CloseToken
        {
            get
            {
                return closeToken;
            }
            internal set
            {
                closeToken = value;
                if (type != TokenOpenCloseType.Main && closeToken.Id != openToken.Id.ClosingPair())
                {
                    throw new NotSupportedException();
                }
            }
        }

        TokenOpenClose parent;
        public TokenOpenClose Parent
        {
            get
            {
                return parent;
            }
        }

        readonly List<TokenOpenClose> children;
        readonly IList<TokenOpenClose> childrenUm;
        public IList<TokenOpenClose> Children
        {
            get
            {
                return childrenUm;
            }
        }

        readonly List<TokenOpenClose> childrenBlock;
        readonly IList<TokenOpenClose> childrenBlockUm;
        public IList<TokenOpenClose> ChildrenBlock
        {
            get
            {
                return childrenBlockUm;
            }
        }

        readonly List<TokenOpenClose> childrenArray;
        readonly IList<TokenOpenClose> childrenArrayUm;
        public IList<TokenOpenClose> ChildrenArray
        {
            get
            {
                return childrenArrayUm;
            }
        }

        readonly List<TokenOpenClose> childrenParen;
        readonly IList<TokenOpenClose> childrenParenUm;
        public IList<TokenOpenClose> ChildrenParen
        {
            get
            {
                return childrenParenUm;
            }
        }

        readonly List<TokenOpenClose> childrenGeneric;
        readonly IList<TokenOpenClose> childrenGenericUm;
        public IList<TokenOpenClose> ChildrenGeneric
        {
            get
            {
                return childrenGenericUm;
            }
        }

        readonly List<Token> semicolons;
        readonly IList<Token> semicolonsUm;
        public IList<Token> Semicolons
        {
            get
            {
                return semicolonsUm;
            }
        }

        readonly List<Token> commas;
        readonly IList<Token> commasUm;
        public IList<Token> Commas
        {
            get
            {
                return commasUm;
            }
        }

        readonly List<Token> assigns;
        readonly IList<Token> assignsUm;
        public IList<Token> Assigns
        {
            get
            {
                return assignsUm;
            }
        }

        readonly List<Token> plusAssigns;
        readonly IList<Token> plusAssignsUm;
        public IList<Token> PlusAssigns
        {
            get
            {
                return plusAssignsUm;
            }
        }

        readonly List<Token> minusAssigns;
        readonly IList<Token> minusAssignsUm;
        public IList<Token> MinusAssigns
        {
            get
            {
                return minusAssignsUm;
            }
        }
        readonly List<Token> mulAssigns;
        readonly IList<Token> mulAssignsUm;
        public IList<Token> MulAssigns
        {
            get
            {
                return mulAssignsUm;
            }
        }
        readonly List<Token> perAssigns;
        readonly IList<Token> perAssignsUm;
        public IList<Token> PerAssigns
        {
            get
            {
                return perAssignsUm;
            }
        }
        readonly List<Token> xorAssigns;
        readonly IList<Token> xorAssignsUm;
        public IList<Token> XorAssigns
        {
            get
            {
                return xorAssignsUm;
            }
        }
        readonly List<Token> andAssigns;
        readonly IList<Token> andAssignsUm;
        public IList<Token> AndAssigns
        {
            get
            {
                return andAssignsUm;
            }
        }
        readonly List<Token> orAssigns;
        readonly IList<Token> orAssignsUm;
        public IList<Token> OrAssigns
        {
            get
            {
                return orAssignsUm;
            }
        }
        readonly List<Token> modAssigns;
        readonly IList<Token> modAssignsUm;
        public IList<Token> ModAssigns
        {
            get
            {
                return modAssignsUm;
            }
        }

        readonly List<Token> lsAssigns;
        readonly IList<Token> lsAssignsUm;
        public IList<Token> LsAssigns
        {
            get
            {
                return lsAssignsUm;
            }
        }

        readonly List<Token> rsAssigns;
        readonly IList<Token> rsAssignsUm;
        public IList<Token> RsAssigns
        {
            get
            {
                return rsAssignsUm;
            }
        }

        readonly List<Token> qmarks;
        readonly IList<Token> qmarksUm;
        public IList<Token> Qmarks
        {
            get
            {
                return qmarksUm;
            }
        }

        readonly List<Token> colons;
        readonly IList<Token> colonsUm;
        public IList<Token> Colons
        {
            get
            {
                return colonsUm;
            }
        }

        readonly List<Token> orOrs;
        readonly IList<Token> orOrsUm;
        public IList<Token> OrOrs
        {
            get
            {
                return orOrsUm;
            }
        }

        readonly List<Token> andAnds;
        readonly IList<Token> andAndsUm;
        public IList<Token> AndAnds
        {
            get
            {
                return andAndsUm;
            }
        }

        readonly List<Token> ors;
        readonly IList<Token> orsUm;
        public IList<Token> Ors
        {
            get
            {
                return orsUm;
            }
        }

        readonly List<Token> xors;
        readonly IList<Token> xorsUm;
        public IList<Token> Xors
        {
            get
            {
                return xorsUm;
            }
        }

        readonly List<Token> ands;
        readonly IList<Token> andsUm;
        public IList<Token> Ands
        {
            get
            {
                return andsUm;
            }
        }

        readonly List<Token> equalss;
        readonly IList<Token> equalssUm;
        public IList<Token> Equalss
        {
            get
            {
                return equalssUm;
            }
        }

        readonly List<Token> nonEqualss;
        readonly IList<Token> nonEqualssUm;
        public IList<Token> NonEqualss
        {
            get
            {
                return nonEqualssUm;
            }
        }

        readonly List<Token> lts;
        readonly IList<Token> ltsUm;
        public IList<Token> Lts
        {
            get
            {
                return ltsUm;
            }
        }

        readonly List<Token> gts;
        readonly IList<Token> gtsUm;
        public IList<Token> Gts
        {
            get
            {
                return gtsUm;
            }
        }

        readonly List<Token> les;
        readonly IList<Token> lesUm;
        public IList<Token> Les
        {
            get
            {
                return lesUm;
            }
        }

        readonly List<Token> ges;
        readonly IList<Token> gesUm;
        public IList<Token> Ges
        {
            get
            {
                return gesUm;
            }
        }

        readonly List<Token> iss;
        readonly IList<Token> issUm;
        public IList<Token> Iss
        {
            get
            {
                return issUm;
            }
        }

        readonly List<Token> ass;
        readonly IList<Token> assUm;
        public IList<Token> Ass
        {
            get
            {
                return assUm;
            }
        }

        readonly List<Token> lss;
        readonly IList<Token> lssUm;
        public IList<Token> Lss
        {
            get
            {
                return lssUm;
            }
        }

        readonly List<Token> rss;
        readonly IList<Token> rssUm;
        public IList<Token> Rss
        {
            get
            {
                return rssUm;
            }
        }

        readonly List<Token> pluss;
        readonly IList<Token> plussUm;
        public IList<Token> Pluss
        {
            get
            {
                return plussUm;
            }
        }

        readonly List<Token> minuss;
        readonly IList<Token> minussUm;
        public IList<Token> Minuss
        {
            get
            {
                return minussUm;
            }
        }

        readonly List<Token> muls;
        readonly IList<Token> mulsUm;
        public IList<Token> Muls
        {
            get
            {
                return mulsUm;
            }
        }

        readonly List<Token> pers;
        readonly IList<Token> persUm;
        public IList<Token> Pers
        {
            get
            {
                return persUm;
            }
        }

        readonly List<Token> mods;
        readonly IList<Token> modsUm;
        public IList<Token> Mods
        {
            get
            {
                return modsUm;
            }
        }

        readonly List<Token> nots;
        readonly IList<Token> notsUm;
        public IList<Token> Nots
        {
            get
            {
                return notsUm;
            }
        }

        readonly List<Token> bwnots;
        readonly IList<Token> bwnotsUm;
        public IList<Token> Bwnots
        {
            get
            {
                return bwnotsUm;
            }
        }

        // NOTE !! unary minus here !!

        readonly List<Token> plusPluss;
        readonly IList<Token> plusPlussUm;
        public IList<Token> PlusPluss
        {
            get
            {
                return plusPlussUm;
            }
        }

        readonly List<Token> minusMinuss;
        readonly IList<Token> minusMinussUm;
        public IList<Token> MinusMinuss
        {
            get
            {
                return minusMinussUm;
            }
        }

        readonly List<Token> wheres;
        readonly IList<Token> wheresUm;
        public IList<Token> Wheres
        {
            get
            {
                return wheresUm;
            }
        }

        readonly List<Token> ins;
        readonly IList<Token> insUm;
        public IList<Token> Ins
        {
            get
            {
                return insUm;
            }
        }

        readonly List<Token> points;
        readonly IList<Token> pointsUm;
        public IList<Token> Points
        {
            get
            {
                return pointsUm;
            }
        }

        int index;
        public int Index
        {
            get
            {
                return index;
            }
        }

        int blockIndex;
        public int BlockIndex
        {
            get
            {
                return blockIndex;
            }
        }

        int arrayIndex;
        public int ArrayIndex
        {
            get
            {
                return arrayIndex;
            }
        }

        int parenIndex;
        public int ParenIndex
        {
            get
            {
                return parenIndex;
            }
        }

        int genericIndex;
        public int GenericIndex
        {
            get
            {
                return genericIndex;
            }
        }

        public IList<Token> OperatorTokens(TokenId operatorTokenId)
        {
            switch (operatorTokenId)
            {
                case TokenId.StatementSepScolon: return semicolonsUm;
                case TokenId.Comma: return commasUm;
                case TokenId.Colon: return colonsUm;
                case TokenId.Assign: return assignsUm;
                case TokenId.PlusAssign: return plusAssignsUm;
                case TokenId.MinusAssign: return minusAssignsUm;
                case TokenId.MulAssign: return mulAssignsUm;
                case TokenId.PerAssign: return perAssignsUm;
                case TokenId.XorAssign: return xorAssignsUm;
                case TokenId.AndAssign: return andAssignsUm;
                case TokenId.OrAssign: return orAssignsUm;
                case TokenId.ModAssign: return modAssignsUm;
                case TokenId.LsAssign: return lsAssignsUm;
                case TokenId.RsAssign: return rsAssignsUm;
                case TokenId.Qmark: return qmarksUm;
                case TokenId.OrOr: return orOrsUm;
                case TokenId.AndAnd: return andAndsUm;
                case TokenId.Or: return orsUm;
                case TokenId.Xor: return xorsUm;
                case TokenId.And: return andsUm;
                case TokenId.Equal: return equalssUm;
                case TokenId.NonEqual: return nonEqualssUm;
                case TokenId.Lt: return ltsUm;
                case TokenId.Gt: return gtsUm;
                case TokenId.Le: return lesUm;
                case TokenId.Ge: return gesUm;
                case TokenId.Is: return issUm;
                case TokenId.As: return assUm;
                //case TokenId.Ls : return lssUm;
                //case TokenId.rs : return rssUm;
                case TokenId.Plus: return plussUm;
                case TokenId.Minus: return minussUm;
                case TokenId.Star: return mulsUm;
                case TokenId.Slash: return persUm;
                case TokenId.Modulo: return modsUm;
                case TokenId.Not: return notsUm;
                case TokenId.NotBitws: return bwnotsUm;
                case TokenId.PlusPlus: return plusPlussUm;
                case TokenId.MinusMinus: return minusMinussUm;
                case TokenId.Where: return wheresUm;
                case TokenId.In: return insUm;
                case TokenId.Point: return pointsUm;
                default: throw new ArgumentException("operatorTokenId:" + operatorTokenId);
            }
        }
        
        public TokenOpenClose Previous()
        {
            TokenOpenClose toc = Parent.Previous(children, index);
            return toc;
        }

        public TokenOpenClose Next()
        {
            TokenOpenClose toc = Parent.Next(Parent.children, index);
            return toc;
        }

        public TokenOpenClose PreviousBlock()
        {
            TokenOpenClose toc = Parent.Previous(Parent.childrenBlock, blockIndex);
            return toc;
        }

        public TokenOpenClose PreviousGeneric()
        {
            TokenOpenClose toc = Parent.Previous(Parent.childrenGeneric, genericIndex);
            return toc;
        }

        public TokenOpenClose NextBlock()
        {
            TokenOpenClose toc = Parent.Next(Parent.childrenBlock, blockIndex);
            return toc;
        }

        public TokenOpenClose PreviousArray()
        {
            TokenOpenClose toc = Parent.Previous(Parent.childrenArray, arrayIndex);
            return toc;
        }

        public TokenOpenClose NextArray()
        {
            TokenOpenClose toc = Parent.Next(Parent.childrenArray, arrayIndex);
            return toc;
        }

        public TokenOpenClose PreviousParen()
        {
            TokenOpenClose toc = Parent.Previous(Parent.childrenParen, parenIndex);
            return toc;
        }

        public TokenOpenClose NextParen()
        {
            TokenOpenClose toc = Parent.Next(Parent.childrenParen, parenIndex);
            return toc;
        }

        public TokenOpenClose NextGeneric()
        {
            TokenOpenClose toc = Parent.Next(Parent.childrenGeneric, genericIndex);
            return toc;
        }

        public Token Operator(TokenId operatorTokenId, Token token)
        {
            if (token.Block != this)
            {
                throw new NotSupportedException();
            }
            int tki = token.OperatorTokenIndex(operatorTokenId);
            List<Token> tks = opTknRw(operatorTokenId);
            if (tki < tks.Count)
            {
                token = tks[tki];
            }
            else
            {
                token = null;
            }
            return token;
        }

        public Token PrevOperator(TokenId operatorTokenId, Token token)
        {
            if (token.Block != this)
            {
                throw new NotSupportedException();
            }
            int tki = token.OperatorTokenIndex(operatorTokenId) - 1;
            List<Token> tks = opTknRw(operatorTokenId);
            if (tki >= 0 && tki < tks.Count)
            {
                token = tks[tki];
            }
            else
            {
                token = null;
            }
            return token;
        }

        TokenOpenClose Previous(List<TokenOpenClose> children, int index)
        {
            TokenOpenClose token;
            if (index > 1)
            {
                token = children[index - 1];
            }
            else
            {
                token = null;
            }
            return token;
        }

        TokenOpenClose Next(List<TokenOpenClose> children, int index)
        {
            TokenOpenClose token;
            if (index < children.Count - 1)
            {
                token = children[index + 1];
            }
            else
            {
                token = null;
            }
            return token;
        }

        void Add(TokenOpenClose child)
        {
            child.index = children.Count;
            children.Add(child);
            switch (child.type)
            {
                case TokenOpenCloseType.Block:
                    child.blockIndex = childrenBlock.Count;
                    childrenBlock.Add(child);
                    break;
                case TokenOpenCloseType.Array:
                    child.arrayIndex = childrenArray.Count;
                    childrenArray.Add(child);
                    break;
                case TokenOpenCloseType.Paren:
                    child.parenIndex = childrenParen.Count;
                    childrenParen.Add(child);
                    break;
                case TokenOpenCloseType.Generic:
                    child.genericIndex = childrenGeneric.Count;
                    childrenGeneric.Add(child);
                    break;
            }
        }

        public void AddOperatorToken(Token child)
        {
            List<Token> tokens = opTknRw(child.Id);
            tokens.Add(child);
        }



        public TokenOpenClose SubToc(Token openToken, Token closeToken)
        {
            if (openToken.Block != this || closeToken.Block != this)
            {
                throw new NotSupportedException();
            }
            TokenOpenClose r = new TokenOpenClose(null, type);
            r.openToken = openToken;
            r.closeToken = closeToken;
            r.index = index;
            r.blockIndex = blockIndex;
            r.arrayIndex = arrayIndex;
            r.parenIndex = parenIndex;
            r.parent = parent;

            foreach (var t in children)
            {
                if (openToken.Index <= t.openToken.Index && t.closeToken != null && t.closeToken.Index <= closeToken.Index)
                {
                    r.children.Add(t);
                    switch (t.type)
                    {
                        case TokenOpenCloseType.Block:
                            r.childrenBlock.Add(t);
                            break;
                        case TokenOpenCloseType.Array:
                            r.childrenArray.Add(t);
                            break;
                        case TokenOpenCloseType.Paren:
                            r.childrenParen.Add(t);
                            break;
                        case TokenOpenCloseType.Generic:
                            r.childrenGeneric.Add(t);
                            break;
                        default: throw new NotSupportedException();
                    }
                }
            }
            foreach (var t in semicolons)
            {
                if (openToken.Index <= t.Index && t.Index <= closeToken.Index)
                {
                    r.semicolons.Add(t);
                }
            }
            foreach (var t in commas)
            {
                if (openToken.Index <= t.Index && t.Index <= closeToken.Index)
                {
                    r.commas.Add(t);
                }
            }
            foreach (var t in colons)
            {
                if (openToken.Index <= t.Index && t.Index <= closeToken.Index)
                {
                    r.colons.Add(t);
                }
            }
            // TODO append all operators

            return r;
        }

        List<Token> opTknRw(TokenId operatorTokenId)
        {
            switch (operatorTokenId)
            {
                case TokenId.StatementSepScolon: return semicolons;
                case TokenId.Comma: return commas;
                case TokenId.Colon: return colons;
                case TokenId.Assign: return assigns;
                case TokenId.PlusAssign: return plusAssigns;
                case TokenId.MinusAssign: return minusAssigns;
                case TokenId.MulAssign: return mulAssigns;
                case TokenId.PerAssign: return perAssigns;
                case TokenId.XorAssign: return xorAssigns;
                case TokenId.AndAssign: return andAssigns;
                case TokenId.OrAssign: return orAssigns;
                case TokenId.ModAssign: return modAssigns;
                case TokenId.LsAssign: return lsAssigns;
                case TokenId.RsAssign: return rsAssigns;
                case TokenId.Qmark: return qmarks;
                case TokenId.OrOr: return orOrs;
                case TokenId.AndAnd: return andAnds;
                case TokenId.Or: return ors;
                case TokenId.Xor: return xors;
                case TokenId.And: return ands;
                case TokenId.Equal: return equalss;
                case TokenId.NonEqual: return nonEqualss;
                case TokenId.Lt: return lts;
                case TokenId.Gt: return gts;
                case TokenId.Le: return les;
                case TokenId.Ge: return ges;
                case TokenId.Is: return iss;
                case TokenId.As: return ass;
                //case TokenId.Ls : return lss;
                //case TokenId.rs : return rss;
                case TokenId.Plus: return pluss;
                case TokenId.Minus: return minuss;
                case TokenId.Star: return muls;
                case TokenId.Slash: return pers;
                case TokenId.Modulo: return mods;
                case TokenId.Not: return nots;
                case TokenId.NotBitws: return bwnots;
                case TokenId.PlusPlus: return plusPluss;
                case TokenId.MinusMinus: return minusMinuss;
                case TokenId.Where: return wheres;
                case TokenId.In: return ins;
                case TokenId.Point: return points;
                default: throw new ArgumentException("operatorTokenId:" + operatorTokenId);
            }
        }

        public override string ToString()
        {
            return type + ":tk" + openToken.Index + (closeToken == null ? "..null" : "..tk" + closeToken.Index);
        }
    }
}