using System.Collections.Generic;

public class BloodImp : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public BloodImp(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(TurnEvent.OnTurnEnd, Triggered) };
        buff = new(
            "Blood Imp",
            new() { new(Status.Health, Operator.Plus, 1) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        new GiveBuff(buff, this, Effect.GetRandomObject(BattleControl.GetAllMinions(), (MinionLogic m) => m.Owner != Owner || m == Minion)).ActivateEffect();
    }

}