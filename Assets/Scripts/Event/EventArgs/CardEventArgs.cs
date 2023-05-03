using System;
using System.Collections.Generic;
using UnityEngine;

public class CardEventArgs : BaseEventArgs {
    public int position;
    public CardBase Card;
    public List<Buff> buffs;

    public CardEventArgs CreateEventArgs(CardEvent eventType, GameObject sender, PlayerLogic player = null, CardBase card = null, int position = 0, List<Buff> buffs = null) {
        base.CreateEventArgs(eventType, sender, player);
        this.position = position;
        Card = card;
        this.buffs = buffs;
        return this;
    }
}