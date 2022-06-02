using System;
using UnityEngine;

namespace Assets.Core
{
    public abstract class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static readonly Lazy<T> _lazyInstance = new Lazy<T>(CreateInstance);
        public static T Instance => _lazyInstance.Value;
        static T CreateInstance()
        {
            var owner = new GameObject($"Lazy{typeof(T).Name}");
            var instance = owner.AddComponent<T>();
            DontDestroyOnLoad(owner);
            return instance;
        }
    }
}
