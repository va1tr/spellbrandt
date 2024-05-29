namespace Spellbrandt
{
    public sealed class PlayerSummonState<T> : State<T>
    {
        private readonly Player _owner;
        private readonly AbilitySystemComponent _abilitySystemComponent;

        public PlayerSummonState(T uniqueID, Player owner, AbilitySystemComponent abilitySystemComponent) : base(uniqueID)
        {
            _owner = owner;

            _abilitySystemComponent = abilitySystemComponent;
        }

        public override void OnEnter()
        {
            InputSystem.onAbility += OnAbilityPressed;
        }

        private void OnAbilityPressed()
        {
            if (_owner.TryGetActiveAbility(out AbilitySpec spec) && spec.CanActivateAbility())
            {
                _abilitySystemComponent.ActivateAbility(spec);
            }
        }

        public override void OnExit()
        {
            InputSystem.onAbility -= OnAbilityPressed;
        }
    }
}
