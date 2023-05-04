using System.Collections.Generic;

public class Doomsayer : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }

    public Doomsayer(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(TurnEvent.OnTurnStart, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Minion.Owner) return;
        Effect.DestroyAllEffect(Minion, (ITakeDamage t) => t is MinionLogic);
    }

}