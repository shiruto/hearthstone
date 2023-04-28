using System.Collections.Generic;
using System.Linq;

public class SouthseaDeckhand : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> Triggers { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public SouthseaDeckhand(CardAsset CA) : base(CA) {
        Triggers = new() { new(CardEvent.OnWeaponDestroy, Triggered), new(CardEvent.OnWeaponEquip, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner || Minion == null) return;
        if (e.EventType is CardEvent.OnWeaponDestroy) { // TODO: is it right?
            Minion.IsCharge = false;
        }
        else if (e.EventType is CardEvent.OnWeaponEquip) {
            Minion.IsCharge = true;
        }
    }

}