using UnityEngine;

namespace Spellbrandt
{
    public class BattleAttackState<T> : State<T>
    {
        private readonly BattleController _controller;

        public BattleAttackState(T uniqueID, BattleController controller) : base(uniqueID)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            Debug.Log("entered battle attack state");

            EventSystem.Subscribe<PlayerRecallEventArgs>(OnEvelynRecalled);
            EventSystem.Subscribe<CombatantDiedEventArgs>(OnCombatantDied);
        }

        public override void OnUpdate()
        {
            _controller.UpdateAllCombatantsAbilitiesAndEffects();
        }

        private void OnEvelynRecalled(PlayerRecallEventArgs args)
        {
            _controller.ChangeState(BattleState.Wait);
        }

        private void OnCombatantDied(CombatantDiedEventArgs args)
        {
            var affinity = args.Affinity;

            if (affinity == Affinity.Hostile)
            {
                _controller.ChangeState(BattleState.Won);
            }
            else
            {
                _controller.ChangeState(BattleState.Lost);
            }
        }

        public override void OnExit()
        {
            EventSystem.Unsubscribe<PlayerRecallEventArgs>(OnEvelynRecalled);
            EventSystem.Unsubscribe<CombatantDiedEventArgs>(OnCombatantDied);
        }
    }
}
