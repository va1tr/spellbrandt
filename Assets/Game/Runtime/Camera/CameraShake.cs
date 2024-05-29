using UnityEngine;

namespace Spellbrandt
{
    public sealed class CameraShake : MonoBehaviour
    {
        private static CameraShake _instance;

        private static float _duration = 0.125f;
        private static float _amplitude = 16f;
        private static float _strength = 0.0625f;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public static void Emit(float duration = 0.125f, float amplitude = 16f, float strength = 0.0625f)
        {
            _duration = duration;
            _amplitude = amplitude;
            _strength = strength;

            _instance.StartCoroutine(ShakeSequence());
        }

        private static System.Collections.IEnumerator ShakeSequence()
        {
            yield return Tween.Position.Shake(_instance.transform, _duration, _amplitude, _strength);
        }
    }
}

