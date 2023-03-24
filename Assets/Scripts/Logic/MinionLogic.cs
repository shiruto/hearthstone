using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLogic : ICharacter, IIdentifiable {
    private PlayerLogic owner;
    private CardAsset ca;
    private int baseHealth;
    private bool canAttackHero = false;
    public int MaxHealth {
        get { return baseHealth; }
    }
    private int _health;
    public int Health {
        get => _health;
        set {
            if (value > baseHealth) _health = baseHealth;
            else if (_health <= 0) Die();
            else _health = value;
        }
    }
    public bool isFrozen;
    public bool CanAttack {
        get {
            bool ownersTurn = TurnManager.Instance().whoseTurn == owner;
            return ownersTurn && (AttacksLeftThisTurn > 0) && !isFrozen;
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

    }
    private int MinionID;
    public int ID {
        get {
            return MinionID;
        }
    }

    public MinionLogic(PlayerLogic owner, CardAsset ca) {
        this.ca = ca;
        baseHealth = ca.MaxHealth;
        Health = ca.MaxHealth;
        baseAttack = ca.Attack;
        attackChance = ca.AttacksChances;
        if (ca.isCharge)
            AttacksLeftThisTurn = attackChance;
        this.owner = owner;
        MinionID = IDFactory.GetID();
        // MinionCreatedThisGame.Add(ID, this);
    }

    public void AttackMinion(MinionLogic target) {
        AttacksLeftThisTurn--;
        // calculate the values so that the creature does not fire the DIE command before the Attack command is sent
        int targetHealthAfter = target.Health - Attack;
        int attackerHealthAfter = Health - target.Attack;
        new MinionAttackMessage(target.ID, ID, target.Attack, Attack, attackerHealthAfter, targetHealthAfter).AddToQueue();

        target.Health -= Attack;
        Health -= target.Attack;
    }
    public void Die() {
        owner.
    }
}
