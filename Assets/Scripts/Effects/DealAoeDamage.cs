using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealAoeDamage : Effect {
    public bool isHeal;
    public int damage;
    readonly Func<ICharacter, bool> range;
    public override string Name => "Deal AoE Damage Effect";
    public bool isMagicDamage;
    public List<ICharacter> CharacterToDamage = new(BattleControl.Instance.GetAllMinions()) {
            BattleControl.you,
            BattleControl.opponent
        };

    public DealAoeDamage(int damage) {
        this.damage = damage;
        range = (ICharacter a) => true;
    }

    public DealAoeDamage(int damage, Func<ICharacter, bool> range, bool isMagicDamage = false) {
        this.damage = damage;
        this.range = range;
        this.isMagicDamage = isMagicDamage;
    }

    public override void ActivateEffect() {
        CharacterToDamage = new(CharacterToDamage.Where(range));
        if (isMagicDamage) damage += BattleControl.Instance.SpellDamage;
        foreach (ICharacter c in CharacterToDamage) {
            c.Health -= damage;
        }
    }
}
