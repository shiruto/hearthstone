using System.Collections.Generic;

public class Lightwarden : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public Lightwarden(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(DamageEvent.Healing, Triggered) };
        buff = new(
            "Lightwarden",
            new() { new(Status.Attack, Operator.Plus, 2) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        if (Minion == null) return;
        (Minion as IBuffable).AddBuff(buff);
    }

}