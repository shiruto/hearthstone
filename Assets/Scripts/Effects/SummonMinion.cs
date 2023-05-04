public class SummonMinion : Effect {

    public MinionLogic MinionToSummon;
    public PlayerLogic owner;
    public new string Name = "SummonMinionEffect";
    public int position;
    // TODO: summon minion's position
    public SummonMinion(MinionCard minionCard, PlayerLogic Owner, int pos = -1) {
        MinionToSummon = new(minionCard);
        owner = Owner;
        position = pos;
    }

    public override void ActivateEffect() {
        owner.Field.SummonMinionAt(position, MinionToSummon);
    }
}
