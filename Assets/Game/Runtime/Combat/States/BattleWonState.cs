using UnityEngine;

namespace Spellbrandt
{
    public class BattleWonState<T> : State<T>
    {
        private readonly BattleController _controller;

        public BattleWonState(T uniqueID, BattleController controller) : base(uniqueID)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            Debug.Log("player has won the battle");
        }
    }
}