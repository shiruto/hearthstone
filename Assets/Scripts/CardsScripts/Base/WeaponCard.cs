using System.Collections.Generic;

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
                Die();
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
        owner.Weapon?.Die();
        owner.Weapon = this;
    }

    private void Die() {
        foreach (Effect e in DeathRattleEffects) {
            e.ActivateEffect();
        }
    }

}
