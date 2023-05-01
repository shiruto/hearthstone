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
    public int Health {
        get => _durability;
        set {
            if (value < 0) {
                _durability = 0;
            }
            else _durability = value;
        }
    }
    private int _durability;

    public WeaponCard(CardAsset CA) : base(CA) {
        _attack = CA.Attack;
        _durability = CA.Health;
    }

    public override void ExtendUse() {
        Owner.Weapon.EquipWeapon(this);
    }

    public override void ReadBuff() {
        base.ReadBuff();
        _attack = CA.Attack;
        _durability = CA.Health;
        if (BuffList.Count != 0) {
            foreach (Buff b in BuffList) {
                if (b.statusChange.Count == 0) break;
                foreach (var sc in b.statusChange) {
                    switch (sc.status) {
                        case Status.Attack:
                            Buff.Modify(ref _attack, sc.op, sc.Num);
                            break;
                        case Status.Health:
                            Buff.Modify(ref _durability, sc.op, sc.Num);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        if (Auras.Count != 0) {
            foreach (Buff b in Auras) {
                if (b.statusChange.Count == 0) break;
                foreach (StatusChange sc in b.statusChange) {
                    switch (sc.status) {
                        case Status.Attack:
                            Buff.Modify(ref _attack, sc.op, sc.Num);
                            break;
                        case Status.Health:
                            Buff.Modify(ref _durability, sc.op, sc.Num);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        EventManager.Allocate<EmptyParaArgs>().CreateEventArgs(EmptyParaEvent.HandVisualUpdate);
    }

}
