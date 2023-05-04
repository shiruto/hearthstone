using System;
using UnityEngine;

public class MindSpike : SkillCard, IDealDamage, ITarget {
    public int Damage => 2;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => true;

    public MindSpike(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Target.TakeDamage(2, this);
    }

}