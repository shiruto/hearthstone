using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckLogic : MonoBehaviour {

    private readonly int MaxCardNum = 60;
    private List<CardBase> Deck;
    private enum Sum { Empty, LastOne, Less, Medium, Alot, Full }

    public List<CardBase> DrawCards(int Num) {
        List<CardBase> temp = new();
        for (; Num > 0; Num--) {
            temp.Add(RemoveCard(0));
        }
        return temp;
    }

    public CardBase RemoveCard(int index) {
        CardBase Card = Deck[index];
        Deck.RemoveAt(index);
        return Card;
    }

    private void Sort() {
        Deck.Sort((CardBase a, CardBase b) => {
            return a.CurManaCost.CompareTo(b.CurManaCost);
        });
    }

    public void AddCard(CardBase Card) {
        if (Deck.Count < MaxCardNum) Deck.Add(Card);
        else Debug.Log("Too Many Cards in Deck");
    }

}
