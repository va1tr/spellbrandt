using System;
using UnityEngine;

namespace Spellbrandt
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class Cinematograph : MonoBehaviour
    {
        public event Action<Cinematograph> onComplete;

        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public virtual void Emit()
        {
            _particleSystem.Play();
        }

        public virtual void Complete()
        {
            onComplete?.Invoke(this);
        }
    }
}
