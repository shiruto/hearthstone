using UnityEngine;

public class SteadyShot : SkillCard, IDealDamage {
    public int Damage => 2;

    public SteadyShot(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(Damage, this, BattleControl.opponent).ActivateEffect();
    }

}