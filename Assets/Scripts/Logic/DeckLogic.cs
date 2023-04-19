using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckLogic {
    private readonly int MaxCardNum = 60;
    public PlayerLogic owner;
    public List<CardBase> Deck = new();
    public List<string> cardName = new();
    public int Fatigue = 0;

    public DeckLogic() {
        UpdateCardName();
    }

    public void DrawCards(int Num) {
        for (; Num > 0; Num--) {
            CardBase CardToDraw = RemoveCardFromDeckAt(0);
            if (CardToDraw == null && Deck.Count == 0) {
                Fatigue++;
                owner.Health -= Fatigue;
            }
            else {
                EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDraw, null, owner, CardToDraw));
            }
        }
        UpdateCardName();
    }

    public CardBase RemoveCardFromDeckAt(int index) {
        if (Deck.Count == 0) {
            Debug.Log("out of Card");
            return null;
        }
        CardBase Card = Deck[index];
        Deck.Remove(Card);
        cardName.RemoveAt(index);
        EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.DeckVisualUpdate));
        UpdateCardName();
        return Card;
    }

    public void SortDeck(Comparison<CardBase> compare) {
        Deck.Sort(compare);
        UpdateCardName();
    }

    public void AddCardToDeck(int position, CardBase Card) {
        if (position == -1) {
            Deck.Insert(Deck.Count, Card);
        }
        else if (Deck.Count == 0) Deck.Add(Card);
        else if (Deck.Count < MaxCardNum) Deck.Insert(position, Card);
        else Debug.Log("Too Many Cards in Deck");
        UpdateCardName();
    }

    public void BackToDeck(CardBase Card) {
        int position = Random.Range(0, Deck.Count);
        AddCardToDeck(position, Card);
        UpdateCardName();
    }

    public void UpdateCardName() {
        cardName.Clear();
        foreach (CardBase card in Deck) {
            cardName.Add(card.CA.name);
        }
    }

    public void ReadCardsFromDeck(DeckAsset da) {
        for (int i = 0; i < da.myCardAssets.Count; i++) {
            object[] parameters = new object[] { da.myCardAssets[i] };
            if (da.myCardNums[i] == 2) {
                Deck.Add(Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name.Replace(" ", "")), parameters) as CardBase);
            }
            Deck.Add(Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name.Replace(" ", "")), parameters) as CardBase);
        }
        Shuffle(Deck);
    }

    public void Shuffle(List<CardBase> cardList) { // Fisher-Yates
        for (int i = cardList.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (cardList[j], cardList[i]) = (cardList[i], cardList[j]);
        }
    }

}
