public class BloodsailCorsair : MinionCard {

    public BloodsailCorsair(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (BattleControl.opponent.Weapon != null) {
            new DealDamageToTarget(false, 1, this, BattleControl.opponent.Weapon).ActivateEffect();
        }
    }

}