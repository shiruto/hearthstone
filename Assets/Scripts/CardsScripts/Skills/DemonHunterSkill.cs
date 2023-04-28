public class DemonHunterSkill : SkillCard {

    public int _healthModifier = 0;
    public int _attackModifier = 1;
    public Buff buff;

    public DemonHunterSkill(CardAsset CA) : base(CA) {
        buff = new(_healthModifier, _attackModifier);
        effects.Add(new GiveBuff(buff, this, Owner));
    }

}