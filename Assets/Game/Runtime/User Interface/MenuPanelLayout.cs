#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public class MenuPanelLayout : PanelLayout
    {
        [SerializeField]
        private MonsterView previousView;

        [SerializeField]
        private MonsterView selectedView;

        [SerializeField]
        private MonsterView nextView;

        [SerializeField]
        private AbilityView abilityView;

        private readonly List<Monster> _monsters = new List<Monster>();

        private int _index = 0;

        private void OnEnable()
        {
            EventSystem.Subscribe<PlayerMonsterAddedEventArgs>(OnMonsterAdded);
            EventSystem.Subscribe<PlayerMonsterRemovedEventArgs>(OnMonsterRemoved);

            InputSystem.onNavigate += OnNavigate;
        }

        private void OnNavigate(Vector2 value)
        {
            _index = Mathf.RoundToInt(Mathf.Repeat(_index + value.x, _monsters.Count));

            Refresh();
        }

        private void OnMonsterAdded(PlayerMonsterAddedEventArgs args)
        {
            var monster = args.Monster;

            if (!_monsters.Contains(monster))
            {
                _monsters.Add(monster);
            }

            Refresh();
        }

        private void OnMonsterRemoved(PlayerMonsterRemovedEventArgs args)
        {
            var monster = args.Monster;

            if (_monsters.Contains(monster))
            {
                _monsters.Remove(monster);
            }

            Refresh();
        }

        private void Refresh()
        {
            var monster = _monsters[_index];
            monster.Owner.Select(_index);

            selectedView.Bind(monster);
            abilityView.Bind(monster);

            int previous = Mathf.RoundToInt(Mathf.Repeat(_index - 1, _monsters.Count));
            previousView.Bind(_monsters[previous]);

            int next = Mathf.RoundToInt(Mathf.Repeat(_index + 1, _monsters.Count));
            nextView.Bind(_monsters[next]);
        }

        private void OnDisable()
        {
            InputSystem.onNavigate -= OnNavigate;

            EventSystem.Unsubscribe<PlayerMonsterAddedEventArgs>(OnMonsterAdded);
            EventSystem.Unsubscribe<PlayerMonsterRemovedEventArgs>(OnMonsterRemoved);
        }
    }
}
