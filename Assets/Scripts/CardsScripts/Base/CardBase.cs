using System.Collections.Generic;

public abstract class CardBase : IIdentifiable, IBuffable {
    public virtual int ID => CardID;
    private readonly int CardID;
    public PlayerLogic Owner;
    public CardAsset CA { get; set; }
    private int _manaCost;
    public int ManaCost { get => _manaCost; set => _manaCost = value; }
    public bool CanBePlayed {
        get {
            if (costHealth) return ManaCost < Owner.Health;
            else return ManaCost <= Owner.Mana.Manas;
        }
    }
    public List<Buff> BuffList { get; set; }
    public int overloadCrystal;
    public bool costHealth = false;
    public List<TriggerStruct> Triggers { get; set; }

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
        else if (ManaCost > 0) Owner.Mana.Manas -= ManaCost;
        ExtendUse();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, null, Owner, this); // TODO: check it
    }

    public virtual void ExtendUse() { }

    public virtual void ReadBuff() {
        if (BuffList.Count == 0) return;
        foreach (Buff b in BuffList) {
            if (b.statusChange.Count != 0) {
                foreach (var sc in b.statusChange) {
                    switch (sc.status) {
                        case Status.ManaCost:
                            Buff.Modify(ref _manaCost, sc.op, sc.Num);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

}
