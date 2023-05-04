using System.Collections.Generic;

public class ManaAddict : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public ManaAddict(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(CardEvent.OnCardUse, Triggered), new(TurnEvent.OnTurnEnd, ClearBuff) };
        buff = new(
            "Mana Addict",
            new() { new(Status.Attack, Operator.Plus, 2) }
        );
    }

    private void ClearBuff(BaseEventArgs e) {
        if (e.Player != Owner) return;
        foreach (Buff b in Minion.BuffList.FindAll((Buff b) => b == buff)) {
            (Minion as IBuffable).RemoveBuff(b);
        }
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Card is not SpellCard) return;
        Effect.GiveBuffEffect(buff, Minion, Minion);
    }

}