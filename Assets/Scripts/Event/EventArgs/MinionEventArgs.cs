using System;
using UnityEngine;

public class MinionEventArgs : BaseEventArgs {

    public MinionLogic minion;
    public int position;

    public MinionEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player, MinionLogic minion, int position = -1) {
        CreateEventArgs(eventType, sender, player);
        this.minion = minion;
        this.position = position;
        return this;
    }

}