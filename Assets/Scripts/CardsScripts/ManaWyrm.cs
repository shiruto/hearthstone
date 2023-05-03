using System.Collections.Generic;

public class ManaWyrm : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public ManaWyrm(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(CardEvent.OnCardUse, Triggered) };
        buff = new(
            "Mana Wyrm",
            new() { new(Status.Attack, Operator.Plus, 1) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Card is not SpellCard) return;
        (Minion as IBuffable).AddBuff(buff);
    }

}