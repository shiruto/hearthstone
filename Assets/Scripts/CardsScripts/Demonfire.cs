using System;

public class Demonfire : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    public int Damage => 2;

    private readonly Buff buff;

    public Demonfire(CardAsset CA) : base(CA) {
        buff = new(
            "Demonfire",
            new() { new(Status.Attack, Operator.Plus, 2), new(Status.Health, Operator.Plus, 2) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        if ((Target as MinionLogic).Card.CA.MinionType == MinionType.Demon && (Target as MinionLogic).Owner == Owner) new GiveBuff(buff, this, Target).ActivateEffect();
        else new DealDamageToTarget(Damage, this, Target, false, true).ActivateEffect();
    }

}