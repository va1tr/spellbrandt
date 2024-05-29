using UnityEngine;

namespace Spellbrandt
{
    public class BattleLostState<T> : State<T>
    {
        private readonly BattleController _controller;

        public BattleLostState(T uniqueID, BattleController controller) : base(uniqueID)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            Debug.Log("player has lost the battle");
        }
    }
}