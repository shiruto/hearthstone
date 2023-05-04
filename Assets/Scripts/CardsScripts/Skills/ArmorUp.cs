public class ArmorUp : SkillCard {

    public ArmorUp(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Effect.GetArmorEffect(2, Owner, this);
    }

}