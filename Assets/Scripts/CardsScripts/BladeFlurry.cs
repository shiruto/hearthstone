public class BladeFlurry : SpellCard, IDealDamage {
    public int Damage => Owner.Weapon.Attack;
    public override bool CanBePlayed => Owner.Weapon.HaveWeapon && base.CanBePlayed;

    public BladeFlurry(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        new DealAoeDamage(Damage, this, (ICharacter c) => c is MinionLogic && Logic.IsEnemy(c, Owner), false, true).ActivateEffect();
    }

}