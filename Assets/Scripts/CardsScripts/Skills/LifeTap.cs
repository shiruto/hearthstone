public class LifeTap : SkillCard, IDealDamage {
    public int Damage => 2;

    public LifeTap(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Owner, true).ActivateEffect();
        Owner.Deck.DrawCards(1);
    }

}