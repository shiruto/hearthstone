using UnityEngine;

public class SnakeTrap : SecretCard {

    public SnakeTrap(CardAsset CA) : base(CA) {
        trigger = new(AttackEvent.BeforeAttack, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.target is not MinionLogic || (evt.target as MinionLogic).Owner != Owner)
            return false;
        for (int i = 0; i < 3; i++)
            Owner.Field.SummonMinionAt(-1, new(new Snake(Resources.Load(GameData.Path + "UnCollectableCard/Snake") as CardAsset)));
        return true;
    }

}