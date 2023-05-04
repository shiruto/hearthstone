using System;

public class LightingBolt : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => true;
    public int Damage => 3;

    public LightingBolt(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(Damage, this, Target, false, true).ActivateEffect();
    }

}