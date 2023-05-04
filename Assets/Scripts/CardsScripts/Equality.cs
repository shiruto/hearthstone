using System.Collections.Generic;

public class Equality : SpellCard {
    private readonly Buff buff;

    public Equality(CardAsset CA) : base(CA) {
        buff = new(
            "Equality",
            new() { new(Status.Health, Operator.equal, 1) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        List<MinionLogic> BuffTarget = BattleControl.GetAllMinions();
        foreach (MinionLogic m in BuffTarget) {
            (m as IBuffable).AddBuff(buff);
        }
    }

}