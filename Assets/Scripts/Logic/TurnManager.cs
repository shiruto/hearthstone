using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    private RopeTimer timer;
    private TurnManager() {

    }
    private static TurnManager instance;
    public static TurnManager Instance() {
        if(instance == null) {
            instance = new();
        }
        return instance;
    }

    private PlayerLogic _whoseTurn;
    public PlayerLogic whoseTurn {
        get {
            return _whoseTurn;
        }

        set {
            _whoseTurn = value;
            timer.StartTimer();
            GlobalSettings.Instance.EnableEndTurnButtonOnStart(_whoseTurn);
            TurnMaker tm = whoseTurn.GetComponent<TurnMaker>();
            tm.OnTurnStart();
            if (tm is PlayerTurnMaker) {
                whoseTurn.HighlightPlayableCards();
            }
            whoseTurn.otherPlayer.HighlightPlayableCards(true);
        }
    }

    void Awake() {
        timer = GetComponent<RopeTimer>();
    }

    void Start() {
        OnGameStart();
    }

    public void OnGameStart() {
        CardLogic.CardsCreatedThisGame.Clear();
        MinionLogic.CreaturesCreatedThisGame.Clear();

        foreach (Player p in Player.Players) {
            p.ManaThisTurn = 0;
            p.ManaLeft = 0;
            p.LoadCharacterInfoFromAsset();
            p.TransmitInfoAboutPlayerToVisual();
            p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
            p.PArea.Portrait.transform.position = p.PArea.handVisual.OtherCardDrawSourceTransform.position;
        }
    }

    public void EndTurn() {
        timer.StopTimer();
        whoseTurn.OnTurnEnd();

        new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
    }

    public void StopTheTimer() {
        timer.StopTimer();
    }
}
