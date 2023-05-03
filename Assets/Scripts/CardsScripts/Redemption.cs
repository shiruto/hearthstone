public class Redemption : SecretCard {
    public Redemption(CardAsset CA) : base(CA) {
        trigger = new(MinionEvent.AfterMinionDie, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.Owner != Owner)
            return false;
        Owner.Field.SummonMinionAt(evt.position, new(evt.minion.Card) { Health = 1 });
        return true;
    }

}