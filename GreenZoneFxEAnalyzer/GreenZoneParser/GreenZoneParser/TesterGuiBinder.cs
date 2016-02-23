using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;
using GreenZoneParser.Xml;
using GreenZoneParser.Parsers.Cs;
using GreenZoneParser.Reflect;

namespace GreenZoneParser
{
    internal abstract class TesterGuiBinder
    {
        protected readonly SortedDictionary<BaseNode, TreeNode> childNodes;
        protected readonly SortedDictionary<Token, DataGridViewRow> tokenRows;
        protected readonly SortedDictionary<CompilationErrorEnty, DataGridViewRow> errorRows;

        protected readonly List<CompilationErrorEnty> pendingParseErrors;

        protected List<int> childPos;
        protected List<int> tokenPos;
        protected List<int> errorPos;

        protected List<BaseNode> children;
        protected List<Token> tokens;
        protected List<CompilationErrorEnty> errors;

        protected HashSet<TreeNode> childControls;
        protected HashSet<DataGridViewRow> tokenControls;
        protected HashSet<DataGridViewRow> errorControls;

        protected readonly HashSet<TreeNode> nodesSetUp; 

        protected TextBox fileTextBox;
        protected RichTextBoxEx sourceTextBox;
        protected TreeView treeView1;
        protected DataGridView tokensTable;
        protected DataGridView errorsTable;
        protected Label posLabel;
        protected Label lineLabel;
        protected Label columnLabel;

        protected Parser parser;

        protected bool _GoTo;

        protected DataGridViewCellEventHandler _errorsTable_CellClick;
        protected EventHandler _errorsTable_SelectionChanged;
        protected DataGridViewCellEventHandler _tokensTable_CellClick;
        protected EventHandler _tokensTable_SelectionChanged;
        protected TreeViewEventHandler _treeView1_AfterSelect;
        protected TreeViewCancelEventHandler _treeView1_BeforeExpand;
        protected EventHandler _treeView1_Click;
        protected EventHandler _sourceTextBox_SelectionChanged;

        internal TesterGuiBinder(TextBox fileTextBox, RichTextBoxEx sourceTextBox, 
                                 TreeView treeView1, DataGridView tokensTable, DataGridView errorsTable, 
                                 Label posLabel, Label lineLabel, Label columnLabel,
                                 Parser parser)
        {
            this.fileTextBox = fileTextBox;
            this.sourceTextBox = sourceTextBox;
            this.treeView1 = treeView1;
            this.tokensTable = tokensTable;
            this.errorsTable = errorsTable;

            this.posLabel = posLabel;
            this.lineLabel = lineLabel;
            this.columnLabel = columnLabel;

            this.parser = parser;

            this.childNodes = new SortedDictionary<BaseNode, TreeNode>();
            this.tokenRows = new SortedDictionary<Token, DataGridViewRow>();
            this.errorRows = new SortedDictionary<CompilationErrorEnty, DataGridViewRow>();

            this.pendingParseErrors = new List<CompilationErrorEnty>();

            this.nodesSetUp = new HashSet<TreeNode>();

            this.errorsTable.CellClick += this._errorsTable_CellClick = new DataGridViewCellEventHandler(this.errorsTable_CellClick);
            this.errorsTable.SelectionChanged += this._errorsTable_SelectionChanged = new EventHandler(this.errorsTable_SelectionChanged);
            if (this.tokensTable != null)
            {
                this.tokensTable.CellClick += this._tokensTable_CellClick = new DataGridViewCellEventHandler(this.tokensTable_CellClick);
                this.tokensTable.SelectionChanged += this._tokensTable_SelectionChanged = new EventHandler(this.tokensTable_SelectionChanged);
            }
            this.treeView1.AfterSelect += this._treeView1_AfterSelect = new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.BeforeExpand += this._treeView1_BeforeExpand = new TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.Click += this._treeView1_Click = new EventHandler(this.treeView1_Click);
            this.sourceTextBox.SelectionChanged += this._sourceTextBox_SelectionChanged = new EventHandler(this.sourceTextBox_SelectionChanged);
        }

        internal void Dispose()
        {
            this.errorsTable.CellClick -= this._errorsTable_CellClick;
            this.errorsTable.SelectionChanged -= this._errorsTable_SelectionChanged;
            if (this.tokensTable != null)
            {
                this.tokensTable.CellClick -= this._tokensTable_CellClick;
                this.tokensTable.SelectionChanged -= this._tokensTable_SelectionChanged;
            }
            this.treeView1.AfterSelect -= this._treeView1_AfterSelect;
            this.treeView1.BeforeExpand -= this._treeView1_BeforeExpand;
            this.treeView1.Click -= this._treeView1_Click;
            this.sourceTextBox.SelectionChanged -= this._sourceTextBox_SelectionChanged;
        }

        bool changingTbls = false;
        internal bool ChangingTbls
        {
            get
            {
                return changingTbls;
            }
            set
            {
                changingTbls = value;
            }
        }

        internal void Clear()
        {
            changingTbls = true;
            try
            {
                Console.WriteLine("DEBUG  TesterGuiBinder.Clear()   parser:" + (parser == null ? "null" : parser.FileName));
                tokensTable.Rows.Clear();
                treeView1.Nodes.Clear();
                errorsTable.Rows.Clear();

                childNodes.Clear();
                tokenRows.Clear();
                errorRows.Clear();
                nodesSetUp.Clear();
                childPos = null;
                tokenPos = null;
                errorPos = null;
                children = null;
                tokens = null;
                errors = null;
                childControls = null;
                tokenControls = null;
                errorControls = null;
            }
            finally
            {
                changingTbls = false;
            }
        }

        internal void GenerateLists(bool tokens=true, bool errors=true, bool nodes=true)
        {
            if (tokens)
            {
                tokenPos = new List<int>();
                foreach (var token in tokenRows.Keys)
                {
                    tokenPos.Add(token.TokenStartPos);
                }
                this.tokens = new List<Token>(tokenRows.Keys);
                this.tokenControls = new HashSet<DataGridViewRow>(tokenRows.Values);
            }

            if (errors)
            {
                errorPos = new List<int>();
                foreach (var error in errorRows.Keys)
                {
                    errorPos.Add(error.Position);
                }
                this.errors = new List<CompilationErrorEnty>(errorRows.Keys);
                this.errorControls = new HashSet<DataGridViewRow>(errorRows.Values);
            }

            if (nodes)
            {
                childPos = new List<int>();
                foreach (var node in childNodes.Keys)
                {
                    childPos.Add(node.StartPos);
                }
                this.children = new List<BaseNode>(childNodes.Keys);
                this.childControls = new HashSet<TreeNode>(childNodes.Values);
            }
        }

