using System;
using System.Collections.Generic;

public class LeperGnome : MinionCard, IDeathrattleCard, IDealDamage {
    public int Damage => 2;

    public LeperGnome(CardAsset CA) : base(CA) {

    }

    public void Deathrattle(MinionLogic caller) {
        Effect.DealDamage(Damage, caller, BattleControl.GetEnemy(caller.Owner));
    }

}