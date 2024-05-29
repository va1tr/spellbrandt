namespace Spellbrandt
{
    public class PawnRecallEventArgs
    {
        public readonly Pawn Owner;
        public readonly AbilitySystemComponent AbilitySystemComponent;

        public PawnRecallEventArgs(Pawn owner, AbilitySystemComponent abilitySystemComponent)
        {
            Owner = owner;
            AbilitySystemComponent = abilitySystemComponent;
        }
    }
}
