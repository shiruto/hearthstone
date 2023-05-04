using System;

public class Savagery : SpellCard, IDealDamage, ITarget {
    public int Damage => Owner.Attack;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;

    public Savagery(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(Damage, this, Target, false, true).ActivateEffect();
    }

}