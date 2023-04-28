using UnityEngine;

public class CardAsset : ScriptableObject {

    [TextArea(2, 3)]
    public string Description;
    public Sprite CardImage;
    public int ManaCost;
    public Rarity rarity;
    public ClassType ClassType;
    public CardType cardType;
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
    public bool isLifeSteal;
    public bool isPoisonous;
    public int Overload;
    public MinionType MinionType;

    [Header("SpellSection")]
    public bool isSecret;
    public TargetingOptions TargetsType;
    public SpellSchool SpellSchool;

}