public class Coin : SpellCard {

    public Coin(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new CrystalChange(true, 1).ActivateEffect();
    }

}