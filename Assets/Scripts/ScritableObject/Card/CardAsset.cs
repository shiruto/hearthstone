using System.Collections.Generic;
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
    public int Overload;
    public MinionType MinionType;
    public List<CharacterAttribute> attributes;
    [Header("SpellSection")]
    public bool isSecret;
    public SpellSchool SpellSchool;

}