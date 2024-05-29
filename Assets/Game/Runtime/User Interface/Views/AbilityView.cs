#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Spellbrandt
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField]
        private Text textComponent;

        private Monster _activeMonster;
        private Monster _selectedMonster;

        private bool _isActive = false;

        private void OnEnable()
        {
            EventSystem.Subscribe<PlayerSummonEventArgs>(OnMonsterSummoned);
            EventSystem.Subscribe<PlayerRecallEventArgs>(OnMonsterRecalled);
        }

        public void Bind(Monster monster)
        {
            _selectedMonster = monster;

            Refresh();
        }

        private void OnMonsterSummoned(PlayerSummonEventArgs args)
        {
            _activeMonster = args.Monster;
            _isActive = true;

            Refresh();
        }

        private void OnMonsterRecalled(PlayerRecallEventArgs args)
        {
            _activeMonster = args.Monster;
            _isActive = false;

            Refresh();
        }

        private void Refresh()
        {
            if (_isActive && _selectedMonster != _activeMonster)
            {
                textComponent.text = string.Empty;
            }
            else if (_selectedMonster.Owner.TryGetActiveAbility(out AbilitySpec ability))
            {
                textComponent.text = ability.Asset.Name;
            }
        }

        private void OnDisable()
        {
            EventSystem.Unsubscribe<PlayerSummonEventArgs>(OnMonsterSummoned);
            EventSystem.Unsubscribe<PlayerRecallEventArgs>(OnMonsterRecalled);
        }
    }
}
