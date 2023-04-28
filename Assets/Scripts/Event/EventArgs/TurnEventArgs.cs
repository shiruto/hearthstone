using System;
using UnityEngine;

public class TurnEventArgs : BaseEventArgs {

    public int TurnNum;
    public GameStatus status;

    public TurnEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player, int TurnNum, GameStatus status = GameStatus.Playing) {
        CreateEventArgs(eventType, sender, player);
        this.TurnNum = TurnNum;
        this.status = status;
        return this;
    }
}