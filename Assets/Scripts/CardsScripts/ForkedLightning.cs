public class ForkedLighting : SpellCard, IDealDamage {
    public int Damage => 2;

    public ForkedLighting(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        foreach (MinionLogic m in Effect.GetMultiRandomObject(BattleControl.GetAllMinions(), 2, (MinionLogic m) => m.Owner == Owner)) {
            new DealDamageToTarget(Damage, this, m, false, true).ActivateEffect();
        }
    }

}