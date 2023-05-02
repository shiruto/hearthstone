using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : ICharacter {
    #region corresponding logic component
    public DeckLogic Deck;
    public HandLogic Hand;
    public ManaLogic Mana;
    public FieldLogic Field;
    public WeaponLogic Weapon;
    public SkillLogic Skill;
    public SecretLogic Secrets;
    public HeroCard Card;
    #endregion

    #region player property
    public ClassType HeroClass;
    public bool isEnemy;
    private readonly int playerID;
    public int ID => playerID;
    private int MaxHealth = 30;
    private int _health = 30;
    public int Health {
        get => _health;
        set {
            if (value > MaxHealth) {
                _health = MaxHealth;
            }
            else if (value < _health) {
                if (DivineShield) {
                    (this as ICharacter).RemoveAttribute(CharacterAttribute.DivineShield);
                    return;
                }
                int d = _health - value;
                Debug.Log("you? " + (this == BattleControl.you) + " damage taking = " + d);
                if (Armor > 0) {
                    if (d > Armor) { // TODO: Debug taking damage with some armor
                        _health -= d - Armor;
                        Armor = 0;
                    }
                    else Armor -= d;
                }
                else {
                    _health = value;
                }
                if (_health <= 0) Die();
            }
            else {
                if (value > _health) {
                    //TODO: recover trigger
                    Debug.Log("recover");
                }
                _health = value;
            }
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate).Invoke();
        }
    }
    private int _attack = 0;
    public int Attack {
        get {
            if (!Weapon.isClosed) {
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
    private int spellDamage;
    public int SpellDamage { get => spellDamage; set => spellDamage = value; }
    public HashSet<CharacterAttribute> Attributes { get; set; }
    public bool IsStealth => Attributes.Contains(CharacterAttribute.DivineShield);
    public bool DivineShield => Attributes.Contains(CharacterAttribute.DivineShield);
    public bool IsElusive => Attributes.Contains(CharacterAttribute.Elusive);
    public bool IsFrozen => Attributes.Contains(CharacterAttribute.DivineShield);
    public bool IsImmune => Attributes.Contains(CharacterAttribute.Immune);
    public bool IsWindfury => Attributes.Contains(CharacterAttribute.Windfury);
    public List<TriggerStruct> Triggers { get; set; }
    public int TurnCount;
    private bool _canAttack = true;
    private bool _windFuryAttack;
    public bool CanAttack {
        get => _canAttack && !Attributes.Contains(CharacterAttribute.Frozen) && BattleControl.Instance.ActivePlayer == this && Attack > 0;
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
    public List<Buff> BuffList { get; set; }
    public List<Buff> Auras { get; set; }
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
        Weapon = new() {
            Owner = this
        };
        Skill = new(classType) {
            Owner = this
        };
        Secrets = new();
        Attributes = new();
        //TODO: get skill and hero depending on its ClassType
        playerID = IDFactory.GetID();
    }

    public void Die() {
        EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnGameOver, null, this, -1, GameStatus.Lose).Invoke();
    }

    public void AttackAgainst(ICharacter target) { // TODO: attack func that won't consume attck chance
        if (Weapon.Card != null) {
            Weapon.AttackAgainst(target);
        }
        else {
            CanAttack = false;
            target.Health -= Attack;
            Health -= target.Attack;
        }
    }

    public void OnTurnStart() {
        EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnStart, null, this, TurnCount).Invoke();
        CanAttack = true;
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate).Invoke();
    }

    public void OnTurnEnd() {
        TurnCount++;
        if (TurnCount >= 45) {
            EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, null, this, TurnCount, GameStatus.Tie).Invoke();
        }
        if (_canAttack) (this as ICharacter).RemoveAttribute(CharacterAttribute.Frozen);
        EventManager.Allocate<TurnEventArgs>().CreateEventArgs(TurnEvent.OnTurnEnd, null, this, TurnCount).Invoke();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate).Invoke();
    }

    public void ReadBuff() {
        _attack = 0;
        SpellDamage = 0;
        MaxHealth = 30;
        if (BuffList.Count != 0) {
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
                if (b.Attributes?.Count == 0) continue;
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
                if (b.Attributes?.Count == 0) continue;
                foreach (var a in b.Attributes) {
                    Attributes.Add(a);
                }
            }
        }
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.PlayerVisualUpdate);
    }

}
