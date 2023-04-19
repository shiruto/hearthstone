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
    [Header("MinionSection")]
    public int Attack;
    public int Health;
    public int SpellDamage = 0;
    public bool isWindFury;
    public bool isTaunt;
    public bool isCharge;
    public bool isRush;
    public bool isDivineShield;
    public bool isStealth;
    public bool isImmune;
    public bool isFrozen;
    public bool isPoisonous;
    public int Overload;
    public GameDataAsset.MinionType MinionType;

    [Header("SpellSection")]
    public bool isSecret;
    public GameDataAsset.TargetingOptions TargetsType;
    public GameDataAsset.SpellSchool SpellSchool;

}