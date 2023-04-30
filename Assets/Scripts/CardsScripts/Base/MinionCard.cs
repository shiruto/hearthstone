public class MinionCard : CardBase {
    public MinionLogic Minion = null;
    public int Health;
    public int Attack;

    public MinionCard(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        MinionLogic minionToSummon = new(this);
        if (this is IBattleCry) (this as IBattleCry).BattleCry();
        minionToSummon.Owner = Owner;
        minionToSummon.BuffList = new(BuffList);
        Owner.Field.SummonMinionAt(0, minionToSummon); // TODO: choose position
        Minion = minionToSummon;
    }

    public override void ReadBuff() {
        base.ReadBuff();
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

}
