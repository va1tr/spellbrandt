namespace Spellbrandt
{
    public class PawnSummonEventArgs
    {
        public readonly Pawn Owner;
        public readonly AbilitySystemComponent AbilitySystemComponent;

        public PawnSummonEventArgs(Pawn owner, AbilitySystemComponent abilitySystemComponent)
        {
            Owner = owner;
            AbilitySystemComponent = abilitySystemComponent;
        }
    }
}
