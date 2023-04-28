public class ExcessMana : SpellCard {

    public ExcessMana(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DrawCard(Owner, 1).ActivateEffect();
    }

}