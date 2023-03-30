using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : IIdentifiable {
    public virtual int ID { get => CardID; }
    private int CardID;
    public PlayerLogic owner;
    public CardAsset CA { get; set; }
    public int CurManaCost { get; set; }
    // public bool CanBePlayed { get => CurManaCost <= owner.Mana.Manas; }
    public List<Buff> Buffs { get; set; }
    public CardBase(CardAsset CA) {
        CardID = IDFactory.GetID();
        this.CA = CA;
        CurManaCost = CA.ManaCost;
        // BattleControl.CardCreated.Add(CardID, this);
    }
    public virtual void Use() {
        Debug.Log("Card Base Use Func");
    }
}
