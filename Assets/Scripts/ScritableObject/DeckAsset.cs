using System.Collections.Generic;
using UnityEngine;

public class DeckAsset : ScriptableObject {
    public int Order;
    public ClassType DeckClass;
    public List<CardAsset> myCardAssets;
    public List<int> myCardNums;
    public int SkinID;

    public DeckAsset(DeckAsset DA) {
        Order = DA.Order;
        DeckClass = DA.DeckClass;
        myCardAssets = new(DA.myCardAssets);
        myCardNums = new(DA.myCardNums);
    }

    public DeckAsset(int Order, ClassType c, List<CardAsset> CAL, List<int> CNL) {
        this.Order = Order;
        DeckClass = c;
        myCardAssets = new(CAL);
        myCardNums = new(CNL);
    }
}
