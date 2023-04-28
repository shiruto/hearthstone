using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    // private RopeTimer timer;
    private TurnManager() {

    }
    private static TurnManager instance;
    public static TurnManager Instance() {
        if (instance == null) {
            instance = new();
        }
        return instance;
    }

    void Awake() {
        // timer = GetComponent<RopeTimer>();
    }

    void Start() {
        OnGameStart();
    }

    public void OnGameStart() {
        BattleControl.CardUsed.Clear();
        BattleControl.MinionSummoned.Clear();
    }

    // public void EndTurn() {
    //     timer.StopTimer();
    //     whoseTurn.OnTurnEnd();

    //     new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
    // }

    // public void StopTheTimer() {
    //     timer.StopTimer();
    // }
}
