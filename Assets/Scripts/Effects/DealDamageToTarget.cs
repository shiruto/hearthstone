using UnityEngine;

public class DealDamageToTarget : Effect {
    public int Value;
    public override string Name => "Deal Damage to Target Effect";
    public ITakeDamage Target { get; set; }
    public IBuffable Attacker { get; set; }
    public bool isMagicDamage;
    public bool isHealing = false;

    public DealDamageToTarget(int damage, IBuffable attacker, ITakeDamage Target, bool isHealing = false, bool isMagicDamage = false) { // target defined by system
        Value = damage;
        Attacker = attacker;
        this.Target = Target;
        this.isMagicDamage = isMagicDamage;
        this.isHealing = isHealing;
    }

    public override void ActivateEffect() {
        if (isHealing) Target?.Healing(Value, Attacker);
        else {
            if (isMagicDamage) Value += BattleControl.Instance.SpellDamage;
            Target?.TakeDamage(Value, Attacker);
        }
    }

}
