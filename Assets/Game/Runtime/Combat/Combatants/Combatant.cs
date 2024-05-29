using UnityEngine;

namespace Spellbrandt
{
    public enum Affinity
    {
        None,
        Hostile,
        Friendly
    }

    public enum CombatantState
    {
        None,
        Idle,
        Move,
        Attack,
        Hit
    }

    [RequireComponent(typeof(AbilitySystemComponent))]
    public abstract class Combatant : MonoBehaviour
    {
        public Affinity Affinity { get; protected set; }
        public CombatantState State { get; protected set; }

        protected GameBoard gameBoard;

        protected SpriteRenderer spriteRenderer;
        protected AbilitySystemComponent abilitySystemComponent;

        protected Attribute health;
        protected Attribute attack;
        protected Attribute defence;
        protected Attribute speed;

        private void Awake()
        {
            gameBoard = FindObjectOfType<GameBoard>();

            spriteRenderer = GetComponent<SpriteRenderer>();
            abilitySystemComponent = GetComponent<AbilitySystemComponent>();

            PreInitialise();
        }

        protected virtual void PreInitialise()
        {

        }

        private void Start()
        {
            Initialise();
        }

        protected virtual void Initialise()
        {

        }

        public virtual void OnUpdate()
        {
            abilitySystemComponent.UpdateAndTryRemoveAppliedEffects();
            abilitySystemComponent.UpdateAllAbilitiesCooldown();
        }

        public virtual void Respawn(Monster monster)
        {
            gameObject.name = monster.Asset.Name;
            spriteRenderer.sprite = monster.Asset.IdleSprite;

            abilitySystemComponent.CopyAttributes(monster.GetAttributes());
            abilitySystemComponent.CopyAbilities(monster.GetAbilities());

            health = abilitySystemComponent.GetAttributeByType(AttributeType.Health);
            health.onAttributeChanged += OnHit;

            attack = abilitySystemComponent.GetAttributeByType(AttributeType.Attack);
            defence = abilitySystemComponent.GetAttributeByType(AttributeType.Defence);
            speed = abilitySystemComponent.GetAttributeByType(AttributeType.Speed);

            VFXSystem.Emit<Sigil>(transform.position);
            CameraShake.Emit();

            SnapCombatantToGameBoardTile();
            AddCombatantToGameBoardTile();
            ChangeState(CombatantState.Idle);
        }

        public virtual void Despawn(Monster monster)
        {
            RemoveCombatantFromGameBoardTile();
            ChangeState(CombatantState.None);

            gameObject.name = string.Empty;
            spriteRenderer.sprite = null;

            monster.CopyAttributes(abilitySystemComponent.GetAttributes());
            monster.CopyAbilities(abilitySystemComponent.GetAbilities());

            health.onAttributeChanged -= OnHit;
            health = null;

            attack = null;
            defence = null;
            speed = null;
        }

        public virtual void OnHit()
        {
            StartCoroutine(HitSequence());

            if (health.Value <= float.Epsilon)
            {
                OnDied();
            }
        }

        public virtual void OnDied()
        {
            var args = new CombatantDiedEventArgs(this, Affinity);
            EventSystem.Publish(args);
        }

        protected bool CanAttack()
        {
            return State == CombatantState.Idle && State != CombatantState.Hit;
        }

        protected bool CanMove()
        {
            return State == CombatantState.Idle;
        }

        protected void ChangeState(CombatantState state)
        {
            State = state;
        }

        protected void AttemptMoveToGameBoardTile(Vector3Int direction)
        {
            var target = gameBoard.WorldToCell(transform.position) + direction;

            if (gameBoard.CanMoveToTile(target, Affinity))
            {
                StartCoroutine(MoveSequence(gameBoard.GetCellCenterWorld(target)));
            }
        }

        protected void AddCombatantToGameBoardTile()
        {
            var cellPosition = gameBoard.WorldToCell(transform.position);
            gameBoard.AddGameObjectToTile(cellPosition, gameObject);
        }

        protected void RemoveCombatantFromGameBoardTile()
        {
            var cellPosition = gameBoard.WorldToCell(transform.position);
            gameBoard.RemoveGameObjectFromTile(cellPosition);
        }

        protected void SnapCombatantToGameBoardTile()
        {
            var cellPosition = gameBoard.WorldToCell(transform.position);
            transform.position = gameBoard.GetCellCenterWorld(cellPosition);
        }

        protected System.Collections.IEnumerator MoveSequence(Vector3 target)
        {
            ChangeState(CombatantState.Move);
            RemoveCombatantFromGameBoardTile();

            yield return Tween.Position.To(transform, target, 0.0625f / speed.Value, Easing.Linear);

            AddCombatantToGameBoardTile();
            ChangeState(CombatantState.Idle);
        }

        protected System.Collections.IEnumerator AttackSequence(Monster monster, AbilitySpec spec)
        {
            ChangeState(CombatantState.Attack);
            spriteRenderer.sprite = monster.Asset.AttackSprite;

            yield return spec.TryActivateAbility();

            spriteRenderer.sprite = monster.Asset.IdleSprite;
            ChangeState(CombatantState.Idle);
        }

        protected System.Collections.IEnumerator HitSequence()
        {
            ChangeState(CombatantState.Hit);

            CameraShake.Emit();

            yield return Tween.Tint.ToOffset(spriteRenderer.material, 1f, 0.1f, Easing.PingPong);
            yield return Tween.Tint.ToOffset(spriteRenderer.material, 1f, 0.1f, Easing.PingPong);

            ChangeState(CombatantState.Idle);
        }
    }
}
