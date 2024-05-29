namespace Spellbrandt
{
    public abstract class BTBlock : BTBranch
    {
        public override BTState OnUpdate()
        {
            switch (childNodes[activeChildIndex].OnUpdate())
            {
                case BTState.Continue:
                    return BTState.Continue;

                default:
                    activeChildIndex++;

                    if (activeChildIndex == childNodes.Count)
                    {
                        activeChildIndex = 0;

                        return BTState.Success;
                    }

                    return BTState.Continue;
            }
        }
    }
}
