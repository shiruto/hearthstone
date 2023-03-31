using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
    public static PlayerLogic you;
    public static PlayerLogic opponent;
    private void Awake() {
        Instance = this;
        CardCreated = new();
        MinionCreated = new();
    }
    public static BattleControl Instance;

    public PlayerLogic ActivePlayer = you;
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
        if (Random.Range(0, 2) > 1) {
            ActivePlayer = you;
        }
        else ActivePlayer = opponent;
        // TODO
    }
    public void OnGameEnd() {

    }
}