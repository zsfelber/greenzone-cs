using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using GreenZoneParser.Parsers.Cs;
using System.Windows.Forms;
using GreenZoneParser.Parsers;
using System.Threading;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;


namespace GreenZoneParser.Xml
{

    public class XmlParser : Parser
    {
        enum State
        {
            TagOpen,
            TagNameOrSlashBegin,
            TagNameAfterSlash,
            AttributeOrSlashOrClose,
            AssignAfterAttributeName,
            LiteralAfterAssign,
            TagCloseAfterSlash
        }

        bool currentTagIsCloseBegin;
        bool currentTagIsSingle;
        Token currentTagStart;
        string currentTagName;
        string currentAttributeName;
        object currentAttributeValue;

        Dictionary<string, object> currentAttrs;
        XmlNode technicalRootNode;
        XmlNode current;

        public XmlParser(string fileName, string fileContent)
            : base(fileName, fileContent)
        {
            this.tokenizer = new XmlTokenizer(this, fileContent);
        }

        public override BaseNode Parse()
        {
            tokenizer.Tokenize();
            XmlTag technicalFirstTag = new XmlTag(this, "xml", XmlTagType.Open, tokenizer.FirstToken, tokenizer.FirstToken, new Dictionary<string, object>());
            XmlTag technicalLastTag = new XmlTag(this, "xml", XmlTagType.Close, tokenizer.LastToken, tokenizer.LastToken, new Dictionary<string, object>());
            technicalRootNode = new XmlNode(this, null, technicalFirstTag);
            technicalRootNode.CloseTag = technicalLastTag;

            rootNodes = technicalRootNode.children;

            State state = State.TagOpen;

            foreach (Token token in tokenizer.Result)
            {
                List<TokenId> expected;
                switch (state)
                {
                    case State.TagOpen:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Lt);
                        break;
                    case State.TagNameOrSlashBegin:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Identifier, TokenId.Slash);
                        break;
                    case State.TagNameAfterSlash:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Identifier);
                        break;
                    case State.AttributeOrSlashOrClose:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Identifier, TokenId.Slash, TokenId.Gt);
                        break;
                    case State.AssignAfterAttributeName:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Assign);
                        break;
                    case State.LiteralAfterAssign:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Identifier, TokenId.StringLiteral, TokenId.IntLiteral);
                        break;
                    case State.TagCloseAfterSlash:
                        expected = GreenZoneSysUtilsBase.AsList(TokenId.Gt);
                        break;
                    default:
                        throw new NotSupportedException();
                }

                if (!expected.Contains(token.Id))
                {
                    var xs = from e in expected select e.ToString();
                    AddError("Invalid xml. Expected tokens : " + string.Join(",", xs), token.TokenStartPos, token.TokenEndPos);
                    break;
                }
                else
                {

                    switch (state)
                    {
                        case State.TagOpen:
                            reset();
                            state = State.TagNameOrSlashBegin;
                            currentTagStart = token;
                            break;
                        case State.TagNameOrSlashBegin:
                            if (token.Id == TokenId.Identifier)
                            {
                                currentTagName = token.ContentString;
                                state = State.AttributeOrSlashOrClose;
                            }
                            else
                            {
                                currentTagIsCloseBegin = true;
                                state = State.TagNameAfterSlash;
                            }
                            break;
                        case State.TagNameAfterSlash:
                            currentAttributeValue = token.Value;
                            currentTagName = token.ContentString;
                            state = State.AttributeOrSlashOrClose;
                            break;
                        case State.AttributeOrSlashOrClose:
                            if (token.Id == TokenId.Identifier)
                            {
                                currentAttributeName = token.ContentString;
                                state = State.AssignAfterAttributeName;
                            }
                            else if (token.Id == TokenId.Slash)
                            {
                                currentTagIsSingle = true;
                                state = State.TagCloseAfterSlash;
                            }
                            else // Gt
                            {
                                state = State.TagOpen;
                            }
                            break;
                        case State.AssignAfterAttributeName:
                            state = State.LiteralAfterAssign;
                            break;
                        case State.LiteralAfterAssign:
                            currentAttributeValue = token.Value;
                            currentAttrs[currentAttributeName] = currentAttributeValue;
                            state = State.AttributeOrSlashOrClose;
                            break;
                        case State.TagCloseAfterSlash:
                            state = State.TagOpen;
                            break;
                    }

                    if (state == State.TagOpen)
                    {
                        bool ok = true;
                        XmlTagType tagType;
                        if (currentTagIsCloseBegin)
                        {
                            if (currentTagIsSingle)
                            {
                                AddError("</xmltag/> is not valid", token.TokenStartPos, token.TokenEndPos);
                                tagType = XmlTagType.Open;
                                ok = false;
                            }
                            else
                            {
                                tagType = XmlTagType.Close;
                            }
                        }
                        else if (currentTagIsSingle)
                        {
                            tagType = XmlTagType.Single;
                        }
                        else
                        {
                            tagType = XmlTagType.Open;
                        }
                        if (ok)
                        {
                            XmlTag tag = new XmlTag(this, currentTagName, tagType, currentTagStart, token, currentAttrs);
                            switch (tagType)
                            {
                                case XmlTagType.Open:
                                    current = new XmlNode(this, current, tag);
                                    break;
                                case XmlTagType.Single:
                                    XmlNode single = new XmlNode(this, current, tag);
                                    single.CloseTag = tag;
                                    break;
                                case XmlTagType.Close:
                                    current.CloseTag = tag;
                                    if (current.Parent == null)
                                    {
                                        rootNodes.Add(current);
                                    }
                                    current = current.Parent;
                                    break;
                            }
                        }
                    }
                }
            }
            reset();
            tokenizer.ClearTokens();
            return technicalRootNode;
        }

        void reset()
        {
            currentTagIsCloseBegin = false;
            currentTagIsSingle = false;
            currentTagStart = null;
            currentTagName = null;
            currentAttributeName = null;
            currentAttributeValue = null;
            currentAttrs = new Dictionary<string, object>();
            //current = ;
        }

        readonly XmlTokenizer tokenizer;
        public override Tokenizer Tokenizer
        {
            get
            {
                return tokenizer;
            }
        }

        List<XmlNode> rootNodes;
        public List<XmlNode> RootNodes
        {
            get
            {
                return rootNodes;
            }
        }
    }

}