using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandLogic {
    public List<CardViewController> Hands;
    private int _maxHandCard = 10;

    public HandLogic() {
        Hands = new();
        EventManager.AddListener(CardEvent.OnCardUse, UseCard);
        EventManager.AddListener(CardEvent.OnCardDraw, DrawCardHandler);
        // GetCard(0, new AbusiveSergeant(AssetDatabase.LoadAssetAtPath<CardAsset>("Assets/Resources/ScriptableObject/Card/Abusive Sergeant.asset")));
    }

    private void DrawCardHandler(BaseEventArgs e) {
        CardEventArgs _event = e as CardEventArgs;
        GetCard(-1, _event.Card);
    }

    public void GetCard(int position, CardViewController newCard) {
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
        EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardGet, null, newCard, position));
    }

    public void UseCard(BaseEventArgs args) {
        CardBase Card = (args as CardEventArgs).Card;
        Hands.Remove(Card);
        EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, null, Card));
    }

    public void CardDiscard(CardBase Card) {
        Hands.Remove(Card);
        EventManager.Invoke(EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardDiscard, null, Card));
    }

    public void ChangeMaxHandCard(int newMaxHandCardNum) {
        _maxHandCard = newMaxHandCardNum;
    }
}
