using System;

public class EarthShock : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    public int Damage => 1;

    public EarthShock(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new SilenceEffect(Target).ActivateEffect();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
    }

}