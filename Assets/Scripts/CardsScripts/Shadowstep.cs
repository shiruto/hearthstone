using System;

public class Shadowstep : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && (c as MinionLogic).Owner == Owner;
    public Buff buff;

    public Shadowstep(CardAsset CA) : base(CA) {
        buff = new(
            "Shadowstep",
            new() { new(Status.ManaCost, Operator.Minus, 2) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        ((Target as MinionLogic).BackToHand() as IBuffable).AddBuff(buff);
    }

}