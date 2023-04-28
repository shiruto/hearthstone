public class ArcaneShot : SpellCard, IDealDamage, ITarget {
    public int Damage => 2;
    public ICharacter Target { get; set; }

    public ArcaneShot(CardAsset CA) : base(CA) {
        Target = null;
    }

    public bool CanBeTarget(CardBase card) {
        if (card is MinionCard or HeroCard) return true;
        else return false;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, ScnBattleUI.Instance.Targeting, true).ActivateEffect();
    }

}