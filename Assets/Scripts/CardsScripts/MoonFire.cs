public class MoonFire : SpellCard, IDealDamage, ITarget {
    public int Damage => 1;
    public ICharacter Target { get; set; }

    public MoonFire(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, ScnBattleUI.Instance.Targeting, true).ActivateEffect();
    }

    public bool CanBeTarget(CardBase Card) {
        return Card is ICharacter;
    }

}
