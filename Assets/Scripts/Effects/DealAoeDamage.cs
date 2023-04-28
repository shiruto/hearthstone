using System;
using System.Collections.Generic;
using System.Linq;

public class DealAoeDamage : Effect {
    public int damage;
    readonly Func<ICharacter, bool> range;
    public override string Name => "Deal AoE Damage Effect";
    public bool isMagicDamage;

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
