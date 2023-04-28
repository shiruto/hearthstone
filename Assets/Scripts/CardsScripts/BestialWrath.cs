public class BestialWrath : SpellCard, ITarget {
    public ICharacter Target { get; set; }

    public BestialWrath(CardAsset CA) : base(CA) {

    }

    public bool CanBeTarget(CardBase Card) {
        return Card is MinionCard && Card.CA.MinionType == MinionType.Beast;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(new(0, 2), this, ScnBattleUI.Instance.Targeting).ActivateEffect();
    }

}