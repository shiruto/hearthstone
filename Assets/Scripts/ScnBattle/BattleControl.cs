using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleControl : MonoBehaviour {
    public static PlayerLogic you;
    public static PlayerLogic opponent;
    public string SelectedDeck;
    private void Awake() {
        Instance = this;
        CardCreated = new();
        MinionCreated = new();
    }
    private void Start() {
        OnGameStart();
    }
    public static BattleControl Instance;

    public PlayerLogic ActivePlayer = you;
    public PlayerLogic AnotherPlayer {
        get {
            if (ActivePlayer == you) return opponent;
            else return you;
        }
    }
    public int CurrentTurn;
    public static Dictionary<int, CardBase> CardCreated;
    public static Dictionary<int, MinionLogic> MinionCreated;
    public ICharacter Targeting;

    public void InitializeGame() {
        you.Health = 30;
        opponent.Health = 30;
        // TODO import the selected deck
    }
    public void OnGameStart() {
        ReadCardsFromDeck(Resources.Load<DeckAsset>($"ScriptableObject/Deck/{SelectedDeck}.asset"));
        if (Random.Range(0, 2) == 1) {
            ActivePlayer = you;
        }
        else ActivePlayer = opponent;
        AnotherPlayer.Hand.GetCard(-1, new Coin(Resources.Load<CardAsset>("ScriptableObject/UnCollectableCard/Coin.asset")));
        // TODO
        Debug.Log($"I have {you.Deck.Deck.Count} Cards");
    }
    public void OnGameEnd() {

    }
    public void ReadCardsFromDeck(DeckAsset da) {
        List<CardBase> CardsInDeck = you.Deck.Deck;
        for (int i = 0; i < da.myCardAssets.Count; i++) {
            if (da.myCardNums[i] == 2) {
                CardsInDeck.Add(Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name), da.myCardAssets[i]) as CardBase);
            }
            CardsInDeck.Add(Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name), da.myCardAssets[i]) as CardBase);
        }
        Shuffle(CardsInDeck);
        you.Deck.Deck = CardsInDeck;
    }
    public void Shuffle(List<CardBase> cardList) { // Fisher-Yates
        for (int i = cardList.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (cardList[j], cardList[i]) = (cardList[i], cardList[j]);
        }
    }
}