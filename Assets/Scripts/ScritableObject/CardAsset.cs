using System.Collections.Generic;
using UnityEngine;
public class CardAsset : ScriptableObject {

    [TextArea(2, 3)]
    public string Description;
    public Sprite CardImage;
    public int ManaCost;
    public GameDataAsset.Rarity rarity;
    public GameDataAsset.ClassType ClassType;
    public bool isTriggered;
    public GameDataAsset.CardType cardType;

    [Header("Creature Info")]
    public int Attack;
    public int MaxHealth;
    public int SpellDamage = 0;
    public int specialCreatureAmount;
    public bool isWindFury;
    public bool isTaunt;
    public bool isCharge;
    public bool isRush;
    public bool isDivineShield;
    public bool isStealth;
    public bool isImmune;
    public bool isFrozen;
    public bool isPoisonous;
    public bool haveDeathRattle;
    public string CreatureScriptName;
    public GameDataAsset.MinionType MinionType;

    [Header("SpellInfo")]
    public string SpellScriptName;
    public int specialSpellAmount;
    public bool isSecret;
    public GameDataAsset.TargetingOptions TargetsType;
    public GameDataAsset.MinionType TargetMinionType;
    public GameDataAsset.Harm TargetsHarm;
    public GameDataAsset.SpellSchool SpellSchool;

}