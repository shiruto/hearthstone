using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAsset : ScriptableObject {
    public int Order;
    public GameDataAsset.ClassType DeckClass;
    public List<CardAsset> myCardAssets;
    public List<int> myCardNums;
}
