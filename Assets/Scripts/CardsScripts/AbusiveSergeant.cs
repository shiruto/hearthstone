using UnityEngine;

public class AbusiveSergeant : MinionCard {
    public AbusiveSergeant(CardAsset CA) : base(CA) {
        _battleCryEffects.Add(new GiveBuff(new Buff(0, 2)));
        TargetedBattleCry = true;
    }
}
