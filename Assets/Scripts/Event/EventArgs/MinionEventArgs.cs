using System;
using UnityEngine;

public class MinionEventArgs : BaseEventArgs {
    public MinionLogic minion;
    public int MinionSummonPos;
    public MinionEventArgs CreateEventArgs(Enum eventType, GameObject sender, MinionLogic minion, int position = -1) {
        CreateEventArgs(eventType, sender);
        this.minion = minion;
        MinionSummonPos = position;
        return this;
    }

}