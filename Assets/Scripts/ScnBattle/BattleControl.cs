using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleControl : MonoBehaviour {
    public static Dictionary<int, CardBase> CardCreated;
    public static Dictionary<int, MinionLogic> MinionCreated;

    #region view stuff
    public static PlayerLogic you;
    public PlayerVisual youVisual;
    public DeckVisual yourDeckVisual;
    public HandVisual yourHandVisual;
    public FieldVisual yourFieldVisual;
    public ManaVisual yourManaVisual;
    public static PlayerLogic opponent;
    public PlayerVisual opponentVisual;
    public DeckVisual opponentsDeckVisual;
    public HandVisual opponentsHandVisual;
    public FieldVisual opponentsFieldVisual;
    public ManaVisual opponentsManaVisual;
    public GameObject PnlStarter;
    public GameObject PnlDiscover;
    #endregion

    public static BattleControl Instance;
    public PlayerLogic ActivePlayer = you;
    public PlayerLogic AnotherPlayer {
        get {
            if (ActivePlayer == you) return opponent;
            else return you;
        }
    }

    public ICharacter Targeting;
    public List<Transform> Options = new();

    private void Awake() {
        Instance = this;
        CardCreated = new();
        MinionCreated = new();
        EventManager.AddListener(TurnEvent.OnGameOver, OnGameEnd);
    }

    private void Start() {
        InitializeGame();
        OnGameStart();
    }

    public void InitializeGame() {
        // DeckAsset SelectedDeck = Resources.Load<DeckAsset>($"ScriptableObject/Deck/New Deck");
        DeckAsset SelectedDeck = AssetDatabase.LoadAssetAtPath<DeckAsset>("Assets/Resources/ScriptableObject/Deck/New Deck.asset");
        Debug.Log("SelectedDeck.name" + Global.DeckName);
        you = new(SelectedDeck.DeckClass);
        you.Deck.Deck = ReadCardsFromDeck(SelectedDeck);
        youVisual.Player = you;
        yourDeckVisual.Deck = you.Deck;
        yourHandVisual.Hand = you.Hand;
        yourFieldVisual.Field = you.Field;
        yourManaVisual.Mana = you.Mana;

        opponent = new(GameDataAsset.ClassType.DemonHunter); // TODO: read opponent's class
        opponentsDeckVisual.Deck = opponent.Deck;
        opponentsFieldVisual.Field = opponent.Field;
        opponentsManaVisual.Mana = opponent.Mana;
        opponentsHandVisual.Hand = opponent.Hand;
    }

    public void OnGameStart() {
        if (Random.Range(0, 2) == 1) {
            ActivePlayer = you;
        }
        else ActivePlayer = opponent;
        AnotherPlayer.Hand.GetCard(-1, new Coin(Resources.Load<CardAsset>("ScriptableObject/UnCollectableCard/Coin.asset")));
        // TODO: card swap
        Debug.Log($"I have {you.Deck.Deck.Count} Cards");
        List<CardBase> _opts = new(3);
        for (int i = 0; i < 3; i++) {
            _opts.Add(you.Deck.RemoveCardFromDeckAt(Random.Range(0, you.Deck.Deck.Count)));
        }
        ShowDiscoverPanel(true, _opts);
    }

    public void OnTurnStart() {
        Instance.ActivePlayer.Mana.CurCrystals++;
        Instance.ActivePlayer.Mana.ManaReset();
        Instance.ActivePlayer.Deck.DrawCards(1);
        EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnStart, gameObject, Instance.ActivePlayer.TurnCount));
    }

    public void OnTurnEnd() {
        if (Instance.ActivePlayer.TurnCount >= 45) {
            OnGameEnd(null); // TODO: tie, no Winner
        }
        Instance.ActivePlayer.TurnCount++;
        EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, gameObject, Instance.ActivePlayer.TurnCount));
    }

    public void OnGameEnd(BaseEventArgs e) {
        //TODO: After GameOver
        //TODO: Winner?
    }

    public List<CardBase> ReadCardsFromDeck(DeckAsset da) {
        List<CardBase> CardsInDeck = you.Deck.Deck;
        for (int i = 0; i < da.myCardAssets.Count; i++) {
            if (da.myCardNums[i] == 2) {
                CardsInDeck.Add(Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name), da.myCardAssets[i]) as CardBase);
            }
            CardsInDeck.Add(Activator.CreateInstance(Type.GetType(da.myCardAssets[i].name), da.myCardAssets[i]) as CardBase);
        }
        Shuffle(CardsInDeck);
        return CardsInDeck;
    }

    public void Shuffle(List<CardBase> cardList) { // Fisher-Yates
        for (int i = cardList.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            (cardList[j], cardList[i]) = (cardList[i], cardList[j]);
        }
    }

    public List<CardAsset> Discover(List<CardAsset> CardPool, Predicate<CardAsset> _filter) {
        CardPool = CardPool.FindAll(_filter);
        List<CardAsset> res = new();
        int randomIndex;
        for (int i = 0; i < 3; i++) {
            randomIndex = Random.Range(0, CardPool.Count);
            res.Add(CardPool[randomIndex]);
            CardPool.RemoveAt(randomIndex);
        }
        return res;
    }

    public List<CardBase> Discover(List<CardBase> CardPool, Predicate<CardBase> _filter) {
        CardPool = CardPool.FindAll(_filter);
        List<CardBase> res = new();
        int randomIndex;
        for (int i = 0; i < 3; i++) {
            randomIndex = Random.Range(0, CardPool.Count);
            res.Add(CardPool[randomIndex]);
            CardPool.RemoveAt(randomIndex);
        }
        return res;
    }

    public void ShowDiscoverPanel(bool _isStart, List<CardBase> _cards) {
        GameObject _panel;
        if (_isStart) {
            _panel = PnlStarter;
        }
        else _panel = PnlDiscover;
        _panel.SetActive(true);

        foreach (Transform child in _panel.transform.GetChild(2)) {
            Options.Add(child);
        }
        for (int i = 0; i < 3; i++) {
            Options[i].GetComponent<BattleCardManager>().Card = _cards[i];
            Options[i].GetComponent<BattleCardManager>().ReadFromAsset();
        }
    }

}