using System;
using UnityEngine;

public class ManaEventArgs : BaseEventArgs {

    public int CrystalNum;
    public ManaEventArgs CreateEventArgs(Enum eventType, GameObject sender, PlayerLogic player, int CrystalNum) {
        CreateEventArgs(eventType, sender, player);
        this.CrystalNum = CrystalNum;
        return this;
    }

}