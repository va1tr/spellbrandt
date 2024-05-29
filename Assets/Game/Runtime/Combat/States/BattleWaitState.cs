using UnityEngine;

namespace Spellbrandt
{
    public sealed class BattleWaitState<T> : State<T>
    {
        private readonly BattleController _controller;

        public BattleWaitState(T uniqueID, BattleController controller) : base(uniqueID)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            Debug.Log("entered battle wait state");

            _controller.CheckAllSummonersStatus();

            EventSystem.Subscribe<PlayerSummonEventArgs>(OnEvelynSummoned);
        }

        private void OnEvelynSummoned(PlayerSummonEventArgs args)
        {
            _controller.ChangeState(BattleState.Attack);
        }

        public override void OnExit()
        {
            EventSystem.Unsubscribe<PlayerSummonEventArgs>(OnEvelynSummoned);
        }
    }
}