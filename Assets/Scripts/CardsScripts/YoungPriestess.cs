using System.Collections.Generic;

public class YoungPriestess : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public YoungPriestess(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(TurnEvent.OnTurnEnd, Triggered) };
        buff = new(
            "Young Priestess",
            new() { new(Status.Health, Operator.Plus, 1) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        new GiveBuff(buff, this, Effect.GetRandomObject(Owner.Field.Minions, (MinionLogic m) => m == Minion)).ActivateEffect();
    }

}