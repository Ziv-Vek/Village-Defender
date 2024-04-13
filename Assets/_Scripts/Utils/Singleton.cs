using UnityEngine;

namespace VillageDefender.Utils
{
    /** A base class to create a static Monobehaviour. */
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => Instance = this as T;

        protected void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    /** A base class to create a singleton Monobehaviour. It does not (!) persist between scenes. */
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            base.Awake();
        }
    }

    /** A base class to create a singleton Monobehaviour that persists between scenes. */
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}