public class FreezingTrap : SecretCard {
    public FreezingTrap(CardAsset CA) : base(CA) {

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
            (evt.attacker as MinionLogic).BackToHand().BuffList.Add(new(0, 0, 2));
        }
        else isTriggered = false;
    }

}