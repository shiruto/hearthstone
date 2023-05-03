using UnityEngine;

public class NobleSacrifice : SecretCard {
    private readonly CardAsset MinionToSummon;

    public NobleSacrifice(CardAsset CA) : base(CA) {
        trigger = new(AttackEvent.BeforeAttack, Triggered);
        MinionToSummon = Resources.Load(GameData.Path + "UnCollectableCard/Defender") as CardAsset;
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (!Logic.IsEnemy(Owner, evt.attacker)) return false;
        MinionLogic minion = new(new Defender(MinionToSummon));
        Owner.Field.SummonMinionAt(-1, minion);
        evt.attacker.AttackTarget = minion;
        return true;
    }

}