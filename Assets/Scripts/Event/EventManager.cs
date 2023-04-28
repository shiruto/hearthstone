using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {
    private static Dictionary<Enum, Action<BaseEventArgs>> _eventTable = new();

    #region Singleton
    private static EventManager _instance = null;
    public static EventManager Instance => _instance ??= new();

    private EventManager() {
        Capacity = 40;
        InitEvent();
    }
    #endregion

    private Action<BaseEventArgs> GetEventList(Enum _event) {
        if (!_eventTable.ContainsKey(_event)) {
            _eventTable.Add(_event, default);
        }
        return _eventTable[_event];
    }

    private void AddEvent(Enum _eventType, Action<BaseEventArgs> action) {
        Action<BaseEventArgs> actions = GetEventList(_eventType);
        if (null != action) {
            Delegate[] delegates = actions?.GetInvocationList();
            if (null != delegates) {
                if (!Array.Exists(delegates, v => v == (Delegate)action)) {
                    actions += action;
                }
                else {
                    Debug.LogWarningFormat("callback {0}.{1} already exists", _eventType.GetType().Name, _eventType.ToString());
                }
            }
            else {
                actions = action;
            }
            _eventTable[_eventType] = actions;
        }
        else {
            Debug.LogWarning("empty callback");
        }
    }

    private void CallEvent(BaseEventArgs args) {
        Action<BaseEventArgs> actions = GetEventList(args.EventType);
        actions?.Invoke(args);
        Recycle(args); //One Shot Event
    }

    private void DelEvent(Enum _eventType, Action<BaseEventArgs> action) {
        if (null != action) {
            Action<BaseEventArgs> actions = GetEventList(_eventType);
            if (null != action) {
                actions -= action;
            }
            _eventTable[_eventType] = actions;
        }
        else {
            Debug.LogWarning("null callback");
        }
    }

    private void DelEvent(Enum eventType) {
        _eventTable.Remove(eventType);
    }

    private void InitEvent() {
        recycled = new Dictionary<Type, Queue<BaseEventArgs>>();
        _eventTable = new Dictionary<Enum, Action<BaseEventArgs>>();
    }

    #region static functions
    public static void AddListener(Enum eventType, Action<BaseEventArgs> callback) {
        Instance.AddEvent(eventType, callback);
    }

    public static void Invoke(BaseEventArgs args) {
        Instance?.CallEvent(args);
    }

    public static void DelListener(Enum _eventType, Action<BaseEventArgs> action) {
        Instance?.DelEvent(_eventType, action);
    }

    public static void DelListener(Enum _eventType) {
        Instance?.DelEvent(_eventType);
    }

    public static void RemoveAllListener() {
        Instance?.InitEvent();
    }
    #endregion

    #region Object pool implement
    private Dictionary<Type, Queue<BaseEventArgs>> recycled;
    public int Capacity { get; set; }

    public static T Allocate<T>() where T : BaseEventArgs, new() { // 分配
        Type type = typeof(T);
        if (Instance.recycled.TryGetValue(type, out Queue<BaseEventArgs> args) && null != args && args.Count > 0) {
            T arg = args.Dequeue() as T; // 从池里取值
            arg.Dispose(); // 清空
            return arg;
        }
        return new T();
    }

    void Recycle(BaseEventArgs arg) {
        Type type = arg.GetType();
        if (Instance.recycled.TryGetValue(type, out Queue<BaseEventArgs> args)) {
            if (args.Count < Instance.Capacity) {
                args.Enqueue(arg);
                return;
            }
        }
        else {
            args = new Queue<BaseEventArgs>();
            args.Enqueue(arg);
            Instance.recycled.Add(type, args);
        }
        arg.Dispose();
    }
    #endregion

}

public static class EventManagerEx {
    /// <summary>
    /// 使用该参数类型的实例分发事件
    /// </summary>
    /// <param name="args">参数实例</param>
    public static void Invoke(this BaseEventArgs args) {
        EventManager.Invoke(args);
    }
}