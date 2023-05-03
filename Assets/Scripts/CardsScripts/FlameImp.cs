using System.Collections.Generic;

public class FlameImp : MinionCard, IBattleCry, IDealDamage {
    public List<Effect> BattleCryEffects { get; set; }
    public int Damage => 3;

    public FlameImp(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        new DealDamageToTarget(false, Damage, this, Owner).ActivateEffect();
    }

}