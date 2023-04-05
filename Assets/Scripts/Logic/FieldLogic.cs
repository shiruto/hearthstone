using System.Collections.Generic;
using UnityEngine;

public class FieldLogic {
    private List<MinionLogic> Minions = null;

    public FieldLogic() {
        Minions = new(7);
    }

    public void SummonMinionAt(int position, MinionLogic MinionToSummon) {
        if (Minions.Count == 0) Minions.Add(MinionToSummon);
        else Minions.Insert(position, MinionToSummon);
        EventManager.Invoke(EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionSummon, null, MinionToSummon, position));
    }

    public void RemoveMinion(MinionLogic Minion) {
        Minions.Remove(Minion);
        EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.FieldVisualUpdate));
    }

    public List<MinionLogic> GetMinions() {
        return Minions;
    }
}
