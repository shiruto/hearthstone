using System;
using System.Collections.Generic;
using UnityEngine;

public class CardEventArgs : BaseEventArgs {
    public int position;
    public CardBase Card;
    public List<Buff> buffs;
    public CardEventArgs CreateEventArgs(CardEvent eventType, GameObject sender, CardBase card, int position = 0, List<Buff> buffs = null) {
        CreateEventArgs(eventType, sender);
        this.position = position;
        Card = card;
        this.buffs = buffs;
        return this;
    }
}