using System.Collections.Generic;

public abstract class StarvingBuzzard : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }

    public StarvingBuzzard(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(MinionEvent.AfterMinionSummon, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner) return;
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.Card.CA.MinionType == MinionType.Beast) {
            evt.minion.Owner.Deck.DrawCards(1);
        }
    }

}