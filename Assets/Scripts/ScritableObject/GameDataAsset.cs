using System;
using System.Collections.Generic;
using UnityEngine;

public struct TriggerStruct {
    public Enum eventType;
    public Action<BaseEventArgs> callback;

    public TriggerStruct(Enum eventType, Action<BaseEventArgs> callback) {
        this.eventType = eventType;
        this.callback = callback;
    }
}

public struct StatusChange {
    public Status status;
    public Operator op;
    public int Num;

    public StatusChange(Status s, Operator o, int n) {
        status = s;
        op = o;
        Num = n;
    }
}

public struct AuraManager {
    public Buff Aura;
    public Func<IBuffable, bool> range;
    public TriggerStruct expireTrigger;

    public AuraManager(Buff a, Func<IBuffable, bool> r, TriggerStruct t = default) {
        Aura = a;
        range = r;
        expireTrigger = t;
    }

}

public enum Status {
    Health,
    Attack,
    ManaCost,
    SpellDamage
}

public enum Operator {
    Plus,
    Minus,
    Time,
    Divide,
    equal
}

public enum ClassType {
    DemonHunter,
    Druid,
    Hunter,
    Mage,
    Paladin,
    Priest,
    Rouge,
    Shaman,
    Warloc,
    Warrior,
    Neutral
}

public enum SpellSchool {
    None,
    Arcane,
    Frost,
    Fire,
    Nature,
    Fel,
    Shadow,
    Holy
}

public enum MinionType {
    None,
    Beast,
    Demon,
    Undead,
    Totem,
    Murloc,
    Mech,
    Elemental,
    Dragon,
    Pirate,
    Quilboar,
    Naga
}

public enum TargetingOptions {
    NoTarget,
    AllMinions,
    EnemyMinions,
    YourMinions,
    AllCharacters,
    EnemyCharacters,
    YourCharacters
}

public enum Harm {
    None,
    Unharmed,
    Harmed
}

public enum Rarity {
    Free,
    Normal,
    Rare,
    Epic,
    Legendary
}

public enum CardType {
    Spell,
    Minion,
    Weapon,
    Hero
}

public enum EffectType {
    CrystalChange,
    DealDamageToAllMinion,
    DealDamageToTarget,
    DrawCard,
    GiveBuff,
    GiveCard,
    ManaChange,
    SummonMinion,
    Discover,
    CastSpell
}

public enum GameStatus {
    Playing,
    Win,
    Lose,
    Tie
}

[Serializable]
public enum CharacterAttribute {
    Windfury,
    Taunt,
    Charge,
    Rush,
    DivineShield,
    Stealth,
    Immune,
    LifeSteal,
    Poisonous,
    Elusive,
    Frozen
}

public class GameDataAsset {
    public static readonly Dictionary<ClassType, Color> SecretColor = new() {
        {ClassType.Mage, new(1, 104/255f, 248/255f, 1)},
        {ClassType.Hunter, Color.green},
        {ClassType.Rouge, Color.gray},
        {ClassType.Paladin, Color.yellow}
    };
}

