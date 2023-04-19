using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : IIdentifiable {
    public virtual int ID { get => CardID; }
    private readonly int CardID;
    public PlayerLogic owner;
    public CardAsset CA { get; set; }
    public int ManaCost { get; set; }
    // public bool CanBePlayed { get => CurManaCost <= owner.Mana.Manas; }
    public List<Buff> Buffs { get; set; }
    public bool ifDrawLine;
    public ICharacter Target;

    public CardBase(CardAsset CA) {
        CardID = IDFactory.GetID();
        this.CA = CA;
        ManaCost = CA.ManaCost;
        // BattleControl.CardCreated.Add(CardID, this);
    }

    public virtual void Use() {
        Debug.Log("Card Base Use Func");
    }
}
