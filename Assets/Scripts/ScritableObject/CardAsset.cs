using UnityEngine;

public enum TargetingOptions { NoTarget, AllMinions, EnemyMinions, YourMinions, AllCharacters, EnemyCharacters, YourCharacters }
public enum Harm { None, Unharmed, Harmed }
public enum MinionType { None, Murloc, Demon, Mech, Elemental, Beast, Totem, Pirate, Dragon, Quilboar, Naga, Undead, All }
public enum SpellSchool { None, Arcane, Fire, Frost, Nature, Holy, Shadow, Fel }
public enum Rarity { Free, Normal, Rare, Epic, Legendary }
public enum ClassType { DemonHunter, Druid, Hunter, Mage, Paladin, Priest, Rouge, Shaman, Warloc, Warrior, Neutral }

public class CardAsset : ScriptableObject {

    [TextArea(2, 3)]
    public string Description;
    public Sprite CardImage;
    public int ManaCost;
    public int OrgManaCost;
    public Rarity rarity;
    public ClassType ClassType;
    public bool isTriggered;
    public CardManager Script;

    [Header("Creature Info")]
    public int Attack;
    public int CurAttack;
    public int MaxHealth;
    public int CurHealth;
    public int AttacksChances = 1;
    public int SpellDamage = 0;
    public int specialCreatureAmount;
    public bool isWeapon;
    public bool isTaunt;
    public bool isCharge;
    public bool isRush;
    public bool isDivineShield;
    public bool isStealth;
    public bool isImmune;
    public bool isFrozen;
    public bool isPoison;
    public string CreatureScriptName;
    public MinionType MinionType;

    [Header("SpellInfo")]
    public string SpellScriptName;
    public int specialSpellAmount;
    public bool isSecret;
    public TargetingOptions TargetsType;
    public MinionType TargetMinionType;
    public Harm TargetsHarm;
    public SpellSchool SpellSchool;

}