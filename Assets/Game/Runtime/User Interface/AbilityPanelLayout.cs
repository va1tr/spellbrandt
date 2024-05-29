#pragma warning disable 0649
using UnityEngine;

namespace Spellbrandt
{
    public class AbilityPanelLayout : PanelLayout
    {
        [SerializeField]
        private AbilityPanel[] abilityPanels;

        private void OnEnable()
        {
            EventSystem.Subscribe<PawnSummonEventArgs>(OnPawnSpawn);
            EventSystem.Subscribe<PawnRecallEventArgs>(OnPawnDespawn);
        }

        private void OnPawnSpawn(PawnSummonEventArgs args)
        {
            var abilitySystemComponent = args.AbilitySystemComponent;

            for (int i = 0; i < abilityPanels.Length; i++)
            {
                if (abilitySystemComponent.TryGetAbility(i, out AbilitySpec spec))
                {
                    abilityPanels[i].Bind(spec);
                    abilityPanels[i].Show();
                }
                else
                {
                    abilityPanels[i].Hide();
                }
            }
        }

        private void OnPawnDespawn(PawnRecallEventArgs args)
        {
            for (int i = 0; i < abilityPanels.Length; i++)
            {
                abilityPanels[i].Hide();
            }
        }

        private void OnDisable()
        {
            EventSystem.Unsubscribe<PawnSummonEventArgs>(OnPawnSpawn);
            EventSystem.Unsubscribe<PawnRecallEventArgs>(OnPawnDespawn);
        }
    }
}
