namespace Spellbrandt
{
    public class BTSelector : BTBranch
    {
        public override BTState OnUpdate()
        {
            var state = childNodes[activeChildIndex].OnUpdate();

            switch (state)
            {
                case BTState.Success:
                    activeChildIndex = 0;
                    return BTState.Success;

                case BTState.Failure:
                    activeChildIndex++;

                    if (activeChildIndex == childNodes.Count)
                    {
                        return BTState.Failure;
                    }
                    else
                    {
                        return BTState.Continue;
                    }

                case BTState.Continue:
                    return BTState.Continue;

                case BTState.Abort:
                    activeChildIndex = 0;
                    return BTState.Abort;
            }

            throw new System.Exception("could not successfully abort");
        }
    }
}
