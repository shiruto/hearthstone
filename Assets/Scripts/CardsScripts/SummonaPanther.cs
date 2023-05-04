using UnityEngine;

public class SummonaPanther : SpellCard {
    private readonly CardAsset Minion;

    public SummonaPanther(CardAsset CA) : base(CA) {
        Minion = Resources.Load(GameData.Path + "UnCollectableCard/Panther") as CardAsset;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        Owner.Field.SummonMinionAt(-1, new(new DefaultMinion(Minion)));
    }

}