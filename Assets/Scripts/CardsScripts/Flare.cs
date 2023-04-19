public class Flare : SpellCard {

    public Flare(CardAsset CA) : base(CA) {

    }

    public override void Use() {
        foreach (MinionLogic minion in BattleControl.opponent.Field.GetMinions()) {
            minion.isStealth = false;
        }
        BattleControl.opponent.IsStealth = false;
    }

}