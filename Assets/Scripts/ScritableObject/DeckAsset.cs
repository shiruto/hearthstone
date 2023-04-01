using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAsset : ScriptableObject {
    public int Order;
    public GameDataAsset.ClassType DeckClass;
    public List<CardAsset> myCardAssets;
    public List<int> myCardNums;
    public int SkinID;
    public DeckAsset(DeckAsset DA) {
        this.Order = DA.Order;
        this.DeckClass = DA.DeckClass;
        this.myCardAssets = new(DA.myCardAssets);
        this.myCardNums = new(DA.myCardNums);
    }

    public DeckAsset(int Order, GameDataAsset.ClassType c, List<CardAsset> CAL, List<int> CNL) {
        this.Order = Order;
        this.DeckClass = c;
        this.myCardAssets = new(CAL);
        this.myCardNums = new(CNL);
    }
}
