using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Types
{

    public abstract class BaseVINode
    {
        internal readonly int Level;
        internal readonly int Priority;

        protected BaseVINode(int level, int priority)
        {
            this.Level = level;
            this.Priority = priority;
        }

        internal abstract void Add(BaseVINode item);
    }

    public class VINode<P> : BaseVINode
        where P : BaseVINode
    {
        internal static readonly Dictionary<int, List<BaseVINode>> LevelNodes = new Dictionary<int, List<BaseVINode>>();

        static void AddNode(BaseVINode node)
        {
            List<BaseVINode> l;
            if (!LevelNodes.TryGetValue(node.Level, out l))
            {
                l = new List<BaseVINode>();
                LevelNodes.Add(node.Level, l);
            }
            l.Add(node);
        }

        protected readonly P parentNode;
        protected readonly List<BaseVINode> childNodes = new List<BaseVINode>();
        protected readonly SortedDictionary<int, List<BaseVINode>> childrenByPriority = new SortedDictionary<int, List<BaseVINode>>();

        public VINode(P parentNode, int level, int priority)
            : base(level, priority)
        {
            this.parentNode = parentNode;
            this.parentNode.Add(this);
            AddNode(this);
        }

        internal sealed override void Add(BaseVINode node)
        {
            List<BaseVINode> cp;
            if (!childrenByPriority.TryGetValue(node.Priority, out cp))
            {
                cp = new List<BaseVINode>();
                childrenByPriority[node.Priority] = cp;
            }

            childNodes.Add(node);
            cp.Add(node);
        }
    }

}
