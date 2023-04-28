using System.Collections.Generic;

public class ScavengingHyena : MinionCard, ITriggerMinionCard {
    private readonly Buff _buff;
    public List<TriggerStruct> Triggers { get; set; }

    public ScavengingHyena(CardAsset CA) : base(CA) {
        _buff = new(1, 2) {
            BuffName = "sc"
        };
        Triggers = new() { new(MinionEvent.AfterMinionDie, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (Minion == null) return;
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.Card.CA.MinionType == MinionType.Beast) {
            new GiveBuff(_buff, this, Minion).ActivateEffect();
        }
    }

}