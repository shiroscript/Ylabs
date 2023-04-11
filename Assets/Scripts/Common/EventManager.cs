using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using Object = UnityEngine.Object;

public class EventManager
{
    private static Dictionary<int, List<string>> instanceId2Key = new Dictionary<int, List<string>>();
    private static Dictionary<string, Delegate> uniqueKey2Callback = new Dictionary<string, Delegate>();
    private static Dictionary<string, List<string>> key2UniqueKey = new Dictionary<string, List<string>>();
    private static Dictionary<string, int> uniqueKey2ArgCount = new Dictionary<string, int>();

    #region Register
    public static void Register(string key, Object target, Action callback)
    {
        _RegisterEvent(key, 0, target, callback);
    }

    public static void Register<T1>(string key, Object target, Action<T1> callback)
    {
        _RegisterEvent(key, 1, target, callback);
    }

    public static void Register<T1, T2>(string key, Object target, Action<T1, T2> callback)
    {
        _RegisterEvent(key, 2, target, callback);
    }

    public static void Register<T1, T2, T3>(string key, Object target, Action<T1, T2, T3> callback)
    {
        _RegisterEvent(key, 3, target, callback);
    }

    public static void Register<T1, T2, T3, T4>(string key, Object target, Action<T1, T2, T3, T4> callback)
    {
        _RegisterEvent(key, 4, target, callback);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void _RegisterEvent(string key, int argCount, Object target, Delegate callback)
    {
        string uniqueKey = GetUniqueKey(key, target);

        if (uniqueKey2Callback.ContainsKey(uniqueKey))    // 已经注册过该事件
        {
            return;
        }
        uniqueKey2Callback[uniqueKey] = callback;
        uniqueKey2ArgCount[uniqueKey] = argCount;

        if (!key2UniqueKey.ContainsKey(key))
        {
            key2UniqueKey.Add(key, new List<string>());
        }
        key2UniqueKey[key].Add(uniqueKey);

        int targetId = target.GetInstanceID();
        if (!instanceId2Key.ContainsKey(targetId))
        {
            instanceId2Key.Add(targetId, new List<string>());
        }
        instanceId2Key[targetId].Add(key);
    }
    #endregion

    #region Deregister

    public static void Deregister(string key, Object target, Action callback)
    {
        Delegate tempDelegate = callback;
        _Deregister(key, target, tempDelegate);
    }

    public static void Deregister<T1>(string key, Object target, Action<T1> callback)
    {
        Delegate tempDelegate = callback;
        _Deregister(key, target, tempDelegate);
    }

    public static void Deregister<T1, T2>(string key, Object target, Action<T1, T2> callback)
    {
        Delegate tempDelegate = callback;
        _Deregister(key, target, tempDelegate);
    }

    public static void Deregister<T1, T2, T3>(string key, Object target, Action<T1, T2, T3> callback)
    {
        Delegate tempDelegate = callback;
        _Deregister(key, target, tempDelegate);
    }

    public static void Deregister<T1, T2, T3, T4>(string key, Object target, Action<T1, T2, T3, T4> callback)
    {
        Delegate tempDelegate = callback;
        _Deregister(key, target, tempDelegate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void _Deregister(string key, Object target, Delegate callback)
    {
        int targetId = target.GetInstanceID();
        string uniqueKey = GetUniqueKey(key, target);
        if (!uniqueKey2Callback.ContainsKey(uniqueKey))
        {
            return;
        }

        uniqueKey2Callback.Remove(uniqueKey);
        uniqueKey2ArgCount.Remove(uniqueKey);
        key2UniqueKey[key].Remove(uniqueKey);
        instanceId2Key[targetId].Remove(key);

        if(key2UniqueKey[key].Count == 0)
        {
            key2UniqueKey.Remove(key);
        }

        if (instanceId2Key[targetId].Count == 0)
        {
            instanceId2Key.Remove(targetId);
        }
    }

    public static void DeregisterAll(Object target)
    {
        int targetId = target.GetInstanceID();
        if(instanceId2Key.TryGetValue(targetId, out List<string> nameOfEvents))
        {
            foreach(string key in nameOfEvents)
            {
                string uniqueKey = GetUniqueKey(key, target);
                uniqueKey2Callback.Remove(uniqueKey);
                uniqueKey2ArgCount.Remove(uniqueKey);
                key2UniqueKey[key].Remove(uniqueKey);
                if(key2UniqueKey[key].Count == 0)
                {
                    key2UniqueKey.Remove(key);
                }
            }
        }
        instanceId2Key.Remove(targetId);
    }
    #endregion

    #region Fire
    // 分发事件时，可以向参数个数小于分发时参数个数的回调发送，不能向多于分发参数个数的回调发送事件，因为不知道多余的数据类型是什么。

    public static void Fire<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        _Fire(key, 4, arg1, arg2, arg3, arg4);
    }

    public static void Fire<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3)
    {
        _Fire(key, 3, arg1, arg2, arg3, arg3);
    }

    public static void Fire<T1, T2>(string key, T1 arg1, T2 arg2)
    {
        _Fire(key, 2, arg1, arg2, arg2, arg2);
    }

    public static void Fire<T>(string key, T arg1)
    {
        _Fire(key, 1, arg1, arg1, arg1, arg1);
    }

    public static void Fire(string key)
    {
        if (key2UniqueKey.TryGetValue(key, out List<string> nameOfEvents))
        {
            foreach (string uniqueKey in nameOfEvents)
            {
                Delegate callback = uniqueKey2Callback[uniqueKey];
                int argCount = uniqueKey2ArgCount[uniqueKey];
                if(argCount != 0)
                {
                    throw new Exception($"callback arg count not zero! {uniqueKey}");
                }
                else
                {
                    ((Action)callback)();
                }
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void _Fire<T1, T2, T3, T4>(string key, int realArgCount, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (key2UniqueKey.TryGetValue(key, out List<string> nameOfEvents))
        {
            foreach (string uniqueKey in nameOfEvents)
            {
                Delegate callback = uniqueKey2Callback[uniqueKey];
                int argCount = uniqueKey2ArgCount[uniqueKey];
                if(argCount > realArgCount)
                {
                    throw new Exception($"callback arg count > {realArgCount}. {uniqueKey}");
                }
                switch (argCount)
                {
                    case 0:
                        ((Action)callback)();
                        break;
                    case 1:
                        ((Action<T1>)callback)(arg1);
                        break;
                    case 2:
                        ((Action<T1, T2>)callback)(arg1, arg2);
                        break;
                    case 3:
                        ((Action<T1, T2, T3>)callback)(arg1, arg2, arg3);
                        break;
                    case 4:
                        ((Action<T1, T2, T3, T4>)callback)(arg1, arg2, arg3, arg4);
                        break;
                }
            }
        }
    }
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetUniqueKey(string key, Object target)
    {
        return $"{target.name}_{target.GetInstanceID()}_{key}";
    }
}