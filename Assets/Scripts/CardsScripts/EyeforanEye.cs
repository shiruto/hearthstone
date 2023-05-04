public class EyeforanEye : SecretCard {

    public EyeforanEye(CardAsset CA) : base(CA) {
        trigger = new(DamageEvent.TakeDamage, Triggered);
    }

    public override bool SecretImplementation(BaseEventArgs e) {
        DamageEventArgs evt = e as DamageEventArgs;
        if (evt.taker != Owner) return false;
        new DealDamageToTarget(evt.Damage, this, BattleControl.GetEnemy(Owner), false, true).ActivateEffect();
        return true;
    }

}