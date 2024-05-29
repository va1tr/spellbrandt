namespace Spellbrandt
{
    public class EnemyRecalledEventArgs
    {
        public readonly AbilitySystemComponent[] AbilitySystemComponents;

        public EnemyRecalledEventArgs(AbilitySystemComponent[] abilitySystemComponents)
        {
            AbilitySystemComponents = abilitySystemComponents;
        }
    }
}
