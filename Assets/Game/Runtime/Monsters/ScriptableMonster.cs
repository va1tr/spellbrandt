#pragma warning disable 0649
using UnityEngine;

namespace Spellbrandt
{
    [CreateAssetMenu(fileName = "new-monster", menuName = "ScriptableObjects/Monsters/Monster")]
    public class ScriptableMonster : ScriptableObject
    {
        [SerializeField]
        private new string name;

        [SerializeField]
        private Sprite idleSprite;

        [SerializeField]
        private Sprite attackSprite;

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

        public string Name
        {
            get => name;
        }

        public Sprite IdleSprite
        {
            get => idleSprite;
        }

        public Sprite AttackSprite
        {
            get => attackSprite;
        }

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

        public Monster CreateMonsterFromAsset()
        {
            return new Monster(this);
        }

        public Monster CreateMonsterFromAsset(Summoner owner)
        {
            return new Monster(this, owner);
        }
    }
}
