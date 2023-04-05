using System;
using UnityEngine;

public class TurnEventArgs : BaseEventArgs {
    public int TurnNum;
    public bool isOver;
    public TurnEventArgs CreateEventArgs(Enum eventType, GameObject sender, int TurnNum, bool isOver = false) {
        CreateEventArgs(eventType, sender);
        this.TurnNum = TurnNum;
        this.isOver = isOver;
        return this;
    }
}