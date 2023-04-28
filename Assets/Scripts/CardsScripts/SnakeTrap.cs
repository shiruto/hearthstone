public class SnakeTrap : SecretCard {

    public SnakeTrap(CardAsset CA) : base(CA) {

    }

    public override void AddTrigger() {
        EventManager.AddListener(AttackEvent.BeforeAttack, Triggered);
    }

    public override void DelTrigger() {
        EventManager.DelListener(AttackEvent.BeforeAttack, Triggered);
    }

    public override void SecretImplement(BaseEventArgs e, out bool isTriggered) {
        AttackEventArgs evt = e as AttackEventArgs;
        if (evt.target is MinionLogic && (evt.target as MinionLogic).Owner == Owner) {
            isTriggered = true;
            // owner.Field.SummonMinionAt(-1, ) TODO: summon three snakes
        }
        else {
            isTriggered = false;
        }
        throw new System.NotImplementedException();
    }

}