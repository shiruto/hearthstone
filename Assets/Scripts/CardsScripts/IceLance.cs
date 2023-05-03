using System;

public class IceLance : SpellCard, IDealDamage, ITarget {
    public int Damage => 4;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => true;
    private readonly Buff buff;

    public IceLance(CardAsset CA) : base(CA) {
        buff = new(
            "Ice Lance",
            null,
            null,
            new() { CharacterAttribute.Frozen }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        if (Target.Attributes.Contains(CharacterAttribute.Frozen)) {
            new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
        }
        else new GiveBuff(buff, this, Target).ActivateEffect();
    }

}