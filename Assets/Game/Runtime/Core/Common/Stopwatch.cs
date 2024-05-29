using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public class Stopwatch
    {
        public bool IsFinished => TimeElapsed > _duration;

        public float Seconds => Mathf.RoundToInt(TimeElapsed % 60f);
        public float Minutes => Mathf.RoundToInt((TimeElapsed / 60f) % 60f);
        public float Hours => Mathf.RoundToInt(TimeElapsed / 3600f);

        public float TimeElapsed => _timestamp += Time.deltaTime;

        private float _duration;
        private float _timestamp;

        public Stopwatch(float duration)
        {
            _duration = duration;
        }

        public void SetDuration(float value)
        {
            _duration = value;
        }

        public void Reset()
        {
            _timestamp = 0f;
        }
    }
}
