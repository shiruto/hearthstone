using System.Collections.Generic;

public class StormwindChampion : MinionCard, IAuraMinionCard {
    private readonly Buff buff;
    public List<AuraManager> AuraToGrant { get; }

    public StormwindChampion(CardAsset CA) : base(CA) {
        buff = new(
            "Stormwind Champion",
            new() { new(Status.Attack, Operator.Plus, 1), new(Status.Health, Operator.Plus, 1) }
        );
        AuraToGrant = new() {
            new(
                buff,
                (IBuffable a) => (a is MinionLogic) && ((a as MinionLogic).Owner == Owner) && ((a as MinionLogic) != Minion)
            )
        };
    }


}