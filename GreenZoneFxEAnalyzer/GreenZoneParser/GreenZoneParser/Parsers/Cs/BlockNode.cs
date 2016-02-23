using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Parsers.Cs
{
    public enum BlockParseMode
    {
        Unspecified,

        MainBlock,
        Namespace,
        ClassOrStruct,
        Interface,
        Enum,
        PropertyOrMethod,
        Property,
        Method,

        Switch,
        For_Substatements,
        For1or3_Substatement,
        Foreach_Substatement,
        Logical_Substatement,
        Expression_Substatement,
        Declaration_Substatement,
        DeclarationWithAssign_Substatement,
        ArrayInitializers,

    }

    public class BlockNode : ControlNode
    {
        static readonly SortedSet<TokenId> substatIds = new SortedSet<TokenId>();
        static readonly SortedSet<TokenId> otherBlockIds = new SortedSet<TokenId>();

        static BlockNode()
        {
            substatIds.Add(TokenId.For);
            substatIds.Add(TokenId.Foreach);
            substatIds.Add(TokenId.While);
            substatIds.Add(TokenId.If);
            substatIds.Add(TokenId.Switch);
            substatIds.Add(TokenId.Catch);
            substatIds.Add(TokenId.Fixed);
            substatIds.Add(TokenId.Lock);
            substatIds.Add(TokenId.Using);


            otherBlockIds.Add(TokenId.Do);
            otherBlockIds.Add(TokenId.Else);
            otherBlockIds.Add(TokenId.Checked);
            otherBlockIds.Add(TokenId.Unchecked);
        }

        public BlockNode(CsParser parser, BlockNode parent, TokenOpenClose blockTokens, bool includesBlockOpCl, Token headerFirstToken, int level, BlockParseMode parseMode)
            : base(parser, parent, blockTokens.OpenToken, blockTokens.CloseToken, headerFirstToken)
        {
            // infinite loop ?
            if (level >= 100000)
            {
                parser.AddError("Input file block hierarchy is too deep (100000).", blockTokens.OpenToken.TokenStartPos, blockTokens.CloseToken.TokenEndPos);
                return;
            }
            if (headerFirstToken != null && headerFirstToken.Index >= StartToken.Index)
            {
                throw new NotSupportedException();
            }

            this.blockTokens = blockTokens;
            this.parseMode = parseMode;
            Token statementFirst = headerFirstToken;

            if (headerFirstToken != null)
            {
                if (modifiersUm != null)
                {
                    statementFirst = modifiersUm[modifiersUm.Count - 1].EndToken;
                    statementFirst = parser.Tokenizer.Next(statementFirst);
                }
                else if (attributesUm != null)
                {
                    statementFirst = attributesUm[attributesUm.Count - 1].EndToken;
                    statementFirst = parser.Tokenizer.Next(statementFirst);
                }

                if (statementFirst.Index >= StartToken.Index)
                {
                    parser.AddError("Invalid statement block header : ", headerFirstToken.TokenStartPos, statementFirst.TokenStartPos);
                    statementFirst = null;
                }
            }

            setSpecialParseMode(ref this.parseMode, statementFirst);

            if (this.parseMode == BlockParseMode.Unspecified)
            {
                return;
            }

            bool enableBlocks;
            bool enableStatements;
            bool checkStatements;
            bool enableFootStatement;
            switch (this.parseMode)
            {
                case BlockParseMode.MainBlock:
                case BlockParseMode.Namespace:
                case BlockParseMode.ClassOrStruct:
                case BlockParseMode.PropertyOrMethod:
                case BlockParseMode.Switch:
                    enableBlocks = true;
                    enableStatements = true;
                    checkStatements = false;
                    enableFootStatement = false;
                    break;
                case BlockParseMode.Interface:
                    // TODO property
                    enableBlocks = true;
                    enableStatements = true;
                    checkStatements = false;
                    enableFootStatement = false;
                    break;
                case BlockParseMode.For1or3_Substatement:
                case BlockParseMode.ArrayInitializers:
                case BlockParseMode.Enum:
                    enableBlocks = false;
                    enableStatements = true;
                    checkStatements = false;
                    enableFootStatement = true;
                    break;
                case BlockParseMode.Foreach_Substatement:
                case BlockParseMode.Logical_Substatement:
                case BlockParseMode.Expression_Substatement:
                case BlockParseMode.Declaration_Substatement:
                case BlockParseMode.DeclarationWithAssign_Substatement:
                    enableBlocks = false;
                    enableStatements = false;
                    checkStatements = true;
                    enableFootStatement = true;
                    break;
                case BlockParseMode.For_Substatements:
                    enableBlocks = false;
                    enableStatements = false;
                    checkStatements = false;
                    enableFootStatement = false;
                    break;
                default: throw new NotSupportedException();
            }


            if (statementFirst != null)
            {
                Token statementLast = parser.Tokenizer.Previous(StartToken);
                headStatement = ParseStatement(statementFirst, statementLast, false, StatementParseMode.Header);
                if (this.parseMode == BlockParseMode.PropertyOrMethod)
                {
                    if (headStatement == null)
                    {
                        this.parseMode = BlockParseMode.Method;
                    }
                    else
                    {
                        switch (headStatement.Id)
                        {
                            case StatementId.MethodDef:
                            case StatementId.PropertyGet:
                            case StatementId.PropertySet:
                            case StatementId.ConstructorDef:
                            case StatementId.DestructorDef:
                                this.parseMode = BlockParseMode.Method;
                                break;
                            case StatementId.IndexerDef:
                            case StatementId.PropertyDef:
                                this.parseMode = BlockParseMode.Property;
                                break;
                            default:
                                if (parent.parseMode == BlockParseMode.Method || parent.parseMode == BlockParseMode.Property || parent.parseMode == BlockParseMode.Switch)
                                {
                                    this.parseMode = BlockParseMode.Method;
                                }
                                else
                                {
                                    parser.AddError("Invalid block header, expected: property or method.", statementFirst.TokenStartPos, statementLast.TokenEndPos);
                                }
                                break;
                        }
                    }
                }
            }
            else if (this.parseMode == BlockParseMode.PropertyOrMethod)
            {
                throw new NotSupportedException();
            }

            Token prev_token2 = blockTokens.OpenToken;

            foreach (var toc in blockTokens.ChildrenBlock)
            {
                Token token1 = toc.OpenToken;
                Token token2 = toc.CloseToken;

                if (token1 != null && token2 != null)
                {
                    if (token1.Index < token2.Index)
                    {
                        Token statementEnd;
                        bool parseBlock;

                        if (token1.Id == TokenId.BlockOpen)
                        {
                            Token prevToken = parser.Tokenizer.Previous(token1);
                            if (prevToken.Id == TokenId.ArrayOrAttributeClose || prevToken.Id == TokenId.Assign)
                            {
                                Token semicolon = blockTokens.Operator(TokenId.StatementSepScolon,token2);
                                TokenOpenClose toc2 = toc.NextBlock();
                                if (semicolon != null && (toc2 == null || semicolon.Index < toc2.OpenToken.Index))
                                {
                                    statementEnd = semicolon;
                                    parseBlock = false;
                                }
                                else
                                {
                                    statementEnd = token1;
                                    parseBlock = true;
                                }
                            }
                            else
                            {
                                statementEnd = token1;
                                parseBlock = true;
                            }
                        }
                        else
                        {
                            statementEnd = token1;
                            parseBlock = true;
                        }
                        if (!enableBlocks)
                        {
                            if (parseBlock)
                            {
                                parser.AddError("Statement block is not allowed here.", token1.TokenStartPos, token2.TokenEndPos);
                            }
                            break;
                        }
                        Token nextToken = null;

                        List<StatementNode> statements;
                        if (enableStatements || checkStatements)
                        {
                            statements = ParseStatements(prev_token2, statementEnd, ref nextToken);
                        }
                        else
                        {
                            statements = null;
                        }
                        if (enableStatements)
                        {
                            controlNodes.AddRange(statements);
                        }
                        else if (checkStatements && statements != null && statements.Count > 0)
                        {
                            parser.AddError("Multiple statements are not allowed here.", prev_token2.TokenStartPos, statementEnd.TokenEndPos);
                        }

                        if (parseBlock)
                        {
                            if (nextToken == null)
                            {
                                nextToken = parser.Tokenizer.Next(prev_token2);
                                if (nextToken == token1)
                                {
                                    nextToken = null;
                                }
                            }
                            if (nextToken == toc.OpenToken)
                            {
                                nextToken = null;
                            }
                            BlockNode block = new BlockNode(parser, this, toc, true, nextToken, level + 1, BlockParseMode.Unspecified);
                            controlNodes.Add(block);
                        }
                        else
                        {
                            // !!
                            prev_token2 = parser.Tokenizer.Next(statementEnd);
                            continue;
                        }
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
                else if (token1 != null)
                {
                    parser.AddError("Unclosed block.", token1.TokenStartPos, blockTokens.CloseToken.TokenEndPos);
                    break;
                }
                else if (token2 != null)
                {
                    prev_token2 = token2;
                    parser.AddError("Unclosed block.", blockTokens.OpenToken.TokenStartPos, token2.TokenEndPos);
                    break;
                }
                else
                {
                    break;
                }
                prev_token2 = token2;
            }

            if (blockTokens.CloseToken != prev_token2)
            {
                Token nextToken2 = null;
                List<StatementNode> statements;
                if (enableStatements || checkStatements)
                {
                    statements = ParseStatements(prev_token2, blockTokens.CloseToken, ref nextToken2);
                }
                else
                {
                    statements = null;
                }
                if (enableStatements)
                {
                    controlNodes.AddRange(statements);
                }
                else if (checkStatements && statements != null && statements.Count > 0)
                {
                    parser.AddError("Multiple statements are not allowed here.", prev_token2.TokenStartPos, blockTokens.CloseToken.TokenStartPos);
                }

                if (nextToken2 == null)
                {
                    if (prev_token2 == blockTokens.OpenToken && !includesBlockOpCl)
                    {
                        nextToken2 = blockTokens.OpenToken;
                    }
                    else
                    {
                        nextToken2 = parser.Tokenizer.Next(prev_token2);
                    }
                }
                if (nextToken2 != blockTokens.CloseToken)
                {
                    if (enableFootStatement)
                    {
                        Token statementLast;
                        if (includesBlockOpCl)
                        {
                            statementLast = parser.Tokenizer.Previous(blockTokens.CloseToken);
                        }
                        else
                        {
                            statementLast = blockTokens.CloseToken;
                        }
                        StatementNode footStatement = ParseStatement(nextToken2, statementLast, false, StatementParseMode.Inside);
                        controlNodes.Add(footStatement);
                    }
                    else if (enableStatements)
                    {
                        parser.AddError("Unexpected tokens before block ending.", nextToken2.TokenStartPos, blockTokens.CloseToken.TokenStartPos - 1);
                    }
                }
            }

            if (this.parseMode == BlockParseMode.For_Substatements)
            {
                IList<Token> statementOrCommaTokens = blockTokens.Semicolons;
                IList<Token> tokens = parser.Tokenizer.Result;

                int prevTokenIndex = blockTokens.OpenToken.Index;

                Token token = blockTokens.OpenToken;
                Token nextToken = null;
                int j = 0;
                for (int i = blockTokens.OpenToken.CommaIndex; i < statementOrCommaTokens.Count && (token = statementOrCommaTokens[i]).Index <= blockTokens.CloseToken.Index; i++, j++, prevTokenIndex = token.Index)
                {
                    Token firstToken = tokens[prevTokenIndex + 1];

                    switch (j)
                    {
                        case 0:
                            TokenOpenClose toc = blockTokens.SubToc(blockTokens.OpenToken, token);
                            BlockNode forSubstatementBlock = new BlockNode(parser, this, toc, true, null, 0, BlockParseMode.For1or3_Substatement);
                            controlNodes.Add(forSubstatementBlock);
                            break;
                        case 1:
                            StatementNode statement = ParseStatement(firstToken, token, true, StatementParseMode.Inside);
                            controlNodes.Add(statement);
                            break;
                        default:
                            parser.AddError("Unexpected tokens at the end of 'for' substatement block.", token.TokenStartPos, blockTokens.CloseToken.TokenStartPos - 1);
                            break;
                    }
                    nextToken = tokens[token.Index + 1];
                }
                if (j == 2)
                {
                    Token lastToken = tokens[blockTokens.CloseToken.Index - 1];
                    TokenOpenClose toc = blockTokens.SubToc(nextToken, lastToken);
                    BlockNode forSubstatementBlock = new BlockNode(parser, this, toc, false, null, 0, BlockParseMode.For1or3_Substatement);
                    controlNodes.Add(forSubstatementBlock);
                }
                else if (j<2)
                {
                    parser.AddError("Missing ';'.", blockTokens.CloseToken.TokenStartPos, blockTokens.CloseToken.TokenEndPos);
                }
            }

            parser.RaiseNodeCreated(this);
        }

        readonly TokenOpenClose blockTokens;
        public TokenOpenClose BlockTokens
        {
            get
            {
                return blockTokens;
            }
        }

        readonly BlockParseMode parseMode;
        public BlockParseMode ParseMode
        {
            get
            {
                return parseMode;
            }
        }

        readonly StatementNode headStatement;
        public StatementNode HeadStatement
        {
            get
            {
                return headStatement;
            }
        }

        readonly List<ControlNode> controlNodes = new List<ControlNode>();
        public IList<ControlNode> ControlNodes
        {
            get
            {
                return controlNodes.AsReadOnly();
            }
        }

        public string TypeName
        {
            get
            {
                TypeDefStatementNode typeDefHeader = (TypeDefStatementNode)headStatement;
                return typeDefHeader == null ? null : typeDefHeader.TypeName;
            }
        }


        protected List<StatementNode> ParseStatements(Token from, Token to, ref Token nextToken)
        {
            if (from.Index > to.Index)
            {
                throw new NotSupportedException();
            }
            IList<Token> statementOrCommaTokens = getStatementOrCommaTokens();
            IList<Token> tokens = parser.Tokenizer.Result;

            int prevTokenIndex;
            if (from.Id == TokenId.BlockOpen || from.Id == TokenId.BlockClose || from.Id == TokenId.ParenOpen)
            {
                prevTokenIndex = from.Index;
            }
            else
            {
                prevTokenIndex = from.Index - 1;
            }

            List<StatementNode> result = new List<StatementNode>();
            List<StatementNode> cases;
            Token semicolonToken;
            Token current = tokens[prevTokenIndex + 1];
            for (int i = getStatementOrCommaIndex(from); i < statementOrCommaTokens.Count && (semicolonToken = statementOrCommaTokens[i]).Index <= to.Index; i++, prevTokenIndex = semicolonToken.Index)
            {
                cases = ParseCaseStatements(current, semicolonToken, ref current);
                if (cases != null)
                {
                    result.AddRange(cases);
                }

                StatementNode statement = ParseStatement(current, semicolonToken, true, StatementParseMode.Inside);
                result.Add(statement);
                current = tokens[semicolonToken.Index + 1];
                nextToken = current;
            }

            cases = ParseCaseStatements(current, to, ref nextToken);
            if (cases != null)
            {
                result.AddRange(cases);
            }
            return result;
        }

        protected List<StatementNode> ParseCaseStatements(Token from, Token to, ref Token nextToken)
        {
            List<StatementNode> result;
            if (parseMode == BlockParseMode.Switch)
            {
                IList<Token> colons = blockTokens.Colons;
                IList<Token> tokens = parser.Tokenizer.Result;

                int prevTokenIndex;
                if (from.Id == TokenId.BlockOpen || from.Id == TokenId.BlockClose || from.Id == TokenId.ParenOpen)
                {
                    prevTokenIndex = from.Index;
                }
                else
                {
                    prevTokenIndex = from.Index - 1;
                }

                result = new List<StatementNode>();
                Token colonToken;
                Token current = tokens[prevTokenIndex + 1];
                for (int i = from.ColonIndex; i < colons.Count && (colonToken = colons[i]).Index <= to.Index; i++, prevTokenIndex = colonToken.Index)
                {
                    if (current.Id == TokenId.Case || current.Id == TokenId.Default)
                    {
                        StatementNode caseStatement = ParseStatement(current, colonToken, false, StatementParseMode.Inside);
                        result.Add(caseStatement);
                        current = tokens[colonToken.Index + 1];
                        nextToken = current;
                    }
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        protected StatementNode ParseStatement(Token firstToken, Token lastToken, bool includesSemicolon, StatementParseMode statementParseMode)
        {
            BlockNode subStatementsBlock = null;
            StatementNode tailStatement = null;

            SortedSet<TokenId> substatIds;
            if (this.parseMode == BlockParseMode.MainBlock)
            {
                substatIds = new SortedSet<TokenId>();
            }
            else
            {
                substatIds = BlockNode.substatIds;
            }

            if (substatIds.Contains(firstToken.Id))
            {
                TokenOpenClose toc = CSParser.CsTokenizer.Paren(firstToken);
                if (toc.OpenToken.Index == firstToken.Index + 1)
                {
                    if (toc != null && toc.OpenToken.Index <= lastToken.Index)
                    {
                        if (toc.CloseToken == null || toc.CloseToken.Index > lastToken.Index)
                        {
                            parser.AddError("Missing closing parenthesis.", toc.OpenToken.TokenStartPos, lastToken.TokenEndPos);
                        }
                        else
                        {
                            BlockParseMode childParseMode = getSubStatementChildParseMode(firstToken);
                            subStatementsBlock = new BlockNode(CSParser, this, toc, true, null, 0, childParseMode);

                            if (toc.CloseToken.Index + (includesSemicolon ? 1 : 0) < lastToken.Index)
                            {
                                Token tailFirstToken = parser.Tokenizer.Next(toc.CloseToken);
                                tailStatement = ParseStatement(tailFirstToken, lastToken, includesSemicolon, statementParseMode);
                                lastToken = toc.CloseToken;
                                includesSemicolon = false;
                            }
                        }
                    }
                }
                else
                {
                    if (toc != null && toc.OpenToken.Index <= lastToken.Index)
                    {
                        parser.AddError(firstToken.ContentString + " should be directly followed by open parenthesis.", firstToken.TokenStartPos, toc.OpenToken.TokenStartPos);
                    }
                    else
                    {
                        parser.AddError(firstToken.ContentString + " should be directly followed by open parenthesis.", firstToken.TokenStartPos, lastToken.TokenStartPos);
                    }
                }
            }
            else if (otherBlockIds.Contains(firstToken.Id))
            {
                if (firstToken.Index + (includesSemicolon ? 1 : 0) < lastToken.Index)
                {
                    Token tailFirstToken = parser.Tokenizer.Next(firstToken);
                    tailStatement = ParseStatement(tailFirstToken, lastToken, includesSemicolon, statementParseMode);
                    lastToken = firstToken;
                    includesSemicolon = false;
                }
            }
            StatementNode result = ParseStatement(firstToken, lastToken, includesSemicolon, subStatementsBlock, tailStatement, statementParseMode);
            return result;
        }

        protected StatementNode ParseStatement(Token firstToken, Token lastToken, bool includesSemicolon, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode statementParseMode)
        {
            if (firstToken.Index > lastToken.Index)
            {
                throw new NotSupportedException();
            }
            if (includesSemicolon)
            {
                TokenId statementOrCommaId = getStatementOrCommaId();

                if (lastToken.Id != statementOrCommaId)
                {
                    throw new NotSupportedException("" + lastToken.Id);
                }
            }

            IList<Token> tokens = parser.Tokenizer.Result;
            BlockNode statementParent;
            if (statementParseMode == StatementParseMode.Header)
            {
                statementParent = Parent;
            }
            else
            {
                statementParent = this;
            }


            List<AttributeNode> attributes = ParseAttributes(statementParent, firstToken, lastToken);
            if (attributes != null)
            {
                firstToken = tokens[attributes[attributes.Count - 1].EndToken.Index + 1];
            }

            List<ModifierNode> modifiers;
            modifiers = ParseModifiers(statementParent, firstToken, lastToken);
            if (modifiers != null)
            {
                firstToken = tokens[modifiers[modifiers.Count - 1].EndToken.Index + 1];
            }

            StatementNode statement = StatementNode.Create(CSParser, statementParent, firstToken, lastToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, statementParseMode);
            return statement;
        }

        protected IList<Token> getStatementOrCommaTokens()
        {
            IList<Token> statementOrCommaTokens;
            switch (this.parseMode)
            {
                case BlockParseMode.MainBlock:
                case BlockParseMode.Namespace:
                case BlockParseMode.ClassOrStruct:
                case BlockParseMode.Interface:
                case BlockParseMode.PropertyOrMethod:
                case BlockParseMode.Method:
                case BlockParseMode.Property:
                case BlockParseMode.Switch:
                case BlockParseMode.For_Substatements:
                case BlockParseMode.Foreach_Substatement:
                case BlockParseMode.Logical_Substatement:
                case BlockParseMode.Expression_Substatement:
                case BlockParseMode.Declaration_Substatement:
                case BlockParseMode.DeclarationWithAssign_Substatement:
                    statementOrCommaTokens = blockTokens.Semicolons;
                    break;
                case BlockParseMode.For1or3_Substatement:
                case BlockParseMode.ArrayInitializers:
                case BlockParseMode.Enum:
                    statementOrCommaTokens = blockTokens.Commas;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return statementOrCommaTokens;
        }

        protected int getStatementOrCommaIndex(Token token)
        {
            int statementOrCommaIndex;
            switch (this.parseMode)
            {
                case BlockParseMode.MainBlock:
                case BlockParseMode.Namespace:
                case BlockParseMode.ClassOrStruct:
                case BlockParseMode.Interface:
                case BlockParseMode.PropertyOrMethod:
                case BlockParseMode.Method:
                case BlockParseMode.Property:
                case BlockParseMode.For_Substatements:
                case BlockParseMode.Foreach_Substatement:
                case BlockParseMode.Logical_Substatement:
                case BlockParseMode.Expression_Substatement:
                case BlockParseMode.Declaration_Substatement:
                case BlockParseMode.DeclarationWithAssign_Substatement:
                case BlockParseMode.Switch:
                    statementOrCommaIndex = token.StatementIndex;
                    break;
                case BlockParseMode.For1or3_Substatement:
                case BlockParseMode.ArrayInitializers:
                case BlockParseMode.Enum:
                    statementOrCommaIndex = token.CommaIndex;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return statementOrCommaIndex;
        }

        protected TokenId getStatementOrCommaId()
        {
            TokenId statementOrCommaId;
            switch (this.parseMode)
            {
                case BlockParseMode.MainBlock:
                case BlockParseMode.Namespace:
                case BlockParseMode.ClassOrStruct:
                case BlockParseMode.Interface:
                case BlockParseMode.PropertyOrMethod:
                case BlockParseMode.Method:
                case BlockParseMode.Property:
                case BlockParseMode.Switch:
                case BlockParseMode.For_Substatements:
                case BlockParseMode.Foreach_Substatement:
                case BlockParseMode.Logical_Substatement:
                case BlockParseMode.Expression_Substatement:
                case BlockParseMode.Declaration_Substatement:
                case BlockParseMode.DeclarationWithAssign_Substatement:
                    statementOrCommaId = TokenId.StatementSepScolon;
                    break;
                case BlockParseMode.For1or3_Substatement:
                case BlockParseMode.ArrayInitializers:
                case BlockParseMode.Enum:
                    statementOrCommaId = TokenId.Comma;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return statementOrCommaId;
        }

        protected void setSpecialParseMode(ref BlockParseMode parseMode, Token firstHeadStatementToken)
        {
            if (firstHeadStatementToken != null)
            {
                switch (firstHeadStatementToken.Id)
                {
                    case TokenId.Namespace:
                        parseMode = BlockParseMode.Namespace;
                        break;
                    case TokenId.Class:
                    case TokenId.Struct:
                        parseMode = BlockParseMode.ClassOrStruct;
                        break;
                    case TokenId.Interface:
                        parseMode = BlockParseMode.Interface;
                        break;
                    case TokenId.Delegate:
                        parseMode = BlockParseMode.Unspecified;
                        parser.AddError("Delegate declaration could not have body.", firstHeadStatementToken.TokenStartPos, firstHeadStatementToken.TokenEndPos);
                        break;
                    case TokenId.Enum:
                        parseMode = BlockParseMode.Enum;
                        break;
                    case TokenId.Switch:
                        parseMode = BlockParseMode.Switch;
                        break;
                    default: 
                        parseMode = BlockParseMode.PropertyOrMethod;
                        break;
                }
            }
        }

        internal static BlockParseMode getSubStatementChildParseMode(Token firstToken)
        {
            BlockParseMode childParseMode;
            switch (firstToken.Id)
            {
                case TokenId.For:
                    childParseMode = BlockParseMode.For_Substatements;
                    break;
                case TokenId.Foreach:
                    childParseMode = BlockParseMode.Foreach_Substatement;
                    break;
                case TokenId.While:
                case TokenId.If:
                    childParseMode = BlockParseMode.Logical_Substatement;
                    break;
                case TokenId.Switch:
                case TokenId.Lock:
                    childParseMode = BlockParseMode.Expression_Substatement;
                    break;
                case TokenId.Catch:
                    childParseMode = BlockParseMode.Declaration_Substatement;
                    break;
                case TokenId.Using:
                case TokenId.Fixed:
                    childParseMode = BlockParseMode.DeclarationWithAssign_Substatement;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return childParseMode;
        }
    }
}