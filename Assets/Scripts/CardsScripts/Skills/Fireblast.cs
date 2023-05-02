using System;

public class Fireblast : SkillCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public int Damage => 1;
    public Func<ICharacter, bool> Match => (ICharacter c) => true;

    public Fireblast(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target);
    }

}