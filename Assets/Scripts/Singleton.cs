using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    private static bool _initialized;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                _instance.Initialize();
            }

            return _instance;
        }
    }

    [SerializeField] protected bool isPersistent;

    public virtual void Awake()
    {
        if (!_instance)
        {
            _instance = (T) this;
        }
        
        if (_instance != this)
        {
            Destroy(gameObject);
        }

        if (!_initialized)
            Initialize();
    }

    public virtual void Initialize()
    {
        if (isPersistent)
            DontDestroyOnLoad(gameObject);
        
        _initialized = true;
    }
}