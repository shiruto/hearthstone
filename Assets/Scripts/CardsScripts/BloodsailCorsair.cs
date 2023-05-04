public class BloodsailCorsair : MinionCard {

    public BloodsailCorsair(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (BattleControl.opponent.Weapon != null) {
            new DealDamageToTarget(1, this, BattleControl.opponent.Weapon).ActivateEffect();
        }
    }

}