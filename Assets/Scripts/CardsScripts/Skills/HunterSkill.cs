public class HunterSkill : SkillCard {

    private int _damage;
    public int Damage {
        get => _damage;
        set {
            if (value < 0) {
                _damage = 0;
            }
            else _damage = value;
        }
    }

    public HunterSkill(CardAsset CA) : base(CA) {
        Damage = 2;
        effects.Add(new DealDamageToTarget(Damage, BattleControl.opponent));
    }
}