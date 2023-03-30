using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem {
    private static Dictionary<Enum, Action<BaseEventArgs>> _eventTable = new();
    #region Singleton
    private static EventSystem _instance = null;
    public static EventSystem Instance => _instance ?? new();
    private EventSystem() {
        this.Capacity = 40;
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
            Debug.LogWarning("no such event exists");
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
        _instance.AddEvent(eventType, callback);
    }
    public static void Invoke(BaseEventArgs args) {
        _instance?.CallEvent(args);
    }
    public static void DelListener(Enum _eventType, Action<BaseEventArgs> action) {
        _instance?.DelEvent(_eventType, action);
    }
    public static void DelListener(Enum _eventType) {
        _instance?.DelEvent(_eventType);
    }
    public static void RemoveAllListener() {
        _instance?.InitEvent();
    }
    #endregion

    #region Object pool implement
    internal Dictionary<Type, Queue<BaseEventArgs>> recycled;
    int Capacity { get; set; }
    public static T Allocate<T>() where T : BaseEventArgs, new() { // 分配
        Type type = typeof(T);
        if (Instance.recycled.TryGetValue(type, out Queue<BaseEventArgs> args)) {
            if (null != args && args.Count == Instance.Capacity) {
                T arg = args.Dequeue() as T; // 从池里取值
                arg.Dispose(); // 清空
                return arg;
            }
        }
        return new T();
    }
    void Recycle(BaseEventArgs target) {
        Type type = target.GetType();
        if (!Instance.recycled.TryGetValue(type, out Queue<BaseEventArgs> args)) {
            args = new Queue<BaseEventArgs>();
        }
        if (args.Count < Instance.Capacity && !args.Contains(target)) {
            args.Enqueue(target);
        }
        else {
            target.Dispose();
        }
        Instance.recycled[type] = args;
    }
    #endregion
}
