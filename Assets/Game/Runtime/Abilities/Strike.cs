#pragma warning disable 0649
using System;
using System.Collections;
using UnityEngine;

namespace Spellbrandt
{
    [CreateAssetMenu(fileName = "new-strike-ability", menuName = "ScriptableObjects/Abilities/Strike")]
    public sealed class Strike : ScriptableAbility
    {
        [SerializeField]
        private ScriptableEffect effect;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new StrikeAbilitySpec(this);
        }

        public override AbilitySpec CreateAbilitySpec(AbilitySystemComponent instigator)
        {
            return new StrikeAbilitySpec(this, instigator);
        }

        internal sealed class StrikeAbilitySpec : AbilitySpec
        {
            private GameBoard _gameBoard;
            private ScriptableEffect _effect;

            private const float delay = 0.1f;
            private const float offset = 0.0625f;

            public StrikeAbilitySpec(ScriptableAbility asset) : base(asset)
            {
                _gameBoard = GameBoard.Instance;
                _effect = ((Strike)asset).effect;
            }

            public StrikeAbilitySpec(ScriptableAbility asset, AbilitySystemComponent instigator) : base(asset, instigator)
            {
                _gameBoard = GameBoard.Instance;
                _effect = ((Strike)asset).effect;
            }

            protected override IEnumerator Activate()
            {
                Debug.Log("activated strike ability");

                var direction = (Vector3.zero - Instigator.transform.position).normalized;
                direction.x = direction.x > 0 ? 1f : -1f;

                var cellPosition = _gameBoard.WorldToCell(Instigator.transform.position);
                var rotation = Instigator.transform.rotation;

                var scale = Vector3.one;
                scale.x *= direction.x;

                while (true)
                {
                    cellPosition.x += 1 * Mathf.RoundToInt(direction.x);

                    if (!_gameBoard.HasTile(cellPosition))
                    {
                        yield break;
                    }

                    var worldPosition = _gameBoard.GetCellCenterWorld(cellPosition);

                    VFXSystem.Emit<Sword>(worldPosition, rotation, scale);

                    if (_gameBoard.TryGetTileGameObject(cellPosition, out GameObject content))
                    {
                        if (content.TryGetComponent(out AbilitySystemComponent target) && target != Instigator)
                        {
                            var spec = new EffectSpec(_effect, Instigator, target);
                            target.ApplyEffectSpecToTarget(spec);
                        }
                    }

                    yield return Tween.Position.ToOffset(Instigator.transform, Vector3.right * direction.x * offset, delay, Easing.PingPong);
                }            
            }
        }
    }
}