public class BloodmageThainos : MinionCard, IDeathrattleCard {

    public BloodmageThainos(CardAsset CA) : base(CA) {

    }

    public void Deathrattle(MinionLogic caller) {
        Effect.DrawCardEffect(1, caller.Owner, caller);
    }

}