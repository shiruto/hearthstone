using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckLogic {
    private readonly int MaxCardNum = 60;

    public List<CardViewController> Deck = new();

    public void DrawCards(int Num) {
        for (; Num > 0; Num--) {
            EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDraw, null, RemoveCardFromDeckAt(0)));
        }
    }

    public CardViewController RemoveCardFromDeckAt(int index) {
        CardViewController Card = Deck[index];
        Deck.RemoveAt(index);
        EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.DeckVisualUpdate));
        return Card;
    }

    public void SortDeck() {
        Deck.Sort((CardViewController a, CardViewController b) => a.ManaCost.CompareTo(b.ManaCost));
    }

    public void AddCardToDeck(int position, CardViewController Card) {
        if (Deck.Count == 0) Deck.Add(Card);
        else if (Deck.Count < MaxCardNum) Deck.Insert(position, Card);
        else Debug.Log("Too Many Cards in Deck");
    }

}
