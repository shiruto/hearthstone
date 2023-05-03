using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionLogic : ICharacter {
    # region minion property
    public PlayerLogic Owner { get; set; }
    public MinionCard Card;
    public List<TriggerStruct> Triggers { get; set; }
    public bool isAlive;
    private readonly int MinionID;
    public int ID => MinionID;
    public ICharacter AttackTarget { get; set; }

    public int MaxHealth;
    private int _health;
    public virtual int Health {
        get => _health;
        set {
            if (value > MaxHealth) _health = MaxHealth;
            else if (value < _health) {
                if (Attributes.Contains(CharacterAttribute.Immune)) {

                }
                else if (!Attributes.Remove(CharacterAttribute.DivineShield)) {
                    _health = value;
                    if (value <= 0) {
                        Die();
                    }
                }
            }
            EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionStatusChange, null, Owner, this).Invoke(); // TODO: change it
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
            EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionStatusChange, null, Owner, this).Invoke();
        }
    }
    private bool _canAttack = true;
    private bool _windFuryAttack;
    public bool CanAttack {
        get => _canAttack &&
                !Attributes.Contains(CharacterAttribute.Frozen) &&
                !Attributes.Contains(CharacterAttribute.CantAttack) &&
                BattleControl.Instance.ActivePlayer == Owner &&
                HaveTarget() &&
                _attack > 0;
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
    public List<Buff> Auras { get; set; }
    public List<AuraManager> AuraToGive;
    public List<Effect> DeathRattleEffects;
    #endregion

    # region constructor
    public MinionLogic(MinionCard MC) {
        Card = MC;
        _attack = MC.CA.Attack;
        _health = MC.CA.Health;
        MaxHealth = _health;
        MinionID = IDFactory.GetID();
        BattleControl.MinionSummoned.Add(ID, this);
        if (MC is IDeathRattle) DeathRattleEffects = new((MC as IDeathRattle).DeathRattleEffects);
        DeathRattleEffects = new();
        Attributes = new(MC.CA.attributes);
        Auras = new();
        NewSummoned = true;
        Triggers = new();
        isAlive = true;
        AttackTarget = null;
        if (MC is ITriggerMinionCard) {
            foreach (TriggerStruct t in (MC as ITriggerMinionCard).TriggersToGrant) {
                (this as IBuffable).AddTrigger(t); // TODO: test it
            }
        }
        if (MC is IAuraMinionCard) {
            AuraToGive = new((MC as IAuraMinionCard).AuraToGrant);
            foreach (AuraManager a in AuraToGive) {
                BattleControl.Instance.AddAura(a);
            }
        }
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
            (this as ICharacter).RemoveAttribute(CharacterAttribute.Frozen);
        }
    }

    // TODO: force attack

    public void AttackAgainst(ICharacter target) {
        AttackTarget = target;
        EventManager.Allocate<AttackEventArgs>().CreateEventArgs(AttackEvent.BeforeAttack, null, this, AttackTarget).Invoke();
        if (!CanAttack || !isAlive) return;
        AttackTarget.TakeDamage(Attack, this);
        (this as ITakeDamage).TakeDamage(AttackTarget.Attack, target);
        (this as ICharacter).RemoveAttribute(CharacterAttribute.Stealth);
        Debug.Log($"{Card.CA.name} is Attacking {AttackTarget}");
        EventManager.Allocate<AttackEventArgs>().CreateEventArgs(AttackEvent.AfterAttack, null, this, AttackTarget).Invoke();
    }

    public CardBase BackToHand(PlayerLogic owner = null) {
        owner ??= Owner;
        owner.Hand.GetCard(-1, Card);
        Remove();
        return Card;
    }

    public void Die() {
        Remove();
        if (Attributes.Contains(CharacterAttribute.Reborn)) {
            MinionLogic Reborn = new(Card) {
                Health = 1
            };
            Reborn.Attributes.Remove(CharacterAttribute.Reborn);
            Owner.Field.SummonMinionAt(-1, Reborn); // TODO: position
        }
        if (DeathRattleEffects.Count > 0) {
            foreach (Effect effect in DeathRattleEffects) {
                effect.ActivateEffect();
            }
        }
        Debug.Log($"{Card.CA.name} die");
        EventManager.Allocate<MinionEventArgs>().CreateEventArgs(MinionEvent.AfterMinionDie, null, BattleControl.Instance.ActivePlayer, this).Invoke();
    }

    public void Remove() {
        (this as IBuffable).RemoveAllTriggers();
        Owner.Field.RemoveMinion(this);
        isAlive = false;
        RemoveAllBuffToGive();
    }

    public void RemoveAllBuffToGive() {
        if (AuraToGive != null && AuraToGive.Count != 0) {
            foreach (AuraManager a in AuraToGive) {
                BattleControl.Instance.RemoveAura(a);
            }
        }
    }

    public void ReadBuff() {
        int orgMaxHealth = MaxHealth;
        ResetStatus();
        if (BuffList.Count != 0) {
            foreach (Buff b in BuffList) {
                if (b.statusChange != null && b.statusChange.Count != 0) {
                    foreach (var sc in b.statusChange) {
                        switch (sc.status) {
                            case Status.Attack:
                                Buff.Modify(ref _attack, sc.op, sc.Num);
                                break;
                            case Status.Health:
                                Buff.Modify(ref MaxHealth, sc.op, sc.Num);
                                break;
                            case Status.SpellDamage:
                                Buff.Modify(ref spellDamage, sc.op, sc.Num);
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (b.Attributes == null || b.Attributes.Count == 0) continue;
                foreach (var a in b.Attributes) {
                    Attributes.Add(a);
                }
            }
        }
        if (Auras.Count != 0) {
            foreach (Buff b in Auras) {
                if (b.statusChange.Count != 0) {
                    foreach (var sc in b.statusChange) {
                        switch (sc.status) {
                            case Status.Attack:
                                Buff.Modify(ref _attack, sc.op, sc.Num);
                                break;
                            case Status.Health:
                                Buff.Modify(ref MaxHealth, sc.op, sc.Num);
                                break;
                            case Status.SpellDamage:
                                Buff.Modify(ref spellDamage, sc.op, sc.Num);
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (b.Attributes == null) continue;
                foreach (var a in b.Attributes) {
                    Attributes.Add(a);
                }
            }
        }
        if (MaxHealth >= orgMaxHealth) {
            _health += MaxHealth - orgMaxHealth;
        }
        else {
            if (_health > MaxHealth) _health = MaxHealth;
        }
        // Debug.Log($"{Card.CA.name} ReadBuff it's health = {_health}");
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.FieldVisualUpdate).Invoke();
    }

    private void ResetStatus() {
        _attack = Card.CA.Attack;
        MaxHealth = Card.CA.Health;
        spellDamage = Card.CA.SpellDamage;
    }

    public bool HaveTarget() {
        List<ICharacter> enemy = new(BattleControl.GetEnemy(Owner).Field.Minions) {
            BattleControl.GetEnemy(Owner)
        };
        return enemy.Where(ValidTarget).ToList().Count != 0;
    }

    public bool ValidTarget(ICharacter Target) {
        if (Target == null)
            return false;
        if (!Logic.IsEnemy(Owner, Target))
            return false;

        if (Target.Attributes != null) {
            if (Target.Attributes.Contains(CharacterAttribute.Immune))
                return false;
            if (Target.Attributes.Contains(CharacterAttribute.Stealth))
                return false;
            if (BattleControl.GetEnemy(Owner).Field.HaveTaunt && !Logic.CanTaunt(Target as MinionLogic))
                return false;
        }
        if (NewSummoned) {
            if (!Attributes.Contains(CharacterAttribute.Rush) && !Attributes.Contains(CharacterAttribute.Rush))
                return false;
            else if (!Attributes.Contains(CharacterAttribute.Rush) && Target == BattleControl.GetEnemy(Owner))
                return false;
        }
        return true;
    }

}