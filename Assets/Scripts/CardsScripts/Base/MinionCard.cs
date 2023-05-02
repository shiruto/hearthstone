using UnityEngine;

public class MinionCard : CardBase {
    public MinionLogic Minion;
    public int Health;
    public int Attack;

    public MinionCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        Minion = new(this) {
            Owner = Owner
        };
        Debug.Log($"summon a minion from card ID = {ID}");
        if (BuffList != null) Minion.BuffList = new(BuffList);
        else Minion.BuffList = new();
        if (this is IBattleCry) (this as IBattleCry).BattleCry();
        Owner.Field.SummonMinionAt(0, Minion); // TODO: choose position
        if (this is IAuraMinionCard) {
            Minion.AuraToGive = new((this as IAuraMinionCard).AuraToGrant);
            foreach (AuraManager a in Minion.AuraToGive) {
                BattleControl.Instance.AddAura(a);
            }
        }
    }

    public override void ReadBuff() {
        base.ReadBuff();
        Attack = CA.Attack;
        Health = CA.Health;
        if (BuffList.Count != 0) {
            foreach (Buff b in BuffList) {
                if (b.statusChange.Count == 0) break;
                foreach (var sc in b.statusChange) {
                    switch (sc.status) {
                        case Status.Attack:
                            Buff.Modify(ref Attack, sc.op, sc.Num);
                            break;
                        case Status.Health:
                            Buff.Modify(ref Health, sc.op, sc.Num);
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
                            Buff.Modify(ref Attack, sc.op, sc.Num);
                            break;
                        case Status.Health:
                            Buff.Modify(ref Health, sc.op, sc.Num);
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
