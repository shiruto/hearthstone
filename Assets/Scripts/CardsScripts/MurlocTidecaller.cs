using System.Collections.Generic;
using System.Linq;

public class MurlocTidecaller : MinionCard, IGrantTrigger {
    public List<TriggerStruct> TriggersToGrant { get; set; }
    private readonly Buff _buff;

    public MurlocTidecaller(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(MinionEvent.AfterMinionSummon, Triggered) };
        _buff = new(
            "MurlocTidecaller",
            new() { new(Status.Attack, Operator.Plus, 1) }
        );
    }

    public void Triggered(BaseEventArgs e) {
        if (Minion == null) return;
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.Card.CA.MinionType == MinionType.Murloc) {
            new GiveBuff(_buff, this, Minion).ActivateEffect();
        }
    }

}