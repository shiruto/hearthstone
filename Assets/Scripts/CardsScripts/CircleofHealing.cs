public class CircleofHealing : SpellCard, IHeal {
    public int Heal => 4;

    public CircleofHealing(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealAoeDamage(4, this, (ICharacter a) => a is MinionLogic) {
            isHeal = true
        }.ActivateEffect();
    }
}