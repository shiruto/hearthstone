public class MadBomber : MinionCard, IBattlecryCard {

    public MadBomber(CardAsset CA) : base(CA) {

    }

    public void BattleCry() {
        for (int i = 0; i < 3; i++) {
            Effect.DealDamage(1, Effect.GetRandomObject(BattleControl.GetAllCharacters(), (ICharacter c) => (c as MinionLogic) == Minion || c.Health <= 0), Minion);
        }
    }

}