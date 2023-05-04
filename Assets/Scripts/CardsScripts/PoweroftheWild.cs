using System.Collections.Generic;
using UnityEngine;

public class PoweroftheWild : SpellCard, IDiscover {
    public List<CardBase> Pool { get; }
    private readonly CardAsset Option1;
    private readonly CardAsset Option2;

    public PoweroftheWild(CardAsset CA) : base(CA) {
        Option1 = Resources.Load(GameData.Path + "UnCollectableCard/Leader of the Pack") as CardAsset;
        Option2 = Resources.Load(GameData.Path + "UnCollectableCard/Summon a Panther") as CardAsset;
        Pool = new() { new LeaderofthePack(Option1), new SummonaPanther(Option2) };
    }

    public void DiscoverHandler(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        evt.Card.ExtendUse();
        EventManager.DelListener(CardEvent.OnDiscover, DiscoverHandler);
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new Discover(this).ActivateEffect();
    }

}