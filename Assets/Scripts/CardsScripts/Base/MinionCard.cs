using System.Collections.Generic;
using UnityEngine;

public class MinionCard : CardBase {
    private int _attack;
    public int Attack {
        get => _attack;
        set {
            if (_attack + value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    private int _health;
    public int Health {
        get => _health;
        set {
            if (_health + value < 0) {
                _health = 0;
            }
            else _health = value;
        }
    }
    public bool IsTaunt { get; set; }
    public bool IsDivineShield { get; set; }
    public bool IsStealth { get; set; }
    public bool IsPoisonous { get; set; }
    public bool IsWindFury { get; set; }
    public bool IsRush { get; set; }
    public bool IsCharge { get; set; }
    protected List<Effect> _battleCryEffects;
    public List<Effect> DeathRattleEffects;

    public MinionCard(CardAsset CA) : base(CA) {
        _attack = CA.Attack;
        _health = CA.Health;
        IsTaunt = CA.isTaunt;
        IsDivineShield = CA.isDivineShield;
        IsStealth = CA.isStealth;
        IsPoisonous = CA.isPoisonous;
        IsWindFury = CA.isWindFury;
        IsRush = CA.isRush;
        IsCharge = CA.isCharge;
        _battleCryEffects = new();
        DeathRattleEffects = new();
    }

    public virtual void BattleCry() {
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
