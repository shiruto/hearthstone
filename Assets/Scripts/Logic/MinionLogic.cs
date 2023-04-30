using System;
using System.Collections.Generic;

public class MinionLogic : ICharacter {
    # region minion property
    public PlayerLogic Owner { get; set; }
    public MinionCard Card;
    public List<TriggerStruct> Triggers { get; set; }

    private readonly int MinionID;
    public int ID => MinionID;

    public int MaxHelath;
    private int _health;
    public virtual int Health {
        get => _health;
        set {
            if (value > MaxHelath) _health = MaxHelath;
            else if (value <= 0) {
                _health = value;
                Die();
            }
            else _health = value;
            EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionStatusChange, null, Owner, this);
        }
    }

    private int _attack;
    public int Attack {
        get => _attack;
        set {
            if (value < 0) {
                _attack = 0;
            }
            else _attack = value;
            EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionStatusChange, null, Owner, this);
        }
    }
    private bool _canAttack = true;
    private bool _windFuryAttack;
    public bool CanAttack {
        get => _canAttack && !Attributes.Contains(CharacterAttribute.Frozen) && BattleControl.Instance.ActivePlayer == Owner && (!NewSummoned || Attributes.Contains(CharacterAttribute.Rush) || Attributes.Contains(CharacterAttribute.Charge)) && _attack > 0;
        set {
            if (value) {
                _windFuryAttack = Attributes.Contains(CharacterAttribute.Windfury);
                _canAttack = true;
            }
            else {
                if (_windFuryAttack) {
                    _windFuryAttack = false;
                }
                else _canAttack = false;
            }
        }
    }

    public bool NewSummoned;
    public List<Buff> BuffList { get; set; }
    public int SpellDamage { get => spellDamage; set => spellDamage = value; }
    private int spellDamage;
    public HashSet<CharacterAttribute> Attributes { get; set; }
    public List<Effect> DeathRattleEffects;
    #endregion

    # region constructor
    public MinionLogic(MinionCard MC) {
        Card = MC;
        _attack = MC.CA.Attack;
        _health = MC.CA.Health;
        MaxHelath = _health;
        MinionID = IDFactory.GetID();
        BattleControl.MinionSummoned.Add(ID, this);
        if (MC is IDeathRattle) DeathRattleEffects = new((MC as IDeathRattle).DeathRattleEffects);
        DeathRattleEffects = null;
        Attributes = new(MC.CA.attributes);
        NewSummoned = true;
        if (MC is IGrantTrigger) {
            foreach (TriggerStruct t in (MC as IGrantTrigger).TriggersToGrant) {
                (this as IBuffable).AddTrigger(t); // TODO: test it
            }
        }
        else Triggers = new();
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
        EventManager.AddListener(TurnEvent.OnTurnEnd, OnTurnEndHandler);
    }

    public MinionLogic(CardAsset CA) { // TODO:
        _attack = CA.Attack;
        _health = CA.Health;
        MinionID = IDFactory.GetID();
        BattleControl.MinionSummoned.Add(ID, this);
        Attributes = new(CA.attributes);
        NewSummoned = true;
        Triggers = new();
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
        EventManager.AddListener(TurnEvent.OnTurnEnd, OnTurnEndHandler);
    }
    # endregion

    public void OnTurnStartHandler(BaseEventArgs e) {
        if (e.Player == Owner) {
            CanAttack = true;
            NewSummoned = false;
        }
    }

    public void OnTurnEndHandler(BaseEventArgs e) {
        if (e.Player == Owner) {
            RemoveAttribute(CharacterAttribute.Frozen);
        }
    }

    public void AttackAgainst(ICharacter target) {
        CanAttack = false;
        target.Health -= Attack;
        Health -= target.Attack;
        EventManager.Allocate<AttackEventArgs>().CreateEventArgs(AttackEvent.AfterAttack, null, this, ScnBattleUI.Instance.Targeting).Invoke();
    }

    public CardBase BackToHand(PlayerLogic owner = null) {
        owner ??= Owner;
        owner.Hand.GetCard(-1, Card);
        Remove();
        return Card;
    }

    public void Die() {
        Remove();
        foreach (Effect effect in DeathRattleEffects) {
            effect.ActivateEffect();
        }
        EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionDie, null, BattleControl.Instance.ActivePlayer, this).Invoke();
    }

    public void Remove() {
        (this as IBuffable).RemoveAllTriggers();
        Owner.Field.RemoveMinion(this);
    }

    public void ReadBuff() {
        if (BuffList.Count == 0) return;
        foreach (Buff b in BuffList) {
            if (b.statusChange.Count != 0) {
                foreach (var sc in b.statusChange) {
                    switch (sc.status) {
                        case Status.Attack:
                            Buff.Modify(ref _attack, sc.op, sc.Num);
                            break;
                        case Status.Health:
                            Buff.Modify(ref _health, sc.op, sc.Num);
                            break;
                        case Status.SpellDamage:
                            Buff.Modify(ref spellDamage, sc.op, sc.Num);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (b.Attributes?.Count != 0) {
                foreach (var a in b.Attributes) {
                    Attributes.Add(a);
                }
            }
        }
    }

    public void RemoveAttribute(CharacterAttribute a) {
        Attributes.Remove(a);
        foreach (var b in BuffList) {
            b.Attributes.Remove(a);
        }
    }

}
