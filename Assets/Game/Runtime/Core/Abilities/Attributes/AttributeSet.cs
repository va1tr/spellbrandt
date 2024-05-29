#pragma warning disable 0649
using UnityEngine;

namespace Spellbrandt
{
    [CreateAssetMenu(fileName = "new-attribute-set", menuName = "ScriptableObjects/Attributes/AttributeSet")]
    public class AttributeSet : ScriptableObject
    {
        [SerializeField]
        private int health;

        [SerializeField]
        private int attack;

        [SerializeField]
        private int defence;

        [SerializeField]
        private int speed;

        [SerializeField]
        private ScriptableAbility[] abilities;

        public int Health
        {
            get => health;
        }

        public int Attack
        {
            get => attack;
        }

        public int Defence
        {
            get => defence;
        }

        public int Speed
        {
            get => speed;
        }

        public ScriptableAbility[] Abilities
        {
            get => abilities;
        }
    }
}
