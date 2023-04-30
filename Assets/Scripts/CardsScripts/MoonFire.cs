public class MoonFire : SpellCard, IDealDamage, ITarget {
    public int Damage => 1;
    public ICharacter Target { get; set; }

    public MoonFire(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
    }

    public bool CanBeTarget(ICharacter Card) {
        return true;
    }

}
