using UnityEngine;

public class Tracking : SpellCard, IDiscover {

    public Tracking(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        EventManager.AddListener(CardEvent.OnDiscover, DiscoverHandler);
        new Discover(BattleControl.you.Deck.Deck).ActivateEffect();
    }

    public void DiscoverHandler(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        owner.Hand.GetCard(-1, evt.Card);
    }

}