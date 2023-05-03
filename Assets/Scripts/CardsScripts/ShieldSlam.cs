using System;

public class ShieldSlam : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    public int Damage => Owner.Armor;

    public ShieldSlam(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
    }

}