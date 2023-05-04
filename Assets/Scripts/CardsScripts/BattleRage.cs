public class BattleRage : SpellCard {

    public BattleRage(CardAsset CA) : base(CA) {

    }

    public override void ExtendUse() {
        base.ExtendUse();
        Effect.DrawCardEffect(BattleControl.GetAllCharacters().FindAll((ICharacter c) => Logic.IsEnemy(c, Owner) && (c.Health < c.MaxHealth)).Count, Owner, this);
    }

}