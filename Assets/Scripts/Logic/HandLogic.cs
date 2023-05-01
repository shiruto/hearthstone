using System.Collections.Generic;
using UnityEngine;

public class HandLogic {
    public List<CardBase> Hands;
    public PlayerLogic owner;
    private int _maxHandCard = 10;

    public HandLogic() {
        Hands = new();
        EventManager.AddListener(CardEvent.BeforeCardUse, UseCard);
        EventManager.AddListener(CardEvent.OnCardDraw, DrawCardHandler);
    }

    private void DrawCardHandler(BaseEventArgs e) {
        CardEventArgs _event = e as CardEventArgs;
        if (_event.Player == owner) {
            GetCard(-1, _event.Card);
        }
    }

    public void GetCard(int position, CardBase newCard) {
        newCard.Owner = owner;
        if (position == -1) position = Hands.Count;
        if (Hands.Count >= _maxHandCard) {
            Debug.Log("Too Many Cards");
            return;
        }
        if (Hands.Count == 0) {
            Hands.Add(newCard);
        }
        else {
            Hands.Insert(position, newCard);
        }
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardGet, null, owner, newCard, position).Invoke();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.HandVisualUpdate).Invoke();
    }

    public void UseCard(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Player == owner) {
            CardBase Card = evt.Card;
            RemoveCard(Card);
        }
    }

    public void CardDiscard(CardBase Card) {
        RemoveCard(Card);
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDiscard, null, owner, Card).Invoke();
    }

    private void RemoveCard(CardBase Card) {
        Hands.Remove(Card);
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.HandVisualUpdate).Invoke();
    }

    public void ChangeMaxHandCard(int newMaxHandCardNum) {
        _maxHandCard = newMaxHandCardNum;
    }
}
