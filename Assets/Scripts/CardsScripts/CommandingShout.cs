public class CommandingShout : SpellCard {
    private AuraManager aura;
    private readonly Buff AuraBuff;

    public CommandingShout(CardAsset CA) : base(CA) {
        AuraBuff = new(
             "Commanding Shout",
                null,
                new() { new(DamageEvent.BeforeTakeDamage, Triggered) }
        );
        aura = new(
            AuraBuff,
            (IBuffable c) => c is MinionLogic && !Logic.IsEnemy(c as MinionLogic, Owner),
            TurnEvent.OnTurnEnd,
            (BaseEventArgs e) => e.Player == Owner
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        BattleControl.Instance.AddAura(aura);
    }

    private void Triggered(BaseEventArgs e) {
        DamageEventArgs evt = e as DamageEventArgs;
        if (evt.taker.Health <= evt.Damage) evt.Damage = evt.taker.Health - 1;
    }

}