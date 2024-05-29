#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public enum BattleState
    {
        Begin,
        Wait,
        Attack,
        Won,
        Lost
    }

    public class BattleController : MonoBehaviour
    {
        [SerializeField]
        private RandomEncounterSpawner randomEncounterSpawner;

        [SerializeField]
        private Summoner[] summoners;

        [SerializeField]
        private Combatant[] combatants;

        private StateMachine<BattleState> _stateMachine;

        private void Awake()
        {
            Create();
        }

        private void Create()
        {
            var stateList = new List<IState<BattleState>>()
            {
                new BattleBeginState<BattleState>(BattleState.Begin, this),
                new BattleWaitState<BattleState>(BattleState.Wait, this),
                new BattleAttackState<BattleState>(BattleState.Attack, this),
                new BattleWonState<BattleState>(BattleState.Won, this),
                new BattleLostState<BattleState>(BattleState.Lost, this)
            };

            _stateMachine = new StateMachine<BattleState>(stateList.ToArray(), BattleState.Begin);
        }

        private void Update()
        {
            _stateMachine.OnUpdate();
        }

        public void SpawnRandomEncounterFromSet()
        {
            randomEncounterSpawner.SpawnRandomEncounterFromSet();
        }

        public void CheckAllSummonersAreReady()
        {
            bool isReady = true;

            for (int i = 0; i < summoners.Length; i++)
            {
                var summoner = summoners[i];

                if (!summoner.HasSummonedAtLeastOneMonster())
                {
                    isReady = false;
                }
            }

            if (isReady)
            {
                ChangeState(BattleState.Attack);
            }
            else
            {
                ChangeState(BattleState.Wait);
            }
        }

        public void CheckAllSummonersStatus()
        {
            for (int i = 0; i < summoners.Length; i++)
            {
                var summoner = summoners[i];

                if (!summoner.HasAtLeastOneMonsterAlive())
                {
                    var affinity = summoner.Affinity;

                    if (affinity == Affinity.Friendly)
                    {
                        ChangeState(BattleState.Lost);
                    }
                    else
                    {
                        ChangeState(BattleState.Won);
                    }
                }
            }
        }

        public void UpdateAllCombatantsAbilitiesAndEffects()
        {
            for (int i = 0; i < combatants.Length; i++)
            {
                combatants[i].OnUpdate();
            }
        }

        public void ChangeState(BattleState state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
