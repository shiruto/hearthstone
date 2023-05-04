using System.Collections.Generic;

public class LeaderofthePack : SpellCard {
    private readonly Buff buff;

    public LeaderofthePack(CardAsset CA) : base(CA) {
        buff = new(
            "Leader of the Pack",
            new() { new(Status.Attack, Operator.Plus, 1), new(Status.Health, Operator.Plus, 1) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        List<MinionLogic> BuffTargets = Owner.Field.GetMinions();
        if (BuffTargets.Count == 0) return;
        foreach (MinionLogic m in BuffTargets) {
            (m as IBuffable).AddBuff(buff);
        }
    }

}