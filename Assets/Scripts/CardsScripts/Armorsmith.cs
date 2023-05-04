using System.Collections.Generic;

public class Armorsmith : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }

    public Armorsmith(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(DamageEvent.TakeDamage, Triggered) };
    }


    public void Triggered(BaseEventArgs e) {
        DamageEventArgs evt = e as DamageEventArgs;
        if (evt.taker != Minion.Owner) return;
        if (evt.taker is not MinionLogic) return;
        Effect.GetArmorEffect(1, Owner, Minion);
    }

}