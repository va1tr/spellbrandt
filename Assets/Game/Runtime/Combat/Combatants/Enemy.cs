using UnityEngine;

namespace Spellbrandt
{
    public class Enemy : Combatant
    {
        protected Monster monster;
        protected BTRoot ai;

        protected override void PreInitialise()
        {
            base.PreInitialise();

            Affinity = Affinity.Hostile;
            State = CombatantState.None;
        }

        protected override void Initialise()
        {
            ai = BT.Root();

            ai.Open(
                BT.Wait(0f, 5f),
                BT.Call(MoveToRandomBoardTile),
                BT.Wait(0f, 1f),
                BT.Call(ActivateRandomAbility)
                );

            ai.IsActive = false;
        }

        public override void Respawn(Monster monster)
        {
            this.monster = monster;

            base.Respawn(monster);

            ai.IsActive = true;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (ai.IsActive)
            {
                ai.OnUpdate();
            }
        }

        public override void Despawn(Monster monster)
        {
            ai.IsActive = false;

            base.Despawn(monster);

            this.monster = null;
        }

        public override void OnDied()
        {
            monster.IsAlive = false;

            Despawn(monster);

            base.OnDied();
        }

        protected void ActivateRandomAbility()
        {
            if (CanAttack())
            {
                var spec = abilitySystemComponent.GetRandomAbilityFromSet();

                StartCoroutine(AttackSequence(monster, spec));
            }
        }

        protected void MoveToRandomBoardTile()
        {
            if (!CanMove())
            {
                return; 
            }

            var direction = new Vector3Int();
            var target = new Vector3Int();

            do
            {
                direction = new Vector3Int(Random.Range(-1, 2), Random.Range(-1, 2), 0);

                if (direction.y != 0 && direction.x != 0)
                {
                    float seed = Random.value;

                    if (seed > 0.5f)
                    {
                        direction.y = 0;
                    }
                    else
                    {
                        direction.x = 0;
                    }
                }

                target = gameBoard.WorldToCell(transform.position) + direction;

            } while (!gameBoard.CanMoveToTile(target, Affinity));

            AttemptMoveToGameBoardTile(direction);
        }
    }
}
