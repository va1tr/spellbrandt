using UnityEngine;

namespace Spellbrandt
{
    public class BTWait : BTNode
    {
        private float _delay;
        private float _timeElapsed;

        public BTWait(float delay)
        {
            _delay = delay;
            _timeElapsed = 0f;
        }

        public override BTState OnUpdate()
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed > _delay)
            {
                Reset();

                return BTState.Success;
            }
            else
            {
                return BTState.Continue;
            }
        }

        private void Reset()
        {
            _timeElapsed = 0f;
        }
    }
}
