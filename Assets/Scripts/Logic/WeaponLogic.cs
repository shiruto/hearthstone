using System.Collections.Generic;

public class WeaponLogic : IBuffable, ITakeDamage {
    public PlayerLogic Owner;
    public List<Effect> DeathRattleEffects;
    public List<Buff> BuffList { get; set; }
    public List<TriggerStruct> Triggers { get; set; }
    public WeaponCard Card;
    public bool isClosed = false;
    private int _health;
    public virtual int Health {
        get => _health;
        set {
            if (value <= 0) {
                _health = value;
                Die();
            }
            else _health = value;
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate).Invoke();
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
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate).Invoke();
        }
    }
    public int SpellDamage { get => spellDamage; set => spellDamage = value; }
    private int spellDamage;
    public HashSet<CharacterAttribute> Attributes { get; set; }
    public List<Buff> Auras { get; set; }

    public WeaponLogic() {
        Card = null;
        DeathRattleEffects = new();
        EventManager.AddListener(TurnEvent.OnTurnStart, OnTurnStartHandler);
        EventManager.AddListener(TurnEvent.OnTurnEnd, OnTurnEndHandler);
    }

    public void EquipWeapon(WeaponCard WC) {
        if (Card != null) Die();
        Card = WC;
        _attack = Card.Attack;
        _health = Card.Health;
        if (WC is IDeathRattle) {
            DeathRattleEffects = new((WC as IDeathRattle).DeathRattleEffects);
        }
        else DeathRattleEffects.Clear();
        if (WC is ITriggerMinionCard) {
            foreach (TriggerStruct t in (WC as ITriggerMinionCard).TriggersToGrant) {
                (this as IBuffable).AddTrigger(t); // TODO: test it
            }
        }
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnWeaponEquip, null, Owner, WC).Invoke();
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate).Invoke();
    }

    public void AttackAgainst(ICharacter Target) {
        Card.AttackEffect(Target);
    }

    private void OnTurnStartHandler(BaseEventArgs e) {
        if (e.Player == Owner) {
            isClosed = false;
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate).Invoke();
        }
    }

    private void OnTurnEndHandler(BaseEventArgs e) {
        if (e.Player == Owner) {
            isClosed = true;
            EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate).Invoke();
        }
    }

    public void Die() {
        _attack = 0;
        _health = 0;
        if (DeathRattleEffects.Count != 0) {
            foreach (Effect e in DeathRattleEffects) {
                e.ActivateEffect();
            }
        }
        if (Triggers != null && Triggers.Count != 0) {
            foreach (var item in Triggers) {
                EventManager.DelListener(item.eventType, item.callback);
            }
        }
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnWeaponDestroy, null, Owner, Card);
        Card = null;
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate).Invoke();
    }

    public void ReadBuff() {
        _attack = Card.CA.Attack;
        _health = Card.CA.Health;
        spellDamage = Card.CA.SpellDamage;
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
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.WeaponVisualUpdate);
    }

}