using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public abstract class TypeSpecifierNode : IdentifierNode
    {
        public TypeSpecifierNode(StatementId id, CsParser parser, BlockNode parent, Token startToken, Token endToken, List<ExpressionNode> firstBracketExpressions, TokenOpenClose initializerRanks, int commaSeparatedRanks, int emptyRanks, TokenOpenClose emptyRanksFirst, TokenOpenClose emptyRanksLast, Token pointerStar)
            : base(id, parser, parent, startToken, endToken)
        {
            this.initializerRanks = initializerRanks;
            this.commaSeparatedRanks = commaSeparatedRanks;
            this.emptyRanks = emptyRanks;
            this.emptyRanksFirst = emptyRanksFirst;
            this.emptyRanksLast = emptyRanksLast;
            this.pointerStar = pointerStar;
            this.firstBracketExpressions = firstBracketExpressions;
            if (firstBracketExpressions != null)
            {
                firstBracketExpressionsUm = firstBracketExpressions.AsReadOnly();
            }
        }

        public abstract string Name
        {
            get;
        }

        public bool IsArray
        {
            get
            {
                return commaSeparatedRanks != -1 || emptyRanks != -1; 
            }
        }

        public bool IsPointer
        {
            get
            {
                return pointerStar != null;
            }
        }

        public virtual bool IsGeneric
        {
            get
            {
                return false;
            }
        }

        internal readonly List<ExpressionNode> firstBracketExpressions;
        readonly IList<ExpressionNode> firstBracketExpressionsUm;
        public IList<ExpressionNode> FirstBracketExpressions
        {
            get
            {
                return firstBracketExpressions;
            }
        }

        internal readonly TokenOpenClose initializerRanks;
        public TokenOpenClose InitializerRanks
        {
            get
            {
                return initializerRanks;
            }
        }

        readonly int commaSeparatedRanks;
        public int CommaSeparatedRanks
        {
            get
            {
                return commaSeparatedRanks;
            }
        }

        readonly int emptyRanks;
        public int EmptyRanks
        {
            get
            {
                return emptyRanks;
            }
        }

        readonly TokenOpenClose emptyRanksFirst;
        public TokenOpenClose EmptyRanksFirst
        {
            get
            {
                return emptyRanksFirst;
            }
        }

        readonly TokenOpenClose emptyRanksLast;
        public TokenOpenClose EmptyRanksLast
        {
            get
            {
                return emptyRanksLast;
            }
        }

        readonly Token pointerStar;
        public Token PointerStar
        {
            get
            {
                return pointerStar;
            }
        }
    }
}