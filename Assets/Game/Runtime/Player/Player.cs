using System.Collections.Generic;

namespace Spellbrandt
{
    public enum PlayerState
    {
        Summon,
        Recall
    }

    public class Player : Summoner
    {
        private StateMachine<PlayerState> _stateMachine;

        protected override void PreInitialise()
        {
            base.PreInitialise();

            Affinity = Affinity.Friendly;
        }

        protected override void Initialise()
        {
            base.Initialise();

            Create();
        }

        private void Create()
        {
            var stateList = new List<IState<PlayerState>>()
            {
                new PlayerSummonState<PlayerState>(PlayerState.Summon, this, abilitySystemComponent),
                new PlayerRecallState<PlayerState>(PlayerState.Recall, this, abilitySystemComponent)
            };

            _stateMachine = new StateMachine<PlayerState>(stateList.ToArray(), PlayerState.Summon);
        }

        public override void Summon(Monster monster)
        {
            base.Summon(monster);

            var ability = abilitySystemComponent.GetAbilityByType<Recall>();
            SetActiveAbility(ability);

            var args = new PlayerSummonEventArgs(monster);
            EventSystem.Publish(args);

            ChangeState(PlayerState.Recall);
        }

        public override void Recall(Monster monster)
        {
            base.Recall(monster);

            var ability = abilitySystemComponent.GetAbilityByType<Summon>();
            SetActiveAbility(ability);

            var args = new PlayerRecallEventArgs(monster);
            EventSystem.Publish(args);

            ChangeState(PlayerState.Summon);
        }

        public override void AddMonsterToSet(Monster monster)
        {
            base.AddMonsterToSet(monster);

            var args = new PlayerMonsterAddedEventArgs(monster);
            EventSystem.Publish(args);
        }

        public override void RemoveMonsterFromSet(Monster monster)
        {
            base.RemoveMonsterFromSet(monster);

            var args = new PlayerMonsterRemovedEventArgs(monster);
            EventSystem.Publish(args);
        }

        public void ChangeState(PlayerState state)
        {
            _stateMachine.ChangeState(state);
        }

        public bool SelectedMonsterIsEqualToActive()
        {
            return selectedMonster == activeMonster;
        }
    }
}
