using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckLogic : MonoBehaviour {
    public static event Action<CardAsset> OnDraw;

    private DeckLogic() {

    }
    private static DeckLogic yourDeck = null;
    private static DeckLogic opponentDeck = null;
    public static DeckLogic YourDeck() {
        if (yourDeck == null) {
            yourDeck = new();
        }
        return yourDeck;
    }
    public static DeckLogic OpponentDeck() {
        if (opponentDeck == null) {
            opponentDeck = new();
        }
        return opponentDeck;
    }
    private int MaxCardNum = 60;
    private List<CardAsset> Deck = new();
    private enum Sum { Empty, LastOne, Less, Medium, Alot, Full }

    private void DrawCard() {
        OnDraw?.Invoke(RemoveCard(0));
    }

    private CardAsset RemoveCard(int index) {
        CardAsset Card = Deck[index - 1];
        Deck.RemoveAt(index - 1);
        return Card;
    }

    private void Sort() {
        Deck.Sort((CardAsset a, CardAsset b) => {
            return a.ManaCost.CompareTo(b.ManaCost);
        });
    }

    private void AddCard(CardAsset Card) {
        Deck.Add(Card);
    }

}
