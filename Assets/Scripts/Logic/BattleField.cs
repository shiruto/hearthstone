using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleField : MonoBehaviour {
    public List<MinionLogic> FriendlyMinionField = new();
    public List<MinionLogic> EnemyMinionField = new();

    private BattleField() {

    }
    private static BattleField yourField = null;
    private static BattleField opponentField = null;
    public static BattleField YourField() {
        if (yourField == null) yourField = new();
        return yourField;
    }
    public static BattleField OpponentField() {
        if (opponentField == null) opponentField = new();
        return opponentField;
    }

    public void PlaceMinionAt(bool isEnemy, int position, MinionLogic Minion) {
        if (!isEnemy) {
            FriendlyMinionField.Insert(position, Minion);
        }
        else EnemyMinionField.Insert(position, Minion);
        Align();
    }

    public void RemoveMinionAt(bool isEnemy, int position, MinionLogic Minion) {
        if (!isEnemy) {
            FriendlyMinionField.Insert(position, Minion);
        }
        else EnemyMinionField.Insert(position, Minion);
        Align();
    }

    private void Align() {
        // TODO
    }
}
