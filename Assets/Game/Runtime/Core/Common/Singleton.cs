using UnityEngine;

namespace Spellbrandt
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        private static T _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if(_instance != this)
            {
                Destroy(gameObject);
            }

            Initialise();
        }

        protected virtual void Initialise()
        {

        }
    }
}
