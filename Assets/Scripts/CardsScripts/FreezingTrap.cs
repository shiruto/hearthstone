public class FreezingTrap : SecretCard {
    private readonly Buff buff;

    public FreezingTrap(CardAsset CA) : base(CA) {
        buff = new(
            "Freeze Trap",
            new() { new(Status.ManaCost, Operator.Plus, 2) }
        );
    }

    public override void AddTrigger() {
        EventManager.AddListener(AttackEvent.BeforeAttack, Triggered);
    }

    public override void DelTrigger() {
        EventManager.DelListener(AttackEvent.BeforeAttack, Triggered);
    }

    public override void SecretImplement(BaseEventArgs e, out bool isTriggered) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.target == Owner && evt.attacker is MinionLogic) {
            isTriggered = true;
            ((evt.attacker as MinionLogic).BackToHand() as IBuffable).AddBuff(buff);
        }
        else isTriggered = false;
    }

}