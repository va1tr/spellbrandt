#pragma warning disable 0649
using UnityEngine;

namespace Spellbrandt
{
    public abstract class ScriptableAbility : ScriptableObject
    {
        [SerializeField]
        private new string name;

        [SerializeField]
        private int cost;

        [SerializeField]
        private int cooldown;

        public string Name
        {
            get => name;
        }

        public int Cost
        {
            get => cost;
        }

        public int Cooldown
        {
            get => cooldown;
        }

        public abstract AbilitySpec CreateAbilitySpec();

        public abstract AbilitySpec CreateAbilitySpec(AbilitySystemComponent instigator);
    }
}
