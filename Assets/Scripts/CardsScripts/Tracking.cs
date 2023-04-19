public class Tracking : SpellCard {

    public Tracking(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        new Discover(BattleControl.you.Deck.Deck).ActivateEffect();
    }

}