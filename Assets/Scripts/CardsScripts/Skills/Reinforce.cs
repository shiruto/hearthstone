public class Reinforce : SkillCard {
    private readonly CardAsset SilverHandRecruit;

    public Reinforce(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Owner.Field.SummonMinionAt(-1, new(new SilverHandRecruit(SilverHandRecruit)));
    }

}