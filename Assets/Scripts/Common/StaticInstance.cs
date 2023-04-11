using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例对象允许重置
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get;
        private set;
    }

    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// 单例，切换场景后会被销毁
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        base.Awake();
    }
}

/// <summary>
/// 常驻节点单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}

public abstract class NormalSingleton<T> where T : new()
{
    private static T _inst;
    public static T Instance
    {
        get
        {
            if (_inst == null)
            {
                _inst = new T();
            }
            return _inst;
        }
    }
}
