using System;
using UnityEngine;

public class AttackEventArgs : BaseEventArgs {
    public ICharacter attacker;
    public ICharacter target;
    public int damage;

    public AttackEventArgs CreateEventArgs(Enum eventType, GameObject Sender, ICharacter attacker, ICharacter target, int damage = 0) {
        CreateEventArgs(eventType, Sender, null);
        this.attacker = attacker;
        this.target = target;
        this.damage = damage;
        return this;
    }

}