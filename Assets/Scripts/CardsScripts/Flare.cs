using UnityEngine;

public class Flare : SpellCard {

    public Flare(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        foreach (MinionLogic minion in BattleControl.opponent.Field.GetMinions()) {
            minion.Attributes.Remove(CharacterAttribute.Stealth);
        }
        BattleControl.opponent.Attributes.Remove(CharacterAttribute.Stealth);
        new DrawCard(Owner, 1).ActivateEffect();
    }

}