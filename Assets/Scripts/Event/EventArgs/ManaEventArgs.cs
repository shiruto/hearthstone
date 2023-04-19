using System;
using UnityEngine;

public class ManaEventArgs : BaseEventArgs {
    public int tempCrystalNum;
    public ManaEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player, int tempCrystalNum) {
        CreateEventArgs(eventType, sender, player);
        this.tempCrystalNum = tempCrystalNum;
        return this;
    }
}