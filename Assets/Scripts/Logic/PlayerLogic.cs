using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : ICharacter {
    #region corresponding logic component
    public DeckLogic Deck;
    public HandLogic Hand;
    public ManaLogic Mana;
    public FieldLogic Field;
    public WeaponCard Weapon;
    public SkillCard Skill;
    public SecretLogic Secrets;
    public HeroCard Card;
    #endregion

    #region player property
    public ClassType HeroClass;
    public bool isEnemy;
    private readonly int playerID;
    public int ID { get => playerID; }
    private int MaxHealth = 30;
    private int _health = 30;
    public int Health {
        get => _health;
        set {
            if (value > MaxHealth) {
                _health = MaxHealth;
            }
            else if (value < _health) {
                int d = _health - value;
                Debug.Log("damage taking = " + d);
                if (Armor > 0) {
                    if (d > Armor) { // TODO: check taking damage with some armor
                        _health -= d - Armor;
                        Armor = 0;
                    }
                    else Armor -= d;
                }
                else {
                    _health = value;
                    Debug.Log("no Armor");
                }
                if (_health <= 0) Die();
            }
            else {
                Debug.Log("recover");
                _health = value;
            }
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate).Invoke();
        }
    }
    private int _attack = 0;
    public int Attack {
        get {
            if (BattleControl.Instance.ActivePlayer == this && Weapon != null) {
                return _attack + Weapon.Attack;
            }
            else return _attack;
        }
        set {
            if (_attack + value < 0) {
                _attack = 0;
            }
            else _attack = value;
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate).Invoke();
        }
    }
    private int _armor = 0;
    public int Armor {
        get => _armor;
        set {
            if (value < 0) _armor = 0;
            else _armor = value;
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate).Invoke();
        }
    }
    public bool IsStealth { get; set; }
    public bool IsImmune { get; set; }
    public bool IsLifeSteal { get; set; }
    public bool IsWindFury { get; set; }
    public bool IsFrozen { get; set; }
    public int TurnCount;
    private bool _canAttack;
    private bool _windFuryAttack;
    public bool CanAttack {
        get => _canAttack && !IsFrozen && BattleControl.Instance.ActivePlayer == this;
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

    public List<Buff> BuffList { get; set; }
    #endregion

    public PlayerLogic(ClassType classType) {
        HeroClass = classType;
        Deck = new() {
            owner = this
        };
        Hand = new() {
            owner = this
        };
        Field = new() {
            owner = this
        };
        Mana = new() {
            owner = this
        };
        Weapon = null;
        Secrets = new();
        //TODO: get skill and hero depending on its ClassType
        playerID = IDFactory.GetID();
    }

    public void ChangeMaxHealth(int newMaxHealth) {
        MaxHealth = newMaxHealth;
    }

    public void Die() {
        EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnGameOver, null, this, -1, GameStatus.Lose).Invoke();
    }

    public void AttackAgainst(ICharacter target) {
        CanAttack = false;
        target.Health -= Attack;
        Health -= target.Attack;
    }

    public void OnTurnStart() {
        EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnStart, null, this, TurnCount).Invoke();
    }

    public void OnTurnEnd() {
        if (TurnCount >= 45) {
            EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, null, this, TurnCount, GameStatus.Tie).Invoke();
        }
        TurnCount++;
        EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, null, this, TurnCount).Invoke();
    }
}
