using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Sharp.AST
{
    class CompilationUnitAST
    {
        internal CompilationUnitAST(Reduction node)
        {
            if (node.Parent.Head().Name() != "Compilation Unit")
            {
                throw new NotSupportedException(node.Parent.Head().Name());
            }
            addUsing(node[0]);
        }

        void addUsing(Token token)
        {
            Reduction node = (Reduction)token.Data;

            if (node.Parent.Head().Name() != "Using List")
            {
                throw new NotSupportedException(node.Parent.Head().Name());
            }
            if (node.Count() > 0)
            {
                Token usingListNd = node[0];
                Token usingDirectiveNd = node[1];
                addUsing(usingListNd);
                UsingDirectiveAST item = new UsingDirectiveAST(usingDirectiveNd);
                usingDirectives.Add(item);
            }
        }

        readonly List<UsingDirectiveAST> usingDirectives = new List<UsingDirectiveAST>();
        public IList<UsingDirectiveAST> UsingDirectives
        {
            get
            {
                return usingDirectives.AsReadOnly();
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            foreach (var u in usingDirectives)
            {
                b.Append(u);
                b.AppendLine();
            }
            return b.ToString();
        }
    }

}
