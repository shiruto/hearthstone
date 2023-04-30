using System.Collections.Generic;

public class AngryChicken : MinionCard, IGrantTrigger {
    private readonly Buff _buff;
    public List<TriggerStruct> TriggersToGrant { get; set; }

    public AngryChicken(CardAsset CA) : base(CA) {
        _buff = new(
            "angrychicken",
            new() { new(Status.Attack, Operator.Plus, 5) }
        );
        TriggersToGrant = new() { new(MinionEvent.AfterMinionStatusChange, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (Minion == null) return;
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion == Minion && Minion.Health < Health && !Minion.BuffList.Contains(_buff)) {
            new GiveBuff(_buff, this, Minion).ActivateEffect();
        }
    }

}