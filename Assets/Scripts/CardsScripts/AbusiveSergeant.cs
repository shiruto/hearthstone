using UnityEngine;

public class AbusiveSergeant : MinionCard {
    public AbusiveSergeant(CardAsset CA) : base(CA) {
        ifDrawLine = true;
        _battleCryEffects.Add(new GiveBuff(new Buff(0, 2)));
    }
}
