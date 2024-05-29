using UnityEngine;

namespace Spellbrandt
{
    public class BTWaitForRandomDelay : BTNode
    {
        private readonly float _minimumDelay;
        private readonly float _maximumDelay;

        private float _delay;
        private float _timeElapsed;

        private bool _isInitialised;

        public BTWaitForRandomDelay(float min, float max)
        {
            _minimumDelay = min;
            _maximumDelay = max;

            _isInitialised = false;
        }

        public override BTState OnUpdate()
        {
            if (!_isInitialised)
            {
                _delay = Random.Range(_minimumDelay, _maximumDelay);
                _isInitialised = true;
            }

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
            _isInitialised = false;
        }
    }
}