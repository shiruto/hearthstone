using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : CardBase {
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
    public int Duration {
        get => _duration;
        set {
            if (_duration + value < 0) {
                _duration = 0;
            }
            else _duration = value;
        }
    }
    private int _duration;
    public List<Effect> BattleCryEffects;
    public List<Effect> DeathRattleEffects;
    public bool IsPoisonous { get; set; }
    public bool IsWindFury { get; set; }
    public WeaponCard(CardAsset CA) : base(CA) {
        IsPoisonous = CA.isPoisonous;
        IsWindFury = CA.isWindFury;
    }

    public override void Use() {
        owner.Weapon = new(CA);
    }
}
