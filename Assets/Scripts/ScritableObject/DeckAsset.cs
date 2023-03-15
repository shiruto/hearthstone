using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAsset: ScriptableObject {
	public int Order;
	public List<CardAsset> myCardAssets;
	public List<int> myCardNums;
}
