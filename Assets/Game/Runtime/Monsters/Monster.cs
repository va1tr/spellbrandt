using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public class Monster
    {
        public bool IsAlive { get; set; }

        public readonly ScriptableMonster Asset;
        public readonly Summoner Owner;

        private readonly Dictionary<AttributeType, Attribute> _attributes = new Dictionary<AttributeType, Attribute>();
        private readonly List<AbilitySpec> _abilities = new List<AbilitySpec>();

        public Monster(ScriptableMonster asset)
        {
            Asset = asset;

            CreateStartupAttributes(asset);
            CreateStartupAbilities(asset);

            IsAlive = true;
        }

        public Monster(ScriptableMonster asset, Summoner owner) : this(asset)
        {
            Owner = owner;
        }

        private void CreateStartupAttributes(ScriptableMonster asset)
        {
            var health = new Attribute(AttributeType.Health, asset.Health);
            AddAttributeToSet(health);

            var attack = new Attribute(AttributeType.Attack, asset.Attack);
            AddAttributeToSet(attack);

            var defence = new Attribute(AttributeType.Defence, asset.Defence);
            AddAttributeToSet(defence);

            var speed = new Attribute(AttributeType.Speed, asset.Speed);
            AddAttributeToSet(speed);
        }

        private void CreateStartupAbilities(ScriptableMonster asset)
        {
            var abilities = asset.Abilities;

            for (int i = 0; i < abilities.Length; i++)
            {
                var spec = abilities[i].CreateAbilitySpec();

                AddAbilityToSet(spec);
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

        public void CopyAbilities(params AbilitySpec[] abilities)
        {
            _abilities.Clear();

            for (int i = 0; i < abilities.Length; i++)
            {
                var spec = abilities[i];
                spec.UnBind();

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

        public AbilitySpec[] GetAbilities()
        {
            return _abilities.ToArray();
        }

        public Attribute[] GetAttributes()
        {
            return _attributes.Retrieve();
        }
    }
}
