using System.Collections.Generic;

public class AmaniBerserker : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public AmaniBerserker(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(DamageEvent.TakeDamage, Triggered) };
        buff = new(
            "Amani Berserker",
            new() { new(Status.Attack, Operator.Plus, 3) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        DamageEventArgs evt = e as DamageEventArgs;
        if (evt.taker != Minion) return;
        if (Minion.BuffList.Exists((Buff b) => b == buff)) return;
        Effect.GiveBuffEffect(buff, Minion, this);
    }

}