public class Repentance : SecretCard {
    private readonly Buff buff;

    public Repentance(CardAsset CA) : base(CA) {
        trigger = new(CardEvent.OnCardUse, Triggered);
        buff = new(
            "Repentance",
            new() { new(Status.Health, Operator.equal, 1) }
        );
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Card is not MinionCard) return false;
        new GiveBuff(buff, this, (evt.Card as MinionCard).Minion).ActivateEffect();
        return true;
    }

}