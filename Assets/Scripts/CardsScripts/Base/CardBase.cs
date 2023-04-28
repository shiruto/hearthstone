using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : IIdentifiable, IBuffable {
    public virtual int ID => CardID;
    private readonly int CardID;
    public PlayerLogic Owner;
    public CardAsset CA { get; set; }
    public int ManaCost { get; set; }
    public bool CanBePlayed {
        get {
            if (costHealth) return ManaCost < Owner.Health;
            else return ManaCost <= Owner.Mana.Manas;
        }
    }
    public List<Buff> BuffList { get; set; }
    public int overloadCrystal;
    public bool costHealth = false;

    public CardBase(CardAsset CA) {
        CardID = IDFactory.GetID();
        this.CA = CA;
        ManaCost = CA.ManaCost;
        overloadCrystal = CA.Overload;
        // BattleControl.CardCreated.Add(CardID, this);
    }

    public virtual void Use() {
        if (overloadCrystal > 0) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnCrystalOverload, null, Owner, overloadCrystal).Invoke();
        if (costHealth) Owner.Health -= ManaCost;
        else if (ManaCost > 0) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnManaSpend, null, Owner, ManaCost).Invoke();
        ExtendUse();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, null, Owner, this); // TODO: check it
    }

    public virtual void ExtendUse() { }

}
