using System;

namespace Spellbrandt
{
    public class BTCondition: BTNode
    {
        public Func<bool> Condition { get; }

        public BTCondition(Func<bool> condition)
        {
            Condition = condition;
        }

        public override BTState OnUpdate()
        {
            return Condition() ? BTState.Success : BTState.Failure;
        }
    }
}
