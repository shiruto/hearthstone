using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFire : SpellCard {
    private readonly int SpellDamage;
    private ICharacter Target;
    public MoonFire(CardAsset CA) : base(CA) {
        SpellDamage = CA.specialSpellAmount;
    }

    public override void Use() {
        Target = BattleControl.Instance.Targeting;
        new DealDamageToTarget(SpellDamage, Target);
    }
}
