using System.Collections;
using UnityEngine;

namespace Spellbrandt
{
    [CreateAssetMenu(fileName = "new-recall-ability", menuName = "ScriptableObjects/Abilities/Recall")]
    public sealed class Recall : ScriptableAbility
    {
        public override AbilitySpec CreateAbilitySpec()
        {
            return new RecallAbilitySpec(this);
        }

        public override AbilitySpec CreateAbilitySpec(AbilitySystemComponent instigator)
        {
            return new RecallAbilitySpec(this, instigator);
        }

        internal sealed class RecallAbilitySpec : AbilitySpec
        {
            private readonly Summoner _summoner;
            private Monster _monster;

            public RecallAbilitySpec(ScriptableAbility asset) : base(asset)
            {

            }

            public RecallAbilitySpec(ScriptableAbility asset, AbilitySystemComponent instigator) : base(asset, instigator)
            {
                _summoner = instigator.GetComponent<Summoner>();
            }

            public override void PreActivate()
            {
                Cooldown.Value = Cooldown.MaxValue;
            }

            protected override IEnumerator Activate()
            {
                //Debug.Log($"activated recall ability");

                _summoner.Recall(_monster);

                yield break;
            }

            public override bool CheckAbilityInstigatorIsNotNull()
            {
                return _summoner != null;
            }

            public override bool CheckAbilityRequirements()
            {
                if (_summoner.TryGetActiveMonster(out Monster monster))
                {
                    _monster = monster;
                    return true;
                }

                return false;
            }
        }
    }
}
