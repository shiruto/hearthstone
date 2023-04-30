public class Counterspell : SecretCard {
    public Counterspell(CardAsset CA) : base(CA) {
    }

    public override void AddTrigger() {
        EventManager.AddListener(CardEvent.BeforeCardUse, Triggered);
    }

    public override void DelTrigger() {
        EventManager.DelListener(CardEvent.BeforeCardUse, Triggered);
    }

    public override void SecretImplement(BaseEventArgs e, out bool isTriggered) {
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Player != Owner) {
            BattleControl.Instance.CardUsing = null;
            isTriggered = true;
        }
        else isTriggered = false;
    }

}