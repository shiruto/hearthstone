using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckLogic : MonoBehaviour {
    private readonly int MaxCardNum = 60;
    public List<CardBase> Deck;
    private enum Sum { Empty, LastOne, Less, Medium, Alot, Full }

    public void DrawCards(int Num) {
        for (; Num > 0; Num--) {
            EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDraw, gameObject, RemoveCardFromDeckAt(0)));
        }
    }

    public CardBase RemoveCardFromDeckAt(int index) {
        CardBase Card = Deck[index];
        Deck.RemoveAt(index);
        return Card;
    }

    public void SortDeck() {
        Deck.Sort((CardBase a, CardBase b) => {
            return a.CurManaCost.CompareTo(b.CurManaCost);
        });
    }

    public void AddCardToDeck(CardBase Card) {
        if (Deck.Count < MaxCardNum) Deck.Add(Card);
        else Debug.Log("Too Many Cards in Deck");
    }

}
