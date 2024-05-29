using UnityEngine;

namespace Spellbrandt
{
    public sealed class Sigil : Cinematograph
    {
        private void OnParticleSystemStopped()
        {
            Complete();
        }
    }
}
