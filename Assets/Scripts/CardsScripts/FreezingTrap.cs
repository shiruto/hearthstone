public class FreezingTrap : SecretCard {
    private readonly Buff buff;

    public FreezingTrap(CardAsset CA) : base(CA) {
        trigger = new(AttackEvent.BeforeAttack, Triggered);
        buff = new(
            "Freeze Trap",
            new() { new(Status.ManaCost, Operator.Plus, 2) }
        );
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.attacker is not MinionLogic) return false;
        ((evt.attacker as MinionLogic).BackToHand() as IBuffable).AddBuff(buff);
        return true;
    }

}