        internal void ClearPendingErrors()
        {
            changingTbls = true;
            try
            {
                foreach (var err in pendingParseErrors)
                {
                    DataGridViewRow row = errorRows[err];
                    errorRows.Remove(err);
                    errorsTable.Rows.Remove(row);
                }

            }
            finally
            {
                changingTbls = false;
            }
            pendingParseErrors.Clear();
        }

        internal void UpdateTables(bool tokens)
        {
            changingTbls = true;
            try
            {
                if (parser != null)
                {
                    foreach (var err in parser.CompilationErrors)
                    {
                        AddError(err);
                    }
                    if (tokens)
                    {
                        foreach (var token in parser.Tokenizer.Result)
                        {
                            AddToken(token);
                        }
                    }
                }
            }
            finally
            {
                changingTbls = false;
            }
        }

        delegate void SimpleDelegate();
        void continueSelectTree()
        {
            try
            {
                if (treeView1.SelectedNode != null)
                {
                    BaseNode node = (BaseNode)treeView1.SelectedNode.Tag;
                    if (node != null && node.Parser == parser)
                    {
                        checkSource();

                        if (node.StartPos != -1)
                        {
                            Parser.Pos pos = parser.FindPos(node.StartPos);
                            Parser.Pos pos2 = parser.FindPos(node.EndPos);
                            if (pos2.Line - pos.Line > sourceTextBox.NumberOfVisibleLines)
                            {
                                sourceTextBox.GoTo(pos.Line, pos.Column, 1);
                            }
                            else
                            {
                                sourceTextBox.GoTo(pos.Line, pos.Column, node.Length);
                            }
                        }
                        else if (parser != null)
                        {
                            Console.WriteLine("Undefined node position : " + node.StartPos + ".." + node.EndPos + "  node:" + node);
                        }
                    }
                }
            }
            finally
            {
                _GoTo = false;
            }
        }

        internal abstract int AddError(CompilationErrorEnty err, bool pending = false);

        internal Parser.Pos AddToken(Token token)
        {
            Parser.Pos pos = parser.FindPos(token.TokenStartPos);
            int ti = tokensTable.Rows.Add(token.ContentString, token.Id, pos.Line, pos.Column, token.Length, token.TokenStartPos);
            tokenRows[token/*.TokenStartPos*/] = tokensTable.Rows[ti];
            return pos;
        }

        internal TreeNode CreateGuiNode0(BaseNode node, string title)
        {
            string title0 = title;

            if (title == null)
            {
                title = "";
            }
            else
            {
                title += " : ";
            }

            if (node is StatementNode)
            {
                StatementNode s = (StatementNode)node;
                title += s.Id + " st";
            }
            else if (node is XmlTag)
            {
                XmlTag s = (XmlTag)node;
                title += s.Name;
            }
            else if (node is ReflClassType)
            {
                title += "Class";
            }
            else if (node is ReflStructType)
            {
                title += "Struct";
            }
            else if (node is ReflInterfaceType)
            {
                title += "Interface";
            }
            else if (node is ReflDelegateType)
            {
                title += "Delegate";
            }
            else if (node is ReflEnumType)
            {
                title += "Enum";
            }
            else if (node is ReflPrimitiveType)
            {
                title += "Primitive";
            }
            else if (node is ReflGenericTypeArg)
            {
                title += "GenericArg";
            }
            else if (node is ReflArrayType)
            {
                title += "Array";
            }
            else if (node is ReflPointerType)
            {
                title += "Pointer";
            }
            else if (node is ReflAttribute)
            {
                title += "Attribute " + ((ReflAttribute)node).Name;
            }
            else if (node is ReflAttributeArg)
            {
                title += "A.Arg " + ((ReflAttributeArg)node).Name + "=" + ((ReflAttributeArg)node).Value;
            }
            else if (node is ReflMethodArg)
            {
                title += "M.Arg " + ((ReflMethodArg)node).MethodParameterName + "=" + ((ReflMethodArg)node).ActualValue;
            }
            else if (node is ReflMethodArgDefinition)
            {
                title += "M.ArgDef " + ((ReflMethodArgDefinition)node).MethodParameterName;
            }
            else if (node is ReflTypeArg)
            {
                title += "T.Arg " + ((ReflTypeArg)node).TypeParameterName + "=" + ((ReflTypeArg)node).ActualType.Name;
            }
            else if (node is ReflTypeArgDefinition)
            {
                title += "T.ArgDef " + ((ReflTypeArgDefinition)node).TypeParameterName;
            }
            else if (node is ReflEnumConstant)
            {
                title += "EnumConst " + ((ReflEnumConstant)node).Name+"="+((ReflEnumConstant)node).Value;
            }
            else
            {
                title += node.GetType().Name;
            }

            if (node is ReflMember)
            {
                title += " " + ((ReflMember)node).Name;
            }

            TreeNode treeNode = CreateTreeNode(title);
            treeNode.Tag = node;
            return treeNode;
        }

        internal TreeNode CreateGuiNode(MainBlockNode node)
        {
            TreeNode root = CreateGuiNode0(node, null);
            SetupGuiNode(root, node);
            return root;
        }

        internal TreeNode CreateGuiNode(XmlNode node)
        {
            TreeNode root = CreateGuiNode0(node, null);
            SetupGuiNode(root, node);
            return root;
        }

        internal TreeNode CreateGuiNode(ParseInfo node, string title)
        {
            TreeNode root = CreateGuiNode0(node, title);
            SetupGuiNode(root, node);
            return root;
        }

        internal TreeNode CreateGuiNode(ReflBaseNode node, string title)
        {
            TreeNode root = CreateGuiNode0(node, title);
            SetupGuiNode(root, node);
            return root;
        }


        internal void AddTextNode(TreeNode parentTn, string title)
        {
            TreeNode tn = CreateTreeNode(title);
            parentTn.Nodes.Add(tn);
        }

