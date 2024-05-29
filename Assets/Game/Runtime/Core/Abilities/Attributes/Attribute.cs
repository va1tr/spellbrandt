using System;

namespace Spellbrandt
{
    public enum AttributeType
    {
        Default,
        Health,
        Attack,
        Defence,
        Speed
    }

    public class Attribute
    {
        public readonly AttributeType AttributeType;

        public event Action onAttributeChanged;
        public event Action onMaxAttributeChanged;

        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                onAttributeChanged?.Invoke();
            }
        }

        public float MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                onMaxAttributeChanged?.Invoke();
            }
        }

        private float _value;
        private float _maxValue;

        public Attribute(AttributeType attributeType, float value)
        {
            AttributeType = attributeType;

            _value = value;
            _maxValue = value;
        }

        public Attribute(AttributeType attributeType, float value, float maxValue)
        {
            AttributeType = attributeType;

            _value = value;
            _maxValue = maxValue;
        }
    }
}