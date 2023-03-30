using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandLogic : MonoBehaviour {
    private List<CardBase> Hands;
    private int _maxHandCard = 10;
    public static event Action<int, CardBase> OnCardGet;
    public static event Action<CardBase> OnCardLose;
    private void Awake() {
        Hands = new();
        Draggable.OnCardUse += UseCard;
    }
    private void Start() {
        GetCard(0, new AbusiveSergeant(AssetDatabase.LoadAssetAtPath<CardAsset>("Assets/Resources/ScriptableObject/Card/Abusive Sergeant.asset")));
    }
    public void GetCard(int position, CardBase newCard) {
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
        OnCardGet?.Invoke(Hands.Count, newCard);
    }

    public void UseCard(CardBase Card) {
        Debug.Log("Using Card :ID = " + Card.ID + "\tname = " + Card.CA.name);
        Hands.Remove(Card);
    }

    public void ChangeMaxHandCard(int newMaxHandCardNum) {
        _maxHandCard = newMaxHandCardNum;
    }
}
