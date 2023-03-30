using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBuff : Effect {
    private Buff BuffToGive;
    public override string Name => "buff effect";
    public GiveBuff(Buff buff) {
        BuffToGive = buff;
    }
    public override void ActivateEffect() {
        BattleControl.Instance.Targeting.Buffs.Add(BuffToGive);
    }

}
