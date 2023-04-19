using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataAsset {

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
        Discover
    }

    public enum GameStatus {
        Playing,
        Win,
        Lose,
        Tie
    }
}
