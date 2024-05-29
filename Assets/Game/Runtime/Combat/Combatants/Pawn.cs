using UnityEngine;

namespace Spellbrandt
{
    public class Pawn : Combatant
    {
        private Monster _monster;

        protected override void PreInitialise()
        {
            base.PreInitialise();

            Affinity = Affinity.Friendly;
            State = CombatantState.None;
        }

        private void OnEnable()
        {
            EventSystem.Subscribe<PlayerSummonEventArgs>(OnMonsterSummoned);
            EventSystem.Subscribe<PlayerRecallEventArgs>(OnMonsterRecalled);
        }

        private void OnMonsterSummoned(PlayerSummonEventArgs args)
        {
            _monster = args.Monster;

            Respawn(_monster);
        }

        private void OnMonsterRecalled(PlayerRecallEventArgs args)
        {
            _monster = args.Monster;

            Despawn(_monster);
        }

        public override void Respawn(Monster monster)
        {
            base.Respawn(monster);

            InputSystem.onMove += OnMove;
            InputSystem.onAttack += OnAttack;

            var args = new PawnSummonEventArgs(this, abilitySystemComponent);
            EventSystem.Publish(args);
        }

        public override void OnDied()
        {
            _monster.IsAlive = false;
            _monster.Owner.Recall(_monster);
        }

        private void OnMove(Vector2 value)
        {
            if (CanMove())
            {
                var direction = new Vector3Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y), 0);

                AttemptMoveToGameBoardTile(direction);
            }
        }

        private void OnAttack()
        {
            if (CanAttack())
            {
                if (abilitySystemComponent.TryGetAbility(0, out AbilitySpec spec) && spec.CanActivateAbility())
                {
                    StartCoroutine(AttackSequence(_monster, spec));
                }
            }
        }

        public override void Despawn(Monster monster)
        {
            base.Despawn(monster);

            InputSystem.onMove -= OnMove;
            InputSystem.onAttack -= OnAttack;

            var args = new PawnRecallEventArgs(this, abilitySystemComponent);
            EventSystem.Publish(args);
        }

        private void OnDisable()
        {
            EventSystem.Unsubscribe<PlayerSummonEventArgs>(OnMonsterSummoned);
            EventSystem.Unsubscribe<PlayerRecallEventArgs>(OnMonsterRecalled);
        }
    }
}
