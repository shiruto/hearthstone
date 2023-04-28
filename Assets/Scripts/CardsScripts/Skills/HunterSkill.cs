public class HunterSkill : SkillCard, IDealDamage {

    public int Damage => 2;

    public HunterSkill(CardAsset CA) : base(CA) {
        effects.Add(new DealDamageToTarget(false, Damage, Owner.Card, BattleControl.opponent)); // damage type?
    }

}