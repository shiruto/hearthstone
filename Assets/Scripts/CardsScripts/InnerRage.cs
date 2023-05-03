using System;

public class InnerRage : SpellCard, IDealDamage, ITarget {
    public int Damage => 1;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    private readonly Buff buff;

    public InnerRage(CardAsset CA) : base(CA) {
        buff = new(
            "Inner Rage",
            new() { new(Status.Attack, Operator.Plus, 2) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}