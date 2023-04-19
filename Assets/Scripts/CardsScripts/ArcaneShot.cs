public class ArcaneShot : SpellCard {
    public int damage = 2;

    public ArcaneShot(CardAsset CA) : base(CA) {
        ifDrawLine = true;
    }

    public override void Use() {
        new DealDamageToTarget(damage, Target).ActivateEffect();
    }
}