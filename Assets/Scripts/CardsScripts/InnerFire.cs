using System;

public class InnerFire : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    private Buff buff;

    public InnerFire(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        buff = new(
            "Inner Fire",
            new() { new(Status.Attack, Operator.equal, Target.Health) }
        );
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}