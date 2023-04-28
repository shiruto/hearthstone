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
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
    }

    public void DrawCards(int Num) {
        for (; Num > 0; Num--) {
            CardBase CardToDraw = RemoveCardFromDeckAt(0);
            if (CardToDraw == null && Deck.Count == 0) {
                Fatigue++;
                owner.Health -= Fatigue;
            }
            else {
                EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDraw, null, owner, CardToDraw).Invoke();
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
        UpdateCardName();
        return Card;
    }

    public void DrawSpecificCard(CardBase card) {
        Deck.Remove(card);
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDraw, null, owner, card).Invoke();
        UpdateCardName();
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
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.DeckVisualUpdate).Invoke();
    }

    public void ReadCardsFromDeck(DeckAsset da) {
        for (int i = 0; i < da.myCardAssets.Count; i++) {
            object[] parameters = new object[] { da.myCardAssets[i] };
            CardBase CardToAdd = Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name.Replace(" ", "")), parameters) as CardBase;
            CardToAdd.Owner = owner;
            if (da.myCardNums[i] == 2) {
                Deck.Add(CardToAdd);
            }
            Deck.Add(CardToAdd);
        }
        Shuffle(Deck);
    }

    public void Shuffle(List<CardBase> cardList) { // Fisher-Yates
        for (int i = cardList.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (cardList[j], cardList[i]) = (cardList[i], cardList[j]);
        }
    }

    private void OnTurnStartHandler(BaseEventArgs e) {
        TurnEventArgs evt = e as TurnEventArgs;
        if (evt.Player == owner) {
            DrawCards(1);
        }
    }

}
