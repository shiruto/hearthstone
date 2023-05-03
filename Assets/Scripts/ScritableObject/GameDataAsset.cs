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

public struct UsedCardStruct {
    public CardBase Card;
    public int CardID;
    public int TurnCount;

    public UsedCardStruct(CardBase Card, int CardID, int TurnCount) {
        this.Card = Card;
        this.CardID = CardID;
        this.TurnCount = TurnCount;
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
    Hero,
    Skill
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
    Frozen,
    Reborn,
    CantAttack
}

public enum DeckStatus { Empty, LastOne, Less, Medium, Alot, Full }

public static class GameData {
    public static readonly string Path = "ScriptableObject/Card/";

    public static readonly Dictionary<ClassType, Color> SecretColor = new() {
        {ClassType.Mage, new(1, 104/255f, 248/255f, 1)},
        {ClassType.Hunter, Color.green},
        {ClassType.Rouge, Color.gray},
        {ClassType.Paladin, Color.yellow}
    };

    public static readonly Dictionary<ClassType, string> HeroPower = new() {
        {ClassType.DemonHunter, "Demon Claws"},
        {ClassType.Druid, "Shapeshift"},
        {ClassType.Hunter, "Steady Shot"},
        {ClassType.Mage, "Fireblast"},
        {ClassType.Paladin, "Reinforce"},
        {ClassType.Priest, "Lesser Heal"},
        {ClassType.Rouge, "Dagger Mastery"},
        {ClassType.Shaman, "Totemic Call"},
        {ClassType.Warloc, "Life Tap"},
        {ClassType.Warrior, "Armor Up"},
    };

    public static Func<CardBase, ICharacter, bool> CanBeTarget = (CardBase Card, ICharacter target) => {
        if (target.Attributes == null) return true;
        if (target.Attributes.Contains(CharacterAttribute.Immune)) return false;
        if (target.Attributes.Contains(CharacterAttribute.Elusive) && Card is SpellCard) return false;
        if (target.Attributes.Contains(CharacterAttribute.Stealth) && ((target as MinionLogic).Owner != Card.Owner || (target as PlayerLogic) != Card.Owner)) return false;
        return true;
    };

}

public static class Logic {
    public static bool CanTaunt(MinionLogic minion) {
        if (minion == null || minion.Attributes == null) return false;
        return minion.Attributes.Contains(CharacterAttribute.Taunt) && !(minion.Attributes.Contains(CharacterAttribute.Stealth) || minion.Attributes.Contains(CharacterAttribute.Immune));
    }

    public static bool IsEnemy(ICharacter a, ICharacter b) {
        PlayerLogic p1 = (a is MinionLogic) ? (a as MinionLogic).Owner : (a as PlayerLogic);
        PlayerLogic p2 = (b is MinionLogic) ? (b as MinionLogic).Owner : (b as PlayerLogic);
        return p1 == p2;
    }

}

public static class Util {
    public static Predicate<T> FuncToPredicate<T>(this Func<T, bool> func) {
        return x => func(x);
    }

    public static Func<T, bool> PredicateToFunc<T>(this Predicate<T> predicate) {
        return x => predicate(x);
    }
}
