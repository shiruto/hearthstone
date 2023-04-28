using System.Collections.Generic;

public abstract class WeaponCard : CardBase, ITakeDamage {
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
    public int Health {
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
        Owner.Weapon?.Die();
        Owner.Weapon = this;
    }

    private void Die() {
        foreach (Effect e in DeathRattleEffects) {
            e.ActivateEffect();
        }
        EventManager.Allocate<CardEventArgs>().CreateEventArgs(CardEvent.OnWeaponDestroy, null, Owner, this);
    }

}
