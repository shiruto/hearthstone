using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMinion : Effect {
    public MinionLogic MinionToSummon;
    public PlayerLogic owner = BattleControl.you;
    public new string Name = "SummonMinionEffect";
    public int position;
    // TODO summon minion's position
    public SummonMinion(CardBase MC) {
        MinionToSummon = new((MinionCard)MC);
    }
    public SummonMinion(PlayerLogic owner, CardBase MC) {
        this.owner = owner;
        MinionToSummon = new((MinionCard)MC);
    }
    public SummonMinion(CardAsset CA) {
        MinionToSummon = new(CA);
    }

    public SummonMinion(PlayerLogic owner, CardAsset CA) {
        MinionToSummon = new(CA);
        this.owner = owner;
    }
    public override void ActivateEffect() {
        owner.Field.SummonMinionAt(position, MinionToSummon);
    }
}
