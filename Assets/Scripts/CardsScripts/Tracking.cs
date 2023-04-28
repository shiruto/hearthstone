using System.Collections.Generic;

public class Tracking : SpellCard, IDiscover {

    public Tracking(CardAsset CA) : base(CA) {

    }

    public void DiscoverHandler(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        Owner.Deck.DrawSpecificCard(evt.Card);
        EventManager.DelListener(CardEvent.OnDiscover, DiscoverHandler);
    }

    public List<CardBase> GetPool() {
        return Owner.Deck.Deck;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new Discover(this).ActivateEffect();
    }

}