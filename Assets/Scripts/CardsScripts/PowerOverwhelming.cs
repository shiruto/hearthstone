using System;

public class PowerOverwhelming : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && !Logic.IsEnemy(c, Owner);
    private readonly Buff buff;

    public PowerOverwhelming(CardAsset CA) : base(CA) {
        buff = new(
            "Power Overwhelming",
            new() { new(Status.Attack, Operator.Plus, 4), new(Status.Health, Operator.Plus, 4) },
            new() { new(TurnEvent.OnTurnEnd, Triggered) }
        );
    }

    private void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        Target.Die();
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}