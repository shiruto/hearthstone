using System;

public class MoonFire : SpellCard, IDealDamage, ITarget {
    public int Damage => 1;
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter a) => a.Attributes.Contains(CharacterAttribute.Elusive);

    public MoonFire(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealDamageToTarget(false, Damage, this, Target, true).ActivateEffect();
    }

}
