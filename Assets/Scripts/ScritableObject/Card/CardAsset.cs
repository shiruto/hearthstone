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

}