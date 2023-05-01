public class Preparation : SpellCard {
    private readonly Buff buff;
    private readonly AuraManager aura;

    public Preparation(CardAsset CA) : base(CA) {
        buff = new(
            "Preparation",
            new() { new(Status.ManaCost, Operator.Minus, 2) }
        );
        aura = new(buff, (IBuffable a) => a is SpellCard && (a as SpellCard).Owner == Owner, new(CardEvent.OnCardUse, Expire));
    }

    public override void ExtendUse() {
        base.ExtendUse();
        BattleControl.Instance.AddAura(aura);
    }

    private void Expire(BaseEventArgs e) {
        CardEventArgs evt = e as CardEventArgs;
        if (evt.Player != Owner || evt.Card is not SpellCard) return;
        BattleControl.Instance.RemoveAura(aura);
    }

}