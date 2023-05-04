public class Preparation : SpellCard {
    private readonly Buff buff;
    private readonly AuraManager aura;

    public Preparation(CardAsset CA) : base(CA) {
        buff = new(
            "Preparation",
            new() { new(Status.ManaCost, Operator.Minus, 2) }
        );
        aura = new(
            buff,
            (IBuffable a) => a is SpellCard && (a as SpellCard).Owner == Owner,
            CardEvent.OnCardUse,
            (BaseEventArgs e) => e.Player == Owner && (e as CardEventArgs).Card is SpellCard
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        BattleControl.Instance.AddAura(aura);
    }

}