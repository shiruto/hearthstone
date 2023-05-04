public class LootHoarder : MinionCard, IDeathrattleCard {

    public LootHoarder(CardAsset CA) : base(CA) {

    }

    public void Deathrattle(MinionLogic caller) {
        Effect.DrawCardEffect(1, caller.Owner, caller);
    }

}