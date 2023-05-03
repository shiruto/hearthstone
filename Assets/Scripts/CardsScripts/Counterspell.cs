public class Counterspell : SecretCard {

    public Counterspell(CardAsset CA) : base(CA) {
        trigger = new(CardEvent.BeforeCardUse, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Card is not SpellCard) return false;
        BattleControl.Instance.CardUsing = null;
        return true;
    }

}