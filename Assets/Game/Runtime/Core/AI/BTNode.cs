namespace Spellbrandt
{
    public enum BTState
    {
        Success,
        Failure,
        Continue,
        Abort
    }

    public abstract class BTNode
    {
        public abstract BTState OnUpdate();
    }
}
