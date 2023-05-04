using System;

public class Slam : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    public int Damage => 2;

    public Slam(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(Damage, this, Target, false, true).ActivateEffect();
        if (!(Target as MinionLogic).isAlive) return;
        Owner.Deck.DrawCards(1);

    }

}