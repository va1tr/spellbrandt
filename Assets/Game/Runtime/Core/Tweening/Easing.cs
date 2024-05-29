using UnityEngine;

namespace Spellbrandt
{
    public struct Easing
    {
        public static float Linear(float time)
        {
            return time;
        }

        public static float EaseOutSine(float time)
        {
            return Mathf.Sin((time * Mathf.PI) / 2f);
        }

        public static float PingPong(float time)
        {
            return Mathf.PingPong(time * 2f, 1f);
        }
    }
}
