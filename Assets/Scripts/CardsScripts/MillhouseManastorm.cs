using System.Collections.Generic;

public class MillhouseManastorm : MinionCard, IAuraMinionCard {
    public List<AuraManager> AuraToGrant { get; }
    private readonly Buff buff;

    public MillhouseManastorm(CardAsset CA) : base(CA) {
        buff = new(
            "Millhouse Manastorm",
            new() { new(Status.ManaCost, Operator.equal, 0) }
        );
        AuraToGrant = new() {
            new(
                buff,
                (IBuffable b) => BattleControl.Instance.ActivePlayer == Owner && b is SpellCard,
                TurnEvent.OnTurnEnd,
                (BaseEventArgs e) => e.Player != Owner
            )
        };
    }

}