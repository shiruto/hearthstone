using UnityEngine;

public class Coin : SpellCard {
    public Coin(CardAsset CA) : base(CA) {

    }
    public override void Use() {
        new CrystalChange(1).ActivateEffect();
        EventManager.Invoke(EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.TemporaryCrystal, null, 1));
    }
}