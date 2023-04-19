using System;
using UnityEngine;

public class MinionEventArgs : BaseEventArgs {

    public MinionLogic minion;
    public int MinionSummonPos;

    public MinionEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player, MinionLogic minion, int position = -1) {
        CreateEventArgs(eventType, sender, player);
        this.minion = minion;
        MinionSummonPos = position;
        return this;
    }

}