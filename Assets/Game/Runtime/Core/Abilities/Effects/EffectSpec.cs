namespace Spellbrandt
{
    public class EffectSpec
    {
        public ScriptableEffect Effect { get; }

        private readonly AbilitySystemComponent _instigator;
        private readonly AbilitySystemComponent _target;

        private readonly Stopwatch _periodicStopwatch;
        private readonly Stopwatch _durationStopwatch;

        private float _timeElapsed;

        public EffectSpec(ScriptableEffect effect, AbilitySystemComponent instigator = null, AbilitySystemComponent target = null, float periodic = 0f, float duration = 0f)
        {
            Effect = effect;

            _instigator = instigator;
            _target = target;

            _periodicStopwatch = new Stopwatch(periodic);
            _durationStopwatch = new Stopwatch(duration);
        }

        public void Tick()
        {
            _timeElapsed = _periodicStopwatch.TimeElapsed;
            _timeElapsed = _durationStopwatch.TimeElapsed;
        }

        public bool CanApplyEffectSpec()
        {
            if (_periodicStopwatch.IsFinished)
            {
                Reset();

                return true;
            }

            return false;
        }

        public bool CanRemoveEffectSpec()
        {
            if (_durationStopwatch.IsFinished)
            {
                return true;
            }

            return false;
        }

        public void SetPeriodic(float value)
        {
            _periodicStopwatch.SetDuration(value);
        }

        public void SetDuration(float value)
        {
            _durationStopwatch.SetDuration(value);
        }

        public void Reset()
        {
            _periodicStopwatch.Reset();
        }
    }
}