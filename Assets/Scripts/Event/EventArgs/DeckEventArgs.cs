using System;
using UnityEngine;

public class DeckEventArgs : BaseEventArgs {
    public DeckAsset Deck;
    public BaseEventArgs CreateEventArgs(Enum eventType, GameObject sender, DeckAsset _deck) {
        base.CreateEventArgs(eventType, sender, null);
        Deck = _deck;
        return this;
    }
}