using System.Collections;
using UnityEngine;

namespace Spellbrandt
{
    public sealed class BattleBeginState<T> : State<T>
    {
        private readonly BattleController _controller;

        public BattleBeginState(T uniqueID, BattleController controller) : base(uniqueID)
        {
            _controller = controller;
        }

        public override void OnEnter()
        {
            _controller.StartCoroutine(BattleBeginSequence());
        }

        private IEnumerator BattleBeginSequence()
        {
            //Debug.Log("start battle begin sequence");

            yield return new WaitForSeconds(0.1f);

            //Debug.Log("end battle begin sequence");

            _controller.SpawnRandomEncounterFromSet();
            _controller.CheckAllSummonersAreReady();
        }

        public override void OnExit()
        {
            
        }
    }
}
