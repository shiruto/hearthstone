using System;

public class ArcaneShot : SpellCard, IDealDamage, ITarget {
    public int Damage => 2;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => true;

    public ArcaneShot(CardAsset CA) : base(CA) {
        Target = null;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
    }

}