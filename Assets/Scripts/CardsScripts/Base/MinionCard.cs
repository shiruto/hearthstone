using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinionCard : CardBase {
    public int Attack {
        get => _attack;
        set {
            if (_attack + value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    private int _attack;
    public int Health {
        get => _health;
        set {
            if (_health + value < 0) {
                _health = 0;
            }
            else _health = value;
        }
    }
    private int _health;
    public bool IsTaunt { get; set; }
    public bool IsDivineShield { get; set; }
    public bool IsStealth { get; set; }
    public bool IsPoisonous { get; set; }
    public bool IsWindFury { get; set; }
    public bool IsRush { get; set; }
    public bool IsCharge { get; set; }
    protected List<Effect> _battleCryEffects;
    protected bool TargetedBattleCry;
    public List<Effect> DeathRattleEffects;
    public MinionCard(CardAsset CA) : base(CA) {
        _attack = CA.Attack;
        _health = CA.MaxHealth;
        IsTaunt = CA.isTaunt;
        IsDivineShield = CA.isDivineShield;
        IsStealth = CA.isStealth;
        IsPoisonous = CA.isPoisonous;
        IsWindFury = CA.AttacksChances == 2;
        IsRush = CA.isRush;
        IsCharge = CA.isCharge;
        _battleCryEffects = new();
        DeathRattleEffects = new();
    }
    public virtual void BattleCry() {
        if (TargetedBattleCry) {
            // TODO: Select a Target
        }
        foreach (Effect effect in _battleCryEffects) {
            Debug.Log(effect.Name);
            effect.ActivateEffect();
        }
    }
    public override void Use() {
        BattleControl.Instance.ActivePlayer.Field.SummonMinionAt(0, new(this));
        BattleCry();
    }
}
