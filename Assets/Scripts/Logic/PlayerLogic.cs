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
    public List<SpellCard> Secrets;
    #endregion

    #region player property
    public GameDataAsset.ClassType HeroClass;
    public bool isEnemy;
    private int playerID;
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
            EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate));
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
            EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate));
        }
    }
    private int _armor = 0;
    public int Armor {
        get => _armor;
        set {
            if (value < 0) _armor = 0;
            else _armor = value;
            EventManager.Invoke(EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate));
        }
    }
    private bool _isStealth = false;
    public bool IsStealth { get => _isStealth; set => _isStealth = value; }
    private bool _isImmune = false;
    public bool IsImmune { get => _isImmune; set => _isImmune = value; }
    private bool _isLifeSteal = false;
    public bool IsLifeSteal { get => _isLifeSteal; set => _isLifeSteal = value; }
    private bool _isWindFury = false;
    public bool IsWindFury { get => _isWindFury; set => _isWindFury = value; }
    private bool _isFrozen;
    public bool IsFrozen { get => _isFrozen; set => _isFrozen = value; }
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

    private List<Buff> _buffs;
    public List<Buff> Buffs { get => _buffs; set => _buffs = value; }
    #endregion

    public PlayerLogic(GameDataAsset.ClassType classType) {
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
        //TODO: get skill and hero depending on its ClassType
        playerID = IDFactory.GetID();
    }

    public void Die() {
        EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnGameOver, null, this, -1, GameDataAsset.GameStatus.Lose));
    }

    public void AttackAgainst(ICharacter target) {
        CanAttack = false;
        target.Health -= Attack;
        Health -= target.Attack;
    }

    public void OnTurnStart() {
        Mana.CurCrystals++;
        Mana.ManaReset();
        Deck.DrawCards(1);
        EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnStart, null, this, TurnCount));
    }

    public void OnTurnEnd() {
        if (TurnCount >= 45) {
            EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, null, this, TurnCount, GameDataAsset.GameStatus.Tie));
        }
        TurnCount++;
        EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, null, this, TurnCount));
    }
}
