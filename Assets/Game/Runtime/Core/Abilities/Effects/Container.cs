using System;
using UnityEngine;

namespace Spellbrandt
{
    public enum EffectType
    {
        Direct,
        Temporary,
        Persistent
    }

    public enum AttributeModifierType
    {
        Add,
        Subtract,
        Mulitply,
        Override
    }

    [Serializable]
    public struct Container
    {
        [SerializeField]
        public EffectType Type;

        [SerializeField]
        public EffectModifiers[] Modifiers;

        [SerializeField]
        public float Periodic;

        [SerializeField]
        public float Duration;
    }

    [Serializable]
    public struct EffectModifiers
    {
        [SerializeField]
        public AttributeType Attribute;

        [SerializeField]
        public AttributeModifierType Type;

        [SerializeField]
        public float Multiplier;
    }
}
