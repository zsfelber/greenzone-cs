using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;
using System.ComponentModel;

namespace GreenZoneParser
{
    public abstract class Tokenizer
    {
        readonly string fileContent;

        char ch1;
        char ch2;

        protected readonly List<TokenRule> rules;

        List<TokenRule> workingRules;
        List<TokenRule> lastMatched;
        int afterLastMatchedPosition;
        int lastInvalidTokenStartPosition;
        StringBuilder lastInvalidToken;

        internal Tokenizer(Parser parser, string fileContent)
        {
            this.parser = parser;
            this.fileContent = fileContent;
            this.totalResult = new List<Token>();
            this.result = new List<Token>();
            this.rules = new List<TokenRule>();

            totalResultUm = totalResult.AsReadOnly();
            resultUm = result.AsReadOnly();
        }

        protected void Init()
        {
            workingRules = new List<TokenRule>(rules);

            lastMatched = null;
            afterLastMatchedPosition = 0;
            lastInvalidTokenStartPosition = 0;
            lastInvalidToken = new StringBuilder();

            position = prereadPosition;
            ch1 = read();
            ch2 = read();
        }

        protected readonly Parser parser;
        public Parser Parser
        {
            get
            {
                return parser;
            }
        }

        protected int prereadPosition = 0;
        protected int position = 0;
        public int Position
        {
            get
            {
                return position;
            }
        }

        public bool Finished
        {
            get
            {
                return ch1 == '\0';
            }
        }

        protected readonly List<Token> totalResult;
        readonly IList<Token> totalResultUm;
        public IList<Token> TotalResult
        {
            get
            {
                return totalResultUm;
            }
        }

        protected readonly List<Token> result;
        readonly IList<Token> resultUm;
        public IList<Token> Result
        {
            get
            {
                return resultUm;
            }
        }

        public Token FirstToken
        {
            get
            {
                return result[0];
            }
        }

        public Token LastToken
        {
            get
            {
                return result[result.Count - 1];
            }
        }


        public Token Previous(Token token)
        {
            if (token.Index > 0)
            {
                token = result[token.Index - 1];
            }
            else
            {
                token = null;
            }
            return token;
        }

        public Token Next(Token token)
        {
            if (token.Index < result.Count - 1)
            {
                token = result[token.Index + 1];
            }
            else
            {
                token = null;
            }
            return token;
        }

        internal void Tokenize()
        {

            while (!Finished)
            {
                ReadNextToken();
            }
        }

        public virtual void ClearTokens()
        {
            result.Clear();
            totalResult.Clear();
        }

        void ReadNextToken()
        {
            bool added = false;
            if (ch1 == '\n')
            {
                parser.AddNewLine();
            }

            List<TokenRule> matched = new List<TokenRule>();
            List<TokenRule> notmatched = new List<TokenRule>();
            List<TokenRule> failed = new List<TokenRule>();
            List<TokenRule> failedNoBackstep = new List<TokenRule>();
            List<CompilationErrorEnty> lastErrors = new List<CompilationErrorEnty>();
            foreach (var rule in workingRules)
            {
                rule.Apply(ch1, ch2);
                switch (rule.CurrentState)
                {
                    case TokenMatchState.MATCHED:
                        matched.Add(rule);
                        break;
                    case TokenMatchState.ERROR:
                        failed.Add(rule);
                        break;
                    case TokenMatchState.ERROR_NO_BACKSTEP:
                        failedNoBackstep.Add(rule);
                        break;
                    case TokenMatchState.NO_MATCH:
                        notmatched.Add(rule);
                        break;
                }
            }

            foreach (var rule in matched)
            {
                workingRules.Remove(rule);
            }
            foreach (var rule in notmatched)
            {
                workingRules.Remove(rule);
                rule.Reset();
            }

            List<TokenRule> rulesWithErrors = null;
            if (failedNoBackstep.Count > 0)
            {
                // NOTE important
                lastMatched = null;
                rulesWithErrors = failedNoBackstep;
            }
            else if (failed.Count > 0)
            {
                rulesWithErrors = failed;
            }

            if (rulesWithErrors != null)
            {
                lastErrors.Clear();
                foreach (var rule in rulesWithErrors)
                {
                    lastErrors.AddRange(rule.CurrentErrors);
                    workingRules.Remove(rule);
                    rule.Reset();
                }
            }


            if (matched.Count > 0)
            {
                if (lastMatched != null)
                {
                    foreach (var rule in lastMatched)
                    {
                        rule.Reset();
                    }
                }
                lastMatched = matched;
                afterLastMatchedPosition = prereadPosition - 1;
            }

            if (workingRules.Count == 0)
            {
                if (lastMatched == null)
                {
                    // NOTE important
                    if (failedNoBackstep.Count > 0)
                    {
                        foreach (var err in lastErrors)
                        {
                            parser.AddError(err);
                        }

                        position = prereadPosition - 1;
                        lastInvalidTokenStartPosition = prereadPosition;
                        lastInvalidToken.Clear();
                    }
                    else
                    {
                        prereadPosition = afterLastMatchedPosition;
                        lastInvalidToken.Append(read());
                        position = prereadPosition;
                        ch2 = read();
                        afterLastMatchedPosition++;
                        foreach (var err in lastErrors)
                        {
                            parser.AddError(err);
                        }
                    }
                }
                else
                {
                    if (lastInvalidToken.Length > 0)
                    {
                        parser.AddError("Invalid token : " + lastInvalidToken, lastInvalidTokenStartPosition, lastMatched[0].TokenStart - 1);
                    }
                    if (lastMatched.Count > 1)
                    {
                        bool hasSimple = false;
                        foreach (var rule in lastMatched)
                        {
                            if (rule is SimpleTokenRule)
                            {
                                hasSimple = true;
                                break;
                            }
                        }
                        if (hasSimple)
                        {
                            foreach (var rule in new List<TokenRule>(lastMatched))
                            {
                                if (!(rule is SimpleTokenRule))
                                {
                                    lastMatched.Remove(rule);
                                    rule.Reset();
                                }
                            }
                        }

                        if (lastMatched.Count > 1)
                        {
                            parser.AddError("Concurrent tokens.", lastMatched[0].TokenStart, lastMatched[0].TokenEnd);
                        }
                    }
                    foreach (var rule in lastMatched)
                    {
                        Token token = processRule(rule);

                        parser.RaiseTokenRead();
                        parser.RaiseTokenAdded(token);
                        rule.Reset();
                        added = true;
                    }
                    prereadPosition = afterLastMatchedPosition;
                    lastInvalidTokenStartPosition = afterLastMatchedPosition;

                    position = prereadPosition;
                    ch2 = read();
                    lastInvalidToken.Clear();
                    lastMatched = null;
                }

                workingRules = new List<TokenRule>(rules);
            }
            else
            {
                position = prereadPosition - 1;
            }

            if (!added)
            {
                parser.RaiseTokenRead();
            }

            // 

            ch1 = ch2;
            ch2 = read();
        }

        protected abstract Token processRule(TokenRule rule);

        protected char read()
        {
            if (prereadPosition >= fileContent.Length)
            {
                prereadPosition++;
                return '\0';
            }
            else
            {
                char ch = fileContent[prereadPosition];
                if (ch == '\r')
                {
                    prereadPosition++;
                    ch = read();
                }
                else if (!char.IsWhiteSpace(ch) && char.IsControl(ch))
                {
                    parser.AddError("Unsupported character.", prereadPosition, prereadPosition);
                    prereadPosition++;
                    ch = read();
                }
                else
                {
                    prereadPosition++;
                }

                return ch;
            }
        }

    }
}