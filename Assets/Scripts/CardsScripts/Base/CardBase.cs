using System;
using System.Collections.Generic;
using System.Linq;

public abstract class CardBase : IIdentifiable, IBuffable {
    public virtual int ID => CardID;
    private readonly int CardID;
    public PlayerLogic Owner;
    public CardAsset CA { get; set; }
    private int _manaCost;
    public int ManaCost { get => _manaCost; set => _manaCost = value; }
    public bool CanUse = true;
    public bool TargetExist => this is not ITarget || (BattleControl.GetAllCharacters().Where((this as ITarget).Match).Where(CanBeTarget).ToList().Count != 0);
    public virtual bool CanBePlayed {
        get {
            if (costHealth) return BattleControl.Instance.ActivePlayer == Owner && ManaCost < Owner.Health && CanUse && (this is MinionCard || TargetExist);
            else return BattleControl.Instance.ActivePlayer == Owner && ManaCost <= Owner.Mana.Manas && CanUse && (this is MinionCard || TargetExist);
        }
    }
    public List<Buff> BuffList { get; set; }
    public int overloadCrystal;
    public bool costHealth = false;
    public List<TriggerStruct> Triggers { get; set; }
    public List<Buff> Auras { get; set; }

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
        if (overloadCrystal > 0) EventManager.Allocate<ManaEventArgs>().CreateEventArgs(ManaEvent.OnCrystalOverload, null, Owner, overloadCrystal).Invoke();
        BattleControl.CardUsed.Add(new(this, ID, Owner.TurnCount));
        ExtendUse();
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnCardUse, null, Owner, this); // TODO: check it
    }

    public virtual void ExtendUse() { }

    public virtual void ReadBuff() {
        ManaCost = CA.ManaCost;
        if (BuffList.Count != 0) {
            foreach (Buff b in BuffList) {
                if (b.statusChange.Count == 0) break;
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
        if (Auras.Count != 0) {
            foreach (Buff b in Auras) {
                if (b.statusChange.Count == 0) break;
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
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.HandVisualUpdate);
    }

    public bool CanBeTarget(ICharacter target) {
        if (target.Attributes == null) return true;
        if (target.Attributes.Contains(CharacterAttribute.Immune)) return false;
        if (target.Attributes.Contains(CharacterAttribute.Elusive) && (this is SpellCard)) return false;
        if (target.Attributes.Contains(CharacterAttribute.Stealth) && ((target as MinionLogic).Owner != Owner || (target as PlayerLogic) != Owner)) return false;
        return true;
    }

}
