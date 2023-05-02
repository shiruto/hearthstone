using System.Collections.Generic;
using System.Linq;

public class StrengthTotem : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff buff;

    public StrengthTotem(CardAsset CA) : base(CA) {
        buff = new(
            "Strength Totem",
            new() { new(Status.Attack, Operator.Plus, 1) }
        );
        TriggersToGrant = new() { new(TurnEvent.OnTurnEnd, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        new GiveBuff(buff, this, Effect.GetRandomObject(Owner.Field.Minions.Cast<IBuffable>().ToList(), (IBuffable a) => a == this)).ActivateEffect();
    }

}