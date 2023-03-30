using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLogic : ICharacter, IIdentifiable {
    public PlayerLogic owner;
    public CardAsset ca;
    private int baseHealth;
    public int MaxHealth {
        get { return baseHealth; }
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
    public bool CanAttack {
        get {
            return (AttacksLeftThisTurn > 0) && !isFrozen;
        }
    }
    private int attackChance = 1;
    public int AttacksLeftThisTurn {
        get;
        set;
    }
    private int baseAttack;
    // attack with buffs
    public int Attack {
        get { return baseAttack; }
        set {
            if (value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    private int _attack;
    private int MinionID;
    public int ID { get => MinionID; }
    public List<Buff> Buffs {
        get => Buffs;
        set => Buffs = value;
    }
    public List<Effect> DeathRattleEffects;
    public MinionLogic(MinionCard CL) {
        ca = CL.CA;
        baseHealth = CL.Health;
        Health = baseHealth;
        baseAttack = CL.Attack;
        attackChance = CL.CA.AttacksChances;
        if (CL.CA.isCharge)
            AttacksLeftThisTurn = attackChance;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
        DeathRattleEffects = new(CL.DeathRattleEffects);
    }
    public MinionLogic(PlayerLogic owner, MinionCard MC) {
        ca = MC.CA;
        baseHealth = MC.Health;
        Health = baseHealth;
        baseAttack = MC.Attack;
        attackChance = MC.CA.AttacksChances;
        if (MC.CA.isCharge)
            AttacksLeftThisTurn = attackChance;
        this.owner = owner;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
        DeathRattleEffects = new(MC.DeathRattleEffects);
    }
    public MinionLogic(CardAsset CA) {
        ca = CA;
        baseHealth = CA.MaxHealth;
        Health = baseHealth;
        baseAttack = CA.Attack;
        attackChance = CA.AttacksChances;
        if (CA.isCharge)
            AttacksLeftThisTurn = attackChance;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
    }
    public MinionLogic(PlayerLogic owner, CardAsset CA) {
        ca = CA;
        baseHealth = CA.MaxHealth;
        Health = baseHealth;
        baseAttack = CA.Attack;
        attackChance = CA.AttacksChances;
        if (CA.isCharge)
            AttacksLeftThisTurn = attackChance;
        this.owner = owner;
        MinionID = IDFactory.GetID();
        BattleControl.MinionCreated.Add(ID, this);
    }

    public void AttackMinion(MinionLogic target) {
        AttacksLeftThisTurn--;
        // calculate the values so that the creature does not fire the DIE command before the Attack command is sent
        target.Health -= Attack;
        Health -= target.Attack;
    }
    public void Die() {
        owner.Field.GetMinions().Remove(this);
        foreach (Effect effect in DeathRattleEffects) {
            effect.ActivateEffect();
        }
    }
}
