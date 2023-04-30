public class BestialWrath : SpellCard, ITarget {
    public ICharacter Target { get; set; }
    private Buff buff;

    public BestialWrath(CardAsset CA) : base(CA) {
        buff = new(
            "BestialWrath",
            new() { new(Status.Attack, Operator.Plus, 2) },
            new() { new(TurnEvent.OnTurnEnd, (BaseEventArgs e) => Target.RemoveBuff(buff)) },
            new() { CharacterAttribute.Immune }
        );
    }

    public bool CanBeTarget(ICharacter Character) {
        return Character is MinionLogic && (Character as MinionLogic).Card.CA.MinionType == MinionType.Beast;
    }

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}