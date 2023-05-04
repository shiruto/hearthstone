using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealAoeDamage : Effect {
    public bool isHeal;
    public int Value;
    private Func<ICharacter, bool> range;
    public override string Name => "Deal AoE Damage Effect";
    public bool isMagicDamage;
    public IBuffable Attacker;

    public DealAoeDamage(int damage, IBuffable Attacker, Func<ICharacter, bool> range = null, bool isHeal = false, bool isMagicDamage = false) {
        Value = damage;
        this.Attacker = Attacker;
        this.isHeal = isHeal;
        this.range = range;
        this.isMagicDamage = isMagicDamage;
    }

    public override void ActivateEffect() {
        range ??= (ICharacter c) => true;
        List<ICharacter> CharacterToDamage = new(BattleControl.GetAllCharacters().Where(range));
        if (isHeal) {
            foreach (ICharacter c in CharacterToDamage) {
                c.Healing(Value, Attacker);
            }
        }
        if (isMagicDamage) Value += BattleControl.Instance.SpellDamage;
        foreach (ICharacter c in CharacterToDamage) {
            c.TakeDamage(Value, Attacker);
        }
    }

}
