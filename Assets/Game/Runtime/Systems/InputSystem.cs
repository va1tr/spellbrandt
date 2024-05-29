using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spellbrandt
{
    public sealed class InputSystem : MonoBehaviour
    {
        public static event Action<Vector2> onMove;
        public static event Action onAbility;
        public static event Action onAttack;

        public static event Action<Vector2> onNavigate;

        private static PlayerInputActions _input;

        private void Awake()
        {
            _input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.Move.performed += ctx => OnMovePerformed(ctx);
            _input.Player.Ability.performed += ctx => OnAbilityPerformed();
            _input.Player.Attack.performed += ctx => OnAttackPerformed();

            _input.UI.Navigate.performed += ctx => OnNavigatePerformed(ctx);
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();

            if (value.y != 0f)
            {
                value.x = 0f;
            }

            onMove?.Invoke(value);
        }

        private void OnAbilityPerformed()
        {
            onAbility?.Invoke();
        }

        private void OnAttackPerformed()
        {
            onAttack?.Invoke();
        }

        private void OnNavigatePerformed(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();

            if (value.sqrMagnitude != 0f)
            {
                onNavigate?.Invoke(value);
            }
        }

        public static void SetActionMapEnabled(string actionMapName)
        {
            _input.asset.FindActionMap(actionMapName).Enable();
        }

        public static void SetActionMapDisabled(string actionMapName)
        {
            _input.asset.FindActionMap(actionMapName).Disable();
        }

        private void OnDisable()
        {
            _input.Player.Move.performed -= ctx => OnMovePerformed(ctx);
            _input.Player.Ability.performed -= ctx => OnAbilityPerformed();
            _input.Player.Attack.performed -= ctx => OnAttackPerformed();

            _input.UI.Navigate.performed -= ctx => OnNavigatePerformed(ctx);
        }
    }
}
