using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventArgs : IDisposable {
    public Enum EventType { get; protected set; }
    public GameObject Sender { get; protected set; }

    public BaseEventArgs CreateEventArgs(Enum eventType, GameObject sender) {
        EventType = eventType;
        Sender = sender;
        return this;
    }

    public virtual void Dispose() {
        Sender = null;
    }
}
