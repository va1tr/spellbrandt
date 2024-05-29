using System.Collections.Generic;

namespace Spellbrandt
{
    public abstract class BTBranch : BTNode
    {
        protected readonly List<BTNode> childNodes = new List<BTNode>();

        protected int activeChildIndex;

        public virtual BTBranch Open(params BTNode[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                childNodes.Add(nodes[i]);
            }

            return this;
        }

        public virtual BTBranch Reset()
        {
            for (int i = 0; i < childNodes.Count; i++)
            {
                var node = childNodes[i];

                if (node.GetType() == typeof(BTBranch))
                {
                    var branch = (BTBranch)node;

                    branch.Reset();
                }
            }

            return this;
        }

        public List<BTNode> GetChildNodes()
        {
            return childNodes;
        }

        public int GetActiveChildNode()
        {
            return activeChildIndex;
        }
    }
}
