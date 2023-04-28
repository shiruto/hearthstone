using System;
using System.Collections.Generic;
using UnityEngine;

public class MinionCard : CardBase {
    public MinionLogic Minion = null;
    public int Health;
    public int Attack;

    public MinionCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        MinionLogic minionToSummon = new(this);
        if (this is IBattleCry) (this as IBattleCry).BattleCry();
        minionToSummon.Owner = Owner;
        Owner.Field.SummonMinionAt(0, minionToSummon); // TODO: choose position
        Minion = minionToSummon;
    }

}
