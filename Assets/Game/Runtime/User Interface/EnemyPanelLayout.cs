#pragma warning disable 0649
using UnityEngine;

namespace Spellbrandt
{
    public class EnemyPanelLayout : PanelLayout
    {
        [SerializeField]
        private MonsterPanel[] healthPanels;

        private AbilitySystemComponent[] _abilitySystemComponents;

        private void OnEnable()
        {
            EventSystem.Subscribe<EnemySummonedEventArgs>(OnEnemySummoned);
            EventSystem.Subscribe<EnemyRecalledEventArgs>(OnEnemyRecalled);
        }

        private void OnEnemySummoned(EnemySummonedEventArgs args)
        {
            _abilitySystemComponents = args.AbilitySystemComponents;

            for (int i = 0; i < healthPanels.Length; i++)
            {
                var panel = healthPanels[i];

                if (_abilitySystemComponents.Length > i)
                {
                    var abilitySystemComponent = _abilitySystemComponents[i];

                    panel.Bind(abilitySystemComponent);
                    panel.Show();
                }
                else
                {
                    panel.Hide();
                }
            }
        }

        private void OnEnemyRecalled(EnemyRecalledEventArgs args)
        {
            var enemies = args.AbilitySystemComponents;
        }

        private void OnDisable()
        {
            EventSystem.Unsubscribe<EnemySummonedEventArgs>(OnEnemySummoned);
            EventSystem.Unsubscribe<EnemyRecalledEventArgs>(OnEnemyRecalled);
        }
    }
}
