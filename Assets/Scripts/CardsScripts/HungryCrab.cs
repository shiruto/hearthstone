using System;
using System.Collections.Generic;

public class HungryCrab : MinionCard, IBattleCry, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && (c as MinionLogic).Card.CA.MinionType == MinionType.Murloc;
    private readonly Buff buff;

    public HungryCrab(CardAsset CA) : base(CA) {
        buff = new(
            "Hungry Crab",
            new() { new(Status.Attack, Operator.Plus, 2), new(Status.Health, Operator.Plus, 2) }
        );
    }

    public void BattleCry() {
        Target?.Die();
        if (Minion == null) return;
        (Minion as IBuffable).AddBuff(buff);
    }

}