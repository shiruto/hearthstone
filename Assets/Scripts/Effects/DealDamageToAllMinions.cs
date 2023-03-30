using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DealAoeDamage : Effect {
    public int damage;
    Func<ICharacter, bool> range;
    public override string Name => "Deal AoE Damage Effect";
    public DealAoeDamage(int damage) {
        this.damage = damage;
        range = (ICharacter a) => true;
    }
    public DealAoeDamage(int damage, Func<ICharacter, bool> range) {
        this.damage = damage;
        this.range = range;
    }

    public override void ActivateEffect() {
        List<ICharacter> CharacterToDamage = new(BattleControl.you.Field.GetMinions());
        CharacterToDamage.AddRange(BattleControl.you.Field.GetMinions());
        CharacterToDamage.Add(BattleControl.you);
        CharacterToDamage.Add(BattleControl.opponent);
        CharacterToDamage.Where(range);
        foreach (ICharacter minion in CharacterToDamage) {
            minion.Health -= damage;
        }
    }
}
