using System.Collections.Generic;

public class Secretkeeper : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public Secretkeeper(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(CardEvent.OnCardUse, Triggered) };
        buff = new(
            "Secretkeeper",
            new() { new(Status.Health, Operator.Plus, 1), new(Status.Attack, Operator.Plus, 1) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Card is not SecretCard) return;
        (Minion as IBuffable).AddBuff(buff);
    }

}