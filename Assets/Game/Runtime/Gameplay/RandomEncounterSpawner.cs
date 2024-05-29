#pragma warning disable 0649
using UnityEngine;

namespace Spellbrandt
{
    public class RandomEncounterSpawner : MonoBehaviour
    {
        [SerializeField]
        private MonsterRuntimeSet monsterSet;

        [SerializeField]
        private Enemy enemy;

        public void SpawnRandomEncounterFromSet()
        {
            var encounter = monsterSet.GetItem(Random.Range(0, monsterSet.Count()));
            var monster = encounter.CreateMonsterFromAsset();

            enemy.Respawn(monster);

            var abilitySystemComponent = enemy.GetComponent<AbilitySystemComponent>();

            var args = new EnemySummonedEventArgs(new AbilitySystemComponent[] { abilitySystemComponent });
            EventSystem.Publish(args);
        }
    }
}
