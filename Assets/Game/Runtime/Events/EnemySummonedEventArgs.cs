namespace Spellbrandt
{
    public class EnemySummonedEventArgs
    {
        public readonly AbilitySystemComponent[] AbilitySystemComponents;

        public EnemySummonedEventArgs(AbilitySystemComponent[] abilitySystemComponents)
        {
            AbilitySystemComponents = abilitySystemComponents;
        }
    }
}
