using System;

public class LesserHeal : SkillCard, IHeal, ITarget {
    public int Heal => 2;
    public ICharacter Target { get; set; }

    public Func<ICharacter, bool> Match => (ICharacter a) => true;

    public LesserHeal(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(Heal, this, Target, true).ActivateEffect();
    }

}