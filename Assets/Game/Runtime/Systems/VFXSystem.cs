#pragma warning disable 0649
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public sealed class VFXSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] vfxPrefabs;

        private static readonly Dictionary<Type, GameObject> _registry = new Dictionary<Type, GameObject>();
        private static readonly Dictionary<Type, Queue<Cinematograph>> _particles = new Dictionary<Type, Queue<Cinematograph>>();

        private static Transform _transform;

        private void Awake()
        {
            CreateStartupRegistry();

            _transform = transform;
        }

        private void CreateStartupRegistry()
        {
            for (int i = 0; i < vfxPrefabs.Length; i++)
            {
                var prefab = vfxPrefabs[i];

                if (prefab.TryGetComponent(out Cinematograph cinematograph))
                {
                    var typeOf = cinematograph.GetType();

                    if (!_registry.ContainsKey(typeOf))
                    {
                        _registry.Add(typeOf, prefab);
                        _particles.Add(typeOf, new Queue<Cinematograph>());
                    }
                }
            }
        }

        public static void Emit<T>() where T : Cinematograph
        {
            Emit<T>(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public static void Emit<T>(Vector3 position) where T : Cinematograph
        {
            Emit<T>(position, Quaternion.identity, Vector3.one);
        }

        public static void Emit<T>(Vector3 position, Quaternion rotation, Vector3 scale) where T : Cinematograph
        {
            var entry = Pop<T>();

            entry.transform.position = position;
            entry.transform.rotation = rotation;
            entry.transform.localScale = scale;

            entry.Emit();
        }

        private static T Pop<T>() where T : Cinematograph
        {
            var typeOf = typeof(T);

            if (_particles.ContainsKey(typeOf))
            {
                var pool = _particles[typeOf];

                if (pool.Count == 0)
                {
                    pool.Enqueue(CreateInstance<T>());
                }

                var entry = pool.Dequeue();

                entry.onComplete += Push;
                entry.gameObject.SetActive(true);

                return (T)entry;
            }

            throw new Exception($"key of type: {typeOf} was not registered");
        }

        private static void Push(Cinematograph entry)
        {
            var typeOf = entry.GetType();

            if (_particles.ContainsKey(typeOf))
            {
                var pool = _particles[typeOf];

                entry.onComplete -= Push;
                entry.gameObject.SetActive(false);

                pool.Enqueue(entry);
            }
        }

        private static T CreateInstance<T>() where T : Cinematograph
        {
            var typeOf = typeof(T);

            if (_registry.TryGetValue(typeOf, out GameObject gameObject))
            {
                var instance = Instantiate(gameObject, _transform);
                var cinematograph = instance.GetComponent<Cinematograph>();

                return (T)cinematograph;
            }

            return null;
        }
    }
}
