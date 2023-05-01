using System.Collections.Generic;
using System.Linq;

public class SouthseaDeckhand : MinionCard, ITriggerMinionCard {
    public List<TriggerStruct> TriggersToGrant { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public SouthseaDeckhand(CardAsset CA) : base(CA) {
        TriggersToGrant = new() { new(CardEvent.OnWeaponDestroy, Triggered), new(CardEvent.OnWeaponEquip, Triggered) };
    }

    public void Triggered(BaseEventArgs e) {
        if (e.Player != Owner || Minion == null) return;
        if (e.EventType is CardEvent.OnWeaponDestroy) { // TODO: is it right?
            Minion.Attributes.Remove(CharacterAttribute.Charge);
        }
        else if (e.EventType is CardEvent.OnWeaponEquip) {
            Minion.Attributes.Remove(CharacterAttribute.Charge);
        }
    }

}