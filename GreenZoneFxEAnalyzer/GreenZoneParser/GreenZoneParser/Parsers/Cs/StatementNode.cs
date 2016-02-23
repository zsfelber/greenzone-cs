using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;

namespace GreenZoneParser.Parsers.Cs
{
    public enum StatementParseMode
    {
        Header,
        Inside
    }

    enum PrimaryExpressionParse
    {
        Field,
        Method,
        Array
    }

    public abstract class StatementNode : ControlNode
    {
        static SortedSet<TokenId> linqKeywords;

        static StatementNode()
        {
            linqKeywords = new SortedSet<TokenId>();
            linqKeywords.Add(TokenId.From);
            linqKeywords.Add(TokenId.Where);
            linqKeywords.Add(TokenId.Let);
            linqKeywords.Add(TokenId.Order);
            linqKeywords.Add(TokenId.Group);
            linqKeywords.Add(TokenId.By);
            linqKeywords.Add(TokenId.Select);
            linqKeywords.Add(TokenId.Join);
            linqKeywords.Add(TokenId.On);
            linqKeywords.Add(TokenId.EqualsLinq);
            linqKeywords.Add(TokenId.Comma);
            linqKeywords.Add(TokenId.Ascending);
            linqKeywords.Add(TokenId.Descending);
        }

