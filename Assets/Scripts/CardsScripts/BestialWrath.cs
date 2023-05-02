using System;

public class BestialWrath : SpellCard, ITarget {
    public ICharacter Target { get; set; }

    public Func<ICharacter, bool> Match => (ICharacter c) => c is MinionLogic && (c as MinionLogic).Card.CA.MinionType == MinionType.Beast && (c as MinionLogic).Owner == Owner;

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

    public override void ExtendUse() {
        base.ExtendUse();
        new GiveBuff(buff, this, Target).ActivateEffect();
    }

}