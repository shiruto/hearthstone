public class ArmorUp : SkillCard {

    public ArmorUp(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Owner.Armor += 2;
    }

}