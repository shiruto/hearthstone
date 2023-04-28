using UnityEngine;

public class DealDamageToTarget : Effect {

    public int damage;
    public override string Name => "Deal Damage to Target Effect";
    public ITakeDamage Target { get; set; }
    public CardBase Attacker { get; set; }
    public bool isMagicDamage;
    public bool isHealing = false;

    public DealDamageToTarget(bool isHealing, int damage, CardBase attacker, bool isMagicDamage = false) { // target defined by player
        if (damage > 0 && isHealing || (damage < 0 && !isHealing)) {
            Debug.Log("wrong damage");
        }
        this.damage = damage;
        Attacker = attacker;
        Target = null;
        this.isMagicDamage = isMagicDamage;
    }

    public DealDamageToTarget(bool isHealing, int damage, CardBase attacker, ITakeDamage Target, bool isMagicDamage = false) { // target defined by system
        if (damage > 0 && isHealing || (damage < 0 && !isHealing)) {
            Debug.Log("wrong damage");
        }
        this.damage = damage;
        Attacker = attacker;
        this.Target = Target;
        this.isMagicDamage = isMagicDamage;
    }

    public override void ActivateEffect() {
        if (isMagicDamage) damage += BattleControl.Instance.SpellDamage;
        Target.Health -= damage;
    }

}
