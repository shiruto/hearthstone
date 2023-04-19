using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventArgs : IDisposable {
    public Enum EventType { get; protected set; }
    public GameObject Sender { get; protected set; }
    public PlayerLogic Player { get; protected set; }

    public BaseEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player) {
        EventType = eventType;
        Sender = sender;
        Player = player;
        return this;
    }

    public virtual void Dispose() {
        Sender = null;
    }
}
