public class Shapeshift : SkillCard {
    private readonly Buff buff;

    public Shapeshift(CardAsset CA) : base(CA) {
        buff = new(
            "DemonClaws",
            new() { new(Status.Attack, Operator.Plus, 1) },
            new() { new(TurnEvent.OnTurnEnd, (BaseEventArgs e) => {
                if(e.Player == Owner) (Owner as IBuffable).RemoveBuff(buff);
            })}
        );
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(buff, this, Owner).ActivateEffect();
        Owner.Armor++;
    }

}