using System;
using System.Collections.Generic;

public class MinionLogic : IBuffable, ICharacter {
    # region minion property
    public PlayerLogic Owner { get; set; }
    public MinionCard Card;
    public List<TriggerStruct> Triggers;

    private readonly int MinionID;
    public int ID => MinionID;

    private int _health;
    public virtual int Health {
        get => _health;
        set {
            if (value > Card.CA.Health) _health = Card.CA.Health;
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
        get => _canAttack && !IsFrozen && BattleControl.Instance.ActivePlayer == Owner && (!NewSummoned || IsRush || IsCharge);
        set {
            if (value) {
                _windFuryAttack = IsWindFury;
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
    public bool IsStealth { get; set; }
    public bool IsImmune { get; set; }
    public bool IsLifeSteal { get; set; }
    public bool IsWindFury { get; set; }
    public bool IsFrozen { get; set; }
    public bool IsRush;
    public bool IsCharge;
    public bool IsTaunt;
    public List<Effect> DeathRattleEffects;
    #endregion

    # region constructor
    public MinionLogic(MinionCard MC) {
        Card = MC;
        _attack = MC.CA.Attack;
        _health = MC.CA.Health;
        MinionID = IDFactory.GetID();
        BattleControl.MinionSummoned.Add(ID, this);
        if (MC is IDeathRattle) DeathRattleEffects = new((MC as IDeathRattle).DeathRattleEffects);
        DeathRattleEffects = null;
        IsStealth = Card.CA.isStealth;
        IsImmune = Card.CA.isImmune;
        IsLifeSteal = Card.CA.isLifeSteal;
        IsWindFury = Card.CA.isWindFury;
        IsRush = Card.CA.isRush;
        IsCharge = Card.CA.isCharge;
        IsTaunt = Card.CA.isTaunt;
        IsFrozen = false;
        NewSummoned = true;
        if (MC is ITriggerMinionCard) {
            Triggers = new((MC as ITriggerMinionCard).Triggers);
            InitTrigger();
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
        IsStealth = CA.isStealth;
        IsImmune = CA.isImmune;
        IsLifeSteal = CA.isLifeSteal;
        IsWindFury = CA.isWindFury;
        IsRush = CA.isRush;
        IsCharge = CA.isCharge;
        IsTaunt = CA.isTaunt;
        IsFrozen = false;
        NewSummoned = true;
        Triggers = new();
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
        EventManager.AddListener(TurnEvent.OnTurnEnd, OnTurnEndHandler);
    }
    # endregion

    public void InitTrigger() {
        foreach (var item in Triggers) {
            EventManager.AddListener(item.eventType, item.callback);
        }
    }

    public void AddTrigger(TriggerStruct trigger) {
        Triggers.Add(trigger);
        EventManager.AddListener(trigger.eventType, trigger.callback);
    }

    public void RemoveTrigger(TriggerStruct trigger) {
        Triggers.Remove(trigger);
        EventManager.DelListener(trigger.eventType, trigger.callback);
    }

    public void OnTurnStartHandler(BaseEventArgs e) {
        if (e.Player == Owner) {
            CanAttack = true;
            NewSummoned = false;
        }
    }

    public void OnTurnEndHandler(BaseEventArgs e) {
        if (e.Player == Owner) {
            if (IsFrozen) {
                IsFrozen = _canAttack;
            }
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
        return Card;
    }

    public void Die() {
        Owner.Field.RemoveMinion(this);
        foreach (Effect effect in DeathRattleEffects) {
            effect.ActivateEffect();
        }
        foreach (var item in Triggers) {
            EventManager.DelListener(item.eventType, item.callback);
        }
        BattleControl.MinionSummoned.Remove(MinionID); // record the sequence of summon order
        EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionDie, null, BattleControl.Instance.ActivePlayer, this).Invoke();
    }

}
