public class StarvingBuzzard : MinionCard {
    public StarvingBuzzard(CardAsset CA) : base(CA) {

    }

    public void Trigger() {
        EventManager.AddListener(MinionEvent.AfterMinionSummon, EventHandler);
    }

    public void EventHandler(BaseEventArgs e) {
        if ((e as MinionEventArgs).minion.ca.MinionType == GameDataAsset.MinionType.Beast) {
            (e as MinionEventArgs).minion.owner.Deck.DrawCards(1);
        }
    }
}