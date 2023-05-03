using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealAoeDamage : Effect {
    public bool isHeal;
    public int damage;
    private Func<ICharacter, bool> range;
    public override string Name => "Deal AoE Damage Effect";
    public bool isMagicDamage;
    public IBuffable Attacker;

    public DealAoeDamage(int damage, IBuffable Attacker) {
        this.damage = damage;
        this.Attacker = Attacker;
        range = (ICharacter a) => true;
    }

    public DealAoeDamage(int damage, IBuffable Attacker, Func<ICharacter, bool> range = null, bool isMagicDamage = false) {
        this.damage = damage;
        this.Attacker = Attacker;
        this.range = range;
        this.isMagicDamage = isMagicDamage;
    }

    public override void ActivateEffect() {
        range ??= (ICharacter c) => true;
        List<ICharacter> CharacterToDamage = new(BattleControl.GetAllCharacters().Where(range));
        if (isMagicDamage) damage += BattleControl.Instance.SpellDamage;
        foreach (ICharacter c in CharacterToDamage) {
            c.TakeDamage(damage, Attacker);
        }
    }

}
