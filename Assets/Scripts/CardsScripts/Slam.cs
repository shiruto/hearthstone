using System;

public class Slam : SpellCard, ITarget, IDealDamage {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic;
    public int Damage => 2;

    public Slam(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
        if ((Target as MinionLogic).isAlive) {
            Owner.Deck.DrawCards(1);
        }
    }

}