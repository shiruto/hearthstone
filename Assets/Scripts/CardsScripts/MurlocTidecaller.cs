using System.Collections.Generic;
using System.Linq;

public class MurlocTidecaller : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> Triggers { get; set; }
    private readonly Buff _buff;

    public MurlocTidecaller(CardAsset CA) : base(CA) {
        Triggers = new() { new(MinionEvent.AfterMinionSummon, Triggered) };
        _buff = new(0, 1) {
            BuffName = "MurlocTidecaller"
        };
    }

    public void Triggered(BaseEventArgs e) {
        if (Minion == null) return;
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.Card.CA.MinionType == MinionType.Murloc) {
            new GiveBuff(_buff, this, Minion).ActivateEffect();
        }
    }

}