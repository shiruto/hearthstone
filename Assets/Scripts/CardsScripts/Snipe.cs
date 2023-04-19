public class Snipe : SecretCard {
    public Snipe(CardAsset CA) : base(CA) {

    }

    public void Trigger() {
        EventManager.AddListener(MinionEvent.AfterMinionSummon, EventHandler);
    }

    public void EventHandler(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.owner != owner) {
            new DealDamageToTarget(4, evt.minion).ActivateEffect(); // TODO: Use EventManager?
        }
    }

}