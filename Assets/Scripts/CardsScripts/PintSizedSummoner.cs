using System.Collections.Generic;

public class PintSizedSummoner : MinionCard, IAuraMinionCard {
    public List<AuraManager> AuraToGrant { get; }
    private readonly Buff buff;

    public PintSizedSummoner(CardAsset CA) : base(CA) {
        buff = new(
            "Pint-Sized Summoner",
            new() { new(Status.ManaCost, Operator.Minus, 1) }
        );
        AuraToGrant = new() { new(buff, (IBuffable b) => !BattleControl.IfUsedThisTurn(typeof(MinionCard)) && b is MinionCard && Logic.IsEnemy((b as MinionCard).Owner, Minion.Owner)) };
    }

}