public class DaggerMastery : SkillCard {
    private readonly CardAsset WickedKnife;

    public DaggerMastery(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Owner.Weapon.EquipWeapon(new WickedKnife(WickedKnife));
    }

}