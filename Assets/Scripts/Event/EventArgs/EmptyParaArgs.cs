using System;
using UnityEngine;

public class EmptyParaArgs : BaseEventArgs {

    public EmptyParaArgs CreateEventArgs(Enum eventType) {
        CreateEventArgs(eventType, null, null);
        return this;
    }
}