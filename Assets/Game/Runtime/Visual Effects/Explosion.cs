using UnityEngine;

namespace Spellbrandt
{
    public sealed class Explosion : Cinematograph
    {
        private void OnParticleSystemStopped()
        {
            Complete();
        }
    }
}
