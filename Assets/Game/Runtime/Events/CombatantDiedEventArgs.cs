namespace Spellbrandt
{
    public class CombatantDiedEventArgs
    {
        public readonly Combatant Combatant;
        public readonly Affinity Affinity;

        public CombatantDiedEventArgs(Combatant combatant, Affinity affinity)
        {
            Combatant = combatant;
            Affinity = affinity;
        }
    }
}