        public StatementNode(StatementId id, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
            : base(parser, parent, startToken, endToken, attributes, modifiers)
        {
            this.id = id;
            this.subStatementsBlock = subStatementsBlock;
            this.tailStatement = tailStatement;
            this.parseMode = parseMode;
        }

        readonly StatementId id;
        public StatementId Id
        {
            get
            {
                return id;
            }
        }

        readonly StatementParseMode parseMode;
        public StatementParseMode ParseMode
        {
            get
            {
                return parseMode;
            }
        }

        BlockNode subStatementsBlock;
        public BlockNode SubStatementsBlock
        {
            get
            {
                return subStatementsBlock;
            }
        }

        StatementNode tailStatement;
        public StatementNode TailStatement
        {
            get
            {
                return tailStatement;
            }
        }

        public static StatementNode Create(CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            StatementNode result;
            List<CompilationErrorEnty> errors = new List<CompilationErrorEnty>();

            if (startToken.Id == TokenId.StatementSepScolon)
            {
                result = new EmptyStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            }
            else
            {

                switch (parseMode)
                {

                    case StatementParseMode.Header:

                        switch (parent.ParseMode)
                        {
                            case BlockParseMode.MainBlock:
                                switch (startToken.Id)
                                {
                                    case TokenId.Namespace:
                                        result = ParseNamespaceDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Class:
                                        result = ParseClassDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Struct:
                                        result = ParseStructDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Interface:
                                        result = ParseInterfaceDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Enum:
                                        result = ParseEnumDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    default:
                                        result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        parser.AddError("Not supported here, expected : using, namespace, class, struct, interface, enum, delegate", startToken.TokenStartPos, endToken.TokenEndPos);
                                        break;
                                }
                                break;
                            case BlockParseMode.Namespace:
                                switch (startToken.Id)
                                {
                                    case TokenId.Namespace:
                                        result = ParseNamespaceDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Class:
                                        result = ParseClassDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Struct:
                                        result = ParseStructDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Interface:
                                        result = ParseInterfaceDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Enum:
                                        result = ParseEnumDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    default:
                                        result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        parser.AddError("Not supported here, expected : namespace, class, struct, interface, enum, delegate", startToken.TokenStartPos, endToken.TokenEndPos);
                                        break;
                                }
                                break;
                            case BlockParseMode.ClassOrStruct:
                                switch (startToken.Id)
                                {
                                    case TokenId.Class:
                                        result = ParseClassDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Struct:
                                        result = ParseStructDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Interface:
                                        result = ParseInterfaceDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Enum:
                                        result = ParseEnumDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    default:
                                        result = ParseMemberDecl(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode, false, true, true, true);
                                        break;
                                }
                                break;
                            case BlockParseMode.Interface:
                                result = ParseMemberDecl(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode, false, false, false, true);
                                break;
                            case BlockParseMode.Method:
                            case BlockParseMode.Switch:
                                result = CreateHeaderMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                break;
                            case BlockParseMode.Property:
                                result = ParseMemberDecl(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode, false, true, false, false);
                                break;

                            default:
                                result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                parser.AddError("Unknown statement block. In parent block:" + parent.ParseMode, startToken.TokenStartPos, endToken.TokenEndPos);
                                break;
                        }
                        break;


                    case StatementParseMode.Inside:

                        result = null;

                        switch (parent.ParseMode)
                        {
                            case BlockParseMode.MainBlock:
                            case BlockParseMode.Namespace:
                                switch (startToken.Id)
                                {
                                    case TokenId.Delegate:
                                        result = ParseDelegateDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                }
                                break;
                            case BlockParseMode.ClassOrStruct:
                            case BlockParseMode.Interface:
                                switch (startToken.Id)
                                {
                                    case TokenId.Delegate:
                                        result = ParseDelegateDefStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                    case TokenId.Event:
                                        result = ParseEventDeclStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                        break;
                                }
                                break;
                        }

                        if (result == null)
                        {
                            switch (parent.ParseMode)
                            {
                                case BlockParseMode.MainBlock:
                                    switch (startToken.Id)
                                    {
                                        case TokenId.Using:
                                            result = ParseUsingDeclStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                            break;
                                        default:
                                            result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                            parser.AddError("Statement is not supported here, expected : using, namespace, class, struct, interface, enum, delegate", startToken.TokenStartPos, endToken.TokenEndPos);
                                            break;
                                    }
                                    break;
                                case BlockParseMode.Namespace:
                                    parser.AddError("Statement is not supported here, expected : namespace, class, struct, interface, enum, delegate", startToken.TokenStartPos, endToken.TokenEndPos);
                                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.ClassOrStruct:
                                    result = ParseMemberDecl(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode, true, true, false, false);
                                    break;
                                case BlockParseMode.Interface:
                                    result = ParseMemberDecl(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode, true, true, false, false);
                                    break;
                                case BlockParseMode.Property:
                                    result = ParseMemberDecl(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode, false, true, false, false);
                                    break;
                                case BlockParseMode.Method:
                                    result = CreateInsideMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.For_Substatements:
                                case BlockParseMode.Logical_Substatement:
                                    result = ParseLogicalExpressionNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.For1or3_Substatement:
                                    result = CreateInsideMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.Foreach_Substatement:
                                    result = ParseForEachSubStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.Expression_Substatement:
                                    if (includesSemicolon || attributes != null || modifiers != null || subStatementsBlock != null || tailStatement != null)
                                    {
                                        throw new NotSupportedException();
                                    }
                                    result = ParseExpression(errors, parser, parent, startToken, endToken);
                                    break;
                                case BlockParseMode.Declaration_Substatement:
                                    result = ParseVarDeclStatementNode(errors, parser, parent, startToken, endToken, false, true, true, false);
                                    break;
                                case BlockParseMode.DeclarationWithAssign_Substatement:
                                    result = ParseVarDeclStatementNode(errors, parser, parent, startToken, endToken, false, false, false, true);
                                    break;
                                case BlockParseMode.ArrayInitializers:
                                    result = ParseArrayInitializerNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.Enum:
                                    result = ParseEnumConstantNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    break;
                                case BlockParseMode.Switch:
                                    switch (startToken.Id)
                                    {
                                        case TokenId.Case:
                                            result = ParseCaseStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                            break;
                                        case TokenId.Default:
                                            result = ParseCaseStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                            break;
                                        default:
                                            result = CreateInsideMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                            break;
                                    }
                                    break;
                                default:
                                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    parser.AddError("Unknown statement.  In block:" + parent.ParseMode, startToken.TokenStartPos, endToken.TokenEndPos);
                                    break;
                            }
                        }
                        break;


                    default:

                        throw new NotSupportedException();
                }
            }

            if (result == null)
            {
                foreach (CompilationErrorEnty e in errors)
                {
                    parser.AddError(e);
                }
            }
            else
            {
                parser.RaiseNodeAdded(parent, result);
            }


            return result;
        }

        static StatementNode CreateCommonMethodStatement(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            StatementNode result;

            switch (startToken.Id)
            {
                case TokenId.For:
                    result = new ForStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Foreach:
                    result = new ForEachStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.While:
                    result = new WhileStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.If:
                    result = new IfStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Do:
                    result = new DoStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Else:
                    result = new ElseStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Using:
                    result = new UsingBlockStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Lock:
                    result = new LockStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Fixed:
                    result = new FixedStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                default:
                    result = ParseMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
            }
            return result;
        }

        static StatementNode CreateHeaderMethodStatement(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            StatementNode result;

            switch (startToken.Id)
            {
                case TokenId.Switch:
                    result = new SwitchStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Try:
                    result = new TryStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Catch:
                    result = new CatchStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Finally:
                    result = new FinallyStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Break:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should not be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Continue:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should not be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Return:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should not be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Throw:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should not be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Checked:
                    result = new CheckedStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Unchecked:
                    result = new UncheckedStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                default:
                    result = CreateCommonMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
            }
            return result;
        }

        static StatementNode CreateInsideMethodStatement(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            StatementNode result;

            switch (startToken.Id)
            {
                case TokenId.Switch:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Try:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Catch:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Finally:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Break:
                    result = new BreakStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Continue:
                    result = new ContinueStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Return:
                    result = ParseReturnStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Throw:
                    result = ParseThrowStatementNode(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    break;
                case TokenId.Checked:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                case TokenId.Unchecked:
                    result = new UnknownStatementNode(parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    parser.AddError("This statement should be followed by block.", startToken.TokenStartPos, endToken.TokenEndPos);
                    break;
                default:
                    result = CreateCommonMethodStatement(errors, parser, parent, startToken, endToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);

                    break;
            }
            return result;
        }

        static StatementNode ParseNamespaceDefStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token t = null;
            List<Token> ids = ParseIdsRight(parser, startToken, endToken, ref t);
            if (t == startToken)
            {
                NamespaceDefStatementNode result = new NamespaceDefStatementNode(parser, parent, startToken, endToken, ids, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                return result;
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid namespace definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("NamespaceDef", _err);
                return null;
            }
        }

        static StatementNode ParseClassDefStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            List<TypeSpecifierNode> supertypes = null;
            UserTypeSpecifierNode utype = null;

            bool headerOk = ParseRefTypeDefHeader(errors, parser, parent, startToken, endToken, ref supertypes, ref utype);
            if (!headerOk)
            {
                return null;
            }

            // TODO parse where -s

            ClassDefStatementNode result = new ClassDefStatementNode(parser, parent, startToken, endToken, utype, supertypes, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            return result;
        }

        static StatementNode ParseStructDefStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            List<TypeSpecifierNode> supertypes = null;
            UserTypeSpecifierNode utype = null;

            bool headerOk = ParseRefTypeDefHeader(errors, parser, parent, startToken, endToken, ref supertypes, ref utype);
            if (!headerOk)
            {
                return null;
            }

            // TODO parse where -s

            StructDefStatementNode result = new StructDefStatementNode(parser, parent, startToken, endToken, utype, supertypes, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            return result;
        }

        static StatementNode ParseInterfaceDefStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            List<TypeSpecifierNode> supertypes = null;
            UserTypeSpecifierNode utype = null;

            bool headerOk = ParseRefTypeDefHeader(errors, parser, parent, startToken, endToken, ref supertypes, ref utype);
            if (!headerOk)
            {
                return null;
            }

            // TODO parse where -s

            InterfaceDefStatementNode result = new InterfaceDefStatementNode(parser, parent, startToken, endToken, utype, supertypes, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            return result;
        }

        static bool ParseRefTypeDefHeader(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref List<TypeSpecifierNode> supertypes, ref UserTypeSpecifierNode utypeDefMain)
        {
            Token t = null;
            Token firstMain = parser.Tokenizer.Next(startToken);
            TypeSpecifierNode typeDefMain = ParseTypeSpecifier(errors, parser, parent, firstMain, endToken, ref t, false, true, false);

            if (typeDefMain == null)
            {
                errors.Add(parser.CreateError("Type definition expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                return false;
            }
            else if (typeDefMain is UserTypeSpecifierNode)
            {
                utypeDefMain = (UserTypeSpecifierNode)typeDefMain;
            }
            else
            {
                errors.Add(parser.CreateError("Invalid type name.", startToken.TokenStartPos, endToken.TokenEndPos));
                return false;
            }

            switch (t.Id)
            {
                case TokenId.Colon:
                    TokenOpenClose toc = parent.BlockTokens.SubToc(t, endToken);
                    supertypes = ParseTypeParameters(errors, parser, parent, toc, ref t, true, false);
                    if (supertypes == null)
                    {
                        errors.Add(parser.CreateError("Invalid type definition : wrong base types.", startToken.TokenStartPos, endToken.TokenEndPos));
                        return false;
                    }
                    break;
            }

            switch (t.Id)
            {
                case TokenId.Where:
                    // TODO parse where -s
                    return true;
                default:
                    if (t.Index != endToken.Index + 1)
                    {
                        errors.Add(parser.CreateError("Invalid type definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                        return false;
                    }
                    else
                    {
                        return true;
                    }
            }
        }

        static StatementNode ParseEnumDefStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token t = null;
            List<Token> ids = ParseIdsRight(parser, startToken, endToken, ref t);
            if (t == startToken)
            {
                EnumDefStatementNode result = new EnumDefStatementNode(parser, parent, startToken, endToken, ids, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                return result;
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid enum definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("EnumDef", _err);
                return null;
            }
        }

        static StatementNode ParseDelegateDefStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            Token methodFirst = parser.Tokenizer.Next(startToken);
            StatementNode methodDef = ParseMemberDecl(errors, parser, parent, methodFirst, lastToken, false, null, null, null, tailStatement, parseMode, false, true, false, false);

            if (methodDef is MethodDefStatementNode)
            {
                DelegateDefStatementNode result = new DelegateDefStatementNode(parser, parent, startToken, endToken, (MethodDefStatementNode)methodDef, includesSemicolon, attributes, modifiers, subStatementsBlock, null, parseMode);
                return result;
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid delegate definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("DelegateDef", _err);
                return null;
            }
        }

        static StatementNode ParseEventDeclStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            Token methodFirst = parser.Tokenizer.Next(startToken);
            StatementNode varDeclHeader = ParseVarDeclStatementNode(errors, parser, parent, methodFirst, lastToken, true, false, true, false);

            if (varDeclHeader is VarDeclStatementNode)
            {
                EventDeclStatementNode result = new EventDeclStatementNode(parser, parent, startToken, endToken, (VarDeclStatementNode)varDeclHeader, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                return result;
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid event declaration.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("EventDecl", _err);
                return null;
            }
        }


        static StatementNode ParseUsingDeclStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token t = null;
            List<Token> ids = ParseIdsRight(parser, startToken, parser.Tokenizer.Previous(endToken), ref t);
            if (t == startToken)
            {
                UsingDeclStatementNode result = new UsingDeclStatementNode(parser, parent, startToken, endToken, ids, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                return result;
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid using declaration.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("UsingDecl", _err);
                return null;
            }
        }

        static StatementNode ParseLogicalExpressionNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            ExpressionNode result = ParseExpression(errors, parser, parent, startToken, lastToken);
            if (result != null)
            {
                result.AddAcceptedType(SystemResolvedType.BOOLEAN);
            }

            return result;
        }

        static StatementNode ParseForEachSubStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token inToken = startToken.Block.Operator(TokenId.In, startToken);
            if (inToken == null || inToken.Index >= endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid foreach substatement. Missing 'in'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("ForEachSub", _err);
                return null;
            }
            else
            {
                Token inNxToken = parser.Tokenizer.Next(inToken);
                ExpressionNode right = ParseExpression(errors, parser, parent, inNxToken, endToken);

                Token t = null;
                List<Token> ids = ParseIdsRight(parser, startToken, parser.Tokenizer.Previous(inToken), ref t);

                if (ids != null)
                {
                    Token t2 = null;
                    TypeSpecifierNode typeSpec = ParseTypeSpecifier(errors, parser, parent, startToken, t, ref t2, false, true, true);
                    if (typeSpec == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid foreach substatement. Invalid type.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("ForEachSub", _err);
                        return null;
                    }
                    ForEachSubStatementNode result = new ForEachSubStatementNode(parser, parent, startToken, endToken, typeSpec, ids, right, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    return result;
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid foreach substatement. Missing identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("ForEachSub", _err);
                    return null;
                }
            }
        }

        static StatementNode ParseArrayInitializerNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            Token lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            ExpressionNode initializerExpression;
            BlockNode arrayInitializerBlock;
            if (startToken.Id == TokenId.BlockOpen)
            {
                TokenOpenClose ablock = startToken.Block;
                if (ablock.CloseToken == null || ablock.CloseToken.Index > lastToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid array initializer. Unclosed initializer block.", ablock.OpenToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("ArrayInit", _err);
                    errors.Add(_err = parser.CreateError("Invalid array initializer.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("ArrayInit", _err);
                    return null;
                }
                else if (ablock.CloseToken.Index < lastToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid array initializer. Unexpected tokens.", ablock.CloseToken.TokenEndPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("ArrayInit", _err);
                    errors.Add(_err = parser.CreateError("Invalid array initializer.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("ArrayInit", _err);
                    return null;
                }
                arrayInitializerBlock = new BlockNode(parser, parent, ablock, true, null, 0, BlockParseMode.ArrayInitializers);
                initializerExpression = null;
            }
            else
            {
                initializerExpression = ParseExpression(errors, parser, parent, startToken, lastToken);
                arrayInitializerBlock = null;
                if (initializerExpression == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid array initializer expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("ArrayInit", _err);
                    return null;
                }
            }

            ArrayInitializerNode result = new ArrayInitializerNode(parser, parent, startToken, endToken, initializerExpression, includesSemicolon, attributes, modifiers, arrayInitializerBlock, tailStatement, parseMode);
            return result;
        }

        static StatementNode ParseEnumConstantNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            ExpressionNode right = null;
            Token lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            Token assignToken = parent.BlockTokens.Operator(TokenId.Assign, startToken);
            Token idsLastToken;
            if (assignToken != null && assignToken.Index < endToken.Index)
            {
                Token assignNxToken = parser.Tokenizer.Next(assignToken);
                right = ParseExpression(errors, parser, parent, assignNxToken, lastToken);
                if (right == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid enum constant declaration. Invalid assignment.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("EnumConst", _err);
                    return null;
                }
                idsLastToken = parser.Tokenizer.Previous(assignToken);
            }
            else if (endToken.Id == TokenId.Comma)
            {
                idsLastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                idsLastToken = endToken;
            }

            Token t = null;
            List<Token> ids = ParseIdsRight(parser, startToken, idsLastToken, ref t);

            if (ids != null)
            {
                if (ids.Count == 1)
                {
                    EnumConstantNode result = new EnumConstantNode(parser, parent, startToken, endToken, ids[0], right, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                    return result;
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid enum constant declaration. Invalid identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("EnumConst", _err);
                    return null;
                }
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid enum constant declaration. Missing identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("EnumConst", _err);
                return null;
            }
        }

        static StatementNode ParseCaseStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            ExpressionNode right;
            if (startToken.Id == TokenId.Default)
            {
                right = null;
            }
            else
            {
                if (endToken.Index - startToken.Index > 1)
                {
                    Token rightFirst = parser.Tokenizer.Next(startToken);
                    Token rightLast = parser.Tokenizer.Previous(endToken);

                    right = ParseExpression(errors, parser, parent, rightFirst, rightLast);
                    if (right == null)
                    {
                        return null;
                    }
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Wrong case statement, missing identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Case", _err);
                    return null;
                }
            }

            CaseStatementNode result = new CaseStatementNode(parser, parent, startToken, endToken, right, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            return result;
        }

        static StatementNode ParseReturnStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            ExpressionNode right;
            if (endToken.Index <= startToken.Index)
            {
                throw new NotSupportedException();
            }
            else if (endToken.Index == startToken.Index + 1)
            {
                right = null;
            }
            else
            {
                Token rightFirst = parser.Tokenizer.Next(startToken);
                Token rightLast = parser.Tokenizer.Previous(endToken);

                right = ParseExpression(errors, parser, parent, rightFirst, rightLast);
                if (right == null)
                {
                    return null;
                }
            }

            ReturnStatementNode result = new ReturnStatementNode(parser, parent, startToken, endToken, includesSemicolon, right, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            return result;
        }

        static StatementNode ParseThrowStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            ExpressionNode right;
            if (endToken.Index <= startToken.Index)
            {
                throw new NotSupportedException();
            }
            else if (endToken.Index == startToken.Index + 1)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Wrong throw statement, missing expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Throw", _err);
                return null;
            }
            else
            {
                Token rightFirst = parser.Tokenizer.Next(startToken);
                Token rightLast = parser.Tokenizer.Previous(endToken);

                right = ParseExpression(errors, parser, parent, rightFirst, rightLast);
                if (right == null)
                {
                    return null;
                }
            }

            ThrowStatementNode result = new ThrowStatementNode(parser, parent, startToken, endToken, includesSemicolon, right, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
            return result;
        }


        static StatementNode ParseMemberDecl(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode, bool enableFields, bool enableMethods, bool enableConstructor, bool enableProperties)
        {
            Token firstToken, lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            bool isDescructor0 = startToken.Id == TokenId.NotBitws;
            if (isDescructor0)
            {
                firstToken = parser.Tokenizer.Next(startToken);
            }
            else
            {
                firstToken = startToken;
            }

            Token t = null;
            TypeSpecifierNode typeLeft = ParseTypeSpecifier(errors, parser, parent, firstToken, lastToken, ref t, false, true, false);

            if (typeLeft == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid member declaration. Unable to parse type.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("MemberDecl", _err);
                return null;
            }

            if (t.Index > lastToken.Index)
            {

                // property get   property set

                
                if (parent.ParseMode == BlockParseMode.Property)
                {
                    if (typeLeft is SystemTypeSpecifierNode)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid property member declaration. Invalid name.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }
                    else
                    {
                        UserTypeSpecifierNode utypeLeft = (UserTypeSpecifierNode)typeLeft;

                        CompilationErrorEnty _err;
                        bool err = false;
                        if (utypeLeft.IdentifierTokens.Count != 1)
                        {
                            errors.Add(_err=parser.CreateError("Invalid get/set declaration. Wrong identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            err = true;
                        }
                        if (utypeLeft.TypeParameters != null)
                        {
                            errors.Add(_err=parser.CreateError("Generic type arguments are not allowed in property get/set definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            err = true;
                        }
                        if (utypeLeft.CommaSeparatedRanks > 0 || utypeLeft.EmptyRanks > 0)
                        {
                            errors.Add(_err=parser.CreateError("Invalid property get/set definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            err = true;
                        }

                        switch (startToken.Id)
                        {
                            case TokenId.Get:
                                if (!err)
                                {
                                    PropertyGetDefStatementNode pgresult = new PropertyGetDefStatementNode(parser, parent, startToken, startToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    return pgresult;
                                }
                                else
                                {
                                    break;
                                }
                            case TokenId.Set:
                                if (!err)
                                {
                                    PropertySetDefStatementNode psresult = new PropertySetDefStatementNode(parser, parent, startToken, startToken, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                    return psresult;
                                }
                                else
                                {
                                    break;
                                }
                            default:
                                errors.Add(_err=parser.CreateError("Invalid property accessor, expected:get, set", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                break;
                        }
                        return null;
                    }
                }
            }

            if (parent.ParseMode == BlockParseMode.Property)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid get/set declaration.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("MemberDecl", _err);
                return null;
            }

            if (t.Id == TokenId.ParenOpen)
            {
                
                // constructor destructor

                CompilationErrorEnty _err;
                
                if (typeLeft is SystemTypeSpecifierNode)
                {
                    errors.Add(_err=parser.CreateError("Invalid member declaration. Invalid name.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                    return null;
                }
                else
                {
                    UserTypeSpecifierNode utypeLeft = (UserTypeSpecifierNode)typeLeft;
                    string name = typeLeft.Name;
                    string inf = isDescructor0 ? "destructor" : "constructor";
                    string expnm = isDescructor0 ? "~" + parent.TypeName : parent.TypeName;

                    bool err = false;

                    bool isConstructor;
                    bool isDestructor;
                    if (name.Equals(parent.TypeName))
                    {
                        isConstructor = !isDescructor0;
                        isDestructor = isDescructor0;
                    }
                    else
                    {
                        isConstructor = false;
                        isDestructor = false;
                    }

                    if (utypeLeft.IdentifierTokens.Count != 1 || (!isConstructor && !isDestructor))
                    {
                        errors.Add(_err=parser.CreateError("Invalid " + inf + " declaration. Name should match with type name : '" + expnm + "'", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        err = true;
                    }
                    if (!enableConstructor)
                    {
                        errors.Add(_err=parser.CreateError(inf + " definition is not allowed here.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        err = true;
                    }
                    if (utypeLeft.TypeParameters != null)
                    {
                        errors.Add(_err=parser.CreateError("Generic type arguments are not allowed in " + inf + " definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        err = true;
                    }
                    if (utypeLeft.CommaSeparatedRanks > 0 || utypeLeft.EmptyRanks > 0)
                    {
                        errors.Add(_err=parser.CreateError("Invalid "+inf+" definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        err = true;
                    }
                    if (parseMode == StatementParseMode.Inside)
                    {
                        errors.Add(_err=parser.CreateError("Constuctor definition must have body.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        err = true;
                    }
                    if (err)
                    {
                        return null;
                    }

                    TokenOpenClose parens = t.Block;
                    List<VarDeclArgumentNode> arguments = ParseVarDeclArgumentList(errors, parser, parent, parens);
                    if (parens.CloseToken.Index - parens.OpenToken.Index > 1 && arguments == null)
                    {
                        errors.Add(_err=parser.CreateError("Invalid " + inf + " arguments.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }
                    t = parser.Tokenizer.Next(parens.CloseToken);

                    MethodExpressionNode baseOrThisCall = null;

                    if (t.Index <= lastToken.Index)
                    {
                        switch (t.Id)
                        {
                            case TokenId.Where:
                                // TODO parse where -s
                                break;
                            case TokenId.Colon:
                                t = parser.Tokenizer.Next(t);
                                if (t != null && t.Index < lastToken.Index)
                                {
                                    switch (t.Id)
                                    {
                                        case TokenId.Base:
                                        case TokenId.This:
                                            ExpressionNode _baseOrThisCall = ParsePrimaryExpression(errors, parser, parent, t, lastToken);
                                            if (_baseOrThisCall is MethodExpressionNode)
                                            {
                                                baseOrThisCall = (MethodExpressionNode)_baseOrThisCall;
                                            }
                                            else
                                            {
                                                errors.Add(_err=parser.CreateError("Invalid constructor forward statement : " + t.ContentString + "(...)", startToken.TokenStartPos, endToken.TokenEndPos));
                                                parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                                return null;
                                            }
                                            break;
                                    }
                                    break;
                                }
                                else
                                {
                                    errors.Add(_err=parser.CreateError("Unexpexted tokens starting from ':'", startToken.TokenStartPos, endToken.TokenEndPos));
                                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                    return null;
                                }
                            default:
                                errors.Add(_err=parser.CreateError("Unexpexted tokens.", t.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                errors.Add(_err=parser.CreateError("Invalid member declaration.", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                return null;
                        }
                    }



                    if (!isConstructor && baseOrThisCall != null)
                    {
                        errors.Add(_err=parser.CreateError("Invalid constructor forward statement. Method is not a constructor.  " + baseOrThisCall.StartToken.ContentString + "(...)", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }

                    if (isConstructor)
                    {
                        ConstructorDefStatementNode cresult = new ConstructorDefStatementNode(parser, parent, startToken, endToken, utypeLeft.IdentifierTokens[0], parens, arguments, baseOrThisCall, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                        return cresult;
                    }
                    else
                    {
                        DestructorDefStatementNode dresult = new DestructorDefStatementNode(parser, parent, startToken, utypeLeft.IdentifierTokens[0], parens, arguments, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                        return dresult;
                    }
                }
            }
            else if (t.Id == TokenId.This)
            {
                
                // indexer


                CompilationErrorEnty _err;

                if (t.Index >= endToken.Index - 3)
                {
                    errors.Add(_err=parser.CreateError("Unexpected tokens. Invalid indexer.", t.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                    errors.Add(_err = parser.CreateError("Invalid indexer.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                    return null;
                }

                Token thisToken = t;
                Token nextToken = parser.Tokenizer.Next(t);
                if (nextToken.Id == TokenId.ArrayOrAttributeOpen)
                {
                    TokenOpenClose brackets = nextToken.Block;
                    List<VarDeclArgumentNode> arguments = ParseVarDeclArgumentList(errors, parser, parent, brackets);
                    if (brackets.CloseToken.Index - brackets.OpenToken.Index > 1 && arguments == null)
                    {
                        errors.Add(_err=parser.CreateError("Invalid indexer arguments.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }
                    t = parser.Tokenizer.Next(brackets.CloseToken);

                    if (t.Index > lastToken.Index)
                    {
                        IndexerDefStatementNode iresult = new IndexerDefStatementNode(parser, parent, startToken, endToken, typeLeft, thisToken, brackets, arguments, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                        return iresult;
                    }
                    else
                    {
                        errors.Add(_err=parser.CreateError("Unexpected tokens. Invalid indexer.", t.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        errors.Add(_err = parser.CreateError("Invalid indexer.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }
                }
                else
                {
                    errors.Add(_err=parser.CreateError("Unexpected tokens. Invalid indexer.", nextToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                    errors.Add(_err = parser.CreateError("Invalid indexer.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                    return null;
                }

            }
            else
            {
                
                // method  property   field


                CompilationErrorEnty _err;

                Token memberNameFirst = t;
                t = null;
                TypeSpecifierNode parsedMemberName = ParseTypeSpecifier(errors, parser, parent, memberNameFirst, lastToken, ref t, false, true, false);

                if (parsedMemberName == null)
                {
                    errors.Add(_err=parser.CreateError("Invalid member declaration. Identifier expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                    return null;
                }

                if (t.Index < lastToken.Index && t.Id == TokenId.ParenOpen)
                {
                    // method

                    if (parsedMemberName is SystemTypeSpecifierNode)
                    {
                        errors.Add(_err=parser.CreateError("Invalid method declaration. Invalid name.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }
                    else
                    {
                        UserTypeSpecifierNode uparsedMemberName = (UserTypeSpecifierNode)parsedMemberName;

                        if (uparsedMemberName.CommaSeparatedRanks > 0 || uparsedMemberName.EmptyRanks > 0)
                        {
                            errors.Add(_err=parser.CreateError("Invalid method definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            return null;
                        }

                        TokenOpenClose parens = t.Block;
                        List<VarDeclArgumentNode> arguments = ParseVarDeclArgumentList(errors, parser, parent, parens);
                        if (parens.CloseToken.Index - parens.OpenToken.Index > 1 && arguments == null)
                        {
                            errors.Add(_err=parser.CreateError("Invalid method arguments.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            return null;
                        }
                        t = parser.Tokenizer.Next(parens.CloseToken);


                        if (t.Index <= lastToken.Index)
                        {
                            switch (t.Id)
                            {
                                case TokenId.Where:
                                    // TODO parse where -s
                                    break;
                                default:
                                    errors.Add(_err=parser.CreateError("Unexpexted tokens.", t.TokenStartPos, endToken.TokenEndPos));
                                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                    errors.Add(_err=parser.CreateError("Invalid method declaration.", startToken.TokenStartPos, endToken.TokenEndPos));
                                    parser.RaiseTmpErrorAdded("MemberDecl", _err);
                                    return null;
                            }
                        }


                        if (enableMethods)
                        {
                            MethodDefStatementNode mresult = new MethodDefStatementNode(parser, parent, startToken, endToken, typeLeft, uparsedMemberName.identifierTokens, parens, arguments, uparsedMemberName.typeParameters, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                            return mresult;
                        }
                        else
                        {
                            errors.Add(_err=parser.CreateError("Method definition is not allowed here.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            return null;
                        }
                    }
                }
                else
                {
                    // property   field

                    if (parsedMemberName is SystemTypeSpecifierNode)
                    {
                        errors.Add(_err=parser.CreateError("Invalid method declaration. Invalid name.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("MemberDecl", _err);
                        return null;
                    }
                    else
                    {
                        UserTypeSpecifierNode uparsedMemberName = (UserTypeSpecifierNode)parsedMemberName;

                        string inf = parseMode == StatementParseMode.Header ? "property" : "field";

                        bool err = false;
                        if (uparsedMemberName.IdentifierTokens.Count != 1)
                        {
                            errors.Add(_err=parser.CreateError("Invalid "+inf+" declaration. Wrong identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            err = true;
                        }
                        if (uparsedMemberName.TypeParameters != null)
                        {
                            errors.Add(_err=parser.CreateError("Generic type arguments are not allowed in "+inf+" definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            err = true;
                        }
                        if (uparsedMemberName.CommaSeparatedRanks > 0 || uparsedMemberName.EmptyRanks > 0)
                        {
                            errors.Add(_err=parser.CreateError("Invalid "+inf+" definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("MemberDecl", _err);
                            err = true;
                        }
                        if (err)
                        {
                            return null;
                        }

                        List<ExpressionNode> expressions;

                        TokenOpenClose toc = parent.BlockTokens.SubToc(parsedMemberName.StartToken, lastToken);
                        expressions = ParseExpressionList(errors, parser, parent, toc, true);

                        VarDeclStatementNode varDecl = ParseVarDeclStatementNode(errors, parser, parent, typeLeft.StartToken, lastToken, typeLeft, parsedMemberName.StartToken, parseMode == StatementParseMode.Inside, true, parseMode == StatementParseMode.Inside);
                        if (varDecl == null)
                        {
                            return null;
                        }

                        switch (parseMode)
                        {
                            case StatementParseMode.Header:
                                PropertyDefStatementNode presult = new PropertyDefStatementNode(parser, parent, startToken, endToken, varDecl, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                return presult;
                                
                            case StatementParseMode.Inside:
                                FieldDeclStatementNode fresult = new FieldDeclStatementNode(parser, parent, startToken, endToken, varDecl, includesSemicolon, attributes, modifiers, subStatementsBlock, tailStatement, parseMode);
                                return fresult;

                            default:
                                throw new NotSupportedException();
                        }
                    }
                }
            }
        }

        static StatementNode ParseMethodStatement(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool includesSemicolon, List<AttributeNode> attributes, List<ModifierNode> modifiers, BlockNode subStatementsBlock, StatementNode tailStatement, StatementParseMode parseMode)
        {
            StatementNode result;
            Token lastToken;
            if (includesSemicolon)
            {
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            else
            {
                lastToken = endToken;
            }

            result = ParseVarDeclStatementNode(errors, parser, parent, startToken, lastToken, true, false, true, true);
            if (result != null)
            {
                return result;
            }

            if (modifiers == null || modifiers.Count == 0)
            {
                ExpressionNode expression = ParseExpression(errors, parser, parent, startToken, lastToken);
                result = expression;
                if (result != null && !expression.ValidStatement)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Expression is an invalid statement.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("VarDecl", _err);
                }
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid declaration statement.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("VarDecl", _err);
            }

            return result;
        }

        static VarDeclStatementNode ParseVarDeclStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool enableMultiple, bool enablePureType, bool enableNoAssign, bool enableWithAssign)
        {

            Token t = null;
            TypeSpecifierNode typeLeft = ParseTypeSpecifier(errors, parser, parent, startToken, endToken, ref t, false, true, false);

            if (typeLeft == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid variable declaration. Unable to parse type.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("VarDecl", _err);
                return null;
            }

            VarDeclStatementNode result;

            if (t == null || t.Index > endToken.Index)
            {
                if (enablePureType)
                {
                    result = new VarDeclStatementNode(parser, parent, startToken, endToken, typeLeft, null, false, null, null, null, null, StatementParseMode.Inside);
                    return result;
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid variable declaration. Identifier expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("VarDecl", _err);
                    return null;
                }
            }

            result = ParseVarDeclStatementNode(errors, parser, parent, startToken, endToken, typeLeft, t, enableMultiple, enableNoAssign, enableWithAssign);
            return result;
        }

        static VarDeclStatementNode ParseVarDeclStatementNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, TypeSpecifierNode typeLeft, Token nextToken, bool enableMultiple, bool enableNoAssign, bool enableWithAssign)
        {
            List<ExpressionNode> expressions;

            Token firstToken = parser.Tokenizer.Previous(nextToken);
            TokenOpenClose toc = firstToken.Block.SubToc(firstToken, endToken);
            expressions = ParseExpressionList(errors, parser, parent, toc, true);

            if (expressions == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid variable declaration. Identifier expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("VarDecl", _err);
                return null;
            }

            if (!enableMultiple && expressions.Count > 1)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid variable declaration. Multiple declaration is not allowed here.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("VarDecl", _err);
                return null;
            }

            bool err = false;
            foreach (ExpressionNode e in expressions)
            {
                if (e is BinaryOperationNode)
                {
                    BinaryOperationNode b = (BinaryOperationNode)e;
                    if (b.OperatorToken.Id != TokenId.Assign)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid statement. Assignment expected.", e.StartPos, e.EndPos));
                        parser.RaiseTmpErrorAdded("VarDecl", _err);
                        err = true;
                    }
                    else if (!enableWithAssign)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Assignment is not allowed here.", e.StartPos, e.EndPos));
                        parser.RaiseTmpErrorAdded("VarDecl", _err);
                        err = true;
                    }
                }
                else if (e is AtomicExpressionNode)
                {
                    AtomicExpressionNode a = (AtomicExpressionNode)e;
                    if (!a.FieldIdentifier.Id.IsIdentifier())
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err = parser.CreateError("Invalid statement. Identifier expected.", e.StartPos, e.EndPos));
                        parser.RaiseTmpErrorAdded("VarDecl", _err);
                        err = true;
                    }
                    else if (!enableNoAssign)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Assignment expected.", e.StartPos, e.EndPos));
                        parser.RaiseTmpErrorAdded("VarDecl", _err);
                        err = true;
                    }
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid statement. Declaration expected.", e.StartPos, e.EndPos));
                    parser.RaiseTmpErrorAdded("VarDecl", _err);
                    err = true;
                }
            }
            
            if (err)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid statement.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("VarDecl", _err);
                return null;
            }

            VarDeclStatementNode result = new VarDeclStatementNode(parser, parent, startToken, endToken, typeLeft, expressions, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static ArgumentNode ParseArgument(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {
            if (startToken.Index > endToken.Index)
            {
                throw new NotSupportedException();
            }
            ArgumentNode result = null;

            ArgumentType argType;
            Token refToken;
            Token xStartToken;
            switch (startToken.Id)
            {
                case TokenId.Ref:
                    argType = ArgumentType.Ref;
                    refToken = startToken;
                    xStartToken = parser.Tokenizer.Next(startToken);
                    break;
                case TokenId.Out:
                    argType = ArgumentType.Out;
                    refToken = startToken;
                    xStartToken = parser.Tokenizer.Next(startToken);
                    break;
                default:
                    argType = ArgumentType.Value;
                    refToken = null;
                    xStartToken = startToken;
                    break;
            }

            if (xStartToken.Index <= endToken.Index)
            {
                ExpressionNode xNode = ParseExpression(errors, parser, parent, xStartToken, endToken);
                if (xNode != null)
                {
                    result = new ArgumentNode(parser, parent, argType, refToken, xNode);
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid argument.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Arg", _err);
                }
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid argument.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Arg", _err);
            }
            return result;
        }

        static VarDeclArgumentNode ParseVarDeclArgument(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {
            VarDeclArgumentNode result = null;

            ArgumentType argType;
            Token refToken;
            Token xStartToken;
            switch (startToken.Id)
            {
                case TokenId.Ref:
                    argType = ArgumentType.Ref;
                    refToken = startToken;
                    xStartToken = parser.Tokenizer.Next(startToken);
                    break;
                case TokenId.Out:
                    argType = ArgumentType.Out;
                    refToken = startToken;
                    xStartToken = parser.Tokenizer.Next(startToken);
                    break;
                case TokenId.Params:
                    argType = ArgumentType.Params;
                    refToken = startToken;
                    xStartToken = parser.Tokenizer.Next(startToken);
                    break;
                case TokenId.This:
                    argType = ArgumentType.This;
                    refToken = startToken;
                    xStartToken = parser.Tokenizer.Next(startToken);
                    break;
                default:
                    argType = ArgumentType.Value;
                    refToken = null;
                    xStartToken = startToken;
                    break;
            }

            if (xStartToken.Index < endToken.Index)
            {
                VarDeclStatementNode xNode = ParseVarDeclStatementNode(errors, parser, parent, xStartToken, endToken, false, false, true, true);
                if (xNode != null)
                {
                    result = new VarDeclArgumentNode(parser, parent, argType, refToken, xNode);
                }
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid argument definition.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("VarDeclArg", _err);
            }
            return result;
        }


        static ExpressionNode ParseExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {
            ExpressionNode result;

            TokenId[] binexp1 = { TokenId.OrAssign, TokenId.XorAssign, TokenId.AndAssign, 
                                  TokenId.RsAssign, TokenId.LsAssign, 
                                  TokenId.MinusAssign, TokenId.PlusAssign,
                                  TokenId.ModAssign, TokenId.PerAssign , TokenId.MulAssign,
                                  TokenId.Assign};
            foreach (TokenId be1 in binexp1)
            {
                result = ParseBinaryExpression(errors, parser, parent, be1, startToken, endToken, false);
                if (result != null)
                {
                    return result;
                }
            }

            result = ParseTernaryExpression(errors, parser, parent, TokenId.Qmark, TokenId.Colon, startToken, endToken, false);
            if (result != null)
            {
                return result;
            }

            TokenId[] binexp2 = { TokenId.OrOr, TokenId.AndAnd, TokenId.Or, TokenId.Xor, TokenId.And, TokenId.NonEqual, TokenId.Equal, TokenId.As, TokenId.Is, TokenId.Ge, TokenId.Le, TokenId.Gt, TokenId.Lt };
            foreach (TokenId be2 in binexp2)
            {
                result = ParseBinaryExpression(errors, parser, parent, be2, startToken, endToken, true);
                if (result != null)
                {
                    return result;
                }
            }

            /*,TokenId.Rs,TokenId.Ls*/
            result = ParseBinaryShiftExpression(errors, parser, parent, startToken, endToken, false);
            if (result != null)
            {
                return result;
            }
            result = ParseBinaryShiftExpression(errors, parser, parent, startToken, endToken, true);
            if (result != null)
            {
                return result;
            }

            TokenId[] binexp3 = { TokenId.Minus, TokenId.Plus, TokenId.Modulo, TokenId.Slash, TokenId.Star };
            foreach (TokenId be3 in binexp3)
            {
                result = ParseBinaryExpression(errors, parser, parent, be3, startToken, endToken, true);
                if (result != null)
                {
                    return result;
                }
            }

            TokenId[] unexpRight = { TokenId.MinusMinus, TokenId.PlusPlus };
            foreach (TokenId ue in unexpRight)
            {
                result = ParseUnaryExpression(errors, parser, parent, ue, startToken, endToken, false);
                if (result != null)
                {
                    return result;
                }
            }

            TokenId[] unexpLeft = { TokenId.NotBitws, TokenId.Not, TokenId.Minus, TokenId.Plus, TokenId.Star, TokenId.MinusMinus, TokenId.PlusPlus };
            foreach (TokenId ue in unexpLeft)
            {
                result = ParseUnaryExpression(errors, parser, parent, ue, startToken, endToken, true);
                if (result != null)
                {
                    return result;
                }
            }


            result = ParseCastOrParenExpression(errors, parser, parent, startToken, endToken);
            if (result != null)
            {
                return result;
            }

            result = ParseObjectOrArrayCreationExpression(errors, parser, parent, startToken, endToken);
            if (result != null)
            {
                return result;
            }

            result = ParseLinqExpression(errors, parser, parent, startToken, endToken);
            if (result != null)
            {
                return result;
            }

            result = ParsePrimaryExpression(errors, parser, parent, startToken, endToken);
            if (result != null)
            {
                return result;
            }

            return null;
        }

        static UnaryOperationNode ParseUnaryExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenId operatorTokenId, Token startToken, Token endToken, bool leftSideOperator)
        {
            UnaryOperationNode result;
            Token operatorToken;
            Token firstToken, lastToken;
            if (leftSideOperator)
            {
                operatorToken = startToken;
                firstToken = parser.Tokenizer.Next(startToken);
                lastToken = endToken;
            }
            else
            {
                operatorToken = endToken;
                firstToken = startToken;
                lastToken = parser.Tokenizer.Previous(endToken);
            }
            if (operatorToken.Id == operatorTokenId && endToken.Index - startToken.Index > 0)
            {
                ExpressionNode right = ParseExpression(errors, parser, parent, firstToken, lastToken);
                if (right != null)
                {
                    result = new UnaryOperationNode(parser, parent, operatorToken, startToken, endToken, right);
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid unary expression (" + operatorToken.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Unary", _err);
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        static BinaryOperationNode ParseBinaryExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenId operatorTokenId, Token startToken, Token endToken, bool leftAssoc)
        {
            BinaryOperationNode result;
            TokenOpenClose toc;
            Token opStartToken;
            switch (startToken.Id)
            {
                case TokenId.ParenOpen:
                case TokenId.BlockOpen:
                case TokenId.ArrayOrAttributeOpen:
                    toc = startToken.Block.Parent;
                    opStartToken = parser.Tokenizer.Previous(startToken);
                    break;
                default:
                    toc = startToken.Block;
                    opStartToken = startToken;
                    break;
            }
            IList<Token> tokens = toc.OperatorTokens(operatorTokenId);
            int i;
            if (leftAssoc)
            {
                i = opStartToken.OperatorTokenIndex(operatorTokenId);
                if (opStartToken.Id == operatorTokenId)
                {
                    i++;
                }
            }
            else
            {
                i = endToken.OperatorTokenIndex(operatorTokenId) - 1;
            }
            if (i >= 0 && i < tokens.Count)
            {
                Token operatorToken = tokens[i];
                if (startToken.Index < operatorToken.Index && operatorToken.Index < endToken.Index)
                {
                    ExpressionNode left = ParseExpression(errors, parser, parent, startToken, parser.Tokenizer.Previous(operatorToken));
                    if (left == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid binary expression (" + operatorToken.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Binary", _err);
                        return null;
                    }

                    ExpressionNode right = ParseExpression(errors, parser, parent, parser.Tokenizer.Next(operatorToken), endToken);
                    if (right == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid binary expression (" + operatorToken.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Binary", _err);
                        return null;
                    }

                    result = new BinaryOperationNode(parser, parent, tokens[i], left, right);
                }
                else
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        static BinaryShiftOperationNode ParseBinaryShiftExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, bool leftShift)
        {
            TokenId operatorTokenId = leftShift ? TokenId.Lt : TokenId.Gt;
            TokenOpenClose toc;
            Token opStartToken;
            switch (startToken.Id)
            {
                case TokenId.ParenOpen:
                case TokenId.BlockOpen:
                case TokenId.ArrayOrAttributeOpen:
                    toc = startToken.Block.Parent;
                    opStartToken = parser.Tokenizer.Previous(startToken);
                    break;
                default:
                    toc = startToken.Block;
                    opStartToken = startToken;
                    break;
            }
            IList<Token> tokens = toc.OperatorTokens(operatorTokenId);

            // NOTE always left assoc
            int i = opStartToken.OperatorTokenIndex(operatorTokenId);
            if (opStartToken.Id == operatorTokenId)
            {
                i++;
            }

            while (i >= 0 && i < tokens.Count - 1)
            {
                Token operatorToken1 = tokens[i];
                Token operatorToken2 = tokens[i + 1];
                if (startToken.Index < operatorToken1.Index && operatorToken1.TotalIndex + 1 == operatorToken2.TotalIndex && operatorToken2.Index < endToken.Index)
                {
                    ExpressionNode left = ParseExpression(errors, parser, parent, startToken, parser.Tokenizer.Previous(operatorToken1));
                    if (left == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid binary shift expression (" + operatorToken1.ContentString + operatorToken2.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("BinarySh", _err);
                        return null;
                    }

                    ExpressionNode right = ParseExpression(errors, parser, parent, parser.Tokenizer.Next(operatorToken2), endToken);
                    if (right == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid binary shift expression (" + operatorToken1.ContentString + operatorToken2.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("BinarySh", _err);
                        return null;
                    }

                    BinaryShiftOperationNode result = new BinaryShiftOperationNode(parser, parent, operatorToken1, operatorToken2, left, right);
                    return result;
                }
                else
                {
                    i++;
                    continue;
                }
            }
            return null;
        }

        static TernaryOperationNode ParseTernaryExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenId operatorTokenId1, TokenId operatorTokenId2, Token startToken, Token endToken, bool leftAssoc)
        {
            TernaryOperationNode result;
            TokenOpenClose toc;
            Token opStartToken;
            switch (startToken.Id)
            {
                case TokenId.ParenOpen:
                case TokenId.BlockOpen:
                case TokenId.ArrayOrAttributeOpen:
                    toc = startToken.Block.Parent;
                    opStartToken = parser.Tokenizer.Previous(startToken);
                    break;
                default:
                    toc = startToken.Block;
                    opStartToken = startToken;
                    break;
            }
            IList<Token> tokens1 = toc.OperatorTokens(operatorTokenId1);
            IList<Token> tokens2 = toc.OperatorTokens(operatorTokenId2);
            int i1, i2;
            if (leftAssoc)
            {
                i1 = opStartToken.OperatorTokenIndex(operatorTokenId1);
                if (opStartToken.Id == operatorTokenId1)
                {
                    i1++;
                }
                i2 = opStartToken.OperatorTokenIndex(operatorTokenId2);
                if (opStartToken.Id == operatorTokenId2)
                {
                    i2++;
                }
            }
            else
            {
                i1 = endToken.OperatorTokenIndex(operatorTokenId1) - 1;
                i2 = endToken.OperatorTokenIndex(operatorTokenId2) - 1;
            }
            if (i1 >= 0 && i1 < tokens1.Count && i2 >= 0 && i2 < tokens2.Count)
            {
                Token operatorToken1 = tokens1[i1];
                Token operatorToken2 = tokens2[i2];
                if (startToken.Index < operatorToken1.Index &&
                    operatorToken1.Index < operatorToken2.Index &&
                    operatorToken2.Index < endToken.Index)
                {
                    Token token1 = parser.Tokenizer.Previous(operatorToken1);
                    Token token2 = parser.Tokenizer.Next(operatorToken1);
                    Token token3 = parser.Tokenizer.Previous(operatorToken2);
                    Token token4 = parser.Tokenizer.Next(operatorToken2);

                    ExpressionNode left = ParseExpression(errors, parser, parent, startToken, token1);
                    if (left == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid ternary expression (" + operatorToken1.ContentString + " " + operatorToken2.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Ternary", _err);
                        return null;
                    }

                    ExpressionNode middle = ParseExpression(errors, parser, parent, token2, token3);
                    if (middle == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid ternary expression (" + operatorToken1.ContentString + " " + operatorToken2.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Ternary", _err);
                        return null;
                    }
                    
                    ExpressionNode right = ParseExpression(errors, parser, parent, token4, endToken);
                    if (right == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid ternary expression (" + operatorToken1.ContentString + " " + operatorToken2.ContentString + ").", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Ternary", _err);
                        return null;
                    }

                    result = new TernaryOperationNode(parser, parent, tokens1[i1], tokens2[i2], left, middle, right);
                }
                else
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        static ExpressionNode ParseCastOrParenExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {
            ExpressionNode result = null;

            if (startToken.Id == TokenId.ParenOpen)
            {
                TokenOpenClose parens = parser.CsTokenizer.Paren(startToken);
                TypeSpecifierNode typeSpecifier;
                ExpressionNode right;

                CompilationErrorEnty _err;
                switch (Math.Sign(parens.CloseToken.Index - endToken.Index))
                {
                    case -1:
                        typeSpecifier = ParseTypeSpecifier(errors, parser, parent, parens);
                        if (typeSpecifier == null)
                        {
                            errors.Add(_err=parser.CreateError("Invalid cast expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Cast", _err);
                            return null;
                        }
                        
                        right = ParseExpression(errors, parser, parent, parser.Tokenizer.Next(parens.CloseToken), endToken);
                        if (right == null)
                        {
                            errors.Add(_err=parser.CreateError("Invalid cast expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Cast", _err);
                            return null;
                        }

                        result = new CastExpressionNode(parser, parent, parens, typeSpecifier, right);
                        break;
                    case 0:
                        if (parens.CloseToken.Index > parens.OpenToken.Index + 1)
                        {
                            result = ParseParenExpression(errors, parser, parent, parens);
                        }
                        else
                        {
                            errors.Add(_err=parser.CreateError("Empty parenthesis expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Cast", _err);
                        }
                        break;
                    default:
                        errors.Add(_err=parser.CreateError("Unclosed parentheses.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Cast", _err);
                        break;
                }
            }
            return result;
        }

        static ExpressionNode ParseObjectOrArrayCreationExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {

            if (startToken.Id == TokenId.BlockOpen)
            {
                TokenOpenClose arrBlock = startToken.Block;
                if (arrBlock.CloseToken == endToken)
                {
                    BlockNode arrayInitializerBlock = new BlockNode(parser, parent, arrBlock, true, null, 0, BlockParseMode.ArrayInitializers);
                    ArrayCreationNode aresult = new ArrayCreationNode(parser, parent, null, startToken, endToken, arrayInitializerBlock);
                    return aresult;
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid array block initializer.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("New", _err);
                    return null;
                }
            }
            else if (startToken.Id == TokenId.New)
            {
                Token nextToken = parser.Tokenizer.Next(startToken);
                Token t = null;
                TypeSpecifierNode typeLeft = ParseTypeSpecifier(errors, parser, parent, nextToken, endToken, ref t, true, true, false);
                BlockNode arrayInitializerBlock = null;
                List<ArgumentNode> newObjArgs = null;
                bool isArray = false;
                bool isObject = false;
                TokenOpenClose parens = null;

                if (typeLeft == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid creation expression. Unable to parse type.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("New", _err);
                    return null;
                }

                if (t.Index <= endToken.Index)
                {
                    if (t.Id == TokenId.BlockOpen)
                    {
                        isArray = true;

                        TokenOpenClose arrBlock = t.Block;
                        if (arrBlock.CloseToken == null || arrBlock.CloseToken.Index > endToken.Index)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid array creation. Missing closing '}'", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            return null;
                        }
                        if (arrBlock.CloseToken.Index < endToken.Index)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid array creation. Unexpected tokens.", arrBlock.CloseToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            errors.Add(_err = parser.CreateError("Invalid array creation expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            return null;
                        }
                        arrayInitializerBlock = new BlockNode(parser, parent, arrBlock, true, null, 0, BlockParseMode.ArrayInitializers);
                    }
                    else if (t.Id == TokenId.ParenOpen)
                    {
                        isObject = true;

                        if (typeLeft.firstBracketExpressions != null)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid object creation expression. First rank expressions are not allowed, except empty ones like this : [,,]", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            return null;
                        }
                        parens = t.Block;
                        if (parens.CloseToken == null || parens.CloseToken.Index > endToken.Index)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid object creation. Missing closing ')'", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            return null;
                        }
                        if (parens.CloseToken.Index < endToken.Index)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid object creation. Unexpected tokens.", parens.CloseToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            errors.Add(_err = parser.CreateError("Invalid object creation expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            return null;
                        }
                        newObjArgs = ParseArgumentList(errors, parser, parent, parens);
                        if (parens.CloseToken.Index - parens.OpenToken.Index > 1 && newObjArgs == null)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid object creation expression. Arguments expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("New", _err);
                            return null;
                        }
                    }
                    else
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid array or object creation. Unexpected tokens.", t.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("New", _err);
                        errors.Add(_err = parser.CreateError("Invalid array or object creation expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("New", _err);
                        return null;
                    }
                }

                if (typeLeft.firstBracketExpressions != null)
                {
                    isArray = true;
                }
                else if (isArray)
                {
                    if (arrayInitializerBlock == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err = parser.CreateError("Invalid array creation expression. Missing dimension or array initializer block.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("New", _err);
                        return null;
                    }
                }
                else if (isObject)
                {
                }
                else
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid object creation expression. Arguments expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("New", _err);
                    return null;
                }

                if (isArray)
                {
                    ArrayCreationNode aresult = new ArrayCreationNode(parser, parent, typeLeft, startToken, endToken, arrayInitializerBlock);
                    return aresult;
                }
                else
                {
                    ObjectCreationNode oresult = new ObjectCreationNode(parser, parent, typeLeft, startToken, parens, newObjArgs);
                    return oresult;
                }
            }
            else
            {
                return null;
            }
        }

        static LinqExpressionNode ParseLinqExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {
            if (startToken.Id == TokenId.From)
            {
                Token nextToken = startToken;
                Token prevToken = null;

                LinqFromExpressionNode firstFrom = ParseLinqFromClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                if (firstFrom == null)
                {
                    return null;
                }

                LinqExpressionNode result = ParseLinqExpressionBody(errors, parser, parent, startToken, endToken, firstFrom, ref nextToken, ref prevToken);
                if (result == null)
                {
                    return null;
                }
                if (prevToken.Index < endToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Unexpected tokens.", nextToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    errors.Add(_err = parser.CreateError("Invalid linq expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
                if (prevToken != endToken)
                {
                    throw new NotSupportedException();
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        static LinqExpressionNode ParseLinqExpressionBody(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, LinqFromExpressionNode firstFrom, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;
            if (firstFrom != null)
            {
                childStartToken = firstFrom.StartToken;
            }

            List<LinqHeadExpressionNode> fromLetWheres = new List<LinqHeadExpressionNode>();
            if (firstFrom != null)
            {
                fromLetWheres.Add(firstFrom);
            }

            while (true)
            {
                LinqFromExpressionNode from;
                LinqLetExpressionNode let;
                LinqWhereExpressionNode where;
                switch (nextToken.Id)
                {
                    case TokenId.From:
                        from = ParseLinqFromClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                        if (from == null)
                        {
                            return null;
                        }
                        fromLetWheres.Add(from);
                        break;
                    case TokenId.Let:
                        let = ParseLinqLetClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                        if (let == null)
                        {
                            return null;
                        }
                        fromLetWheres.Add(let);
                        break;
                    case TokenId.Where:
                        where = ParseLinqWhereClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                        if (where == null)
                        {
                            return null;
                        }
                        fromLetWheres.Add(where);
                        break;
                    default:
                        goto break_wh;
                }
                if (nextToken == null)
                {
                    throw new NotSupportedException();
                }
            }
        break_wh:

            LinqOrderByExpressionNode orderBy = null;
            switch (nextToken.Id)
            {
                case TokenId.Order:
                    orderBy = ParseLinqOrderByClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                    if (orderBy == null)
                    {
                        return null;
                    }
                    break;
            }

            LinqFootExpressionNode selectGroup = null;
            switch (nextToken.Id)
            {
                case TokenId.Group:
                    selectGroup = ParseLinqGroupByClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                    if (selectGroup == null)
                    {
                        return null;
                    }
                    break;
                case TokenId.Select:
                    selectGroup = ParseLinqSelectClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                    if (selectGroup == null)
                    {
                        return null;
                    }
                    break;
                default:
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
            }

            LinqContinuationExpressionNode continuation = null;
            if (nextToken != null && nextToken.Index <= endToken.Index)
            {
                switch (nextToken.Id)
                {
                    case TokenId.Into:
                        continuation = ParseLinqContinuationExpression(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                        if (continuation == null)
                        {
                            return null;
                        }
                        break;
                }
            }

            LinqExpressionNode result = new LinqExpressionNode(parser, parent, childStartToken, prevToken, fromLetWheres, orderBy, selectGroup, continuation, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqContinuationExpressionNode ParseLinqContinuationExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            nextToken = parser.Tokenizer.Next(nextToken);

            if (nextToken == null || nextToken.Index > endToken.Index || !nextToken.Id.IsIdentifier())
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Identifier expected after 'into'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            Token id = nextToken;
            nextToken = parser.Tokenizer.Next(nextToken);

            if (nextToken == null || nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by' after 'into'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }


            List<LinqJoinExpressionNode> joins = null;
            while (nextToken != null && nextToken.Id == TokenId.Join)
            {
                LinqJoinExpressionNode join = ParseLinqJoinClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                if (join == null)
                {
                    return null;
                }
                if (nextToken == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by' after 'into'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }

                joins.Add(join);
            }

            AtomicExpressionNode idNode = new AtomicExpressionNode(parser, parent, id, id, null, id);
            LinqExpressionNode body = ParseLinqExpressionBody(errors, parser, parent, startToken, endToken, null, ref nextToken, ref prevToken);
            if (body == null)
            {
                return null;
            }
            LinqContinuationExpressionNode result = new LinqContinuationExpressionNode(parser, parent, childStartToken, prevToken, idNode, joins, body, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqFromExpressionNode ParseLinqFromClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            nextToken = parser.Tokenizer.Next(nextToken);
            TypeSpecifierNode typeLeft = ParseTypeSpecifier(errors, parser, parent, nextToken, endToken, ref nextToken, false, true, false);

            if (typeLeft == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Identifier or type expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            if (nextToken == null || nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            Token id;
            if (nextToken.Id == TokenId.In)
            {
                if (typeLeft is SystemTypeSpecifierNode || typeLeft.IsArray || typeLeft.IsGeneric || typeLeft.IsPointer)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid identifier.", typeLeft.StartPos, typeLeft.EndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    errors.Add(_err = parser.CreateError("Invalid linq expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
                else
                {
                    UserTypeSpecifierNode utype = (UserTypeSpecifierNode)typeLeft;
                    if (utype.identifierTokens.Count!=1) {
                        CompilationErrorEnty _err;
                        errors.Add(_err = parser.CreateError("Invalid identifier.", typeLeft.StartPos, typeLeft.EndPos));
                        parser.RaiseTmpErrorAdded("Linq", _err);
                    }
                    id = utype.identifierTokens[0];
                    typeLeft = null;
                }
            }
            else
            {
                id = nextToken;
                nextToken = parser.Tokenizer.Next(nextToken);

                if (nextToken == null || nextToken.Index > endToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
            }

            ExpressionNode inExpression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.In, "in", ref nextToken, ref prevToken);
            if (inExpression == null)
            {
                return null;
            }

            List<LinqJoinExpressionNode> joins = null;
            while (nextToken != null && nextToken.Id == TokenId.Join)
            {
                LinqJoinExpressionNode join = ParseLinqJoinClause(errors, parser, parent, startToken, endToken, ref nextToken, ref prevToken);
                if (join == null)
                {
                    return null;
                }
                if (nextToken == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }

                joins.Add(join);
            }

            AtomicExpressionNode idNode = new AtomicExpressionNode(parser, parent, id, id, null, id);
            LinqFromExpressionNode result = new LinqFromExpressionNode(parser, parent, childStartToken, prevToken, typeLeft, idNode, inExpression, joins, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqJoinExpressionNode ParseLinqJoinClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            nextToken = parser.Tokenizer.Next(nextToken);
            TypeSpecifierNode typeLeft = ParseTypeSpecifier(errors, parser, parent, nextToken, endToken, ref nextToken, false, true, false);

            if (typeLeft == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Identifier or type expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            if (nextToken == null || nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            Token id;
            if (nextToken.Id == TokenId.In)
            {
                if (typeLeft is SystemTypeSpecifierNode || typeLeft.IsArray || typeLeft.IsGeneric || typeLeft.IsPointer)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid identifier.", typeLeft.StartPos, typeLeft.EndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    errors.Add(_err = parser.CreateError("Invalid linq expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
                else
                {
                    UserTypeSpecifierNode utype = (UserTypeSpecifierNode)typeLeft;
                    if (utype.identifierTokens.Count != 1)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err = parser.CreateError("Invalid identifier.", typeLeft.StartPos, typeLeft.EndPos));
                        parser.RaiseTmpErrorAdded("Linq", _err);
                    }
                    id = utype.identifierTokens[0];
                    typeLeft = null;
                }
            }
            else
            {
                id = nextToken;
                nextToken = parser.Tokenizer.Next(nextToken);

                if (nextToken == null || nextToken.Index > endToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
            }


            ExpressionNode inExpression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.In, "in", ref nextToken, ref prevToken);
            if (inExpression == null)
            {
                return null;
            }

            ExpressionNode onExpression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.On, "on", ref nextToken, ref prevToken);
            if (onExpression == null)
            {
                return null;
            }

            ExpressionNode equalsExpression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.EqualsLinq, "equals", ref nextToken, ref prevToken);
            if (equalsExpression == null)
            {
                return null;
            }

            AtomicExpressionNode intoIdNode = null;
            if (nextToken.Id == TokenId.Into)
            {
                nextToken = parser.Tokenizer.Next(nextToken);

                if (nextToken == null || nextToken.Index > endToken.Index || !nextToken.Id.IsIdentifier())
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Identifier expected after 'on ... equals ... into'.", startToken.TokenStartPos, prevToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }

                intoIdNode = new AtomicExpressionNode(parser, parent, nextToken, nextToken, null, nextToken);
                nextToken = parser.Tokenizer.Next(nextToken);
                if (nextToken == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
            }


            AtomicExpressionNode idNode = new AtomicExpressionNode(parser, parent, id, id, null, id);
            LinqJoinExpressionNode result = new LinqJoinExpressionNode(parser, parent, childStartToken, prevToken, typeLeft, idNode, inExpression, onExpression, equalsExpression, intoIdNode, false, null, null, null, null, StatementParseMode.Inside);

            return result;
        }

        static LinqLetExpressionNode ParseLinqLetClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            nextToken = parser.Tokenizer.Next(nextToken);

            if (nextToken == null || nextToken.Index > endToken.Index || !nextToken.Id.IsIdentifier())
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Identifier expected after 'let'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            Token id = nextToken;
            nextToken = parser.Tokenizer.Next(nextToken);

            if (nextToken == null || nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            ExpressionNode expression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.Assign, "=", ref nextToken, ref prevToken);
            if (expression == null)
            {
                return null;
            }

            AtomicExpressionNode idNode = new AtomicExpressionNode(parser, parent, id, id, null, id);
            LinqLetExpressionNode result = new LinqLetExpressionNode(parser, parent, childStartToken, prevToken, idNode, expression, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqWhereExpressionNode ParseLinqWhereClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            ExpressionNode expression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.Where, "where", ref nextToken, ref prevToken);
            if (expression == null)
            {
                return null;
            }

            // NOTE logical
            expression.AddAcceptedType(SystemResolvedType.BOOLEAN);

            LinqWhereExpressionNode result = new LinqWhereExpressionNode(parser, parent, childStartToken, prevToken, expression, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqOrderByExpressionNode ParseLinqOrderByClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            nextToken = parser.Tokenizer.Next(nextToken);

            if (nextToken == null || nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            List<LinqOrderingExpressionNode> orderings = new List<LinqOrderingExpressionNode>();
            LinqOrderingExpressionNode ord0 = linqOrderingToNext(errors, parser, parent, nextToken, endToken, TokenId.By, "order by", ref nextToken, ref prevToken);
            if (ord0 == null)
            {
                return null;
            }
            orderings.Add(ord0);
            while (nextToken.Id == TokenId.Comma)
            {
                LinqOrderingExpressionNode ord = linqOrderingToNext(errors, parser, parent, nextToken, endToken, TokenId.Comma, ",", ref nextToken, ref prevToken);
                if (ord == null)
                {
                    return null;
                }
                orderings.Add(ord);
            }

            LinqOrderByExpressionNode result = new LinqOrderByExpressionNode(parser, parent, childStartToken, prevToken, orderings, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqGroupByExpressionNode ParseLinqGroupByClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            ExpressionNode groupExpression = linqExpressionToNext(errors, parser, parent, nextToken, endToken, TokenId.Group, "group", ref nextToken, ref prevToken);
            if (groupExpression == null)
            {
                return null;
            }

            ExpressionNode byExpression = linqExpressionToNext(errors, parser, parent, nextToken, endToken, TokenId.By, "group by", ref nextToken, ref prevToken);
            if (byExpression == null)
            {
                return null;
            }

            LinqGroupByExpressionNode result = new LinqGroupByExpressionNode(parser, parent, childStartToken, prevToken, groupExpression, byExpression, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqSelectExpressionNode ParseLinqSelectClause(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            ExpressionNode expression = linqExpressionToNext(errors, parser, parent, startToken, endToken, TokenId.Select, "select", ref nextToken, ref prevToken);
            if (expression == null)
            {
                return null;
            }

            LinqSelectExpressionNode result = new LinqSelectExpressionNode(parser, parent, childStartToken, prevToken, expression, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static LinqOrderingExpressionNode linqOrderingToNext(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, TokenId expectedTokenId, string expectedTokenInf, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            ExpressionNode expression = linqExpressionToNext(errors, parser, parent, startToken, endToken, expectedTokenId, expectedTokenInf, ref nextToken, ref prevToken);
            if (expression == null)
            {
                return null;
            }

            LinqOrderingType type;
            if (nextToken.Id == TokenId.Descending)
            {
                type = LinqOrderingType.Descending;
            }
            else
            {
                type = LinqOrderingType.Ascending;
            }
            if (nextToken.Id == TokenId.Ascending || nextToken.Id == TokenId.Descending)
            {
                nextToken = parser.Tokenizer.Next(nextToken);

                if (nextToken == null || nextToken.Index > endToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Linq", _err);
                    return null;
                }
            }

            LinqOrderingExpressionNode result = new LinqOrderingExpressionNode(parser, parent, childStartToken, prevToken, expression, type, false, null, null, null, null, StatementParseMode.Inside);
            return result;
        }

        static ExpressionNode linqExpressionToNext(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, TokenId expectedTokenId, string expectedTokenInf, ref Token nextToken, ref Token prevToken)
        {
            Token childStartToken = nextToken;

            if (nextToken.Id != expectedTokenId)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing '" + expectedTokenInf + "'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            nextToken = parser.Tokenizer.Next(nextToken);

            if (nextToken == null || nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Expression expected after '" + expectedTokenInf + "'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            Token expFirstToken = nextToken;
            nextToken = linqFindNextKeyword(parser, startToken.Block, nextToken, endToken, ref prevToken);

            if (nextToken == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing 'select' or 'group by'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }
            if (prevToken == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid linq expression. Missing expression after '" + expectedTokenInf + "'.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            ExpressionNode expression = ParseExpression(errors, parser, parent, expFirstToken, prevToken);

            if (expression == null)
            {
                CompilationErrorEnty _err;
                errors.Add(_err = parser.CreateError("Invalid expression after '" + expectedTokenInf + "'.", expFirstToken.TokenStartPos, prevToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                errors.Add(_err = parser.CreateError("Invalid linq expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Linq", _err);
                return null;
            }

            return expression;
        }

        static Token linqFindNextKeyword(CsParser parser, TokenOpenClose parentBlock, Token from, Token endToken, ref Token prevToken)
        {
            IList<Token> tks = parser.Tokenizer.Result;
            int ei = endToken.Index;

            prevToken = null;
            for (int i = from.Index; i <= ei; i++)
            {
                Token tk = tks[i];
                if (tk.Block != parentBlock)
                {
                    continue;
                }
                if (linqKeywords.Contains(tk.Id))
                {
                    return tk;
                }
                prevToken = tk;
            }
            if (ei < tks.Count - 2)
            {
                Token tke = tks[ei + 1];
                return tke;
            }
            else
            {
                return null;
            }
        }

        static ExpressionNode ParsePrimaryExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken)
        {
            PrimaryExpressionParse parse;
            Token t2 = null;

            switch (endToken.Id)
            {
                case TokenId.ParenClose:
                    parse = PrimaryExpressionParse.Method;
                    break;
                case TokenId.ArrayOrAttributeClose:
                    parse = PrimaryExpressionParse.Array;
                    break;
                default:
                    parse = PrimaryExpressionParse.Field;
                    break;
            }
            //errors.Add(_err=parser.CreateError("')' expected.", endToken.TokenStartPos, endToken.TokenEndPos);

            ExpressionNode methodThis = null;
            Token lastPoint = null;
            Token memberIdToken = null;

            switch (parse)
            {
                case PrimaryExpressionParse.Field:
                case PrimaryExpressionParse.Method:
                    lastPoint = endToken.Block.PrevOperator(TokenId.Point, endToken);
                    if (lastPoint != null && startToken.Index < lastPoint.Index)
                    {
                        memberIdToken = parser.Tokenizer.Next(lastPoint);
                    }
                    else
                    {
                        memberIdToken = startToken;
                        lastPoint = null;
                    }

                    if (memberIdToken != null)
                    {
                        Token nextToken = parser.Tokenizer.Next(memberIdToken);
                        if (memberIdToken == endToken || (nextToken != null && nextToken.Index < endToken.Index && (nextToken.Id == TokenId.Lt || nextToken.Id == TokenId.ParenOpen)))
                        {
                            if (lastPoint != null)
                            {
                                Token expressionLast = parser.Tokenizer.Previous(lastPoint);
                                methodThis = ParseExpression(errors, parser, parent, startToken, expressionLast);
                                if (methodThis == null)
                                {
                                    CompilationErrorEnty _err;
                                    errors.Add(_err=parser.CreateError("Unrecognized field or method expression (left side is unknown).", startToken.TokenStartPos, endToken.TokenEndPos));
                                    parser.RaiseTmpErrorAdded("Primary", _err);
                                    return null;
                                }
                            }
                        }
                        else
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Unrecognized field or method expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Primary", _err);
                            return null;
                        }
                    }
                    else
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Unrecognized field or method expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Primary", _err);
                        return null;
                    }
                    break;
            }

            switch (parse)
            {
                case PrimaryExpressionParse.Method:
                    TokenOpenClose generic = parser.CsTokenizer.Generic(memberIdToken);
                    List<TypeSpecifierNode> typeParameters;
                    int expParenIndex;
                    if (generic != null)
                    {
                        if (generic.OpenToken.Index == memberIdToken.Index + 1)
                        {
                            if (generic.CloseToken != null && generic.CloseToken.Index <= endToken.Index)
                            {
                                typeParameters = ParseTypeParameters(errors, parser, parent, generic, ref t2, false, true);
                                if (typeParameters == null)
                                {
                                    CompilationErrorEnty _err;
                                    errors.Add(_err=parser.CreateError("Invalid generic type parameters block.", startToken.TokenStartPos, endToken.TokenEndPos));
                                    parser.RaiseTmpErrorAdded("Primary", _err);
                                    return null;
                                }
                                expParenIndex = generic.CloseToken.Index + 1;
                            }
                            else
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("Unclosed generic type parameters block.", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                        }
                        else
                        {
                            typeParameters = null;
                            expParenIndex = memberIdToken.Index + 1;
                        }
                    }
                    else
                    {
                        typeParameters = null;
                        expParenIndex = memberIdToken.Index + 1;
                    }

                    List<ArgumentNode> arguments = null;
                    TokenOpenClose parens = parser.CsTokenizer.Paren(memberIdToken);
                    if (parens != null)
                    {
                        if (parens.OpenToken.Index == expParenIndex)
                        {
                            if (parens.CloseToken != null && parens.CloseToken.Index <= endToken.Index)
                            {
                                if (parens.CloseToken == endToken)
                                {
                                    arguments = ParseArgumentList(errors, parser, parent, parens);
                                    if (parens.CloseToken.Index - parens.OpenToken.Index > 1 && arguments == null)
                                    {
                                        CompilationErrorEnty _err;
                                        errors.Add(_err=parser.CreateError("Unable to parse arguments.", startToken.TokenStartPos, endToken.TokenEndPos));
                                        parser.RaiseTmpErrorAdded("Primary", _err);
                                        return null;
                                    }
                                }
                                else
                                {
                                    CompilationErrorEnty _err;
                                    errors.Add(_err=parser.CreateError("Unexpected tokens after ')'.", startToken.TokenStartPos, endToken.TokenEndPos));
                                    parser.RaiseTmpErrorAdded("Primary", _err);
                                    return null;
                                }
                            }
                            else
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("Unclosed parenthesis.", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                        }
                        else if (parens.OpenToken.Index <= endToken.Index)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid parenthesis block.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Primary", _err);
                            return null;
                        }
                        else
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Missing parentheses.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Primary", _err);
                            return null;
                        }
                    }
                    else
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Missing parentheses.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Primary", _err);
                        return null;
                    }

                    switch (memberIdToken.Id)
                    {
                        case TokenId.Checked:
                        case TokenId.Unchecked:
                        case TokenId.Typeof:
                        case TokenId.Default:
                            if (arguments.Count != 1)
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("Invalid number of arguments (expected : 1).", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                            if (arguments[0].Type != ArgumentType.Value)
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("'" + arguments[0].RefToken.ContentString + "' is not valid here.", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                            if (methodThis != null)
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("Invalid method call. Reserved keyword : '" + memberIdToken.ContentString + "'", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                            break;
                    }

                    switch (memberIdToken.Id)
                    {
                        case TokenId.Checked:
                            CheckedNode chresult = new CheckedNode(parser, parent, startToken, endToken, parens, memberIdToken, arguments[0].Right);
                            return chresult;
                        case TokenId.Default:
                            DefaultExpressionNode dxresult = new DefaultExpressionNode(parser, parent, startToken, endToken, parens, memberIdToken, arguments[0].Right);
                            return dxresult;
                        case TokenId.Unchecked:
                            UncheckedNode unchresult = new UncheckedNode(parser, parent, startToken, endToken, parens, memberIdToken, arguments[0].Right);
                            return unchresult;
                        case TokenId.Typeof:
                            TypeofNode toresult = new TypeofNode(parser, parent, startToken, endToken, parens, memberIdToken, arguments[0].Right);
                            return toresult;
                        default:
                            if (memberIdToken.Id.IsIdentifier() || memberIdToken.Id == TokenId.Base || memberIdToken.Id == TokenId.This)
                            {
                                MethodExpressionNode mresult = new MethodExpressionNode(parser, parent, startToken, endToken, lastPoint, methodThis, typeParameters, parens, memberIdToken, arguments);
                                return mresult;
                            }
                            else
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("Unrecognized expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                    }
                case PrimaryExpressionParse.Field:
                    if (startToken != memberIdToken && methodThis == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid object expression to the left of '.'", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Primary", _err);
                        return null;
                    }
                    if (endToken == memberIdToken)
                    {
                        if (methodThis != null)
                        {
                            if (memberIdToken.Id.IsIdentifier())
                            {
                                FieldAccessOrIdNode fresult = new FieldAccessOrIdNode(parser, parent, startToken, endToken, lastPoint, methodThis, memberIdToken);
                                return fresult;
                            }
                            else
                            {
                                CompilationErrorEnty _err;
                                errors.Add(_err=parser.CreateError("Invalid field identifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                                parser.RaiseTmpErrorAdded("Primary", _err);
                                return null;
                            }
                        }
                        else
                        {

                            if (memberIdToken.Id.IsIdentifier())
                            {
                                AtomicExpressionNode fresult = new AtomicExpressionNode(parser, parent, startToken, endToken, lastPoint, memberIdToken);
                                return fresult;
                            }
                            else
                            {
                                switch (memberIdToken.Id)
                                {
                                    case TokenId.This:
                                    case TokenId.Base:
                                    case TokenId.ByteLiteral:
                                    case TokenId.CharLiteral:
                                    case TokenId.DoubleLiteral:
                                    case TokenId.FloatLiteral:
                                    case TokenId.IntLiteral:
                                    case TokenId.LongLiteral:
                                    case TokenId.ShortLiteral:
                                    case TokenId.UIntLiteral:
                                    case TokenId.ULongLiteral:
                                    case TokenId.UShortLiteral:
                                    case TokenId.StringLiteral:
                                    case TokenId.Null:
                                    case TokenId.True:
                                    case TokenId.False:
                                        AtomicExpressionNode fresult = new AtomicExpressionNode(parser, parent, startToken, endToken, lastPoint, memberIdToken);
                                        return fresult;
                                    default:
                                        TypeSpecifierNode idresult = ParseTypeSpecifier(errors, parser, parent, startToken, endToken, ref t2, false, true, true);
                                        if (idresult == null)
                                        {
                                            CompilationErrorEnty _err;
                                            errors.Add(_err=parser.CreateError("Invalid type specifier as primary expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                                            parser.RaiseTmpErrorAdded("Primary", _err);
                                            errors.Add(_err = parser.CreateError("Invalid atomic expression token : '" + memberIdToken.ContentString + "'", startToken.TokenStartPos, endToken.TokenEndPos));
                                            parser.RaiseTmpErrorAdded("Primary", _err);
                                        }
                                        return idresult;
                                }
                            }
                        }
                    }
                    else
                    {
                        TypeSpecifierNode idresult = ParseTypeSpecifier(errors, parser, parent, startToken, endToken, ref t2, false, true, true);
                        if (idresult == null)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Invalid type specifier as primary expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Primary", _err);
                        }
                        return idresult;
                    }
                case PrimaryExpressionParse.Array:
                    TokenOpenClose brackets = parser.CsTokenizer.ArrayByClose(endToken);
                    List<ExpressionNode> parameters = null;
                    ExpressionNode left;
                    if (brackets == null)
                    {
                        parameters = null;
                        left = null;
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Missing brackets.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Primary", _err);
                        return null;
                    }
                    else if (startToken.Index < brackets.OpenToken.Index && brackets.CloseToken == endToken)
                    {
                        parameters = ParseExpressionList(errors, parser, parent, brackets);
                        if (brackets.CloseToken.Index - brackets.OpenToken.Index > 1 && parameters == null)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("Unable to parse array parameters.", startToken.TokenStartPos, endToken.TokenEndPos));
                            parser.RaiseTmpErrorAdded("Primary", _err);
                            return null;
                        }
                        Token prevToken = parser.Tokenizer.Previous(brackets.OpenToken);
                        left = ParseExpression(errors, parser, parent, startToken, prevToken);
                    }
                    else
                    {
                        parameters = null;
                        left = null;
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid brackets.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Primary", _err);
                        return null;
                    }
                    if (left != null)
                    {
                        ArrayAccessExpressionNode aresult = new ArrayAccessExpressionNode(parser, parent, startToken, endToken, left, brackets, parameters);
                        return aresult;
                    }
                    else
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid expression.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Primary", _err);
                        return null;
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        static ParenExpressionNode ParseParenExpression(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose parens)
        {
            ParenExpressionNode result = null;

            ExpressionNode inner = ParseExpression(errors, parser, parent, parser.Tokenizer.Next(parens.OpenToken), parser.Tokenizer.Previous(parens.CloseToken));
            if (inner != null)
            {
                result = new ParenExpressionNode(parser, parent, parens, inner);
            }
            else
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid parenthesized expression.", parens.OpenToken.TokenStartPos, parens.CloseToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Paren", _err);
            }

            return result;
        }

        static TypeSpecifierNode ParseTypeSpecifier(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose castParen)
        {
            Token startToken = parser.Tokenizer.Next(castParen.OpenToken);
            Token endToken = parser.Tokenizer.Previous(castParen.CloseToken);
            Token t = null;
            TypeSpecifierNode result = ParseTypeSpecifier(errors, parser, parent, startToken, endToken, ref t, false, true, true);
            return result;
        }

        static TypeSpecifierNode ParseTypeSpecifier(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, ref Token nextToken, bool definition, bool declaration, bool exact)
        {
            Token t2 = null;
            bool systype = false;
            nextToken = null;
            List<Token> ids;
            switch (startToken.Id)
            {
                case TokenId.Float:
                case TokenId.Double:
                case TokenId.Decimal:
                case TokenId.Bool:
                case TokenId.Void:
                case TokenId.Object:
                case TokenId.String:
                case TokenId.Sbyte:
                case TokenId.Byte:
                case TokenId.Short:
                case TokenId.UShort:
                case TokenId.Int:
                case TokenId.UInt:
                case TokenId.Long:
                case TokenId.ULong:
                case TokenId.Char:
                case TokenId.Var:
                    systype = true;
                    ids = GreenZoneSysUtilsBase.AsList(startToken);
                    nextToken = parser.Tokenizer.Next(startToken);
                    break;
                default:
                    ids = ParseIdsLeft(parser, startToken, endToken, ref nextToken);
                    if (ids == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid type specifier. Identifier expected.", startToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Type", _err);
                        return null;
                    }
                    break;
            }

            if (nextToken.Index == endToken.Index + 1)
            {
                TypeSpecifierNode result = CreateTypeSpecifierNode(errors, parser, parent, startToken, endToken, null, null, null, null, null, ids, systype, 0, 0, null);
                return result;
            }
            else if (nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Type", _err);
                return null;
            }

            List<TypeSpecifierNode> typeParameters = null;
            if (nextToken.Id == TokenId.Lt)
            {
                TokenOpenClose generic = nextToken.GenericBlock;
                if (generic.CloseToken == null || generic.CloseToken.Index>endToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid type specifier. Missing closing '>'.", generic.OpenToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Type", _err);
                    errors.Add(_err = parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Type", _err);
                    return null;
                }
                typeParameters = ParseTypeParameters(errors, parser, parent, generic, ref t2, false, true);
                if (typeParameters == null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid type specifier. Error in type parameters.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Type", _err);
                    return null;
                }
                else if (t2 != generic.CloseToken)
                {
                    throw new NotSupportedException();
                }
                nextToken = parser.Tokenizer.Next(generic.CloseToken);
            }

            if (nextToken.Index == endToken.Index + 1)
            {
                TypeSpecifierNode result = CreateTypeSpecifierNode(errors, parser, parent, startToken, endToken, null, typeParameters, null, null, null, ids, systype, 0, 0, null);
                return result;
            }
            else if (nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Type", _err);
                return null;
            }

            int commaSeparatedRanks = -1;
            int emptyRanks = -1;
            List<ExpressionNode> firstBracketExpressions = null;
            TokenOpenClose array = null;
            TokenOpenClose firstRanks = null;
            TokenOpenClose lastRanks = null;

            if (nextToken.Id == TokenId.ArrayOrAttributeOpen)
            {
                array = nextToken.Block;
                if (array.CloseToken == null || array.CloseToken.Index > endToken.Index)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Invalid type specifier. Missing closing ']'.", array.OpenToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Type", _err);
                    errors.Add(_err = parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Type", _err);
                    return null;
                }

                bool ranksOk = ParseArrayRanks(errors, parser, parent, array, endToken, ref firstBracketExpressions, ref firstRanks, ref lastRanks, ref commaSeparatedRanks, ref emptyRanks, declaration, definition);
                if (!ranksOk)
                {
                    return null;
                }
                if (!definition && firstBracketExpressions != null)
                {
                    bool err = false;
                    foreach (var x in firstBracketExpressions)
                    {
                        if (x != null)
                        {
                            CompilationErrorEnty _err;
                            errors.Add(_err=parser.CreateError("All ranks should be empty in a type specifier.", x.StartPos, x.EndPos));
                            parser.RaiseTmpErrorAdded("Type", _err);
                            err = true;
                        }
                    }
                    if (err)
                    {
                        return null;
                    }
                }
                nextToken = parser.Tokenizer.Next(lastRanks.CloseToken);
            }

            if (nextToken.Index == endToken.Index + 1)
            {
                TypeSpecifierNode result = CreateTypeSpecifierNode(errors, parser, parent, startToken, endToken, firstBracketExpressions, typeParameters, array, firstRanks, lastRanks, ids, systype, commaSeparatedRanks, emptyRanks, null);
                return result;
            }
            else if (nextToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Type", _err);
                return null;
            }


            Token lastToken;
            Token pointerStar = null;

            if (nextToken.Id == TokenId.Star)
            {
                pointerStar = nextToken;
                lastToken = nextToken;
            }
            else
            {
                lastToken = parser.Tokenizer.Previous(nextToken);
            }


            if (lastToken.Index == endToken.Index)
            {
                TypeSpecifierNode result = CreateTypeSpecifierNode(errors, parser, parent, startToken, endToken, firstBracketExpressions, typeParameters, array, firstRanks, lastRanks, ids, systype, commaSeparatedRanks, emptyRanks, pointerStar);
                return result;
            }
            else if (lastToken.Index > endToken.Index)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Type", _err);
                return null;
            }
            else if (exact)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("Invalid type specifier.", startToken.TokenStartPos, endToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Type", _err);
                return null;
            }
            else
            {
                TypeSpecifierNode result = CreateTypeSpecifierNode(errors, parser, parent, startToken, lastToken, firstBracketExpressions, typeParameters, array, firstRanks, lastRanks, ids, systype, commaSeparatedRanks, emptyRanks, pointerStar);
                return result;
            }
        }

        static TypeSpecifierNode CreateTypeSpecifierNode(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, Token startToken, Token endToken, List<ExpressionNode> firstBracketExpressions, List<TypeSpecifierNode> typeParameters, TokenOpenClose array, TokenOpenClose firstRanks, TokenOpenClose lastRanks, List<Token> ids, bool systype, int commaSeparatedRanks, int emptyRanks, Token pointerStar)
        {
            TypeSpecifierNode result;
            if (systype)
            {
                if (typeParameters != null)
                {
                    CompilationErrorEnty _err;
                    errors.Add(_err=parser.CreateError("Generic is not allowed for this type.", startToken.TokenStartPos, endToken.TokenEndPos));
                    parser.RaiseTmpErrorAdded("Type", _err);
                    return null;
                }
                result = new SystemTypeSpecifierNode(parser, parent, startToken, firstBracketExpressions, array, commaSeparatedRanks, emptyRanks, firstRanks, lastRanks, pointerStar);
            }
            else
            {
                result = new UserTypeSpecifierNode(parser, parent, startToken, endToken, ids, firstBracketExpressions, typeParameters, array, commaSeparatedRanks, emptyRanks, firstRanks, lastRanks, pointerStar);
            }
            return result;
        }

        static bool ParseArrayRanks(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose array, Token endToken, ref List<ExpressionNode> firstBracketExpressions, ref TokenOpenClose firstRanks, ref TokenOpenClose lastRanks, ref int commaSeparatedRanks, ref int emptyRanks, bool enableEmpty, bool enableNonEmpty)
        {
            bool isFirstEmpty = (array.CloseToken.Index - array.OpenToken.Index) == array.Commas.Count + 1;

            if (enableEmpty)
            {
                commaSeparatedRanks = isFirstEmpty ? array.Commas.Count : -1;
            }
            else if (isFirstEmpty)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("First ranks should not be empty.", array.OpenToken.TokenStartPos, array.CloseToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Ranks", _err);
                return false;
            }

            if (enableNonEmpty)
            {
                if (isFirstEmpty)
                {
                    firstBracketExpressions = null;
                }
                else
                {
                    firstBracketExpressions = ParseExpressionList(errors, parser, parent, array);
                    if (array.CloseToken.Index - array.OpenToken.Index > 1 && firstBracketExpressions == null)
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Unable to parse arguments.", array.OpenToken.TokenStartPos, array.CloseToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Ranks", _err);
                        return false;
                    }
                }
            }
            else if (!isFirstEmpty)
            {
                CompilationErrorEnty _err;
                errors.Add(_err=parser.CreateError("First ranks should be empty.", array.OpenToken.TokenStartPos, array.CloseToken.TokenEndPos));
                parser.RaiseTmpErrorAdded("Ranks", _err);
                return false;
            }

            TokenOpenClose currentLast;
            firstRanks = lastRanks = currentLast = array;

            if (firstBracketExpressions != null)
            {
                currentLast = array.NextArray();
            }

            emptyRanks = 0;
            while (true)
            {
                if (currentLast != null && currentLast.OpenToken.Index < endToken.Index)
                {
                    if (currentLast.CloseToken.Index <= endToken.Index)
                    {
                        lastRanks = currentLast;
                        TokenOpenClose nextLast = currentLast.NextArray();
                        if (nextLast != null && nextLast.OpenToken.Index == currentLast.CloseToken.Index + 1)
                        {
                            currentLast = nextLast;
                            emptyRanks++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        CompilationErrorEnty _err;
                        errors.Add(_err=parser.CreateError("Invalid empty array rank block, missing closing ']'.", currentLast.OpenToken.TokenStartPos, endToken.TokenEndPos));
                        parser.RaiseTmpErrorAdded("Ranks", _err);
                        return false;
                    }
                }
                else
                {
                    break;
                }
            }
            return true;
        }

        static List<TypeSpecifierNode> ParseTypeParameters(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose genericBlock, ref Token nextToken, bool lastInclusive, bool exact)
        {
            if (genericBlock.CloseToken == null)
            {
                return null;
            }

            List<TypeSpecifierNode> result = null;

            Token current = parser.Tokenizer.Next(genericBlock.OpenToken);

            Token t = null;
            int inclMod = lastInclusive ? 1 : 0;
            while (true)
            {
                TypeSpecifierNode ts = ParseTypeSpecifier(errors, parser, parent, current, genericBlock.CloseToken, ref t, false, true, false);
                if (ts != null)
                {
                    if (result == null)
                    {
                        result = new List<TypeSpecifierNode>();
                    }
                    result.Add(ts);
                }
                nextToken = t;

                if (ts != null && (t.Index == genericBlock.CloseToken.Index + inclMod))
                {
                    return result;
                }
                else if ((t.Index >= genericBlock.CloseToken.Index + inclMod) || t.Id != TokenId.Comma)
                {
                    if (exact)
                    {
                        return null;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    current = parser.Tokenizer.Next(t);
                }
            }
        }

        static List<ExpressionNode> ParseExpressionList(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose toc, bool lastInclusive = false)
        {
            if (toc.CloseToken == null)
            {
                return null;
            }

            Token from = parser.Tokenizer.Next(toc.OpenToken);
            Token to;
            if (lastInclusive)
            {
                to = toc.CloseToken;
            }
            else
            {
                to = parser.Tokenizer.Previous(toc.CloseToken);
            }
            if (from.Index > to.Index)
            {
                return null;
            }

            List<ExpressionNode> result = ParseExpressionList(errors, parser, parent, toc, 0, toc.Commas.Count, from, to);
            return result;
        }

        static List<ExpressionNode> ParseExpressionList(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose toc, int commaFrom, int commaTo, Token from, Token to)
        {
            IList<Token> commas = toc.Commas;
            for (int i = commaTo - 1; i >= commaFrom; i--)
            {
                Token comma = commas[i];
                Token prev = parser.Tokenizer.Previous(comma);
                Token next = parser.Tokenizer.Next(comma);

                ExpressionNode item = ParseExpression(errors, parser, parent, next, to);
                if (item == null)
                {
                    continue;
                }
                List<ExpressionNode> left = ParseExpressionList(errors, parser, parent, toc, commaFrom, i, from, prev);
                if (left != null)
                {
                    left.Add(item);
                    return left;
                }
            }

            List<ExpressionNode> result;
            ExpressionNode singleitem = ParseExpression(errors, parser, parent, from, to);
            if (singleitem != null)
            {
                result = GreenZoneSysUtilsBase.AsList(singleitem);
            }
            else
            {
                result = null;
            }

            return result;
        }

        static List<ArgumentNode> ParseArgumentList(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose toc, bool lastInclusive = false)
        {
            if (toc.CloseToken == null)
            {
                return null;
            }

            Token from = parser.Tokenizer.Next(toc.OpenToken);
            Token to;
            if (lastInclusive)
            {
                to = toc.CloseToken;
            }
            else
            {
                to = parser.Tokenizer.Previous(toc.CloseToken);
            }
            if (from.Index > to.Index)
            {
                return null;
            }

            List<ArgumentNode> result = ParseArgumentList(errors, parser, parent, toc, 0, toc.Commas.Count, from, to);
            return result;
        }

        static List<ArgumentNode> ParseArgumentList(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose toc, int commaFrom, int commaTo, Token from, Token to)
        {
            IList<Token> commas = toc.Commas;
            for (int i = commaTo - 1; i >= commaFrom; i--)
            {
                Token comma = commas[i];
                Token prev = parser.Tokenizer.Previous(comma);
                Token next = parser.Tokenizer.Next(comma);

                ArgumentNode item = ParseArgument(errors, parser, parent, next, to);
                if (item == null)
                {
                    continue;
                }
                List<ArgumentNode> left = ParseArgumentList(errors, parser, parent, toc, commaFrom, i, from, prev);
                if (left != null)
                {
                    left.Add(item);
                    return left;
                }
            }

            List<ArgumentNode> result;
            ArgumentNode singleitem = ParseArgument(errors, parser, parent, from, to);
            if (singleitem != null)
            {
                result = GreenZoneSysUtilsBase.AsList(singleitem);
            }
            else
            {
                result = null;
            }

            return result;
        }

        static List<VarDeclArgumentNode> ParseVarDeclArgumentList(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose toc, bool lastInclusive = false)
        {
            if (toc.CloseToken == null)
            {
                return null;
            }

            Token from = parser.Tokenizer.Next(toc.OpenToken);
            Token to;
            if (lastInclusive)
            {
                to = toc.CloseToken;
            }
            else
            {
                to = parser.Tokenizer.Previous(toc.CloseToken);
            }
            if (from.Index > to.Index)
            {
                return null;
            }

            List<VarDeclArgumentNode> result = ParseVarDeclArgumentList(errors, parser, parent, toc, 0, toc.Commas.Count, from, to);
            return result;
        }

        static List<VarDeclArgumentNode> ParseVarDeclArgumentList(List<CompilationErrorEnty> errors, CsParser parser, BlockNode parent, TokenOpenClose toc, int commaFrom, int commaTo, Token from, Token to)
        {
            IList<Token> commas = toc.Commas;
            for (int i = commaTo - 1; i >= commaFrom; i--)
            {
                Token comma = commas[i];
                Token prev = parser.Tokenizer.Previous(comma);
                Token next = parser.Tokenizer.Next(comma);

                VarDeclArgumentNode item = ParseVarDeclArgument(errors, parser, parent, next, to);
                if (item == null)
                {
                    continue;
                }
                List<VarDeclArgumentNode> left = ParseVarDeclArgumentList(errors, parser, parent, toc, commaFrom, i, from, prev);
                if (left != null)
                {
                    left.Add(item);
                    return left;
                }
            }

            List<VarDeclArgumentNode> result;
            VarDeclArgumentNode singleitem = ParseVarDeclArgument(errors, parser, parent, from, to);
            if (singleitem != null)
            {
                result = GreenZoneSysUtilsBase.AsList(singleitem);
            }
            else
            {
                result = null;
            }

            return result;
        }


        static List<Token> ParseIdsLeft(CsParser parser, Token startToken, Token endToken, ref Token t)
        {
            List<Token> ids = null;
            t = startToken;
            while (t.Index <= endToken.Index && t.Id.IsIdentifier())
            {
                if (ids == null)
                {
                    ids = new List<Token>();
                }
                ids.Add(t);
                t = parser.Tokenizer.Next(t);
                if (t == null || t.Index > endToken.Index)
                {
                    break;
                }
                else if (t.Id == TokenId.Point)
                {
                    t = parser.Tokenizer.Next(t);
                }
                else
                {
                    break;
                }
            }
            return ids;
        }

        static List<Token> ParseIdsRight(CsParser parser, Token startToken, Token endToken, ref Token t)
        {
            List<Token> ids = null;
            t = endToken;
            while (t.Index >= startToken.Index && t.Id.IsIdentifier())
            {
                if (ids == null)
                {
                    ids = new List<Token>();
                }
                ids.Insert(0, t);
                t = parser.Tokenizer.Previous(t);
                if (t == null || t.Index < startToken.Index)
                {
                    break;
                }
                else if (t.Id == TokenId.Point)
                {
                    t = parser.Tokenizer.Previous(t);
                }
                else
                {
                    break;
                }
            }
            return ids;
        }
    }
}