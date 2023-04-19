using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponCard : CardBase {
    private int _attack;
    public int Attack {
        get => _attack;
        set {
            if (value < 0) {
                _attack = 0;
            }
            else _attack = value;
        }
    }
    public int Durability {
        get => _durability;
        set {
            if (value < 0) {
                _durability = value;
            }
            else _durability = value;
        }
    }
    private int _durability;
    public List<Effect> BattleCryEffects;
    public List<Effect> DeathRattleEffects;
    public bool IsPoisonous { get; set; }
    public bool IsWindFury { get; set; }

    public WeaponCard(CardAsset CA) : base(CA) {
        IsPoisonous = CA.isPoisonous;
        IsWindFury = CA.isWindFury;
    }

    public override void Use() {
        owner.Weapon = this;
    }

}
