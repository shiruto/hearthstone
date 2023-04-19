public class SummonMinion : Effect {

    public MinionLogic MinionToSummon;
    public PlayerLogic owner = BattleControl.you;
    public new string Name = "SummonMinionEffect";
    public int position;
    // TODO: summon minion's position
    public SummonMinion(MinionCard minionCard) {
        MinionToSummon = new(minionCard);
    }

    public SummonMinion(PlayerLogic owner, MinionCard minionCard) {
        this.owner = owner;
        MinionToSummon = new(minionCard);
    }

    public SummonMinion(CardAsset CA) {
        MinionToSummon = new(CA);
    }

    public SummonMinion(PlayerLogic owner, CardAsset CA) {
        MinionToSummon = new(CA);
        this.owner = owner;
    }

    public override void ActivateEffect() {
        owner.Field.SummonMinionAt(position, MinionToSummon);
    }
}
