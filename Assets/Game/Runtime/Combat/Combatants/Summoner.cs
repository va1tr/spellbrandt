#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public class Summoner : Combatant
    {
        [SerializeField]
        private AttributeSet attributeSet;

        [SerializeField]
        private MonsterRuntimeSet monsterRuntimeSet;

        protected readonly List<Monster> monsters = new List<Monster>();

        protected Monster selectedMonster;
        protected Monster activeMonster;

        protected AbilitySpec activeAbility;

        protected override void Initialise()
        {
            CreateStartupAttributes();
            CreateStartupAbilities();

            CreateStartupMonsterSet();
        }

        private void CreateStartupAttributes()
        {
            var health = new Attribute(AttributeType.Health, attributeSet.Health);
            abilitySystemComponent.AddAttributeToSet(health);

            var attack = new Attribute(AttributeType.Attack, attributeSet.Attack);
            abilitySystemComponent.AddAttributeToSet(attack);

            var defence = new Attribute(AttributeType.Defence, attributeSet.Defence);
            abilitySystemComponent.AddAttributeToSet(defence);

            var speed = new Attribute(AttributeType.Speed, attributeSet.Speed);
            abilitySystemComponent.AddAttributeToSet(speed);
        }

        private void CreateStartupAbilities()
        {
            var abilities = attributeSet.Abilities;

            for (int i = 0; i < abilities.Length; i++)
            {
                var spec = abilities[i].CreateAbilitySpec(abilitySystemComponent);
                abilitySystemComponent.AddAbilityToSet(spec);                
            }

            activeAbility = abilitySystemComponent.GetAbilityByType<Summon>();
        }
         
        private void CreateStartupMonsterSet()
        {
            int count = monsterRuntimeSet.Count();

            for (int i = 0; i < count; i++)
            {
                var asset = monsterRuntimeSet.GetItem(i);
                var monster = asset.CreateMonsterFromAsset(this);

                AddMonsterToSet(monster);
            }
        }

        public virtual void Summon(Monster monster)
        {
            activeMonster = monster;
        }

        public virtual void Recall(Monster monster)
        {
            activeMonster = null;
        }

        public virtual void Select(int index)
        {
            selectedMonster = monsters[index];
        }

        public virtual void Deselect()
        {
            selectedMonster = null;
        }

        public virtual void AddMonsterToSet(Monster monster)
        {
            if (!monsters.Contains(monster))
            {
                monsters.Add(monster);
            }
        }

        public virtual void RemoveMonsterFromSet(Monster monster)
        {
            if (monsters.Contains(monster))
            {
                monsters.Remove(monster);
            }
        }

        public void SetActiveAbility(AbilitySpec spec)
        {
            if (abilitySystemComponent.ContainsAbility(spec))
            {
                activeAbility = spec;
            }
        }

        public bool TryGetActiveMonster(out Monster monster)
        {
            monster = null;

            if (activeMonster != null)
            {
                monster = activeMonster;
                return true;
            }

            return false;
        }

        public bool TryGetSelectedMonster(out Monster monster)
        {
            monster = null;

            if (selectedMonster != null)
            {
                monster = selectedMonster;
                return true;
            }

            return false;
        }

        public bool TryGetActiveAbility(out AbilitySpec ability)
        {
            ability = null;

            if (activeAbility != null)
            {
                ability = activeAbility;
                return true;
            }

            return false;
        }

        public bool HasAtLeastOneMonsterAlive()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsAlive)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasSummonedAtLeastOneMonster()
        {
            return activeMonster != null;
        }
    }
}