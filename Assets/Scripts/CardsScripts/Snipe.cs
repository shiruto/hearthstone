using System.Linq;

public class Snipe : SecretCard, IDealDamage {
    public int Damage => 4;

    public Snipe(CardAsset CA) : base(CA) {
        trigger = new(MinionEvent.AfterMinionSummon, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        MinionEventArgs evt = e as MinionEventArgs;
        if (evt.minion.Owner == Owner) return false;
        new DealDamageToTarget(false, Damage, this, evt.minion, true).ActivateEffect();
        return true;
    }

}