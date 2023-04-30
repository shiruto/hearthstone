public class Shadowstep : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    public Buff buff;

    public Shadowstep(CardAsset CA) : base(CA) {
        buff = new(
            "Shadowstep",
            new() { new(Status.ManaCost, Operator.Minus, 2) }
        );
    }

    public bool CanBeTarget(ICharacter Character) {
        return (Character is MinionLogic) && (Character as MinionLogic).Owner == Owner;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        ((Target as MinionLogic).BackToHand() as IBuffable).AddBuff(buff);
    }

}