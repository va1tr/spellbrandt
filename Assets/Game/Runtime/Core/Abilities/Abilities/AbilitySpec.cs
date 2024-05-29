using System;
using System.Collections;

namespace Spellbrandt
{
    public abstract class AbilitySpec
    {
        public ScriptableAbility Asset { get; private set; }
        public AbilitySystemComponent Instigator { get; private set; }

        public Attribute Cost { get; set; }
        public Attribute Cooldown { get; set; }

        public AbilitySpec(ScriptableAbility asset)
        {
            Asset = asset;

            Cost = new Attribute(AttributeType.Default, asset.Cost);
            Cooldown = new Attribute(AttributeType.Default, 0f, asset.Cooldown);
        }

        public AbilitySpec(ScriptableAbility asset, AbilitySystemComponent instigator) : this(asset)
        {
            Instigator = instigator;
        }

        public virtual IEnumerator TryActivateAbility()
        {
            if (CanActivateAbility())
            {
                PreActivate();

                yield return Activate();

                PostActivate();
            }
        }
        
        protected abstract IEnumerator Activate();

        public bool CanActivateAbility()
        {
            return CheckAbilityInstigatorIsNotNull() && CheckAbilityRequirements() && CheckAbilityCost() && CheckAbilityCooldown();
        }

        public virtual bool CheckAbilityInstigatorIsNotNull()
        {
            return Instigator != null;
        }

        public virtual bool CheckAbilityRequirements()
        {
            return true;
        }

        public virtual bool CheckAbilityCost()
        {
            return Cost.Value > float.Epsilon;
        }

        public virtual bool CheckAbilityCooldown()
        {
            return Cooldown.Value <= float.Epsilon;
        }

        public virtual void PreActivate()
        {
            Cost.Value--;
            Cooldown.Value = Cooldown.MaxValue;
        }

        public virtual void PostActivate()
        {
            
        }

        public void Bind<T>(T component) where T : AbilitySystemComponent
        {
            Instigator = component;
        }

        public void UnBind()
        {
            Instigator = null;
        }
    }
}
