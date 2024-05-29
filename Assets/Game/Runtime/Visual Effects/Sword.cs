using UnityEngine;

namespace Spellbrandt
{
    public sealed class Sword : Cinematograph
    {
        private void OnParticleSystemStopped()
        {
            Complete();
        }
    }
}
