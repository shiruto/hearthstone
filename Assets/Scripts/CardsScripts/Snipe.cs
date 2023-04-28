using System.Linq;

public class Snipe : SecretCard, IDealDamage {
    public int Damage => 4;

    public Snipe(CardAsset CA) : base(CA) {

    }

    public override void AddTrigger() {
        EventManager.AddListener(MinionEvent.AfterMinionSummon, Triggered);
    }

    public override void DelTrigger() {
        EventManager.DelListener(MinionEvent.AfterMinionSummon, Triggered);
    }

    public override void SecretImplement(BaseEventArgs e, out bool isTriggered) {
        MinionEventArgs evt = e as MinionEventArgs;
        new DealDamageToTarget(false, Damage, this, evt.minion, true).ActivateEffect();
        isTriggered = true;
    }

}