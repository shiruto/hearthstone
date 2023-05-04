public class LifeTap : SkillCard, IDealDamage {
    public int Damage => 2;

    public LifeTap(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(Damage, this, Owner).ActivateEffect();
        Owner.Deck.DrawCards(1);
    }

}