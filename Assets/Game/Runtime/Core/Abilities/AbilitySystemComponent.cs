using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        private readonly Dictionary<AttributeType, Attribute> _attributes = new Dictionary<AttributeType, Attribute>();

        private readonly List<AbilitySpec> _abilities = new List<AbilitySpec>();
        private readonly List<EffectSpec> _appliedEffects = new List<EffectSpec>();

        public void UpdateAndTryRemoveAppliedEffects()
        {
            for (int i = 0; i < _appliedEffects.Count; i++)
            {
                var spec = _appliedEffects[i];

                if (spec.CanRemoveEffectSpec())
                {
                    RemoveTemporaryOrPersistentEffectSpecFromSet(spec);
                }
                else if (spec.CanApplyEffectSpec())
                {
                    ApplyInstantEffectSpec(spec);
                }
            }
        }

        public void UpdateAllAbilitiesCooldown()
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                var cooldown = _abilities[i].Cooldown;

                cooldown.Value = Mathf.Max(cooldown.Value - 1 * Time.deltaTime, 0);
            }
        }

        public void ApplyEffectSpecToTarget(EffectSpec spec)
        {
            var container = spec.Effect.Container;

            switch (container.Type)
            {
                case EffectType.Direct:
                    ApplyInstantEffectSpec(spec);
                    break;
                case EffectType.Temporary:
                    AddTemporaryOrPersistentEffectSpecToSet(spec);
                    break;
                case EffectType.Persistent:
                    AddTemporaryOrPersistentEffectSpecToSet(spec);
                    break;
                default:
                    break;
            }
        }

        private void ApplyInstantEffectSpec(EffectSpec spec)
        {
            var container = spec.Effect.Container;

            for (int i = 0; i < container.Modifiers.Length; i++)
            {
                var modifier = container.Modifiers[i];

                if (TryGetAttribute(modifier.Attribute, out Attribute attribute))
                {
                    switch (modifier.Type)
                    {
                        case AttributeModifierType.Add:
                            attribute.Value += modifier.Multiplier;
                            break;

                        case AttributeModifierType.Subtract:
                            attribute.Value -= modifier.Multiplier;
                            break;

                        case AttributeModifierType.Mulitply:
                            attribute.Value *= modifier.Multiplier;
                            break;

                        case AttributeModifierType.Override:
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void AddTemporaryOrPersistentEffectSpecToSet(EffectSpec spec)
        {
            if (!_appliedEffects.Contains(spec))
            {
                _appliedEffects.Add(spec);
            }
        }

        public void RemoveTemporaryOrPersistentEffectSpecFromSet(EffectSpec spec)
        {
            if (_appliedEffects.Contains(spec))
            {
                _appliedEffects.Remove(spec);
            }
        }

        public void ActivateAbility(int index)
        {
            if (TryGetAbility(index, out AbilitySpec spec))
            {
                StartCoroutine(spec.TryActivateAbility());
            }
        }

        public void ActivateAbility(AbilitySpec spec)
        {
            if (_abilities.Contains(spec))
            {
                StartCoroutine(spec.TryActivateAbility());
            }
        }

        public void AddAbilityToSet(AbilitySpec spec)
        {
            if (!_abilities.Contains(spec))
            {
                _abilities.Add(spec);
            }
        }

        public void RemoveAbilityFromSet(AbilitySpec spec)
        {
            if (_abilities.Contains(spec))
            {
                _abilities.Remove(spec);
            }
        }

        public bool TryGetAbility(int index, out AbilitySpec spec)
        {
            spec = null;

            if (_abilities.Count > index)
            {
                spec = _abilities[index];
                return true;
            }

            return false;
        }

        public AbilitySpec GetAbilityByType<T>() where T : ScriptableAbility
        {
            var typeOf = typeof(T);

            for (int i = 0; i < _abilities.Count; i++)
            {
                var spec = _abilities[i];
                var asset = spec.Asset;

                if (asset.GetType() == typeOf)
                {
                    return spec;
                }         
            }

#if UNITY_EDITOR
            Debug.LogError($"Could not find ability of type: {typeOf.Name}");
#endif

            return null;
        }

        public AbilitySpec GetRandomAbilityFromSet()
        {
            int seed = Random.Range(0, _abilities.Count);

            return _abilities[seed];
        }

        public void AddAttributeToSet(Attribute attribute)
        {
            var key = attribute.AttributeType;

            if (!_attributes.ContainsKey(key))
            {
                _attributes.Add(key, attribute);
            }
        }

        public void RemoveAttributeFromSet(Attribute attribute)
        {
            var key = attribute.AttributeType;

            if (_attributes.ContainsKey(key))
            {
                _attributes.Remove(key);
            }
        }

        public bool TryGetAttribute(AttributeType key, out Attribute attribute)
        {
            attribute = null;

            if (_attributes.ContainsKey(key))
            {
                attribute = _attributes[key];
                return true;
            }

            return false;
        }

        public Attribute GetAttributeByType(AttributeType type)
        {
            if (_attributes.ContainsKey(type))
            {
                return _attributes[type];
            }

            throw new System.Exception($"Could not find attribute of type: {type}");
        }

        public AbilitySpec[] GetAbilities()
        {
            return _abilities.ToArray();
        }

        public Attribute[] GetAttributes()
        {
            return _attributes.Retrieve();
        }

        public void CopyAbilities(params AbilitySpec[] abilities)
        {
            _abilities.Clear();

            for (int i = 0; i < abilities.Length; i++)
            {
                var spec = abilities[i];
                spec.Bind(this);

                AddAbilityToSet(spec);
            }
        }

        public void CopyAttributes(params Attribute[] attributes)
        {
            _attributes.Clear();

            for (int i = 0; i < attributes.Length; i++)
            {
                var attribute = attributes[i];
                AddAttributeToSet(attribute);
            }
        }

        public bool ContainsAbility(AbilitySpec spec)
        {
            return _abilities.Contains(spec);
        }
    }
}
