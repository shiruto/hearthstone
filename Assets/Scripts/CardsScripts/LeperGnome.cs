using System.Collections.Generic;

public class LeperGnome : MinionCard, IDeathRattle, IDealDamage {
    public List<Effect> DeathRattleEffects { get; set; }
    public int Damage => 2;

    public LeperGnome(CardAsset CA) : base(CA) {
        DeathRattleEffects = new() { new DealDamageToTarget(false, Damage, this, BattleControl.GetEnemy(Owner)) };
    }

}