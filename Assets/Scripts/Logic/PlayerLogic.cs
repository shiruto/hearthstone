using System.Collections;
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
    #endregion

    #region player property
    public bool isEnemy;
    private int playerID;
    public int ID { get => playerID; }
    private int MaxHealth;
    private int _health = 30;
    public int Health {
        get => _health;
        set {
            if (value > MaxHealth) {
                _health = MaxHealth;
            }
            else if (value < _health) {
                int d = _health - value;
                if (d > Armor) {
                    _health -= d - Armor;
                    Armor = 0;
                }
                else Armor -= d;
                if (_health < 0) Die();
            }
            else _health = value;
        }
    }
    private int _attack = 0;
    public int Attack {
        get {
            if (BattleControl.Instance.ActivePlayer == this) {
                return _attack + Weapon.Attack;
            }
            else return _attack;
        }
        set {
            if (_attack + value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    private int _armor;
    public int Armor {
        get => _armor;
        set {
            if (value < 0) _armor = 0;
            else _armor = value;
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
    public int TurnCount;

    private List<Buff> _buffs;
    public List<Buff> Buffs { get => _buffs; set => _buffs = value; }
    #endregion

    public PlayerLogic(GameDataAsset.ClassType classType) {
        Deck = new();
        Hand = new();
        Field = new();
        Mana = new();
        //TODO: get skill and hero depending on its ClassType
        playerID = IDFactory.GetID();
    }

    public void Die() {
        EventManager.Invoke(EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnGameOver, null, -1, true));
    }
}
