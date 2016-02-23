using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Sharp.AST
{
    class UsingDirectiveAST
    {
        internal UsingDirectiveAST(Token token)
        {
            Reduction node = (Reduction)token.Data;

            if (node.Parent.Head().Name() != "Using Directive")
            {
                throw new NotSupportedException(node.Parent.Head().Name());
            }

            int ind0;
            switch (node.Count())
            {
                case 5:
                    identifier = (string)node[1].Data;
                    ind0 = 3;
                    break;
                case 3:
                    ind0 = 1;
                    break;
                default:
                    throw new NotSupportedException("" + node.Count());
            }

            StringBuilder namespaceB = new StringBuilder();
            memberName = parseNamespaceAndMember(node[ind0], namespaceB);

            _namespace = namespaceB.ToString();
        }

        string parseNamespaceAndMember(Token token, StringBuilder namespaceB)
        {
            Reduction node = (Reduction)token.Data;

            if (node.Parent.Head().Name() != "Qualified ID")
            {
                throw new NotSupportedException(node.Parent.Head().Name());
            }

            Token validIdNd = node[0];
            Reduction validIdIdNd = (Reduction)validIdNd.Data;
            string ns0 = (string)validIdIdNd[0].Data;

            Token memberListNd = node[1];
            string lastMember = parseNamespaceMember(memberListNd, namespaceB);
            if (lastMember == null)
            {
                return ns0;
            }
            else
            {
                namespaceB.Insert(0, ns0);
                return lastMember;
            }
        }

        string parseNamespaceMember(Token token, StringBuilder namespaceB)
        {
            Reduction node = (Reduction)token.Data;

            if (node.Parent.Head().Name() != "Member List")
            {
                throw new NotSupportedException(node.Parent.Head().Name());
            }
            if (node.Count() > 0)
            {
                Token memberListNd = node[0];
                string member = parseNamespaceMember(memberListNd, namespaceB);
                if (member != null)
                {
                    namespaceB.Append(member);
                }

                Token memberNameNd = node[1];
                return (string)memberNameNd.Data;
            }
            else
            {
                return null;
            }
        }


        // using Identifier '=' <Qualified ID> ';'
        readonly string identifier;
        public string Identifier
        {
            get
            {
                return identifier;
            }
        }

        readonly string _namespace;
        public string Namespace
        {
            get
            {
                return _namespace;
            }
        }

        readonly string memberName;
        public string MemberName
        {
            get
            {
                return memberName;
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("using ");
            if (identifier != null)
            {
                b.Append(identifier);
                b.Append(" = "); 
            }
            if (_namespace != null)
            {
                b.Append(_namespace);
            }
            b.Append(memberName);
            b.Append(";");
            return b.ToString();
        }
    }
}
