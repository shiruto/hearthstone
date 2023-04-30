public class DemonHunterSkill : SkillCard {
    public Buff buff;

    public DemonHunterSkill(CardAsset CA) : base(CA) {
        buff = new(
            "DemonHunterSkill",
            new() { new(Status.Health, Operator.Plus, 1) }
        );
        effects.Add(new GiveBuff(buff, this, Owner));
    }

}