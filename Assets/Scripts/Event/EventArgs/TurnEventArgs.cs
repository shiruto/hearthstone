using System;
using UnityEngine;

public class TurnEventArgs : BaseEventArgs {

    public int TurnNum;
    public GameDataAsset.GameStatus status;

    public TurnEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player, int TurnNum, GameDataAsset.GameStatus status = GameDataAsset.GameStatus.Playing) {
        CreateEventArgs(eventType, sender, player);
        this.TurnNum = TurnNum;
        this.status = status;
        return this;
    }
}