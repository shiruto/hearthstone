public class ArcaneShot : SpellCard, IDealDamage, ITarget {
    public int Damage => 2;
    public ICharacter Target { get; set; }

    public ArcaneShot(CardAsset CA) : base(CA) {
        Target = null;
    }

    public bool CanBeTarget(ICharacter card) {
        return true;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
    }

}