public class SnakeTrap : SecretCard {
    public SnakeTrap(CardAsset CA) : base(CA) {

    }

    public void Trigger() {
        EventManager.AddListener(MinionEvent.BeforeMinonAttack, EventHandler);
    }

    public void EventHandler(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        // TODO:
    }
}