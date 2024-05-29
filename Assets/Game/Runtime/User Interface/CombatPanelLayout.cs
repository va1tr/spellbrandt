#pragma warning disable 0649
using UnityEngine;


namespace Spellbrandt
{
    public class CombatPanelLayout : PanelLayout
    {
        [SerializeField]
        private MonsterPanel healthPanel;

        private void OnEnable()
        {
            EventSystem.Subscribe<PawnSummonEventArgs>(OnPawnSummoned);
            EventSystem.Subscribe<PawnRecallEventArgs>(OnPawnRecalled);
        }

        private void OnPawnSummoned(PawnSummonEventArgs args)
        {
            healthPanel.Bind(args.AbilitySystemComponent);
            healthPanel.Show();
        }

        private void OnPawnRecalled(PawnRecallEventArgs args)
        {
            healthPanel.Lock();
        }

        private void OnDisable()
        {
            EventSystem.Unsubscribe<PawnSummonEventArgs>(OnPawnSummoned);
            EventSystem.Unsubscribe<PawnRecallEventArgs>(OnPawnRecalled);
        }
    }
}
