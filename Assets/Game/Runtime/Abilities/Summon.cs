using System.Collections;
using UnityEngine;

namespace Spellbrandt
{
    [CreateAssetMenu(fileName = "new-summon-ability", menuName = "ScriptableObjects/Abilities/Summon")]
    public sealed class Summon : ScriptableAbility
    {
        public override AbilitySpec CreateAbilitySpec()
        {
            return new SummonAbilitySpec(this);
        }

        public override AbilitySpec CreateAbilitySpec(AbilitySystemComponent instigator)
        {
            return new SummonAbilitySpec(this, instigator);
        }

        internal sealed class SummonAbilitySpec : AbilitySpec
        {
            private readonly Summoner _summoner;
            private Monster _monster;

            public SummonAbilitySpec(ScriptableAbility asset) : base(asset)
            {

            }

            public SummonAbilitySpec(ScriptableAbility asset, AbilitySystemComponent instigator) : base(asset, instigator)
            {
                _summoner = instigator.GetComponent<Summoner>();
            }

            public override void PreActivate()
            {
                Cooldown.Value = Cooldown.MaxValue;
            }

            protected override IEnumerator Activate()
            {
                //Debug.Log($"activated summon ability");

                _summoner.Summon(_monster);

                yield break;
            }

            public override bool CheckAbilityInstigatorIsNotNull()
            {
                return _summoner != null;
            }

            public override bool CheckAbilityRequirements()
            {
                if (_summoner.TryGetSelectedMonster(out Monster monster))
                {
                    _monster = monster;

                    return _monster.IsAlive;
                }

                return false;
            }

        }
    }
}
