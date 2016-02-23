using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneParser.Lexer
{
    public enum TokenMatchState
    {
        NO_MATCH,
        STARTED,
        MATCHED,
        ERROR,
        ERROR_NO_BACKSTEP
    }

    public abstract class TokenRule
    {

        protected TokenRule(Parser parser, TokenId tokenId)
        {
            this.parser = parser;
            this.tokenId = tokenId;
            this.currentErrors = new List<CompilationErrorEnty>();
        }

        protected TokenRule(Parser parser)
        {
            this.parser = parser;
            this.currentErrors = new List<CompilationErrorEnty>();
        }

        protected readonly Parser parser;
        public Parser Parser
        {
            get
            {
                return parser;
            }
        }

        protected TokenMatchState currentState = TokenMatchState.NO_MATCH;
        public TokenMatchState CurrentState
        {
            get
            {
                return currentState;
            }
        }

        protected TokenId tokenId;
        public TokenId TokenId
        {
            get
            {
                return tokenId;
            }
        }

        protected int tokenStart;
        public int TokenStart
        {
            get
            {
                return tokenStart;
            }
        }

        protected int tokenEnd;
        public int TokenEnd
        {
            get
            {
                return tokenEnd;
            }
        }

        public virtual string CurrentTokenContent
        {
            get
            {
                return parser.GetContent(tokenStart, tokenEnd);
            }
        }

        public virtual object Value
        {
            get
            {
                return null;
            }
        }

        protected readonly List<CompilationErrorEnty> currentErrors;
        public IList<CompilationErrorEnty> CurrentErrors
        {
            get
            {
                return currentErrors.AsReadOnly();
            }
        }

        public Token GenerateToken(int totalIndex, int index, int blockIndex, int arrayIndex, int parenIndex, int genericIndex, TokenOpenClose block, TokenOpenClose genericBlock)
        {
            return new Token(tokenId, CurrentTokenContent, tokenStart, tokenEnd, totalIndex, index, blockIndex, arrayIndex, parenIndex, genericIndex, block, genericBlock, Value);
        }

        internal abstract void Reset();

        internal abstract void Apply(char ch, char next_ch);

    }
}
