using System;
using UnityEngine;

public class ManaEventArgs : BaseEventArgs {
    public int tempCrystalNum;
    public ManaEventArgs CreateEventArgs(Enum eventType, GameObject sender, int tempCrystalNum) {
        CreateEventArgs(eventType, sender);
        this.tempCrystalNum = tempCrystalNum;
        return this;
    }
}