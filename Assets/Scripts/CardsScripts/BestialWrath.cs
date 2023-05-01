public class BestialWrath : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    private readonly Buff buff;

    public BestialWrath(CardAsset CA) : base(CA) {
        buff = new(
            "BestialWrath",
            new() { new(Status.Attack, Operator.Plus, 2) },
            new() { new(TurnEvent.OnTurnEnd, (BaseEventArgs e) => {
                if(e.Player == Owner) Target.RemoveBuff(buff);
            }) },
            new() { CharacterAttribute.Immune }
        );
    }

    public bool CanBeTarget(ICharacter Character) {
        return Character is MinionLogic && (Character as MinionLogic).Card.CA.MinionType == MinionType.Beast && (Character as MinionLogic).Owner == Owner;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}