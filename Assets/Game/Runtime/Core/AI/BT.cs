using System;
using System.Collections.Generic;

namespace Spellbrandt
{
    public static class BT
    {
        public static BTRoot Root() { return new BTRoot(); }

        public static BTSelector Selector() { return new BTSelector(); }
        public static BTTerminate Terminate() { return new BTTerminate(); }

        public static BTWait Wait(float delayInSeconds) { return new BTWait(delayInSeconds); }
        public static BTWaitForRandomDelay Wait(float minimumDelayInSeconds, float maximumDelayInSeconds) { return new BTWaitForRandomDelay(minimumDelayInSeconds, maximumDelayInSeconds); }

        public static BTAction Call(Action action) { return new BTAction(action); }
        public static BTAction Coroutine(Func<IEnumerator<BTState>> coroutine) { return new BTAction(coroutine); }

        public static BTCondition Condition(Func<bool> func) { return new BTCondition(func); }
    }
}
