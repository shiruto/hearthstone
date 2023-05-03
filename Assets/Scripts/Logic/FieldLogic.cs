using System.Collections.Generic;
using UnityEngine;

public class FieldLogic {
    public List<MinionLogic> Minions;
    public PlayerLogic owner;
    public bool HaveTaunt = false;

    public FieldLogic() {
        Minions = new(7);
    }

    public void SummonMinionAt(int position, MinionLogic MinionToSummon) {
        if (position == -1) position = Minions.Count - 1;
        if (Minions.Count == 0) Minions.Add(MinionToSummon);
        else Minions.Insert(position, MinionToSummon);
        UpdateHaveTaunt();
        Debug.Log("Summoned a Minion named " + MinionToSummon.Card.CA.name);
        EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionSummon, null, owner, MinionToSummon).Invoke();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.FieldVisualUpdate).Invoke();
    }

    public void RemoveMinion(MinionLogic Minion) {
        Minions.Remove(Minion);
        UpdateHaveTaunt();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.FieldVisualUpdate).Invoke();
    }

    public List<MinionLogic> GetMinions() {
        return Minions;
    }

    public void UpdateHaveTaunt() {
        HaveTaunt = Minions.Exists((MinionLogic m) => Logic.CanTaunt(m));
    }

    public int GetPosition(MinionLogic minion) {
        return Minions.IndexOf(minion);
    }

}
