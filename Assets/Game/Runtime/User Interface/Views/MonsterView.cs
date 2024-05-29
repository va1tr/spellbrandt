#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Spellbrandt
{
    public class MonsterView : MonoBehaviour
    {
        [SerializeField]
        private Image background;

        private Monster _monster;

        public void Bind(Monster monster)
        {
            _monster = monster;

            Refresh();
        }

        private void Refresh()
        {
            background.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }
}