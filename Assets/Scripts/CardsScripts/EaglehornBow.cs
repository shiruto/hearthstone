using System.Collections.Generic;

public class EaglehornBow : WeaponCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get; set; }

    public EaglehornBow(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(CardEvent.OnSecretReveal, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player == Owner) Owner.Weapon.Health++;
    }

}