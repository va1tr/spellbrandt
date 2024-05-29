using System;
using System.Collections.Generic;

namespace Spellbrandt
{
    public class BTAction : BTNode
    {
        private readonly Action _onActionPerformed;

        private readonly Func<IEnumerator<BTState>> _onActionSequencePerformed;

        private IEnumerator<BTState> _coroutine;

        public BTAction(Action onActionPerformed)
        {
            _onActionPerformed = onActionPerformed;
        }

        public BTAction(Func<IEnumerator<BTState>> onActionSequencePerformed)
        {
            _onActionSequencePerformed = onActionSequencePerformed;
        }

        public override BTState OnUpdate()
        {
            if (_onActionPerformed != null)
            {
                _onActionPerformed();

                return BTState.Success;
            }
            else
            {
                if (_coroutine == null)
                {
                    _coroutine = _onActionSequencePerformed();
                }

                if (_coroutine.MoveNext())
                {
                    _coroutine = null;

                    return BTState.Success;
                }

                var result = _coroutine.Current;

                if (result == BTState.Continue)
                {
                    return BTState.Continue;
                }
                else
                {
                    _coroutine = null;

                    return result;
                }
            }
        }
    }
}
