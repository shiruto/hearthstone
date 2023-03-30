using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToTarget : Effect {
    public ICharacter Target;
    public int damage;
    public override string Name => "Deal Damage to Target Effect";
    public DealDamageToTarget(int damage, ICharacter Target) {
        this.damage = damage;
        this.Target = Target;
    }
    public override void ActivateEffect() {
        Target.Health -= damage;
    }
}
