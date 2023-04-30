using System.Collections.Generic;

public class AbusiveSergeant : MinionCard, ITarget, IBattleCry {
    private readonly Buff _buff;
    public ICharacter Target { get; set; }
    public List<Effect> BattleCryEffects { get; set; }

    public AbusiveSergeant(CardAsset CA) : base(CA) {
        _buff = new(
            "abusiveSergeant",
            new() { new(Status.Attack, Operator.Plus, 2) },
            new() { new(TurnEvent.OnTurnEnd, (BaseEventArgs e) => Target.RemoveBuff(_buff)) }
        );
    }

    public bool CanBeTarget(ICharacter Card) {
        return Card is MinionLogic;
    }

    public void BattleCry() {
        new GiveBuff(_buff, this, Target).ActivateEffect();
    }

}
