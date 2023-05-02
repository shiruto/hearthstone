using System.Collections.Generic;
using UnityEngine;

public class ScavengingHyena : MinionCard, ITriggerMinionCard {
    private readonly Buff _buff;
    public List<TriggerStruct> TriggersToGrant { get; set; }

    public ScavengingHyena(CardAsset CA) : base(CA) {
        _buff = new(
            "sc",
            new() { new(Status.Attack, Operator.Plus, 2), new(Status.Health, Operator.Plus, 1) }

        );
        TriggersToGrant = new() { new(MinionEvent.AfterMinionDie, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        Debug.Log($"Card ID = {ID} Triggered");
        if (Minion == null) return;
        MinionEventArgs evt = e as MinionEventArgs;
        if ((evt.minion.Owner == Owner) && (evt.minion != Minion) && (evt.minion.Card.CA.MinionType == MinionType.Beast)) {
            new GiveBuff(_buff, this, Minion).ActivateEffect();
        }
    }

}