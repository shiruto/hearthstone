using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLogic : ICharacter, IIdentifiable {
    public PlayerLogic owner;
    public CardAsset ca;
    private int baseHealth;
    public int MaxHealth {
        get {
            int temp = 0;
            foreach (Buff _buff in Buffs) {
                temp += _buff.HealthChange;
            }
            return baseHealth + temp;
        }
    }
    private int _health;
    public virtual int Health {
        get => _health;
        set {
            if (value > baseHealth) _health = baseHealth;
            else if (value <= 0) {
                _health = value;
                Die();
            }
            else _health = value;
        }
    }
    public bool isFrozen;
    public bool isTaunt;
    private bool _canAttack;
    private bool _windFuryAttack;
    public bool isRush;
    public bool isImmune;
    public bool isStealth;
    public bool isCharge;
    public bool NewSummoned;
    public bool CanAttack {
        get => _canAttack && !isFrozen;
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
    private int _attack;
    public int Attack {
        get => _attack;
        set {
            if (value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    private int MinionID;
    public int ID { get => MinionID; }
    public List<Buff> Buffs {
        get => Buffs;
        set => Buffs = value;
    }
    private bool _isStealth;
    public bool IsStealth { get => _isStealth; set => _isStealth = value; }
    private bool _isImmune;
    public bool IsImmune { get => _isImmune; set => _isImmune = value; }
    private bool _isLifeSteal;
    public bool IsLifeSteal { get => _isLifeSteal; set => _isLifeSteal = value; }
    private bool _isWindFury;
    public bool IsWindFury { get => _isWindFury; set => _isWindFury = value; }

    public List<Effect> DeathRattleEffects;
    public MinionLogic(MinionCard MC) {
        ca = MC.CA;
        baseHealth = MC.Health;
        Health = baseHealth;
        _attack = MC.Attack;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
        DeathRattleEffects = new(MC.DeathRattleEffects);
        isTaunt = MC.IsTaunt;
        NewSummoned = true;
        isRush = MC.IsRush;
        isCharge = MC.IsCharge;
    }
    public MinionLogic(PlayerLogic owner, MinionCard MC) {
        ca = MC.CA;
        baseHealth = MC.Health;
        Health = baseHealth;
        _attack = MC.Attack;
        IsWindFury = MC.CA.isWindFury;
        this.owner = owner;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
        DeathRattleEffects = new(MC.DeathRattleEffects);
        NewSummoned = true;
        isRush = MC.IsRush;
        isCharge = MC.IsCharge;
    }
    public MinionLogic(CardAsset CA) {
        ca = CA;
        baseHealth = CA.MaxHealth;
        Health = baseHealth;
        _attack = CA.Attack;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
        NewSummoned = true;
        isRush = CA.isRush;
        isCharge = CA.isCharge;
    }
    public MinionLogic(PlayerLogic owner, CardAsset CA) {
        ca = CA;
        baseHealth = CA.MaxHealth;
        Health = baseHealth;
        _attack = CA.Attack;
        this.owner = owner;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
        NewSummoned = true;
        isRush = CA.isRush;
        isCharge = CA.isCharge;
    }
    public void NewTurn() {
        CanAttack = true;
        NewSummoned = false;
    }
    public void EndTurn() {
        if (isFrozen) {
            isFrozen = _canAttack;
        }
    }
    public virtual void AttackAgainst(ICharacter target) {
        CanAttack = false;
        target.Health -= Attack;
        Health -= target.Attack;
    }
    public void Die() {
        owner.Field.GetMinions().Remove(this);
        foreach (Effect effect in DeathRattleEffects) {
            effect.ActivateEffect();
        }
        BattleControl.MinionCreated.Remove(MinionID);
    }
}
