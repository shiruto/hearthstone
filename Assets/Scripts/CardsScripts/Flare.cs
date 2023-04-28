using UnityEngine;

public class Flare : SpellCard {

    public Flare(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        foreach (MinionLogic minion in BattleControl.opponent.Field.GetMinions()) {
            minion.IsStealth = false;
        }
        BattleControl.opponent.IsStealth = false;
        new DrawCard(Owner, 1).ActivateEffect();
        Debug.Log(Owner);
    }

}