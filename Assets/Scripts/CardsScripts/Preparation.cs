public class Preparation : SpellCard {
    private readonly Buff buff;

    public Preparation(CardAsset CA) : base(CA) {
        buff = new(
            "Preparation",
            new() { new(Status.ManaCost, Operator.Minus, 2) }
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
    }
}