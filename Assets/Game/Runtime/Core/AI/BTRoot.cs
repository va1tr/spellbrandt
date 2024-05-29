namespace Spellbrandt
{
    public class BTRoot : BTBlock
    {
        public bool IsActive { get; set; }
        public bool IsTerminated { get; set; }

        public override BTState OnUpdate()
        {
            if (IsTerminated)
            {
                return BTState.Abort;
            }

            while (true)
            {
                switch (childNodes[activeChildIndex].OnUpdate())
                {
                    case BTState.Continue:
                        return BTState.Continue;

                    case BTState.Abort:
                        IsTerminated = true;
                        return BTState.Abort;

                    default:
                        activeChildIndex++;

                        if (activeChildIndex == childNodes.Count)
                        {
                            activeChildIndex = 0;
                            return BTState.Success;
                        }

                        continue;
                }
            }
        }
    }
}