        internal void AddBlockNode(TreeNodeCollection parentTnNodes, BlockNode node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddStatementNode(TreeNodeCollection parentTnNodes, StatementNode node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddXmlNode(TreeNodeCollection parentTnNodes, XmlNode node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddXmlTag(TreeNodeCollection parentTnNodes, XmlTag node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddParseInfo(TreeNodeCollection parentTnNodes, ParseInfo node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddAttributeConstArg(TreeNodeCollection parentTnNodes, AttributeConstrArg node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddGenericConstraint(TreeNodeCollection parentTnNodes, GenericConstraint node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddReflNode(TreeNodeCollection parentTnNodes, ReflBaseNode node, string title)
        {
            if (node == null)
            {
                return;
            }
            TreeNode tn = CreateGuiNode0(node, title);
            SetupGuiNode(tn, node);
            parentTnNodes.Add(tn);
        }

        internal void AddParseNode(TreeNodeCollection parentTnNodes, BaseNode node)
        {
            if (node == null)
            {
                return;
            }
            if (node is XmlNode)
            {
                AddXmlNode(parentTnNodes, (XmlNode)node, "ParseNode");
            }
            else if (node is BlockNode)
            {
                AddBlockNode(parentTnNodes, (BlockNode)node, "ParseNode");
            }
            else if (node is StatementNode)
            {
                AddStatementNode(parentTnNodes, (StatementNode)node, "ParseNode");
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        internal void AddStatementNodes<T>(TreeNode parentTn, IList<T> nodes, string title) where T : StatementNode
        {
            if (nodes == null)
            {
                return;
            }
            TreeNode childNode = CreateTreeNode(title);
            parentTn.Nodes.Add(childNode);
            foreach (StatementNode child in nodes)
            {
                AddStatementNode(childNode.Nodes, child, null);
            }
        }

        internal void AddParseInfos(TreeNode parentTn, IList<ParseInfo> nodes, string title)
        {
            if (nodes == null)
            {
                return;
            }
            TreeNode childNode = CreateTreeNode(title);
            parentTn.Nodes.Add(childNode);
            foreach (ParseInfo child in nodes)
            {
                AddParseInfo(childNode.Nodes, child, null);
            }
        }

        internal void AddReflNodes<T>(TreeNode parentTn, IList<T> nodes, string title) where T : ReflBaseNode
        {
            if (nodes == null)
            {
                return;
            }
            TreeNode childNode = CreateTreeNode(title);
            parentTn.Nodes.Add(childNode);
            foreach (ReflBaseNode child in nodes)
            {
                AddReflNode(childNode.Nodes, child, null);
            }
        }

        internal void SetupGuiNode(TreeNode treeNode, BlockNode node)
        {
            if (node.ParseMode == BlockParseMode.ArrayInitializers)
            {
                treeNode.Text += " (a)";
            }
            SetupGuiNode(treeNode, (ControlNode)node);
            if (node.HeadStatement != null)
            {
                AddStatementNode(treeNode.Nodes, node.HeadStatement, "HeadStatement");
            }
            foreach (var child in node.ControlNodes)
            {
                if (child is BlockNode)
                {
                    AddBlockNode(treeNode.Nodes, (BlockNode)child, null);
                }
                else if (child != null)
                {
                    AddStatementNode(treeNode.Nodes, (StatementNode)child, null);
                }
                else
                {
                    AddTextNode(treeNode, "null");
                }
            }
        }

        internal void SetupGuiNode(TreeNode treeNode, StatementNode node)
        {
            switch (node.Id)
            {
                case StatementId.ForEachSub:
                    AddStatementNode(treeNode.Nodes, ((ForEachSubStatementNode)node).TypeSpecifier, "Type");
                    AddStatementNode(treeNode.Nodes, ((ForEachSubStatementNode)node).Right, "Right");
                    break;
                case StatementId.ClassDef:
                case StatementId.StructDef:
                case StatementId.InterfaceDef:
                    AddStatementNode(treeNode.Nodes, ((RefTypeDefStatementNode)node).TypeDefinitionMain, "Name");
                    AddStatementNodes(treeNode, ((RefTypeDefStatementNode)node).Supertypes, "Supertypes");
                    break;
                case StatementId.EnumDef:
                    AddTextNode(treeNode, "Name: " + ((EnumDefStatementNode)node).TypeName);
                    break;
                case StatementId.DelegateDef:
                    AddTextNode(treeNode, "Name: " + ((DelegateDefStatementNode)node).TypeName);
                    AddStatementNode(treeNode.Nodes, ((DelegateDefStatementNode)node).Signature, "Signature");
                    break;
                case StatementId.NamespaceDef:
                    AddTextNode(treeNode, "Id: " + ((NamespaceDefStatementNode)node).NamespaceId);
                    break;

                case StatementId.ArrayInitializer:
                    AddStatementNode(treeNode.Nodes, ((ArrayInitializerNode)node).InitializerExpression, "Initializer");
                    AddBlockNode(treeNode.Nodes, ((ArrayInitializerNode)node).InitializerBlock, "Initializer");
                    break;
                case StatementId.Case:
                    AddStatementNode(treeNode.Nodes, ((CaseStatementNode)node).Right, "Right");
                    break;
                case StatementId.Return:
                    AddStatementNode(treeNode.Nodes, ((ReturnStatementNode)node).Right, "Right");
                    break;
                case StatementId.Throw:
                    AddStatementNode(treeNode.Nodes, ((ThrowStatementNode)node).Right, "Right");
                    break;
                case StatementId.EnumConstant:
                    AddTextNode(treeNode, "Id: " + ((EnumConstantNode)node).IdentifierToken.ContentString);
                    AddStatementNode(treeNode.Nodes, ((EnumConstantNode)node).Right, "Right");
                    break;
                case StatementId.FieldDecl:
                    AddStatementNode(treeNode.Nodes, ((FieldDeclStatementNode)node).VarDeclHeader, "VarDeclHeader");
                    break;
                case StatementId.EventDecl:
                    AddStatementNode(treeNode.Nodes, ((EventDeclStatementNode)node).VarDeclHeader, "VarDeclHeader");
                    break;
                case StatementId.PropertyDef:
                    AddStatementNode(treeNode.Nodes, ((PropertyDefStatementNode)node).VarDeclHeader, "VarDeclHeader");
                    break;
                case StatementId.MethodDef:
                    AddTextNode(treeNode, "Id: " + ((MethodDefStatementNode)node).MethodId);
                    AddStatementNode(treeNode.Nodes, ((MethodDefStatementNode)node).TypeExpression, "Type");
                    AddStatementNodes(treeNode, ((MethodDefStatementNode)node).ArgumentList, "Arguments");
                    AddStatementNodes(treeNode, ((MethodDefStatementNode)node).TypeParameters, "TypeParameters");
                    break;
                case StatementId.IndexerDef:
                    AddStatementNode(treeNode.Nodes, ((IndexerDefStatementNode)node).TypeExpression, "Type");
                    AddStatementNodes(treeNode, ((IndexerDefStatementNode)node).ArgumentList, "Arguments");
                    break;
                case StatementId.ConstructorDef:
                    AddTextNode(treeNode, "Id: " + ((ConstructorDefStatementNode)node).MethodId);
                    AddStatementNodes(treeNode, ((ConstructorDefStatementNode)node).ArgumentList, "Arguments");
                    AddStatementNode(treeNode.Nodes, ((ConstructorDefStatementNode)node).BaseOrThisCall, "BaseOrThisCall");
                    break;
                case StatementId.DestructorDef:
                    AddTextNode(treeNode, "Id: " + ((DestructorDefStatementNode)node).MethodId);
                    AddStatementNodes(treeNode, ((DestructorDefStatementNode)node).ArgumentList, "Arguments");
                    break;
                case StatementId.VarDecl:
                    AddStatementNode(treeNode.Nodes, ((VarDeclStatementNode)node).TypeSpecifier, "Type");
                    AddStatementNodes(treeNode, ((VarDeclStatementNode)node).Variables, "Variables");
                    break;

                case StatementId.Argument:
                    AddTextNode(treeNode, "Type: " + ((ArgumentNode)node).Type);
                    AddStatementNode(treeNode.Nodes, ((ArgumentNode)node).Right, "Right");
                    break;
                case StatementId.ArrayAccess:
                    AddStatementNode(treeNode.Nodes, ((ArrayAccessExpressionNode)node).Left, "Left");
                    AddStatementNodes(treeNode, ((ArrayAccessExpressionNode)node).ParameterList, "Parameters");
                    break;
                case StatementId.ArrayCreation:
                    AddStatementNode(treeNode.Nodes, ((ArrayCreationNode)node).TypeExpression, "Type");
                    AddBlockNode(treeNode.Nodes, ((ArrayCreationNode)node).ArrayInitializerBlock, "Initializer");
                    break;
                case StatementId.BinaryOperation:
                    AddTextNode(treeNode, "Operator: " + ((BinaryOperationNode)node).OperatorToken.ContentString);
                    AddStatementNode(treeNode.Nodes, ((BinaryOperationNode)node).Left, "Left");
                    AddStatementNode(treeNode.Nodes, ((BinaryOperationNode)node).Right, "Right");
                    break;
                case StatementId.BinaryShiftOperation:
                    AddTextNode(treeNode, "Operator: " + (((BinaryShiftOperationNode)node).LeftShift ? "<<" : ">>"));
                    AddStatementNode(treeNode.Nodes, ((BinaryShiftOperationNode)node).Left, "Left");
                    AddStatementNode(treeNode.Nodes, ((BinaryShiftOperationNode)node).Right, "Right");
                    break;
                case StatementId.Cast:
                    AddStatementNode(treeNode.Nodes, ((CastExpressionNode)node).TypeSpecifier, "Type");
                    AddStatementNode(treeNode.Nodes, ((CastExpressionNode)node).Right, "Right");
                    break;
                case StatementId.Checked:
                    AddStatementNode(treeNode.Nodes, ((CheckedNode)node).InnerExpression, "Inner");
                    break;
                case StatementId.DefaultExpression:
                    AddStatementNode(treeNode.Nodes, ((DefaultExpressionNode)node).InnerExpression, "Inner");
                    break;
                case StatementId.FieldAccessOrId:
                    AddTextNode(treeNode, "Id: " + ((FieldAccessOrIdNode)node).FieldIdentifier.ContentString);
                    AddStatementNode(treeNode.Nodes, ((FieldAccessOrIdNode)node).Left, "Left");
                    break;
                case StatementId.AtomicExpression:
                    AddTextNode(treeNode, "Id: " + ((AtomicExpressionNode)node).FieldIdentifier.ContentString);
                    break;
                case StatementId.MethodExpression:
                    AddTextNode(treeNode, "Id: " + ((MethodExpressionNode)node).MethodIdentifier.ContentString);
                    AddStatementNode(treeNode.Nodes, ((MethodExpressionNode)node).Left, "Left");
                    AddStatementNodes(treeNode, ((MethodExpressionNode)node).TypeParameters, "TypeParameters");
                    AddStatementNodes(treeNode, ((MethodExpressionNode)node).ArgumentList, "Arguments");
                    break;
                case StatementId.ObjectCreation:
                    AddStatementNode(treeNode.Nodes, ((ObjectCreationNode)node).TypeExpression, "Type");
                    AddStatementNodes(treeNode, ((ObjectCreationNode)node).ArgumentList, "Arguments");
                    break;
                case StatementId.Paren:
                    AddStatementNode(treeNode.Nodes, ((ParenExpressionNode)node).Inner, "Inner");
                    break;
                case StatementId.TernaryOperation:
                    AddTextNode(treeNode, "Operators: " + ((TernaryOperationNode)node).OperatorToken1.ContentString + " " + ((TernaryOperationNode)node).OperatorToken2.ContentString);
                    AddStatementNode(treeNode.Nodes, ((TernaryOperationNode)node).Left, "Left");
                    AddStatementNode(treeNode.Nodes, ((TernaryOperationNode)node).Middle, "Middle");
                    AddStatementNode(treeNode.Nodes, ((TernaryOperationNode)node).Right, "Right");
                    break;
                case StatementId.Typeof:
                    AddStatementNode(treeNode.Nodes, ((TypeofNode)node).InnerExpression, "Inner");
                    break;
                case StatementId.UnaryOperation:
                    AddTextNode(treeNode, "Operator: " + ((UnaryOperationNode)node).OperatorToken.ContentString);
                    AddStatementNode(treeNode.Nodes, ((UnaryOperationNode)node).Right, "Right");
                    break;
                case StatementId.Unchecked:
                    AddStatementNode(treeNode.Nodes, ((UncheckedNode)node).InnerExpression, "Inner");
                    break;
                case StatementId.SystemType:
                case StatementId.UserType:
                    AddTextNode(treeNode, "Name: " + ((TypeSpecifierNode)node).Name);
                    string inf = "";
                    if (((TypeSpecifierNode)node).IsArray)
                    {
                        inf += "array ";
                    }
                    if (((TypeSpecifierNode)node).IsPointer)
                    {
                        inf += "pointer ";
                    }
                    if (((TypeSpecifierNode)node).IsGeneric)
                    {
                        inf += "generic";
                    }
                    if (inf == "")
                    {
                        inf = "plain type";
                    }
                    AddTextNode(treeNode, "info: " + inf);
                    AddTextNode(treeNode, "CS Ranks: " + ((TypeSpecifierNode)node).CommaSeparatedRanks);
                    AddTextNode(treeNode, "Empty Ranks: " + ((TypeSpecifierNode)node).EmptyRanks);
                    if (((TypeSpecifierNode)node).PointerStar != null)
                    {
                        AddTextNode(treeNode, "Pointer star: " + ((TypeSpecifierNode)node).PointerStar.ContentString);
                    }
                    if (node.Id == StatementId.UserType)
                    {
                        AddStatementNodes(treeNode, ((UserTypeSpecifierNode)node).TypeParameters, "TypeParameters");
                    }
                    AddStatementNodes(treeNode, ((TypeSpecifierNode)node).FirstBracketExpressions, "FirstBracketExpressions");
                    break;
                case StatementId.UsingDecl:
                    AddTextNode(treeNode, "Id: " + ((UsingDeclStatementNode)node).Name);
                    break;
                case StatementId.VarDeclArgument:
                    AddTextNode(treeNode, "Type: " + ((VarDeclArgumentNode)node).Type);
                    AddStatementNode(treeNode.Nodes, ((VarDeclArgumentNode)node).Right, "Right");
                    break;
                case StatementId.Linq:
                    AddStatementNodes(treeNode, ((LinqExpressionNode)node).FromLetWheres, "FromLetWheres");
                    AddStatementNode(treeNode.Nodes, ((LinqExpressionNode)node).OrderBy, "OrderBy");
                    AddStatementNode(treeNode.Nodes, ((LinqExpressionNode)node).SelectGroup, "SelectGroup");
                    AddStatementNode(treeNode.Nodes, ((LinqExpressionNode)node).Continuation, "Continuation");
                    break;
                case StatementId.LinqContinuation:
                    AddStatementNode(treeNode.Nodes, ((LinqContinuationExpressionNode)node).IntoIdentifier, "IntoIdentifier");
                    AddStatementNodes(treeNode, ((LinqContinuationExpressionNode)node).Joins, "Joins");
                    AddStatementNode(treeNode.Nodes, ((LinqContinuationExpressionNode)node).LinqExpression, "LinqExpression");
                    break;
                case StatementId.LinqFrom:
                    AddStatementNode(treeNode.Nodes, ((LinqFromExpressionNode)node).TypeSpecifier, "TypeSpecifier");
                    AddStatementNode(treeNode.Nodes, ((LinqFromExpressionNode)node).Identifier, "Identifier");
                    AddStatementNode(treeNode.Nodes, ((LinqFromExpressionNode)node).InExpression, "InExpression");
                    AddStatementNodes(treeNode, ((LinqFromExpressionNode)node).Joins, "Joins");
                    break;
                case StatementId.LinqGroupBy:
                    AddStatementNode(treeNode.Nodes, ((LinqGroupByExpressionNode)node).GroupExpression, "GroupExpression");
                    AddStatementNode(treeNode.Nodes, ((LinqGroupByExpressionNode)node).ByExpression, "ByExpression");
                    break;
                case StatementId.LinqJoin:
                    AddStatementNode(treeNode.Nodes, ((LinqJoinExpressionNode)node).TypeSpecifier, "TypeSpecifier");
                    AddStatementNode(treeNode.Nodes, ((LinqJoinExpressionNode)node).Identifier, "Identifier");
                    AddStatementNode(treeNode.Nodes, ((LinqJoinExpressionNode)node).InExpression, "InExpression");
                    AddStatementNode(treeNode.Nodes, ((LinqJoinExpressionNode)node).OnExpression, "OnExpression");
                    AddStatementNode(treeNode.Nodes, ((LinqJoinExpressionNode)node).OnEqualsExpression, "OnEqualsExpression");
                    AddStatementNode(treeNode.Nodes, ((LinqJoinExpressionNode)node).IntoIdentifier, "IntoIdentifier");
                    break;
                case StatementId.LinqLet:
                    AddStatementNode(treeNode.Nodes, ((LinqLetExpressionNode)node).Identifier, "Identifier");
                    AddStatementNode(treeNode.Nodes, ((LinqLetExpressionNode)node).Expression, "Expression");
                    break;
                case StatementId.LinqOrderBy:
                    AddStatementNodes(treeNode, ((LinqOrderByExpressionNode)node).Orderings, "Orderings");
                    break;
                case StatementId.LinqOrdering:
                    AddStatementNode(treeNode.Nodes, ((LinqOrderingExpressionNode)node).Expression, "Expression");
                    AddTextNode(treeNode, "Type: " + ((LinqOrderingExpressionNode)node).Type);
                    break;
                case StatementId.LinqSelect:
                    AddStatementNode(treeNode.Nodes, ((LinqSelectExpressionNode)node).SelectExpression, "SelectExpression");
                    break;
                case StatementId.LinqWhere:
                    AddStatementNode(treeNode.Nodes, ((LinqWhereExpressionNode)node).Expression, "Expression");
                    break;

            }

            SetupGuiNode(treeNode, (ControlNode)node);
            if (node.SubStatementsBlock != null)
            {
                AddBlockNode(treeNode.Nodes, node.SubStatementsBlock, "SubStatements");
            }
            if (node.TailStatement != null)
            {
                TreeNode tailNode = CreateGuiNode0(node.TailStatement, "Tail");
                SetupGuiNode(tailNode, node.TailStatement);
                treeNode.Nodes.Add(tailNode);
            }
        }

        internal void SetupGuiNode(TreeNode treeNode, ControlNode node)
        {
            SetupGuiNode(treeNode, (BaseNode)node);
            if (node.Attributes != null)
            {
                TreeNode attrsNode = CreateTreeNode("Attributes");
                treeNode.Nodes.Add(attrsNode);
                foreach (AttributeNode attr in node.Attributes)
                {
                    TreeNode attrNode = CreateGuiNode0(attr, null);
                    SetupGuiNode(attrNode, attr);
                    attrsNode.Nodes.Add(attrNode);
                }
            }
            if (node.Modifiers != null)
            {
                TreeNode modfrsNode = CreateTreeNode("Modifiers");
                treeNode.Nodes.Add(modfrsNode);
                foreach (ModifierNode modfr in node.Modifiers)
                {
                    TreeNode modfrNode = CreateGuiNode0(modfr, null);
                    SetupGuiNode(modfrNode, modfr);
                    modfrsNode.Nodes.Add(modfrNode);
                }
            }
        }

        internal void SetupGuiNode(TreeNode treeNode, XmlNode node)
        {
            SetupGuiNode(treeNode, (BaseNode)node);

            if (node.OpenTag != node.CloseTag)
            {
                AddXmlTag(treeNode.Nodes, node.OpenTag, "Open");
                AddXmlTag(treeNode.Nodes, node.CloseTag, "Close");
            }
            else
            {
                AddXmlTag(treeNode.Nodes, node.OpenTag, "Single");
            }

            foreach (var child in node.Children)
            {
                AddXmlNode(treeNode.Nodes, child, null);
            }
        }

        internal void SetupGuiNode(TreeNode treeNode, XmlTag node)
        {
            SetupGuiNode(treeNode, (BaseNode)node);

            foreach (var a in node.Attributes)
            {
                AddTextNode(treeNode, a.Key + "=" + a.Value);
            }
        }

        void SetupGuiNode(TreeNode treeNode, ParseInfo node)
        {
            AddTextNode(treeNode, "TypeId:" + node.TypeId);
        }

        internal void SetupAfterSelect(TreeNode treeNode, ParseInfo node)
        {
            Console.WriteLine("SetupAfterSelect : " + treeNode.Text);
            SetupGuiNode(treeNode, (ParserBuf)node);

            AddTextNode(treeNode, "FullName:" + node.FullName);
            AddTextNode(treeNode, "GenericId:" + node.GenericId);
            AddTextNode(treeNode, "IsArray:" + node.IsArray);
            AddTextNode(treeNode, "IsPointer:" + node.IsPointer);
            AddTextNode(treeNode, "IsGeneric:" + node.IsGeneric);
            AddTextNode(treeNode, "IsGenericArg:" + node.IsGenericArg);
            AddTextNode(treeNode, "IsAttribute:" + node.IsAttribute);
            AddTextNode(treeNode, "TpType:" + node.TpType);
            AddTextNode(treeNode, "Namespace:" + node.Namespace);
            AddTextNode(treeNode, "Name:" + node.Name);
            AddTextNode(treeNode, "Id:" + node.Id);
            AddTextNode(treeNode, "Dims:" + node.Dims);
            AddTextNode(treeNode, "AttrConstrId:" + node.AttrConstrId);
            AddTextNode(treeNode, "Modifiers:" + node.Modifiers);

            AddParseInfo(treeNode.Nodes, node.BaseType, "BaseType");
            AddParseInfos(treeNode, node.BaseInterfaces, "BaseInterfaces");
            AddParseInfo(treeNode.Nodes, node.ElementType, "ElementType");
            AddParseInfos(treeNode, node.GenericArgs, "GenericArgs");
            AddParseInfos(treeNode, node.Attributes, "Attributes");

            if (node.GenericConstraints != null)
            {
                foreach (var gc in node.GenericConstraints)
                {
                    AddGenericConstraint(treeNode.Nodes, gc, null);
                }
            }
            if (node.EnumConstants != null)
            {
                foreach (var ec in node.EnumConstants)
                {
                    AddTextNode(treeNode, ec.EnumConstName + "=" + ec.EnumConstValue);
                }
            }
            if (node.AttributeConstrArgs != null)
            {
                foreach (var ac in node.AttributeConstrArgs)
                {
                    AddAttributeConstArg(treeNode.Nodes, ac, null);
                }
            }
            if (node.AttributeNamedArgs != null)
            {
                foreach (var ac in node.AttributeNamedArgs)
                {
                    AddAttributeConstArg(treeNode.Nodes, ac, null);
                }
            }

            if (node.Children != null)
            {
                foreach (var e in node.Children)
                {
                    AddParseInfo(treeNode.Nodes, e.Value, "Child " + e.Key);
                }
            }
            if (node.UnparsedMembers != null)
            {
                foreach (var um in node.UnparsedMembers)
                {
                    AddXmlNode(treeNode.Nodes, um, "Member");
                }
            }
        }

        void SetupGuiNode(TreeNode treeNode, AttributeConstrArg node)
        {
            SetupGuiNode(treeNode, (ParserBuf)node);

            AddTextNode(treeNode, node.Property + "=" + node.Value);

            AddParseInfo(treeNode.Nodes, node.Type, "Type");
        }

        void SetupGuiNode(TreeNode treeNode, GenericConstraint node)
        {
            SetupGuiNode(treeNode, (ParserBuf)node);

            AddTextNode(treeNode, "Rule:" + node.Rule);

            AddParseInfo(treeNode.Nodes, node.Type, "Type");
        }

        void SetupGuiNode(TreeNode treeNode, ParserBuf node)
        {
            SetupGuiNode(treeNode, (BaseNode)node);

            AddParseNode(treeNode.Nodes, node.ParseNode);
        }

        internal void SetupGuiNode(TreeNode treeNode, ReflBaseNode node)
        {
            AddTextNode(treeNode, "Loading " + node.ReflTypeId);
        }

        internal void SetupAfterSelect(TreeNode treeNode, ReflBaseNode node)
        {
            treeNode.Nodes.Clear();
            Console.WriteLine("SetupAfterSelect : " + treeNode.Text);
            switch (node.ReflTypeId)
            {
                case ReflTypeId.ArrayType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddTextNode(treeNode, "Dims:" + ((ReflArrayType)node).Dims);
                    AddReflNode(treeNode.Nodes, ((ReflArrayType)node).ElementType, "ElementType");
                    break;
                case ReflTypeId.Attribute:
                    AddTextNode(treeNode, "Name:" + ((ReflAttribute)node).Name);
                    AddReflNode(treeNode.Nodes, ((ReflAttribute)node).AttributeConstr, "AttributeConstr");
                    AddReflNodes(treeNode, ((ReflAttribute)node).Args, "Args");
                    AddReflNodes(treeNode, ((ReflAttribute)node).NamedArgs, "NamedArgs");
                    AddReflNode(treeNode.Nodes, ((ReflAttribute)node).DeclaringType, "DeclaringType");
                    break;
                case ReflTypeId.AttributeArg:
                    AddTextNode(treeNode, "ArgIndex:" + ((ReflAttributeArg)node).ArgIndex);
                    AddTextNode(treeNode, "ArgProperty:" + ((ReflAttributeArg)node).ArgProperty);
                    AddTextNode(treeNode, "Value:" + ((ReflAttributeArg)node).Value);
                    AddReflNode(treeNode.Nodes, ((ReflAttributeArg)node).MethodArg, "MethodArg");
                    break;
                case ReflTypeId.ClassType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgDefs, "GenericTypeArgDefs");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgs, "GenericTypeArgs");
                    AddReflNodes(treeNode, ((ReflObjType)node).BaseInterfaces, "BaseInterfaces");
                    AddReflNodes(treeNode, ((ReflObjType)node).Members, "Members");
                    AddReflNode(treeNode.Nodes, ((ReflClassType)node).BaseClass, "BaseClass");
                    break;
                case ReflTypeId.Constructor:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    break;
                case ReflTypeId.DelegateType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgDefs, "GenericTypeArgDefs");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgs, "GenericTypeArgs");
                    AddReflNode(treeNode.Nodes, ((ReflDelegateType)node).InvokeMethod, "InvokeMethod");
                    break;
                case ReflTypeId.EnumConstant:
                    AddTextNode(treeNode, "Name:" + ((ReflEnumConstant)node).Name);
                    AddTextNode(treeNode, "Value:" + ((ReflEnumConstant)node).Value);
                    AddReflNode(treeNode.Nodes, ((ReflEnumConstant)node).DeclaringType, "DeclaringType");
                    break;
                case ReflTypeId.EnumType: 
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNode(treeNode.Nodes, ((ReflEnumType)node).BaseType, "BaseType");
                    AddReflNodes(treeNode, ((ReflEnumType)node).EnumConstants, "EnumConstants");
                    break;
                case ReflTypeId.Event:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNode(treeNode.Nodes, ((ReflEvent)node).EventType, "EventType");
                    break;
                case ReflTypeId.EventAdd:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNode(treeNode.Nodes, ((ReflEventAdd)node).Parent, "Parent");
                    break;
                case ReflTypeId.EventRemove:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNode(treeNode.Nodes, ((ReflEventRemove)node).Parent, "Parent");
                    break;
                case ReflTypeId.Field:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "IsConstant:" + ((ReflField)node).IsConstant);
                    AddTextNode(treeNode, "ConstantValue:" + ((ReflField)node).ConstantValue);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNode(treeNode.Nodes, ((ReflField)node).FieldType, "FieldType");
                    break;
                case ReflTypeId.GenericTypeArg:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    break;
                case ReflTypeId.IndexerGet:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNode(treeNode.Nodes, ((ReflIndexerGet)node).Parent, "Parent");
                    break;
                case ReflTypeId.IndexerProperty:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "SetModifiers:" + ((ReflIndexerProperty)node).SetModifiers);
                    AddTextNode(treeNode, "PropertyMethodType:" + ((ReflIndexerProperty)node).PropertyMethodType);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNode(treeNode.Nodes, ((ReflIndexerProperty)node).PropertyType, "PropertyType");
                    AddReflNodes(treeNode, ((ReflIndexerProperty)node).IndexerArgs, "IndexerArgs");
                    break;
                case ReflTypeId.IndexerSet:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNode(treeNode.Nodes, ((ReflIndexerSet)node).Parent, "Parent");
                    break;
                case ReflTypeId.InterfaceType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgDefs, "GenericTypeArgDefs");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgs, "GenericTypeArgs");
                    AddReflNodes(treeNode, ((ReflObjType)node).BaseInterfaces, "BaseInterfaces");
                    AddReflNodes(treeNode, ((ReflObjType)node).Members, "Members");
                    break;
                case ReflTypeId.Method:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNodes(treeNode, ((ReflMethodBaseGen)node).GenericTypeArgDefs, "GenericTypeArgDefs");
                    AddReflNode(treeNode.Nodes, ((ReflMethod)node).ReturnType, "ReturnType");
                    break;
                case ReflTypeId.MethodArgDefinition:
                    AddTextNode(treeNode, "MethodParameterName:" + ((ReflMethodArgDefinition)node).MethodParameterName);
                    AddTextNode(treeNode, "ArgType:" + ((ReflMethodArgDefinition)node).ArgType);
                    AddReflNode(treeNode.Nodes, ((ReflMethodArgDefinition)node).ParameterType, "ParameterType");
                    break;
                case ReflTypeId.PointerType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNode(treeNode.Nodes, ((ReflPointerType)node).ElementType, "ElementType");
                    break;
                case ReflTypeId.Property:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "SetModifiers:" + ((ReflProperty)node).SetModifiers);
                    AddTextNode(treeNode, "PropertyMethodType:" + ((ReflProperty)node).PropertyMethodType);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNode(treeNode.Nodes, ((ReflProperty)node).PropertyType, "PropertyType");
                    break;
                case ReflTypeId.PrimitiveType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    break;
                case ReflTypeId.PropertyGet:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNode(treeNode.Nodes, ((ReflPropertyGet)node).Parent, "Parent");
                    break;
                case ReflTypeId.PropertySet:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflMethodBase)node).MethodArgs, "MethodArgs");
                    AddReflNode(treeNode.Nodes, ((ReflPropertySet)node).Parent, "Parent");
                    break;
                case ReflTypeId.StructType:
                    AddTextNode(treeNode, "IsTopLevel:" + ((ReflMember)node).IsTopLevel);
                    AddTextNode(treeNode, "Id:" + ((ReflMember)node).Id);
                    AddTextNode(treeNode, "NamespaceId:" + ((ReflMember)node).NamespaceId);
                    AddTextNode(treeNode, "MemberName:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Name:" + ((ReflMember)node).Name);
                    AddTextNode(treeNode, "Modifiers:" + ((ReflMember)node).Modifiers);
                    AddReflNodes(treeNode, ((ReflMember)node).Attributes, "Attributes");
                    AddTextNode(treeNode, "FullName:" + ((ReflType)node).FullName);
                    AddTextNode(treeNode, "TypeId:" + ((ReflType)node).TypeId);
                    AddTextNode(treeNode, "GenericId:" + ((ReflType)node).GenericId);
                    AddReflNode(treeNode.Nodes, ((ReflMember)node).DeclaringType, "DeclaringType");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgDefs, "GenericTypeArgDefs");
                    AddReflNodes(treeNode, ((ReflCallableType)node).GenericTypeArgs, "GenericTypeArgs");
                    AddReflNodes(treeNode, ((ReflObjType)node).BaseInterfaces, "BaseInterfaces");
                    AddReflNodes(treeNode, ((ReflObjType)node).Members, "Members");
                    break;
                case ReflTypeId.TypeArgDefinition:
                    AddTextNode(treeNode, "TypeParameterName:" + ((ReflTypeArgDefinition)node).TypeParameterName);
                    AddReflNodes(treeNode, ((ReflTypeArgDefinition)node).GenericTypeArgRules, "GenericTypeArgRules");
                    break;
                case ReflTypeId.TypeArgRule:
                    AddTextNode(treeNode, "SpecialRule:" + ((ReflTypeArgRule)node).SpecialRule);
                    AddReflNode(treeNode.Nodes, ((ReflTypeArgRule)node).SingleType, "SingleType");
                    break;
                case ReflTypeId.MethodArg:
                    AddTextNode(treeNode, "ActualValue:" + ((ReflMethodArg)node).ActualValue);
                    AddReflNode(treeNode.Nodes, ((ReflMethodArg)node).Definition, "Definition");
                    break;
                case ReflTypeId.TypeArg:
                    AddReflNode(treeNode.Nodes, ((ReflTypeArg)node).ActualType, "ActualType");
                    AddReflNode(treeNode.Nodes, ((ReflTypeArg)node).Definition, "Definition");
                    break;
            }

            AddParseNode(treeNode.Nodes, node.ParseNode);
        }

        internal void SetupGuiNode(TreeNode treeNode, BaseNode node)
        {
            Parser.Pos pos = parser.FindPos(node.StartPos);
            treeNode.Text += " " + pos.Line + ":" + pos.Column;
            childNodes[node/*.StartPos*/] = treeNode;
        }

        internal void GoTo(Parser.Pos pos, int length)
        {
            _GoTo = true;
            try
            {
                sourceTextBox.GoTo(pos.Line, pos.Column, length);
            }
            finally
            {
                _GoTo = false;
            }
        }

        internal void GoTo(CompilationErrorEnty err)
        {
            _GoTo = true;
            try
            {
                sourceTextBox.GoTo(err.Line, err.Column, err.Length);
            }
            finally
            {
                _GoTo = false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_GoTo && treeView1.SelectedNode != null && treeView1 == e.Node.TreeView)
            {
                BaseNode node = (BaseNode)treeView1.SelectedNode.Tag;
                if (node != null && node.Parser == parser)
                {
                    _GoTo = true;
                    treeView1.BeginInvoke(new SimpleDelegate(continueSelectTree));
                }
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            BaseNode node = (BaseNode)e.Node.Tag;
            if (node != null && node.Parser == parser)
            {
                if (node is ReflBaseNode)
                {
                    ReflBaseNode rnode = (ReflBaseNode)node;
                    if (!nodesSetUp.Contains(e.Node))
                    {
                        nodesSetUp.Add(e.Node);
                        SetupAfterSelect(e.Node, rnode);
                    }
                }
                else if (node is ParseInfo)
                {
                    ParseInfo pnode = (ParseInfo)node;
                    if (!nodesSetUp.Contains(e.Node))
                    {
                        nodesSetUp.Add(e.Node);
                        SetupAfterSelect(e.Node, pnode);
                    }
                }
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            if (!_GoTo)
            {
                _GoTo = true;
                continueSelectTree();
            }
        }



        private void errorsTable_SelectionChanged(object sender, EventArgs e)
        {
            if (!_GoTo && !changingTbls)
            {
                _GoTo = true;
                try
                {
                    if (!errorControls.Contains(errorsTable.CurrentRow))
                    {
                        return;
                    }
                    checkSource();

                    int line = (int)errorsTable.CurrentRow.Cells[1].Value;
                    int column = (int)errorsTable.CurrentRow.Cells[2].Value;
                    int len = (int)errorsTable.CurrentRow.Cells[3].Value;
                    sourceTextBox.GoTo(line, column, len);
                }
                finally
                {
                    _GoTo = false;
                }
            }
        }

        private void tokensTable_SelectionChanged(object sender, EventArgs e)
        {
            if (!_GoTo && !changingTbls)
            {
                _GoTo = true;
                try
                {
                    if (!tokenControls.Contains(tokensTable.CurrentRow))
                    {
                        return;
                    }
                    checkSource();

                    int line = (int)tokensTable.CurrentRow.Cells[2].Value;
                    int column = (int)tokensTable.CurrentRow.Cells[3].Value;
                    int len = (int)tokensTable.CurrentRow.Cells[4].Value;
                    sourceTextBox.GoTo(line, column, len);
                }
                finally
                {
                    _GoTo = false;
                }
            }
        }

        private void errorsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorsTable_SelectionChanged(null, null);
        }

        private void tokensTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tokensTable_SelectionChanged(null, null);
        }

        private void sourceTextBox_SelectionChanged(object sender, EventArgs e)
        {
            posLabel.Text = "Pos: " + sourceTextBox.SelectionStart;
            lineLabel.Text = "Line: " + sourceTextBox.CurrentLine;
            columnLabel.Text = "Col: " + sourceTextBox.CurrentColumn;
            if (!_GoTo && !changingTbls)
            {
                _GoTo = true;
                try
                {
                    if (parser == null)
                    {
                        return;
                    }
                    if (fileTextBox != null && !fileTextBox.Text.Equals(parser.FileName))
                    {
                        return;
                    }

                    int pos = sourceTextBox.SelectionStart;
                    int ind;

                    if (childNodes.Count > 0)
                    {
                        ind = childPos.BinarySearch(pos);
                        if (ind < 0)
                        {
                            ind = ~ind - 1;
                            if (ind < 0)
                            {
                                ind = 0;
                            }
                        }
                        if (ind >= childNodes.Count)
                        {
                            ind = childNodes.Count - 1;
                        }
                        treeView1.SelectedNode = childNodes[children[ind]];

                        BaseNode node0 = (BaseNode)treeView1.SelectedNode.Tag;
                        bool sel = node0.StartPos <= pos && pos <= node0.EndPos;

                        if (!sel)
                        {
                            TreeNode parent = treeView1.SelectedNode.Parent;
                            List<TreeNode> parents = new List<TreeNode>();
                            while (parent != null)
                            {
                                BaseNode node = (BaseNode)parent.Tag;
                                if (node != null)
                                {
                                    if (pos == node.EndPos)
                                    {
                                        treeView1.SelectedNode = parent;
                                        sel = true;
                                        break;
                                    }
                                    parents.Add(parent);
                                }
                                parent = parent.Parent;
                            }
                            if (!sel)
                            {
                                parents.Reverse();
                                foreach (var p in parents)
                                {
                                    BaseNode node = (BaseNode)p.Tag;
                                    if (pos >= node.EndPos)
                                    {
                                        treeView1.SelectedNode = p;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (tokensTable != null && tokenPos != null && tokenRows.Count > 0)
                    {
                        ind = tokenPos.BinarySearch(pos);
                        if (ind < 0)
                        {
                            ind = ~ind - 1;
                            if (ind < 0)
                            {
                                ind = 0;
                            }
                        }
                        if (ind >= tokenRows.Count)
                        {
                            ind = tokenRows.Count - 1;
                        }
                        tokensTable.CurrentCell = tokenRows[tokens[ind]].Cells[0];
                    }

                    if (errorPos != null && errorRows.Count > 0)
                    {
                        ind = errorPos.BinarySearch(pos);
                        if (ind < 0)
                        {
                            ind = ~ind - 1;
                            if (ind < 0)
                            {
                                ind = 0;
                            }
                        }
                        if (ind >= errorRows.Count)
                        {
                            ind = errorRows.Count - 1;
                        }
                        errorsTable.CurrentCell = errorRows[errors[ind]].Cells[0];
                    }
                }
                finally
                {
                    _GoTo = false;
                }
            }
        }

        void checkSource()
        {
            if (parser == null)
            {
                return;
            }
            if (fileTextBox != null && !fileTextBox.Text.Equals(parser.FileName))
            {
                fileTextBox.Text = parser.FileName;
                sourceTextBox.Text = parser.FileContent;
            }
        }

        TreeNode CreateTreeNode(string text)
        {
            //TreeNode result = new WorkingTreeNode(text);
            TreeNode result = new TreeNode(text); 
            return result;
        }
    }

    public class WorkingTreeNode : TreeNode
    {
        static int cntId = 0;
        int id = 0;

        public WorkingTreeNode(string text)
            : base(text)
        {
            id = cntId++;
        }

        public override bool Equals(object obj)
        {
            if (obj is WorkingTreeNode)
            {
                return this.id == ((WorkingTreeNode)obj).id;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.id;
        }
    }
}
