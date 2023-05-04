using UnityEngine;

public class DefiasRingleader : MinionCard {
    private readonly CardAsset DefiasBandit = Resources.Load(GameData.Path + "UnCollectableCard/Defias Bandit") as CardAsset;

    public DefiasRingleader(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (!BattleControl.IfUsedThisTurn()) return;
        Owner.Field.SummonMinionAt(-1, new(new DefaultMinion(DefiasBandit)));
    }

}