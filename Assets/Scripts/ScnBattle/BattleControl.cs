using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleControl : MonoBehaviour {
    public static Dictionary<int, CardBase> CardUsed;
    public static Dictionary<int, MinionLogic> MinionSummoned;

    #region view stuff
    public static PlayerLogic you;
    public PlayerVisual youVisual;
    public DeckVisual yourDeckVisual;
    public HandVisual yourHandVisual;
    public FieldVisual yourFieldVisual;
    public ManaVisual yourManaVisual;
    public SkillVisual yourSkill;
    public WeaponVisual yourWeapon;
    public SecretVisual yourSecret;
    public static PlayerLogic opponent;
    public PlayerVisual opponentVisual;
    public DeckVisual opponentsDeckVisual;
    public HandVisual opponentsHandVisual;
    public FieldVisual opponentsFieldVisual;
    public ManaVisual opponentsManaVisual;
    public SkillVisual opponentsSkill;
    public WeaponVisual opponentsWeapon;
    public SecretVisual opponentsSecret;
    public GameObject PnlStarter;
    public GameObject PnlDiscover;
    public GameObject BtnEndTurn;
    #endregion

    public static BattleControl Instance;
    public PlayerLogic ActivePlayer;
    public PlayerLogic AnotherPlayer {
        get {
            if (ActivePlayer == you) return opponent;
            else return you;
        }
    }

    public int SpellDamage = 0;
    public CardBase CardUsing;
    public ICharacter Targeting;

    private void Awake() {
        Instance = this;
        CardUsed = new();
        MinionSummoned = new();
        EventManager.AddListener(TurnEvent.OnTurnEnd, OnTurnEndHandler);
        EventManager.AddListener(TurnEvent.OnGameOver, OnGameEnd);
    }

    private void Start() {
        InitializeGame();
        OnGameStart();
    }

    public void InitializeGame() {
        DeckAsset SelectedDeck = AssetDatabase.LoadAssetAtPath<DeckAsset>("Assets/Resources/ScriptableObject/Deck/New Deck.asset");
        you = new(SelectedDeck.DeckClass);
        opponent = new(SelectedDeck.DeckClass);

        you.Deck.ReadCardsFromDeck(SelectedDeck);
        youVisual.Player = you;
        youVisual.ReadFromLogic();
        yourDeckVisual.Deck = you.Deck;
        yourHandVisual.Hand = you.Hand;
        yourFieldVisual.Field = you.Field;
        yourManaVisual.Mana = you.Mana;
        yourWeapon.WL = you.Weapon;
        yourWeapon.gameObject.SetActive(false);
        yourSecret.sl = you.Secrets;
        // TODO: Create Skill Card Asset
        // object[] parameters = new object[] { AssetDatabase.LoadAllAssetsAtPath("Assets/ResourcesScriptableObject/UnCollectableCard/" + SelectedDeck.DeckClass + ".asset") as object};
        // yourSkill.InitSkill(Activator.CreateInstance(Type.GetType(SelectedDeck.DeckClass.ToString("G") + "Skill"), parameters) as SkillCard);
        opponent.Deck.ReadCardsFromDeck(SelectedDeck); // TODO: change opponent's deck
        opponentVisual.Player = opponent;
        opponentVisual.ReadFromLogic();
        opponentsDeckVisual.Deck = opponent.Deck;
        opponentsFieldVisual.Field = opponent.Field;
        opponentsManaVisual.Mana = opponent.Mana;
        opponentsHandVisual.Hand = opponent.Hand;
        opponentsWeapon.WL = opponent.Weapon;
        opponentsWeapon.gameObject.SetActive(false);
        opponentsSecret.sl = opponent.Secrets;
        opponent.Deck.UpdateCardName();
        you.Deck.UpdateCardName();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.FieldVisualUpdate).Invoke();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.ManaVisualUpdate).Invoke();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.SecretVisualUpdate).Invoke();
    }

    public void OnGameStart() {
        if (Random.Range(0, 2) == 1) {
            ActivePlayer = you;
        }
        else {
            ActivePlayer = opponent;
        }
        if (you != ActivePlayer) {
            BtnEndTurn.GetComponent<BtnEndTurn>().SwapText();
        }
        List<CardBase> _opts = new(3);
        for (int i = 0; i < 3; i++) {
            _opts.Add(you.Deck.RemoveCardFromDeckAt(Random.Range(0, you.Deck.Deck.Count)));
        }
        ShowStarterPanel(_opts);
    }

    private void OnTurnEndHandler(BaseEventArgs e) {
        TurnEventArgs evt = e as TurnEventArgs;
        ActivePlayer = AnotherPlayer;
        ActivePlayer.OnTurnStart();
    }

    public void OnGameEnd(BaseEventArgs e) {
        //TODO: implement these scene
        TurnEventArgs evt = e as TurnEventArgs;
        GameStatus status = evt.status;
        switch (status) {
            case GameStatus.Tie:
                Debug.Log("Tie");
                break;
            case GameStatus.Lose:
                Debug.Log("Lose");
                break;
            case GameStatus.Win:
                Debug.Log("Win");
                break;
            default:
                Debug.Log("wrong call");
                break;
        }
    }

    public void ShowDiscoverPanel(List<CardBase> _cards) {
        if (_cards.Count <= 0) return;
        PnlDiscover.SetActive(true);
        PnlDiscover.GetComponent<DiscoverPnlController>().showingCardNum = _cards.Count;
        int index = 0;
        foreach (Transform child in PnlDiscover.transform.GetChild(1)) {
            if (index >= _cards.Count) {
                child.gameObject.SetActive(false);
            }
            else {
                child.gameObject.SetActive(true);
                child.GetComponent<BattleCardViewController>().Card = _cards[index];
                child.GetComponent<BattleCardViewController>().ReadFromAsset();
            }
            index++;
        }
    }

    public void ShowStarterPanel(List<CardBase> _cards) {
        PnlStarter.SetActive(true);
        int index = 0;
        foreach (Transform child in PnlStarter.transform.GetChild(1)) {
            child.GetComponent<BattleCardViewController>().Card = _cards[index];
            child.GetComponent<BattleCardViewController>().ReadFromAsset();
            index++;
        }
    }

    public List<MinionLogic> GetAllMinions() {
        List<MinionLogic> minions = new(you.Field.GetMinions());
        minions.AddRange(opponent.Field.GetMinions());
        return minions;
    }

}