using UnityEngine;

public class Flare : SpellCard {

    public Flare(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        foreach (MinionLogic minion in BattleControl.GetAllMinions()) {
            minion.Attributes.Remove(CharacterAttribute.Stealth);
        }
        BattleControl.GetEnemy(Owner).Secrets.RemoveAllSecret();
        new DrawCard(Owner, 1).ActivateEffect();
    }

}