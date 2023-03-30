using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleField : MonoBehaviour {
    private List<MinionLogic> Minions = null;
    public static event Action<int, MinionLogic> OnMinionSummon;

    private BattleField() {
        Minions = new(7);
    }

    public void SummonMinionAt(int position, MinionLogic MinionToSummon) {
        Minions.Insert(position, MinionToSummon);
        OnMinionSummon?.Invoke(position, MinionToSummon);
    }

    public void RemoveMinion(MinionLogic Minion) {
        Minions.Remove(Minion);
    }

    public List<MinionLogic> GetMinions() {
        return Minions;
    }
}
