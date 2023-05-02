public class Gorehowl : WeaponCard {

    public Gorehowl(CardAsset CA) : base(CA) {

    }

    public override void AttackEffect(ICharacter Target) {
        Owner.CanAttack = false;
        Target.Health -= Owner.Attack;
        Owner.Health -= Target.Attack;
        if (Target is MinionLogic) {
            Owner.Weapon.Attack -= 1;
        }
        else Owner.Weapon.Health -= 1;
    }

}