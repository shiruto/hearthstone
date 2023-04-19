public class BestialWrath : SpellCard {
    Buff _buff = new Buff(0, 2);

    public BestialWrath(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        new GiveBuff(_buff).ActivateEffect();
    }
}