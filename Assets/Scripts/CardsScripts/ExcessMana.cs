public class ExcessMana : SpellCard {
    public ExcessMana(CardAsset CA) : base(CA) {

    }
    public override void Use() {
        BattleControl.you.Deck.DrawCards(1);
    }
}